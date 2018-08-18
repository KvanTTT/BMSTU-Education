using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Text;
using Networking;

namespace DataBase
{
	public enum DataBaseCommand
	{
		None,
		Add,        
		RemoveAt,
		Change,
		Clear,
		CheckLogin,
		Serialize,
		TransactCommited,
		SetNumber,
		Load		
	};

	public enum DataBaseType
	{
		Server,
		Client
	}

	public enum DataBaseLogging
	{
		None,
		Time,
		RealTime
	}

	public enum DataBaseSaving
	{
		None,
		Time,
		RealTime,
		AtTheEnd
	}

	public enum DataBaseCommitting
	{
		None,
		Time,
		RealTime
	}

	public delegate void ByteSender(byte NetworkUnitNumber, byte[] Buffer);
	public delegate object InvokeDelegate(Delegate method);

	public class BooksDatabase : BindingList<Book>
	{
		/*public NetworkUnit NetworkUnit
		{
			get;
			protected set;
		}*/

		public List<byte> CurData
		{
			get;
			protected set;
		}

		bool IsUpdating; 
		public readonly ByteSender ByteSender;
		public InvokeDelegate Invoke;        
		string TransactLogFileName;

		Queue Argums;
		Queue<string> AppendedLog;


		public readonly string LocalStorage;

		public bool RealTimeCommit
		{
			get;
			set;
		}

		public bool RealTimeUpdate
		{
			get;
			set;            
		}

		public bool TransactionRequest
		{
			get;
			set;
		}

		public bool TransactionApproval
		{
			get;
			set;
		}

		public DataBaseSaving SaveMode
		{
			get;
			set;
		}

		public DataBaseLogging Logging
		{
			get;
			set;
		}

		public readonly byte Type;

		// client constructor
		public BooksDatabase(ByteSender Sender, InvokeDelegate Invoke, byte ClientNumber) : base()
		{
			/*NetworkUnit = new ClientBase(ServerIP, Port);
			NetworkUnit.DataRecieved += new NetworkEventHandler(Update);*/
			Type = ClientNumber;
			CurData = new List<byte>(256);
			//CurData.Add(Type);

			RealTimeCommit = true;
			RealTimeUpdate = true;
			TransactionRequest = true;
			TransactionApproval = false;
			SaveMode = DataBaseSaving.None;
			Logging = DataBaseLogging.None;
			ByteSender = Sender;

			CurData = new List<byte>(256);
			this.Invoke = Invoke;
		}	
		
		// server constructor
		public BooksDatabase(ByteSender Sender, string FileName, string TransactLogFileName)
		{
			/*NetworkUnit = new ServerBase(ServerIP, Port);
			NetworkUnit.DataRecieved += new NetworkEventHandler(Update);*/
			Type = 0; // 0 is server
			CurData = new List<byte>(256);
			//CurData.Add(Type);

			RealTimeCommit = true;
			RealTimeUpdate = true;
			TransactionRequest = false;
			TransactionApproval = true;
			SaveMode = DataBaseSaving.RealTime;
			Logging = DataBaseLogging.RealTime;
			if (Logging != DataBaseLogging.None)
				AppendedLog = new Queue<string>();
			ByteSender = Sender;

			IsUpdating = true;
			Load(FileName);	// Server load from file		
			IsUpdating = false;

			
			this.LocalStorage = FileName;
			this.TransactLogFileName = TransactLogFileName;
		}

		// Server save from file
		public void Save()
		{
			XmlSerializer Serializer = new XmlSerializer(typeof(List<Book>));
			StreamWriter Writer = new StreamWriter(LocalStorage);
			Serializer.Serialize(Writer, this.Items);
			Writer.Close();
		}
		
		public void Load(string FileName)
		{
			XmlSerializer Serializer = new XmlSerializer(typeof(List<Book>));
			StreamReader Reader = new StreamReader(FileName);

			if (!IsUpdating)
			{
				CurData.Add((byte)DataBaseCommand.Load);
				byte[] buffer = new byte[Reader.BaseStream.Length];
				Reader.BaseStream.Read(buffer, 0, (int)Reader.BaseStream.Length);
				CurData.AddRange(buffer);
				if (RealTimeCommit)
					Commit();
			}

			base.Clear();
			List<Book> Books = (List<Book>)Serializer.Deserialize(Reader);
			foreach (Book B in Books)
				base.Add(B);
			Reader.Close();
		}

