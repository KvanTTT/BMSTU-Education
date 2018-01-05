using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Project2_Enigma
{
    public partial class frmEnigma : Form
    {
        EnigmaMachine Enigma = new EnigmaMachine(new byte[] {155});
        FileStream Stream = File.Create("asdf.txt");

        public frmEnigma()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           /* StreamWriter Writer = new StreamWriter("asdf.txt");
            byte[] bytes = new byte[20000000];
            for (long i = 0; i < 20000000; i++)
                bytes[i] = 97;
            Writer.BaseStream.Write(bytes, 0, 20000000);
            Writer.Close();*/

            //udDiskCount.Value = 0;
            ofdKeys.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            ofdKeys.FileName = "key1.key";
            sfdKeys.InitialDirectory = ofdKeys.InitialDirectory;
            sfdKeys.FileName = "key1.key";
            ofdMachine.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            ofdMachine.FileName = "Enigma1.Enigma";
            sfdMachine.InitialDirectory = ofdMachine.InitialDirectory;
            sfdMachine.FileName = "Enigma1.Enigma";

            Random Rand = new Random(0);
            gvDisks.Rows.Add(new object[] { 0, (byte)Rand.Next(0, 256), false, 127 });
            gvDisks.Rows.Add(new object[] { 1, (byte)Rand.Next(0, 256), true, 22 });
            gvDisks.Rows.Add(new object[] { 2, (byte)Rand.Next(0, 256), true, 89 });
            gvDisks.Rows[0].HeaderCell.Value = 1.ToString();
            gvDisks.Rows[1].HeaderCell.Value = 2.ToString();
            gvDisks.Rows[2].HeaderCell.Value = 3.ToString();

            udDiskCount.Value = 3;

            BuildMachine();
            tbSrcStr_TextChanged(null, null);
        }

        private void BuildMachine()
        {
            //tbKey.Text = Convert.ToString(udDiskCount.Value); 

            byte[] Seeds = new byte[(byte)udDiskCount.Value];
            for (byte i = 0; i < udDiskCount.Value; i++)
            {
                Seeds[i] = Convert.ToByte(gvDisks[1, i].Value);        
            }
            //tbKey.Text += Convert.ToString((byte)udStartPos.Value, 16);
            Enigma = new EnigmaMachine(Seeds);

            //tbSrcStr_TextChanged(null, null);
        }

        private void CustomizeMachine()
        {
            if (Enigma == null)
                return;

            bool[] Dirs = new bool[(int)udDiskCount.Value];
            byte[] StartPoses = new byte[(byte)udDiskCount.Value];
            for (byte i = 0; i < udDiskCount.Value; i++)
            {
                Dirs[i] = Convert.ToBoolean(gvDisks[2, i].Value);
                StartPoses[i] = Convert.ToByte(gvDisks[3, i].Value);
            }
            //tbKey.Text += Convert.ToString((byte)udStartPos.Value, 16);
            
            Enigma.Customize(Dirs, StartPoses);

            //tbSrcStr_TextChanged(null, null);
        }

        private void BuildCustomize()
        {
            byte[] Seeds = new byte[(byte)udDiskCount.Value];
            bool[] Dirs = new bool[(int)udDiskCount.Value];
            byte[] StartPoses = new byte[(byte)udDiskCount.Value];
            for (byte i = 0; i < udDiskCount.Value; i++)
            {
                Seeds[i] = Convert.ToByte(gvDisks[1, i].Value);
                Dirs[i] = Convert.ToBoolean(gvDisks[2, i].Value);
                StartPoses[i] = Convert.ToByte(gvDisks[3, i].Value);
            }

            Enigma = new EnigmaMachine(Seeds);
            Enigma.Customize(Dirs, StartPoses);
        }


        private void btnCrypt_Click(object sender, EventArgs e)
        {
            CustomizeMachine();

            StreamReader Stream = new StreamReader(tbSorceFile.Text);
            byte[] buffer = new byte[Stream.BaseStream.Length];
            int Count = (int)Stream.BaseStream.Length;
            Stream.BaseStream.Read(buffer, 0, Count);
            Stream.Close();
          
            byte[] EncodeArray;
            Enigma.Encode(buffer, out EncodeArray);
            
            StreamWriter StreamW;
            if (tbEncodeFile.Text == "")
            {
                StreamW = new StreamWriter(tbSorceFile.Text + ".encode");
                StreamW.BaseStream.Write(EncodeArray, 0, Count);
                StreamW.Close();
                tbEncodeFile.Text = tbSorceFile.Text + ".encode";
            }
            else
            {
                StreamW = new StreamWriter(tbEncodeFile.Text);
                StreamW.BaseStream.Write(EncodeArray, 0, Count);
                StreamW.Close();
            }        

            MessageBox.Show("File successfully encoded");
        }

        private void udDiskCount_ValueChanged(object sender, EventArgs e)
        {
            if (udDiskCount.Value + 1 > gvDisks.RowCount)
            {
                gvDisks.RowCount = (int)udDiskCount.Value + 1;
                gvDisks.Rows[gvDisks.RowCount - 2].HeaderCell.Value = Convert.ToString(gvDisks.RowCount - 1);
                gvDisks[1, gvDisks.RowCount - 2].Value = (byte)0;
                gvDisks[2, gvDisks.RowCount - 2].Value = Convert.ToBoolean(0);
                gvDisks[3, gvDisks.RowCount - 2].Value = (byte)0;
            }
            else
                gvDisks.RowCount = (int)udDiskCount.Value + 1;

            BuildMachine();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random Rand = new Random();
            for (int i = 0; i < (int)udDiskCount.Value; i++)
            {
                gvDisks[2, i].Value = Convert.ToBoolean(Rand.Next(0, 2));
                gvDisks[3, i].Value = Rand.Next(0, 256);
            }

            CustomizeMachine();
        }

        private void btnRandomDisks_Click(object sender, EventArgs e)
        {
            Random Rand = new Random();
            for (int i = 0; i < (int)udDiskCount.Value; i++)
                gvDisks[1, i].Value = Rand.Next(0, 256);

            BuildMachine();
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            CustomizeMachine();

            if (tbEncodeFile.Text != "")
            {
                StreamReader Stream = new StreamReader(tbEncodeFile.Text);

                byte[] buffer = new byte[Stream.BaseStream.Length];
                int Count = (int)Stream.BaseStream.Length;
                Stream.BaseStream.Read(buffer, 0, Count);
                Stream.Close();

                byte[] DecodeArray;

                Enigma.Decode(buffer, out DecodeArray, cbInverseDecoding.Checked);

                StreamWriter StreamW;
                if (tbDecodeFile.Text == "")
                {
                    string str = tbEncodeFile.Text.Replace(".encode", "");
                    str = str.Insert(str.LastIndexOf('.'), " (decoded)");
                    StreamW = new StreamWriter(str);
                    StreamW.BaseStream.Write(DecodeArray, 0, Count);
                    StreamW.Close();
                    tbDecodeFile.Text = str;
                }
                else
                {
                    StreamW = new StreamWriter(tbDecodeFile.Text);
                    StreamW.BaseStream.Write(DecodeArray, 0, Count);
                    StreamW.Close();
                }

                StreamReader Reader = new StreamReader(tbSorceFile.Text);
                buffer = new byte[Reader.BaseStream.Length];
                Reader.BaseStream.Read(buffer, 0, (int)Reader.BaseStream.Length);
                buffer = SHA1.Create().ComputeHash(buffer);
                DecodeArray = SHA1.Create().ComputeHash(DecodeArray);
                for (int i = 0; i < buffer.Length; i++)
                    if (buffer[i] != DecodeArray[i])
                    {
                        MessageBox.Show("Original and decoded files are not identical. Decoding fail");
                        return;
                    }
                MessageBox.Show("Original and decoded files identical!!! Decoding success");
                    

                //MessageBox.Show("File successfully decoded");
            }
        }

        private void tbSrcStr_TextChanged(object sender, EventArgs e)
        {
            bool[] Dirs = new bool[(int)udDiskCount.Value];
            byte[] StartPoses = new byte[(byte)udDiskCount.Value];
            for (byte i = 0; i < udDiskCount.Value; i++)
            {
                Dirs[i] = Convert.ToBoolean(gvDisks[2, i].Value);
                StartPoses[i] = Convert.ToByte(gvDisks[3, i].Value);
            }

            byte[] SrcAr = new byte[tbSrcStr.Text.Length];
            for (int i = 0; i < SrcAr.Length; i++)
                SrcAr[i] = Convert.ToByte(tbSrcStr.Text[i]);
            
            Enigma.Customize(Dirs, StartPoses);
            byte[] EncodeArray;
            Enigma.Encode(SrcAr, out EncodeArray);
            StringBuilder SB = new StringBuilder(EncodeArray.Length);
            for (int i = 0; i < SrcAr.Length; i++)
                SB.Append((char)EncodeArray[i]);
            tbEncodeStr.Text = SB.ToString();
      
            Enigma.Customize(Dirs, StartPoses);
            byte[] DecodeArray;
            Enigma.Decode(EncodeArray, out DecodeArray, cbInverseDecoding.Checked);
            SB = new StringBuilder(DecodeArray.Length);
            for (int i = 0; i < DecodeArray.Length; i++)
                SB.Append((char)DecodeArray[i]);
            tbDecodeStr.Text = SB.ToString();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (ofdKeys.ShowDialog() == DialogResult.OK)
            {
                StreamReader Reader = new StreamReader(ofdKeys.FileName);
                udDiskCount.Value = Convert.ToDecimal(Reader.ReadLine());
                for (int i = 0; i < (int)udDiskCount.Value; i++)
                {
                    gvDisks[2, i].Value = Convert.ToBoolean(Reader.ReadLine());
                    gvDisks[3, i].Value = Convert.ToByte(Reader.ReadLine());
                }
                Reader.Close();
            }

            CustomizeMachine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sfdKeys.ShowDialog() == DialogResult.OK)
            {
                StreamWriter Writer = new StreamWriter(sfdKeys.FileName);
                Writer.WriteLine((int)udDiskCount.Value);
                for (int i = 0; i < (int)udDiskCount.Value; i++)
                {
                    Writer.WriteLine(gvDisks[2, i].Value);
                    Writer.WriteLine(gvDisks[3, i].Value);
                }
                Writer.Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (sfdMachine.ShowDialog() == DialogResult.OK)
            {
                StreamWriter Writer = new StreamWriter(sfdMachine.FileName);
                Writer.WriteLine((int)udDiskCount.Value);
                for (int i = 0; i < (int)udDiskCount.Value; i++)
                {
                    Writer.WriteLine(gvDisks[1, i].Value);
                }
                Writer.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ofdMachine.ShowDialog() == DialogResult.OK)
            {
                StreamReader Reader = new StreamReader(ofdMachine.FileName);
                udDiskCount.Value = Convert.ToDecimal(Reader.ReadLine());
                for (int i = 0; i < (int)udDiskCount.Value; i++)
                {
                    gvDisks[1, i].Value = Convert.ToByte(Reader.ReadLine());
                }
                Reader.Close();
            }

            BuildMachine();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                tbSorceFile.Text = openFileDialog1.FileName;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Encode files (*.encode)|*.encode|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                tbEncodeFile.Text = openFileDialog1.FileName;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                tbDecodeFile.Text = openFileDialog1.FileName;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            btnCrypt_Click(sender, e);
            btnDecrypt_Click(sender, e);
        }
    }
}
