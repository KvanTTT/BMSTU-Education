using System;
using System.Collections.Generic;
using System.IO;

namespace Project5_Haffman
{
    public struct ByteCount
    {
        public int Count;
        public byte Byte;

        public ByteCount(byte Byte, int Count)
        {
            this.Byte = Byte;
            this.Count = Count;
        }
    }

    public struct CompressByte
    {
        public ushort Value;
        public byte Length;

        public CompressByte(ushort Value, byte Length)
        {
            this.Value = Value;
            this.Length = Length;
        }

        public CompressByte(CompressByte CompressByte)
        {
            this.Value = CompressByte.Value;
            this.Length = CompressByte.Length;
        }
    }

    // узел дерева
    class HaffmanTreeNode
    {
        public HaffmanTreeNode Left, Right, Parent;  // указатели на левого, правого потомка и на родителя
        public byte Value;  // значение данного узла
        public int ByteCount;  // количество байт (у всех потомков)

        // конструкторы
        public HaffmanTreeNode(HaffmanTreeNode Left, HaffmanTreeNode Right)
        {
            this.Left = Left;
            this.Right = Right;
            Left.Parent = this;
            Right.Parent = this;
            this.ByteCount = Left.ByteCount + Right.ByteCount;    
            //this.Value = 0;         // default value
        }

        public HaffmanTreeNode(byte Value, int ByteCount, bool Leaf,
            HaffmanTreeNode Left, HaffmanTreeNode Right, HaffmanTreeNode Parent)
        {
            this.Value = Value;
            this.ByteCount = ByteCount;
            this.Left = Left;
            this.Right = Right;
            this.Parent = Parent;
        }

        public HaffmanTreeNode(byte Value, int ByteCount)
        {
            this.Value = Value;
            this.ByteCount = ByteCount;
            this.Left = null;
            this.Right = null;
            this.Parent = null;
        }

        public HaffmanTreeNode(ByteCount ByteCount)
        {
            this.Value = ByteCount.Byte;
            this.ByteCount = ByteCount.Count;
            this.Left = null;
            this.Right = null;
            this.Parent = null;
        }
    }

    class HaffmanTree
    {
        int MaxHeight;
        HaffmanTreeNode Root;
        Dictionary<int, CompressByte> Bytes;

        public HaffmanTree()
        {
        }
       
        public HaffmanTree(ByteCount[] MeetingBytes)
        {
            MaxHeight = 0;
            List<HaffmanTreeNode> Nodes = new List<HaffmanTreeNode>();
            for (int i = 0; i < MeetingBytes.Length; i++)
                    Nodes.Add(new HaffmanTreeNode(MeetingBytes[i]));

            HaffmanTreeNode Left, Right;
            while (Nodes.Count > 1)
            {
                int Min = Nodes[0].ByteCount;
                int MinInd = 0;
                for (int j = 1; j < Nodes.Count; j++)
                    if (Min > Nodes[j].ByteCount)
                    {
                        Min = Nodes[j].ByteCount;
                        MinInd = j;
                    }
                Left = Nodes[MinInd];
                Nodes.RemoveAt(MinInd);

                Min = Nodes[0].ByteCount;
                MinInd = 0;
                for (int j = 1; j < Nodes.Count; j++)
                    if (Min > Nodes[j].ByteCount)
                    {
                        Min = Nodes[j].ByteCount;
                        MinInd = j;
                    }
                Right = Nodes[MinInd];
                Nodes.RemoveAt(MinInd);

                Nodes.Add(new HaffmanTreeNode(Left, Right));
            }

            Root = Nodes[0];

            CalculBytes();
        }

