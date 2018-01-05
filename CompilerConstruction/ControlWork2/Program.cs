using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlWork2
{
    class Program
	{
		static char[] spaces = new char[] { ' ' };
		
		static int slice;
		static string[] ready;
		static string[] wait;
		static Dictionary<string, int> vars;
		static Dictionary<string, List<Command>> subprograms;
		static Dictionary<string, List<Command>> programs;

		static void Main(string[] args)
		{
			while (true)
			{
				try
				{
					ReadFile();
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					Console.Read();
					return;
				}

				Console.WriteLine();
				Console.WriteLine("Result:");
				Process();

				Console.WriteLine();
			}
		}

		static void ReadFile()
		{
			string[] lines;
			int currentLine;

			string currentString;
			string[] strs;

			Console.Write("Enter file for processing (for example test1.txt or test2.txt): ");
			var filename = Console.ReadLine();

			lines = System.IO.File.ReadAllLines(filename);
			Console.WriteLine();
			Console.WriteLine("InputFile: ");
			foreach (var line in lines)
				Console.WriteLine(line);

			currentLine = 0;

			currentString = lines[currentLine++];
			try
			{
				slice = int.Parse(currentString.Split(spaces, StringSplitOptions.RemoveEmptyEntries)[1]);
			}
			catch (Exception e)
			{
				throw new Exception("Error in read \"silce\" section." + Environment.NewLine +
					e.Message);
			}

			currentString = lines[currentLine++];
			try
			{
				strs = currentString.Split(spaces, StringSplitOptions.RemoveEmptyEntries);
				ready = new string[strs.Length - 1];
				Array.Copy(strs, 1, ready, 0, ready.Length);
			}
			catch (Exception e)
			{
				throw new Exception("Error in read \"ready\" section." + Environment.NewLine +
					e.Message);
			}

			currentString = lines[currentLine++];
			try
			{
				strs = currentString.Split(spaces, StringSplitOptions.RemoveEmptyEntries);
				wait = new string[strs.Length - 1];
				Array.Copy(strs, 1, wait, 0, wait.Length);
			}
			catch (Exception e)
			{
				throw new Exception("Error in read \"wait\" section" + Environment.NewLine +
					e.Message);
			}

			currentString = lines[currentLine++];

			try
			{
				vars = new Dictionary<string, int>();
				strs = currentString.Split(spaces, StringSplitOptions.RemoveEmptyEntries);
				while (strs[0] == "var")
				{
					vars.Add(strs[1], int.Parse(strs[2]));
					currentString = lines[currentLine++];
					strs = currentString.Split(spaces, StringSplitOptions.RemoveEmptyEntries);
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error in read \"var\" section" + Environment.NewLine +
					e.Message);
			}

			subprograms = new Dictionary<string, List<Command>>();
			programs = new Dictionary<string, List<Command>>();
			try
			{
				strs = currentString.Split(spaces, StringSplitOptions.RemoveEmptyEntries);
				while (strs[0] == "sub" || strs[0] == "prog")
				{
					var name = strs[1];
					var commands = new List<Command>();

					if (strs[0] == "sub")
						subprograms.Add(name, commands);
					else
						programs.Add(name, commands);

					currentString = lines[currentLine++];
					strs = currentString.Split(spaces, StringSplitOptions.RemoveEmptyEntries);
					while (strs[1] != "end")
					{
						switch (strs[1])
						{
							case "if":
								commands.Add(new Command()
								{
									Number = int.Parse(strs[0]),
									CommandType = CommandType.If,
									arg1 = strs[2],
									arg2 = strs[3],
									arg3 = strs[4],
								});
								break;
							case "inc":
								commands.Add(new Command()
								{
									Number = int.Parse(strs[0]),
									CommandType = CommandType.Inc,
									arg1 = strs[2]
								});
								break;
							case "dec":
								commands.Add(new Command()
								{
									Number = int.Parse(strs[0]),
									CommandType = CommandType.Dec,
									arg1 = strs[2]
								});
								break;
							case "print":
								commands.Add(new Command()
								{
									Number = int.Parse(strs[0]),
									CommandType = CommandType.Print,
									arg1 = strs[2]
								});
								break;
							case "move":
								commands.Add(new Command()
								{
									Number = int.Parse(strs[0]),
									CommandType = CommandType.Move,
									arg1 = strs[2],
									arg2 = strs[3]
								});
								break;
							case "call":
								commands.Add(new Command()
								{
									Number = int.Parse(strs[0]),
									CommandType = CommandType.Call,
									arg1 = strs[2]
								});
								break;
							default:
								throw new Exception("Unexpected command name at line " + currentLine);
						}

						if (currentLine >= lines.Length - 1)
							break;
						currentString = lines[currentLine++];
						strs = currentString.Split(spaces, StringSplitOptions.RemoveEmptyEntries);
					}

					if (currentLine >= lines.Length - 1)
						break;
					currentString = lines[currentLine++];
					strs = currentString.Split(spaces, StringSplitOptions.RemoveEmptyEntries);
				}
			}
			catch (Exception e)
			{
				throw new Exception("Unable to read \"var\" section" + Environment.NewLine + e.Message);
			}
		}

		class ProgramIterator
		{
			public int ProcedureNumber = -1;
			public int Number;
		}

		static void Process()
		{
			var readyQueue = new Queue<string>(ready);
			
			var processCommandNumber = new Dictionary<string, ProgramIterator>();
			foreach (var program in programs)
				processCommandNumber.Add(program.Key, new ProgramIterator()
					{ ProcedureNumber = -1, Number = 0 } );

			var waitQueue = new Dictionary<string, Queue<string>>();
			for (int i = 0; i < wait.Length; i++)
				waitQueue.Add(wait[i], new Queue<string>());
			
			string processName;

			do
			{
				processName = readyQueue.Peek();

				List<Command> commands;
				if (processCommandNumber[processName].ProcedureNumber == -1)
					commands = programs[processName];
				else
					commands = subprograms[programs[processName][processCommandNumber[processName].ProcedureNumber].arg1];

				var currentNumber = processCommandNumber[processName].Number;
				for (int i = currentNumber; i < currentNumber + slice; i++)
				{
					if (i < commands.Count)
					{
						switch (commands[i].CommandType)
						{
							case CommandType.If:
								bool result;
								if (IsList(commands[i].arg1))
									result = waitQueue[commands[i].arg1].Count != 0;
								else
									result = vars[commands[i].arg1] != 0;
								if (result)
									i = int.Parse(commands[i].arg2) - 1;
								else
									i = int.Parse(commands[i].arg3) - 1;
								break;
							case CommandType.Inc:
								vars[commands[i].arg1]++;
								break;
							case CommandType.Dec:
								vars[commands[i].arg1]--;
								break;
							case CommandType.Print:
								Console.WriteLine(vars[commands[i].arg1]);
								break;
							case CommandType.Move:
								if (commands[i].arg1 == "ready")
								{
									if (commands[i].arg2 == "ready")
										readyQueue.Enqueue(readyQueue.Dequeue());
									else
										waitQueue[commands[i].arg2].Enqueue(readyQueue.Dequeue());
								}
								else
								{
									if (commands[i].arg2 == "ready")
										readyQueue.Enqueue(waitQueue[commands[i].arg1].Dequeue());
									else
										waitQueue[commands[i].arg2].Enqueue(
											waitQueue[commands[i].arg1].Dequeue());
								}
								break;
							case CommandType.Call:
								processCommandNumber[processName].ProcedureNumber = i;
								processCommandNumber[processName].Number = 0;
								readyQueue.Enqueue(readyQueue.Dequeue());
								goto callExit;
						}
					}
					else
					{
						if (processCommandNumber[processName].ProcedureNumber == -1)
							readyQueue.Dequeue();
						else
						{
							processCommandNumber[processName].Number = processCommandNumber[processName].ProcedureNumber + 1;
							processCommandNumber[processName].ProcedureNumber = -1;
							readyQueue.Enqueue(readyQueue.Dequeue());
						}
						break;
					}
				}

			callExit: ;
			}
			while (readyQueue.Count != 0);

			foreach (var queue in waitQueue)
				if (queue.Value.Count != 0)
				{
					Console.Write(queue.Key + ": ");
					foreach (var proc in queue.Value)
						Console.Write(proc + " ");
				}
			Console.WriteLine();
		}

		static bool IsList(string name)
		{
			return wait.Contains(name);
		}
	}
}