		public void Get()
		{
			CurData.Add((byte)DataBaseCommand.Serialize);
			if (RealTimeCommit)
				Commit();
		}

		// server save to stream
		public void Serialize()
		{
			XmlSerializer Serializer = new XmlSerializer(typeof(List<Book>));
			byte[] ar = new byte[8192];
			MemoryStream Stream = new MemoryStream(ar);
			Serializer.Serialize(Stream, base.Items);
			Array.Resize(ref ar, (int)Stream.Position);
			Stream.Close();
			CurData.Add((byte)DataBaseCommand.Load);
			CurData.AddRange(ar);
			if (RealTimeCommit)
				Commit();
		}

		// client load from stream
		public void Load(byte[] buffer, int index, int length)
		{
			XmlSerializer Serializer = new XmlSerializer(typeof(List<Book>));
			MemoryStream Stream = new MemoryStream(buffer, index, length);
			List<Book> Books = (List<Book>)Serializer.Deserialize(Stream);
			if (Invoke != null)
				Invoke((Action) delegate { base.Clear(); });
			else
				base.Clear();
			foreach (Book B in Books)
				if (Invoke != null)
					Invoke((Action)delegate { base.Add(B); });
				else
					base.Add(B);
			Stream.Close();
		}

		#region IList<Book>   
		// Write methods. Commit is required
		new public Book this[int index]
		{
			get 
			{ 
				return base[index]; 
			}
			set 
			{
				if (IsUpdating)
				{
					if (Invoke != null)
					{
						//Invoke((Action)delegate { base.SetItem(index, value); });
						// надо что-то с этим делать
						Invoke((Action)delegate { base.RemoveAt(index); });
						Invoke((Action)delegate { base.Insert(index, value); });
					}
					else
						this[index] = value;
				}
				else
				{
					CurData.Add((byte)DataBaseCommand.Change);
					AddInteger(index);
					CurData.AddRange(Encoding.UTF8.GetBytes(value.ToString()));

					if (TransactionRequest)
					{
						Argums = new Queue();
						Argums.Enqueue(DataBaseCommand.Change);
						Argums.Enqueue(index);
						Argums.Enqueue(value);
					}
					else
					{
						if (Invoke != null)
						{
							//Invoke((Action)delegate { base.SetItem(index, value); });
							// надо что-то с этим делать
							Invoke((Action)delegate { base.RemoveAt(index); });
							Invoke((Action)delegate { base.Insert(index, value); });
						}
						else
							this[index] = value;
					}

					if (RealTimeCommit)
						Commit();
				}            
			}
		}

		new public void Add(Book item)
		{
			if (IsUpdating)
			{
				if (Invoke != null)
					Invoke((Action)delegate { base.Add(item); });
				else
					base.Add(item);
			}
			else
			{
				CurData.Add((byte)DataBaseCommand.Add);
				CurData.AddRange(Encoding.UTF8.GetBytes(item.ToString()));

				if (TransactionRequest)
				{
					Argums = new Queue();
					Argums.Enqueue(DataBaseCommand.Add);
					Argums.Enqueue(item);                    
				}
				else
				{
					if (Invoke != null)
						Invoke((Action)delegate { base.Add(item); });
					else
						base.Add(item);
				}

				if (RealTimeCommit)
					Commit();
			}
		}
	   
		new public void Clear()
		{
			if (IsUpdating)
			{
				if (Invoke != null)
					Invoke((Action)delegate { base.Clear(); });
				else
					base.Clear();
			}
			else
			{
				CurData[0] = (byte)DataBaseCommand.Clear;                

				if (TransactionRequest)
				{
					Argums = new Queue();
					Argums.Enqueue(DataBaseCommand.Clear);
				}
				else
				{
					if (Invoke != null)
						Invoke((Action)delegate { base.Clear(); });
					else
						base.Clear();
				}

				if (RealTimeCommit)
					Commit();
			}
		}