        // рекурсивная функция обхода дерева
        void TraverseTree(HaffmanTreeNode Node, CompressByte NewCompressByte)
        {
            // пока не добрались до листьев
            if (Node.Left != null && Node.Right != null)
            {
                NewCompressByte.Length++;  // высота (ну или глубина) данного узла
                // если идем в левый потомок, то добавляем 0
                TraverseTree(Node.Left, NewCompressByte);

                // если идем в правый потомок, то добавляем 1
                NewCompressByte.Value |= (ushort)(1 << (NewCompressByte.Length - 1));
                TraverseTree(Node.Right, NewCompressByte);
            }
            else
            {
                // добрались до листьев
                if (NewCompressByte.Length > MaxHeight)  // если новая высота дерева больше текущей
                    MaxHeight = NewCompressByte.Length;
                Bytes[Node.Value] = new CompressByte(NewCompressByte);  // заполняем массив символов
                                                                    // его длина = 256 (т.к. кодируем по 8 бит)
                                                                    // т.е. для каждого байта вычисляем его код
            }
        }

        void CalculBytes()
        {
            Bytes = new Dictionary<int, CompressByte>();
            CompressByte NewCompressByte = new CompressByte();
            TraverseTree(Root, NewCompressByte);
        }

        public void EncodeBytes(byte[] Data, ref byte[] CompessData, ref int BytePos, ref byte BitPos)
        {
            for (int j = 0; j < Data.Length; j++)
            {
                CompressByte B = Bytes[Data[j]];
                for (int i = 0; i < B.Length; i++)
                {
                    CompessData[BytePos] |= (byte)(((B.Value >> i) & 0x1) << BitPos);
                    BitPos = (byte)((BitPos + 1) % 8);
                    if (BitPos == 0)
                        BytePos++;
                }
            }
        }

        public void DecodeBytes(byte[] CompressData, ref byte[] Data, ref int BytePos, ref byte BitPos)
        {
            for (int j = 0; j < Data.Length; j++)
            {
                int Bit;
                HaffmanTreeNode Node = Root;
                byte h = 0;
                while (Node.Left != null)
                {
                    Bit = CompressData[BytePos] & (1 << BitPos);
                    if (Bit == 0)
                        Node = Node.Left;
                    else
                        Node = Node.Right;
                    BitPos = (byte)((BitPos + 1) % 8);
                    if (BitPos == 0)
                        BytePos++;
                    h++;
                }
                Data[j] = Node.Value;
            }
        }

        public void WriteHeader(byte[] CompressData, ref int BytePos, ref byte BitPos)
        {
            for (int i = 0; i < 256; i++)
            {
                CompressData[BytePos++] = Bytes[i].Length;
                if (Bytes[i].Length != 0)
                {
                    if (Bytes[i].Length > 8)
                        CompressData[BytePos++] = (byte)(Bytes[i].Value & 0xFF00);
                    CompressData[BytePos++] = (byte)(Bytes[i].Value & 0xFF00);                    
                }
            }
        }

        public void ReadHeader(byte[] CompressData, ref int BytePos, ref byte BitPos)
        {
            /*for (int i = 0; i < 256; i++)
            {
                if (CompressData[BytePos] != 0)
                {
                    Bytes[i].Length = CompressData[BytePos];
                    if (CompressData[BytePos] > 8)
                        Bytes[i].Value |= (byte)(CompressData[BytePos++] & 0xFF00);
                    Bytes[i].Value |= (byte)(CompressData[BytePos++] & 0x00FF);
                }
            }*/
        }
    }

    class HaffmanCompressor
    {
        ByteCount[] MeetingBytes = new ByteCount[256];
        byte[] Data;
        HaffmanTree Tree;

        public HaffmanCompressor()
        {
        }

        void BuildFreqArray()
        {
            for (int i = 0; i < 256; i++) 
                MeetingBytes[i] = new ByteCount((byte)i, 0);

            for (int i = 0; i < Data.Length; i++)
                MeetingBytes[Data[i]].Count++;

            Array.Sort(MeetingBytes, (A, B) => B.Count.CompareTo(A.Count));

            for (int i = 0; i < MeetingBytes.Length; i++)
                if (MeetingBytes[i].Count == 0)
                {
                    ByteCount[] TempAr = new ByteCount[i];
                    Array.Copy(MeetingBytes, 0, TempAr, 0, i);
                    MeetingBytes = TempAr;
                    break;
                }
        }

