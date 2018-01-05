using System;
using System.IO;

namespace Project4_RSA
{
    public class SimpleNumber
    {
        public static bool IsSimple64(long N)
        {            
            if (N % 2 == 0)
                return false;
            long Sqrt = (long)Math.Sqrt(N);
            for (long i = 3; i <= Sqrt; i += 2)
            {
                if (N % i == 0)
                    return false;
            }
            return true;
        }

        public static long Generate64(long minValue, long maxValue)
        {
            long Result;
            Random Rand = new Random();
            do
            {
                //Result = (long)Rand.Next() + ((long)(Rand.Next()) << 32);
                Result = (long)Rand.Next();
                if (Result < minValue || Result >= maxValue)
                    continue;
            }
            while (!IsSimple64(Result));
            return Result;
        }

        public static long Generate64()
        {
            long Result;
            Random Rand = new Random();
            do
                Result = (long)Rand.Next() + ((long)(Rand.Next()) << 32);
            while (!IsSimple64(Result));
            return Result;
        }

        public static bool IsSimple(int N)
        {
            if (N % 2 == 0)
                return false;
            int Sqrt = (int)Math.Sqrt(N);
            for (int i = 3; i <= Sqrt; i += 2)
            {
                if (N % i == 0)
                    return false;
            }
            return true;
        }

        public static int Generate()
        {
            int Result;
            Random Rand = new Random();
            do
                Result = (int)Rand.Next() + 1;
            while (!IsSimple(Result));
            return Result;
        }

        public static int Generate16()
        {
            int Result;
            Random Rand = new Random();
            do
                Result = (int)Rand.Next(Int16.MaxValue) + 1;
            while (!IsSimple(Result));
            return Result;
        }

        public static void Generate(int seed, out int P, out int Q)
        {
            Random Rand = new Random((int)seed);
            do
                P = (int)Rand.Next() + 1;
            while (!IsSimple(P));
            do
                Q = (int)Rand.Next() + 1;
            while (!IsSimple(Q));
        }


        public long[] SimpleNumbers;

        public void PrepareSimpleNumbers(long maxValue)
        {
            long[] SimpleSparseNumbers = new long[maxValue];
            long i, j, dec;
            for (i = 0; i < maxValue; i++)
                SimpleSparseNumbers[i] = i;
            for (i = 2; i < maxValue; i++)
            {
                if (SimpleSparseNumbers[i] == 0)
                    continue;
                dec = i;
                for (j = i + dec; j < maxValue; j += dec)
                    SimpleSparseNumbers[j] = 0;
            }

            j = 0;
            for (i = 1; i < maxValue; i++)
                if (SimpleSparseNumbers[i] != 0)
                {
                    SimpleSparseNumbers[j] = SimpleSparseNumbers[i];
                    j++;
                }

            SimpleNumbers = new long[j];
            for (i = 0; i < j; i++)
                SimpleNumbers[i] = SimpleSparseNumbers[i];
        }

        public void WriteSimpleNumbers(string FileName, long maxValue)
        {
            long[] TempNumbers = new long[maxValue];

            TempNumbers[0] = 1;
            TempNumbers[1] = 2;
            TempNumbers[2] = 3;
            TempNumbers[3] = 5;
            TempNumbers[4] = 7;
            long i, j = 5, k;
            bool simple;

            if (maxValue == long.MaxValue)
                maxValue -= 2;
            for (i = 11; i <= maxValue; i += 2)
            {
                simple = true;
                long Sqrt = (long)Math.Sqrt(i);
                for (k = 3; k <= Sqrt; k += 2)
                {
                    if (i % k == 0)
                    {
                        simple = false;
                        break;
                    }
                }
                if (simple)
                {
                    TempNumbers[j] = i;
                    j++;
                }
            }

            StreamWriter Writer = new StreamWriter(FileName);
            for (i = 0; i < j; i++)
                Writer.WriteLine(TempNumbers[i]);
            Writer.Close();
        }
    }

    public class RSAKey
    {
        public RSAKey() { }
        public RSAKey(long Key, long N)
        {
            this.Key = Key;
            this.N = N;
        }

        public long Key
        {
            private set;
            get;
        }
        public long N
        {
            protected set;
            get;
        }
        public override string ToString()
        {
            return String.Format("{0}   {1}", Key, N);
        }
    }

