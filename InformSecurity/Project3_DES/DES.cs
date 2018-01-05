using System.IO;
using System.Runtime.InteropServices;

namespace Project3_DES
{
    class DESCrypt
    {
        const string DllName = "../../Cryptography/Cryptography.dll";
        [DllImport(DllName, EntryPoint = "Encode")]
        public static extern void Encode(byte[] InArray, byte[] OutArray, int Length, ulong Key);
        [DllImport(DllName, EntryPoint = "Decode")]
        public static extern void Decode(byte[] InArray, byte[] OutArray, int Length, ulong Key);

        public DESCrypt()
        {
        }

        public static void Encode(string SourceFile, string DestFile, ulong Key)
        {
            StreamReader Reader = new StreamReader(SourceFile);
            int OrigLength = (int)Reader.BaseStream.Length;
            int ULongLength = (OrigLength + 7) / 8;
            int Length = ULongLength * 8;

            byte[] InArray = new byte[Length];
            Reader.BaseStream.Read(InArray, 0, OrigLength);
            Reader.Close();

            byte[] OutArray = new byte[Length + 1];

            Encode(InArray, OutArray, ULongLength, Key);
            OutArray[Length] = (byte)(Length - OrigLength);

            StreamWriter Writer = new StreamWriter(DestFile);
            Writer.BaseStream.Write(OutArray, 0, Length + 1);
            Writer.Close();
        }

        public static void Decode(string SourceFile, string DestFile, ulong Key)
        {
            StreamReader Reader = new StreamReader(SourceFile);
            int OrigLength = (int)Reader.BaseStream.Length;
            int ULongLength = (OrigLength + 7 - 1) / 8;
            int Length = ULongLength * 8;

            byte[] InArray = new byte[OrigLength];
            Reader.BaseStream.Read(InArray, 0, OrigLength);
            Reader.Close();

            byte[] OutArray = new byte[Length];

            Decode(InArray, OutArray, ULongLength, Key);

            /*int LastInd = Array.FindIndex(OutArray, Length - 7, X => X == 0);
            if (LastInd == -1)
                LastInd = Length;*/

            int LastInd = Length - InArray[OrigLength - 1];
            StreamWriter Writer = new StreamWriter(DestFile);
            Writer.BaseStream.Write(OutArray, 0, LastInd);
            Writer.Close();
        }
    }
}