		public void Insert(int index, Book item)
		{
			if (IsUpdating)
			{
				if (Invoke != null)
					Invoke((Action)delegate { base.Insert(index, item); });
				else
					base.Insert(index, item);
			}
			else
			{
				CurData.Add((byte)DataBaseCommand.Add);
				AddInteger(index);
				CurData.AddRange(Encoding.Unicode.GetBytes(item.ToString()));


				if (TransactionRequest)
				{
					Argums = new Queue();
					Argums.Enqueue(DataBaseCommand.Add);
					Argums.Enqueue(index);
					Argums.Enqueue(item);
				}
				else
				{
					if (Invoke != null)
						Invoke((Action)delegate { base.Insert(index, item); });
					else
						base.Insert(index, item);
				}

				if (RealTimeCommit)
					Commit();
			}		
		}

		new public bool Remove(Book item)
		{
			int index = IndexOf(item);
			if (index != -1)
			{
				RemoveAt(index);
				return true;
			}
			else
				return false;
		}

		new public void RemoveAt(int index)
		{
			if (IsUpdating)
			{
				if (Invoke != null)
					Invoke((Action)delegate { base.RemoveAt(index); });
				else
					base.RemoveAt(index);
			}
			else
			{
				CurData.Add((byte)DataBaseCommand.RemoveAt);
				AddInteger(index);
				if (TransactionRequest)
				{
					Argums = new Queue();
					Argums.Enqueue(DataBaseCommand.RemoveAt);
					Argums.Enqueue(index);
				}
				else
				{
					if (Invoke != null)
						Invoke((Action)delegate { base.RemoveAt(index); });
					else
						base.RemoveAt(index);
				}

				if (RealTimeCommit)
					Commit();
			}
		}

		#endregion

		void ExecuteCommand()
		{
			DataBaseCommand Command = (DataBaseCommand)Argums.Dequeue();
			switch (Command)
			{
				case DataBaseCommand.Add:
					if (Invoke != null)
						Invoke((Action)delegate { base.Add((Book)Argums.Dequeue()); });
					else
						base.Add((Book)Argums.Dequeue());
					break;
				case DataBaseCommand.RemoveAt:
					if (Invoke != null)
						Invoke((Action)delegate { base.RemoveAt((int)Argums.Dequeue()); });
					else
						base.RemoveAt((int)Argums.Dequeue());
					break;
				case DataBaseCommand.Change:
					int index = (int)Argums.Dequeue();
					if (Invoke != null)
					{
						/*Invoke((Action)delegate { base.SetItem((int)Argums.Dequeue(), (Book)Argums.Dequeue()); });*/
						Invoke((Action)delegate { base.RemoveAt(index); });
						Invoke((Action)delegate { base.Insert(index, (Book)Argums.Dequeue()); });
					}
					else
						base.SetItem((int)Argums.Dequeue(), (Book)Argums.Dequeue());
					break;
				case DataBaseCommand.Clear:
					if (Invoke != null)
						Invoke((Action)delegate { base.Clear(); });
					else
						base.Clear();
					break;
			}
		}


		void AddInteger(int number)
		{
			CurData.Add((byte)((number & 0xFF000000) >> 24));
			CurData.Add((byte)((number & 0x00FF0000) >> 16));
			CurData.Add((byte)((number & 0x0000FF00) >> 8));
			CurData.Add((byte)((number & 0x000000FF)));
		}

		byte[] GetBytes(int number)
		{
			int number1 = number + 4;
			byte[] bytes = new byte[4];            
			bytes[0] = (byte)((number1 & 0xFF000000) >> 24);
			bytes[1] = (byte)((number1 & 0x00FF0000) >> 16);
			bytes[2] = (byte)((number1 & 0x0000FF00) >> 8);
			bytes[3] = (byte)((number1 & 0x000000FF));
			return bytes;
		}

