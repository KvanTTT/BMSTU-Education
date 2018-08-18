using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Networking
{
    public sealed class SymmetricCryption
    {
        private RijndaelManaged Algorithm;

        private MemoryStream MemoryStream;

        private ICryptoTransform EncryptorDecryptor;

        private CryptoStream CryptoStream;

        private StreamWriter StreamWriter;
        private StreamReader StreamReader;

        private string m_key;
        private string m_iv;

        private byte[] key;
        private byte[] iv;

        private byte[] pwd_byte;

        public SymmetricCryption(string key_val, string iv_val)
        {
            //key = new byte[32];
            //iv = new byte[32];
            m_key = key_val;
            m_iv = iv_val;

            key = System.Text.Encoding.ASCII.GetBytes(key_val);
            iv = System.Text.Encoding.ASCII.GetBytes(iv_val);
        }

        public byte[] Encrypt(string s)
        {
            Algorithm = new RijndaelManaged();

            Algorithm.BlockSize = 256;
            Algorithm.KeySize = 256;

            MemoryStream = new MemoryStream();

            EncryptorDecryptor = Algorithm.CreateEncryptor(key, iv);

            CryptoStream = new CryptoStream(MemoryStream, EncryptorDecryptor,
            CryptoStreamMode.Write);

            StreamWriter = new StreamWriter(CryptoStream);

            StreamWriter.Write(s);

            StreamWriter.Flush();
            CryptoStream.FlushFinalBlock();

            pwd_byte = new byte[MemoryStream.Length];
            MemoryStream.Position = 0;
            MemoryStream.Read(pwd_byte, 0, (int)pwd_byte.Length);

            return pwd_byte;
            /* pwd_str = new UnicodeEncoding().GetString(pwd_byte);

             return pwd_str;*/
        }

        public string Decrypt(byte[] s)
        {
            string Result = "";
            MemoryStream = new MemoryStream(s);
            try
            {
                Algorithm = new RijndaelManaged();

                Algorithm.BlockSize = 256;
                Algorithm.KeySize = 256;                

                ICryptoTransform EncryptorDecryptor =
                    Algorithm.CreateDecryptor(key, iv);

                MemoryStream.Position = 0;

                CryptoStream crStream = new CryptoStream(
                    MemoryStream, EncryptorDecryptor, CryptoStreamMode.Read);
                StreamReader = new StreamReader(crStream);
                Result = StreamReader.ReadToEnd();
            }
            finally
            {
                MemoryStream.Close();
            }
            return Result;
        }

        public void Encrypt(string FileName, string s)
        {
            StreamWriter Writer = new StreamWriter(FileName);
            byte[] buffer = Encrypt(s);
            Writer.BaseStream.Write(buffer, 0, buffer.Length);
            Writer.Close();
        }

        public string Decrypt(string FileName)
        {
            StreamReader Reader = new StreamReader(FileName);
            byte[] buffer = new byte[Reader.BaseStream.Length];
            Reader.BaseStream.Read(buffer, 0, (int)Reader.BaseStream.Length);
            Reader.Close();
            return Decrypt(buffer);
        }
    }
}