    class RSACrypt
    {
        public long D
        {
            protected set;
            get;
        }
        
        public long E
        {
            protected set;
            get;
        }

        public long N
        {
            protected set;
            get;
        }

        long GCD(long A, long B)
        {
            //long X;
            //while (A != 0)
            //{
            //    X = A;
            //    A = B % A;
            //    B = X;
            //}
            //return B;

            int shift;

            /* GCD(0,x) := x */
            if (A == 0 || B == 0)
                return A | B;

            /* Let shift := lg K, where K is the greatest power of 2
               dividing both A and B. */
            for (shift = 0; ((A | B) & 1) == 0; ++shift)
            {
                A >>= 1;
                B >>= 1;
            }

            while ((A & 1) == 0)
                A >>= 1;

            /* From here on, A is always odd. */
            do
            {
                while ((B & 1) == 0)  /* Loop X */
                    B >>= 1;

                /* Now A and B are both odd, so diff(A, B) is even.
                   Let A = min(A, B), B = diff(A, B)/2. */
                if (A < B)
                    B -= A;
                else
                {
                    long diff = A - B;
                    A = B;
                    B = diff;
                }
                B >>= 1;
            }
            while (B != 0);

            return A << shift;
        }

        long ExtendedGCD(long x, long n)
        {
            if (x == 1)
                return x + n;
            long p;
            long p0 = 0;
            long p1 = 1;
            long q0, q1;
            long r;
            long y = n;

            r = y % x;
            q0 = y / x;
            y = x;
            x = r;
            r = y % x;
            q1 = y / x;
            y = x;
            x = r;
            while (r > 0)
            {
                r = y % x;
                p = p0 - p1 * q0;
                if (p < 0)
                    p = n - (-p % n);
                else
                    p %= n;
                p0 = p1;
                p1 = p;
                q0 = q1;
                q1 = y / x;
                y = x;
                x = r;
            }
            p = p0 - p1 * q0;
            if (p < 0)
                return (long)(n - (-p % n));
            else
                return (long)(p % n);
        }

        static long ApowerKmodN(long a, long k, long n)
        {
            long b = 1;
            while (k != 0)
            {
                if (k % 2 == 0)
                {
                    k /= 2;
                    a = (a * a) % n;
                }
                else
                {
                    k--;
                    b = (b * a) % n;
                }
            }
            return b;
        }

        public void GenerateNewKeys()
        {
            int P, Q;

            P = SimpleNumber.Generate16();
            do
                Q = SimpleNumber.Generate16();
            while (Q == P);

            N = (long)P * (long)Q;
            long F = (long)(P - 1) * (long)(Q - 1);

            do
                E = SimpleNumber.Generate();
            while (GCD(E, F) != 1);

            D = ExtendedGCD(E, F);
        }

        public RSACrypt()
        {
        }

        public static void Encode(string SourceFile, string DestFile, RSAKey PublicKey)
        {
            FileStream SourceFileStream = new FileStream(SourceFile, FileMode.Open),
                       DestFileStream = new FileStream(DestFile, FileMode.Create);
            BinaryReader Reader = new BinaryReader(SourceFileStream);
            BinaryWriter Writer = new BinaryWriter(DestFileStream);

            while (SourceFileStream.Position < SourceFileStream.Length)
                Writer.Write(ApowerKmodN(Reader.ReadByte(), PublicKey.Key, PublicKey.N));
            
            Reader.Close();
            Writer.Close();
        }

        public static  void Decode(string SourceFile, string DestFile, RSAKey PrivateKey)
        {
            FileStream SourceFileStream = new FileStream(SourceFile, FileMode.Open),
              DestFileStream = new FileStream(DestFile, FileMode.Create);
            BinaryReader Reader = new BinaryReader(SourceFileStream);
            BinaryWriter Writer = new BinaryWriter(DestFileStream);

            while (SourceFileStream.Position < SourceFileStream.Length)
                Writer.BaseStream.WriteByte((byte)ApowerKmodN(Reader.ReadInt64(), PrivateKey.Key, PrivateKey.N));
            
            Reader.Close();
            Writer.Close();
        }
    }
}