		int GetInt(byte[] bytes, ref int ind)
		{
			int result = (int)(bytes[ind] << 24) + (int)(bytes[ind+1] << 16) + (int)(bytes[ind+2] << 8) + (int)(bytes[ind+3]);
			ind += 4;
			return result;
		}		

		public void TransactCommit()
		{
			CurData.Add((Byte)DataBaseCommand.TransactCommited);
			Commit();
		}        

		void AppendToLog(string Action)
		{
			AppendedLog.Enqueue("[" + DateTime.Now.ToString() + "] " + Action);
		}

		void SaveLog()
		{
			FileStream Stream = new FileStream(TransactLogFileName, FileMode.Append);
			StreamWriter Writer = new StreamWriter(Stream);
			while (AppendedLog.Count != 0)
			{
				string Str = AppendedLog.Dequeue();
				Writer.WriteLine(Str);
				Console.WriteLine(Str);
			}
			
			Writer.Close();
			AppendedLog.Clear();
		}

		// отправка изменений
		public void Commit()
		{
			CurData.Insert(0, 0);
			CurData.InsertRange(0, GetBytes(CurData.Count));
			byte[] Result = CurData.ToArray();
			CurData.Clear();
			
			//NetworkUnit.Send(Result);
			ByteSender(0, Result);
		}  

		// обновление БД
		public void Update(byte[] Buffer)
		//public void Update(object sender, NetworkEventArgs EventArgs)
		{
			//byte[] Buffer = EventArgs.Info.Buffer;
			IsUpdating = true;
			int ind = 0;
			int BufferLength = GetInt(Buffer, ref ind);
			byte NetworkUnitNumber = Buffer[ind++];
			DataBaseCommand Command = (DataBaseCommand)Buffer[ind++]; 
			int number;
			switch (Command)
			{
				case DataBaseCommand.None:
					break;
				case DataBaseCommand.Add:
					Add(new Book(Encoding.UTF8.GetString(Buffer, ind, BufferLength)));
					if (SaveMode == DataBaseSaving.RealTime)
						Save();
					if (TransactionApproval)
						TransactCommit();
					if (Logging != DataBaseLogging.None)
						AppendToLog(@"Item with name """ + this[Count-1].Name + @""" and ISBN " + 
						this[Count-1].ISBN + " has been added under the number " + (Count-1).ToString());
					if (Logging == DataBaseLogging.RealTime)
						SaveLog();
					break;
				case DataBaseCommand.RemoveAt:
					number = GetInt(Buffer, ref ind);
					RemoveAt(number);
					if (SaveMode == DataBaseSaving.RealTime)
						Save();
					if (TransactionApproval)
						TransactCommit();
					if (Logging != DataBaseLogging.None)
						AppendToLog("Item under the number " + number.ToString() + " has been removed");
					if (Logging == DataBaseLogging.RealTime)
						SaveLog();
					break;
				case DataBaseCommand.Change:
					number = GetInt(Buffer, ref ind);
					base[number] = new Book(Encoding.UTF8.GetString(Buffer, ind, BufferLength - ind));
					if (SaveMode == DataBaseSaving.RealTime)
						Save();
					if (TransactionApproval)
						TransactCommit();
					if (Logging != DataBaseLogging.None)
						AppendToLog("Item under the number " + number.ToString() + " has been changed");
					if (Logging == DataBaseLogging.RealTime)
						SaveLog();
					break;
				case DataBaseCommand.Clear:
					Clear();
					if (TransactionApproval)
						TransactCommit();
					if (Logging != DataBaseLogging.None)
						AppendToLog("Database has been changed");
					if (Logging == DataBaseLogging.RealTime)
						SaveLog();
					break;

				case DataBaseCommand.Load:
					Load(Buffer, ind, BufferLength - ind);
					if (SaveMode == DataBaseSaving.RealTime)
						Save();
					//if (TransactionApproval)
					//	TransactCommit();
					break;				
				case DataBaseCommand.Serialize:
					Serialize();
					break;
				case DataBaseCommand.TransactCommited:
					ExecuteCommand();
					break;
			}
			IsUpdating = false;
		}
	}
}
