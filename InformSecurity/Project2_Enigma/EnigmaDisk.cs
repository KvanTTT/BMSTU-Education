using System;

namespace Project2_Enigma
{
    class EnigmaDisk
    {
        byte[] Cells;
        byte[] InvCells;

        public bool RightDir
        {
            get;
            private set;
        }

        public byte this[byte i]
        {
            get
            {
                return Cells[(byte)(Pos + i)];
            }
        }

        public byte GetInvCell(byte i)
        {
            return (byte)(InvCells[i] - Pos);
        }

        public byte Pos
        {
            get;
            private set;
        }

        public void SetPos(byte NewPos)
        {
            Pos = NewPos;
        }

        public EnigmaDisk(byte Seed)
        {
            byte tind, t;
            Random Rand = new Random(Seed);
            Cells = new byte[256];
            InvCells = new byte[256];
            byte i = 255;
            do
            {
                i++;
                Cells[i] = i;
            }
            while (i != 255);
            do
            {
                i++;
                tind = (byte)Rand.Next(0, 256);
                t = Cells[tind];
                Cells[tind] = Cells[i];
                Cells[i] = t;                
            }
            while (i != 255);
            do
            {
                i++;
                InvCells[Cells[i]] = i;
            }
            while (i != 255);
        }

        public void Custimize(byte StartPos, bool RightDir)
        {
            this.RightDir = RightDir;
            this.Pos = StartPos;
        }

        public void Step()
        {
            if (RightDir)     
                Pos++;
            else
                Pos--;
        }

        public void InvStep()
        {
            if (RightDir)
                Pos--;
            else
                Pos++;
        }

        public bool IsFullTurn()
        {
            if (RightDir)
                return (Pos == 0 ? true : false);
            else
                return (Pos == 255 ? true : false);
        }

        public bool IsFullInvTurn()
        {
            if (RightDir)
                return (Pos == 255 ? true : false);
            else
                return (Pos == 0 ? true : false);
        }


    }
}
