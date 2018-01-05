using System;
using System.Windows.Forms;
using Utils;

namespace Project5_Haffman
{
    public partial class Form1 : Form
    {
        HaffmanCompressor Compressor;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCrypt_Click(object sender, EventArgs e)
        {
            Compressor = new HaffmanCompressor();

            if (tbEncodeFile.Text == "")
                tbEncodeFile.Text = tbSorceFile.Text + ".hcs";

            PermCounter Counter = new PermCounter();
            Counter.Start();
            float Ratio = Compressor.Compress(tbSorceFile.Text, tbEncodeFile.Text);
            tbEncodeTime.Text = Counter.Finish().ToString("F03");
            MessageBox.Show("File successfully has been compressed");

            tbRatio.Text = Ratio.ToString("F04");
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            Compressor = new HaffmanCompressor();

            if (tbDecodeFile.Text == "")
            {
                string str = tbEncodeFile.Text;
                int ind1 = str.LastIndexOf('.');
                int ind2 = str.LastIndexOf('.', ind1 - 1);
                tbDecodeFile.Text = tbEncodeFile.Text + str.Substring(ind2, ind1 - ind2);
            }

            PermCounter Counter = new PermCounter();
            Counter.Start();
            bool Success = Compressor.Decompress(tbEncodeFile.Text, tbDecodeFile.Text);
            tbDecodeTime.Text = Counter.Finish().ToString("F03");
            //MessageBox.Show("File successfully has been decompressed");

            if (Success)
                MessageBox.Show("Original and decoded files identical!!! Decoding success");
            else
                MessageBox.Show("Original and decoded files are not identical. Decoding fail");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            btnCrypt_Click(sender, e);
            btnDecrypt_Click(sender, e);
        }
    }
}