        public float Compress(string SourceFile, string DestFile)
        {
            StreamReader Reader = new StreamReader(SourceFile);
            Data = new byte[Reader.BaseStream.Length];
            Reader.BaseStream.Read(Data, 0, (int)Reader.BaseStream.Length);
            Reader.Close();

            BuildFreqArray();
            Tree = new HaffmanTree(MeetingBytes);

            int BytePos = 0;
            byte BitPos = 0;
            byte[] CompressData = new byte[Data.Length];

            // длина исходного файла (в байтах)
            CompressData[BytePos++] = (byte)((Data.Length & 0xFF000000) >> 24);
            CompressData[BytePos++] = (byte)((Data.Length & 0x00FF0000) >> 16);
            CompressData[BytePos++] = (byte)((Data.Length & 0x0000FF00) >> 8);
            CompressData[BytePos++] = (byte)(Data.Length & 0x000000FF);

            // запись таблицы символов
            //Tree.WriteHeader(CompressData, ref BytePos, ref BitPos);

            CompressData[BytePos++] = (byte)(MeetingBytes.Length);
            for (int i = 0; i < MeetingBytes.Length; i++)
            {
                CompressData[BytePos++] = MeetingBytes[i].Byte;
                CompressData[BytePos++] = (byte)((MeetingBytes[i].Count & 0xFF000000) >> 24);
                CompressData[BytePos++] = (byte)((MeetingBytes[i].Count & 0x00FF0000) >> 16);
                CompressData[BytePos++] = (byte)((MeetingBytes[i].Count & 0x0000FF00) >> 8);
                CompressData[BytePos++] = (byte)(MeetingBytes[i].Count & 0x000000FF);
            }

            // запись сжатого массива байтов
            Tree.EncodeBytes(Data, ref CompressData, ref BytePos, ref BitPos);

           /* // запись хеш суммы исходного файла
            byte[] OrigFileHash = MD5.Create().ComputeHash(Data);
            for (int i = 0; i < OrigFileHash.Length; i++)
                CompressData[BytePos++] = OrigFileHash[i];*/

            StreamWriter Writer = new StreamWriter(DestFile);
            Writer.BaseStream.Write(CompressData, 0, BytePos);
            Writer.Close();

            return (float)BytePos / Data.Length;
        }

        public bool Decompress(string SourceFile, string DestFile)
        {
            int BytePos = 0;
            byte BitPos = 0;
            StreamReader Reader = new StreamReader(SourceFile);
            byte[] CompressData = new byte[Reader.BaseStream.Length];
            Reader.BaseStream.Read(CompressData, 0, (int)Reader.BaseStream.Length);
            Reader.Close();

            // чтение длины результирующего файла
            int Length = 0;
            Length |= (int)CompressData[BytePos++] << 24;
            Length |= (int)CompressData[BytePos++] << 16;
            Length |= (int)CompressData[BytePos++] << 8;
            Length |= (int)CompressData[BytePos++];
            Data = new byte[Length];

            // чтение таблицы символов
            //Tree.ReadHeader(CompressData, ref BytePos, ref BitPos);
            MeetingBytes = new ByteCount[CompressData[BytePos++]];
            for (int i = 0; i < MeetingBytes.Length; i++)
            {
                MeetingBytes[i] = new ByteCount(CompressData[BytePos++], 
                    (CompressData[BytePos++] << 24) +
                    (CompressData[BytePos++] << 16) + 
                    (CompressData[BytePos++] << 8) +
                    CompressData[BytePos++]);
            }

            Tree = new HaffmanTree(MeetingBytes);

            // чтение байтов сжатого массива байтов
            Tree.DecodeBytes(CompressData, ref Data, ref BytePos, ref BitPos);

            StreamWriter Writer = new StreamWriter(DestFile);
            Writer.BaseStream.Write(Data, 0, Length);
            Writer.Close();

             /*
            byte[] SavedHash = new byte[16];
            Array.Copy(CompressData, BytePos, SavedHash, 0, 16);
            if (MD5.Create().ComputeHash(Data).SequenceEqual(SavedHash))
                return true;
            else
                return false;*/
            return true;
        }
    }
}
