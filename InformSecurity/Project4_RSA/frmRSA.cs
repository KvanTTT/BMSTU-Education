using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using Utils;

namespace Project4_RSA
{
    public partial class frmRSA : Form
    {
        public frmRSA()
        {
            InitializeComponent();
        }

        private void frmRSA_Load(object sender, EventArgs e)
        {
            btnRandSeed_Click(sender, e);
        }

        private void btnCrypt_Click(object sender, EventArgs e)
        {
            if (tbEncodeFile.Text == "")
                tbEncodeFile.Text = tbSorceFile.Text + ".encode";

            PermCounter Counter = new PermCounter();
            Counter.Start();
            RSACrypt.Encode(tbSorceFile.Text, tbEncodeFile.Text, new RSAKey(Convert.ToInt64(tbKey1.Text), Convert.ToInt64(tbModule.Text)));
            tbEncodeTime.Text = Counter.Finish().ToString("F03");

            MessageBox.Show("File successfully encoded");
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (tbDecodeFile.Text == "")
            {
                string str = tbEncodeFile.Text;
                int ind1 = str.LastIndexOf('.');
                int ind2 = str.LastIndexOf('.', ind1-1);
                tbDecodeFile.Text = tbEncodeFile.Text + str.Substring(ind2, ind1 - ind2);
            }
            PermCounter Counter = new PermCounter();
            Counter.Start();
            RSACrypt.Decode(tbEncodeFile.Text, tbDecodeFile.Text, new RSAKey(Convert.ToInt64(tbKey2.Text), Convert.ToInt64(tbModule.Text)));
            tbDecodeTime.Text = Counter.Finish().ToString("F03");

            StreamReader Reader = new StreamReader(tbSorceFile.Text);
            byte[] OrigArray = new byte[Reader.BaseStream.Length];
            Reader.BaseStream.Read(OrigArray, 0, (int)Reader.BaseStream.Length);
            Reader.Close();
            Reader = new StreamReader(tbDecodeFile.Text);
            byte[] DecodeArray = new byte[Reader.BaseStream.Length];
            Reader.BaseStream.Read(DecodeArray, 0, (int)Reader.BaseStream.Length);
            Reader.Close();
  
            OrigArray = SHA1.Create().ComputeHash(OrigArray);
            DecodeArray = SHA1.Create().ComputeHash(DecodeArray);
            if (OrigArray.SequenceEqual(DecodeArray))
                MessageBox.Show("Original and decoded files identical!!! Decoding success");
            else
                MessageBox.Show("Original and decoded files are not identical. Decoding fail");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            btnCrypt_Click(sender, e);
            btnDecrypt_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sfdKey.ShowDialog() == DialogResult.OK)
            {
                StreamWriter Writer = new StreamWriter(sfdKey.FileName);
                Writer.WriteLine(tbKey1.Text);
                Writer.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ofdKey.ShowDialog() == DialogResult.OK)
            {
                StreamReader Reader = new StreamReader(ofdKey.FileName);
                tbKey1.Text = Reader.ReadLine();
                Reader.Close();
            }
        }

        private void btnRandSeed_Click(object sender, EventArgs e)
        {
            /*Random Rand = new Random();
            tbKey.Text = Convert.ToString((ulong)Rand.Next() + ((ulong)(Rand.Next()) << 32));*/
            RSACrypt Rsa = new RSACrypt();
            Rsa.GenerateNewKeys();
            tbKey1.Text = Convert.ToString(Rsa.E);
            tbKey2.Text = Convert.ToString(Rsa.D);
            tbModule.Text = Convert.ToString(Rsa.N);

            RSACrypt Crypt = new RSACrypt();
            Crypt.GenerateNewKeys();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                tbSorceFile.Text = openFileDialog1.FileName;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                tbEncodeFile.Text = openFileDialog1.FileName;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                tbDecodeFile.Text = openFileDialog1.FileName;
        }

        private void tbKey_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < '0' || e.KeyChar > '9')
                e.KeyChar = (char)0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SimpleNumber SN = new SimpleNumber();
            PermCounter PC = new PermCounter();
            PC.Start();
            SN.WriteSimpleNumbers("../../Data/Simple Numbers.dat", uint.MaxValue);
            MessageBox.Show("Complete at " + PC.Finish().ToString());
        }
    }
}
