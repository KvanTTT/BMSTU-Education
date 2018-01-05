namespace Project2_Enigma
{
    class EnigmaMachine
    {
        EnigmaDisk[] Disks;

        public EnigmaMachine(byte[] Seeds)
        {
            Disks = new EnigmaDisk[Seeds.Length];
            for (byte i = 0; i < Seeds.Length; i++)
                Disks[i] = new EnigmaDisk(Seeds[i]);
        }

        public void Customize(bool[] Dirs, byte[] StartPoses)
        {
            for (byte i = 0; i < Dirs.Length; i++)
                Disks[i].Custimize(StartPoses[i], Dirs[i]);
        }

        public void Encode(byte[] InArray, out byte[] EncodeArray)
        {
            EncodeArray = new byte[InArray.Length];
            byte j;

            for (long i = 0; i < InArray.Length; i++)
            {
                byte c = Disks[0][InArray[i]];
                for (j = 1; j < Disks.Length; j++)
                    c = Disks[j][c];

                EncodeArray[i] = c;

                Disks[0].Step();
                for (j = 1; j < Disks.Length; j++)
                    if (Disks[j - 1].IsFullTurn())
                        Disks[j].Step();
                    else
                        break;
                //if (Disks[Disks.Length-1].IsFullTurn())
                //    Disks[0].Step();                
            }
        }

        public void Decode(byte[] InArray, out byte[] DecodeArray, bool InvertDecoding)
        {
            byte c, j;
            byte DisksHigh = (byte)(Disks.Length - 1);
            DecodeArray = new byte[InArray.Length];
            if (InvertDecoding)
            {
                long sum = InArray.Length;
                for (j = 0; j < Disks.Length; j++)
                {
                    if (Disks[j].RightDir)
                    {
                        sum += Disks[j].Pos;
                        Disks[j].SetPos((byte)(sum));
                    }
                    else
                    {
                        sum += 256 - Disks[j].Pos;
                        Disks[j].SetPos((byte)(256 - sum));
                    }
                    sum /= 256;
                }


                for (long i = InArray.Length - 1; i >= 0; i--)
                {
                    c = InArray[i];

                    Disks[0].InvStep();
                    for (j = 1; j <= DisksHigh; j++)
                        if (Disks[j - 1].IsFullInvTurn())
                            Disks[j].InvStep();
                        else
                            break;
                    // if (Disks[DisksHigh].IsFullInvTurn())
                    //     Disks[0].InvStep();

                    for (j = DisksHigh; j != 0; j--)
                        c = Disks[j].GetInvCell(c);

                    DecodeArray[i] = Disks[0].GetInvCell(c);
                }
            }
            else
            {
                byte DiskHighM2 = (byte)(DisksHigh - 1);

                for (long i = 0; i < InArray.Length; i++)
                {
                    c = Disks[DisksHigh].GetInvCell(InArray[i]);
                    for (j = DiskHighM2; j != 255; j--)
                        c = Disks[j].GetInvCell(c);

                    DecodeArray[i] = c;

                    Disks[0].Step();
                    for (j = 1; j <= DisksHigh; j++)
                        if (Disks[j - 1].IsFullTurn())
                            Disks[j].Step();
                        else
                            break;
                    //if (Disks[Disks.Length-1].IsFullTurn())
                    //    Disks[0].Step();                
                }
            }
        }
    }
}