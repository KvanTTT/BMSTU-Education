using System;
using System.Windows.Forms;

namespace AdvForms
{
    public partial class MessageForm : Form
    {
        int PointCount = 0;

        public MessageForm()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PointCount++;
            if (PointCount == 8)
                PointCount = 1;
            label3.Text = "";
            for (int i = 0; i < PointCount; i++)
                label3.Text += '.';
        }
    }
}
