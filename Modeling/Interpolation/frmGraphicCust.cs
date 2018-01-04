using AdvanceDrawing;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Interpolation
{
	public partial class frmGraphicCust : Form
    {
        public frmGraphicCust()
        {
            InitializeComponent();
        }

        public void CustomizeGraphic(Graphic Graph, bool ShowForm)
        {
            if (ShowForm)
                if (this.ShowDialog() != DialogResult.OK)
                    return;

            Graph.Funcs[0].Pen = new Pen(btnGraph1Color.BackColor);
            Graph.Funcs[0].Pen.DashStyle = (System.Drawing.Drawing2D.DashStyle)cmbGraph1.SelectedIndex;
            Graph.Funcs[1].Pen = new Pen(btnGraph2Color.BackColor);
            Graph.Funcs[1].Pen.DashStyle = (System.Drawing.Drawing2D.DashStyle)cmbGraph2.SelectedIndex;
            Graph.AxisPen = new Pen(btnAxisColor.BackColor);
            Graph.AxisPen.DashStyle = (System.Drawing.Drawing2D.DashStyle)cmbAxisStyle.SelectedIndex;
            Graph.ConstAxis = cbConstAxis.Checked;
            Graph.SaveProportion = cbSaveProp.Checked;
            Graph.GridPen = new Pen(btnGridColor.BackColor);
            Graph.GridPen.DashStyle = (System.Drawing.Drawing2D.DashStyle)cmbGridStyle.SelectedIndex;
            if (cbShowGrid.Checked)
            {
                Graph.gridXCount = (int)udGridXNumber.Value;
                Graph.gridYCount = (int)udGridYNumber.Value;
            }
            else
            {
                Graph.gridXCount = 0;
                Graph.gridYCount = 0;
            }
        }

        public static void LoadGraphic(Graphic Graph)
        {
            StreamReader Reader = new StreamReader("GraphSettings.dat");
            Graph.Funcs = new GraphFunc[2];
            Graph.Funcs[0] = new GraphFunc(new Pen(Color.FromArgb(Convert.ToInt32(Reader.ReadLine()))));
            Graph.Funcs[0].Pen.DashStyle = (System.Drawing.Drawing2D.DashStyle)Convert.ToInt32(Reader.ReadLine());
            Graph.Funcs[0].Visible = true;
            Graph.Funcs[1] = new GraphFunc(new Pen(Color.FromArgb(Convert.ToInt32(Reader.ReadLine()))));
            Graph.Funcs[1].Pen.DashStyle = (System.Drawing.Drawing2D.DashStyle)Convert.ToInt32(Reader.ReadLine());
            Graph.Funcs[1].Visible = true;
            Graph.AxisPen = new Pen(Color.FromArgb(Convert.ToInt32(Reader.ReadLine())));
            Graph.AxisPen.DashStyle = (System.Drawing.Drawing2D.DashStyle)Convert.ToInt32(Reader.ReadLine());
            Graph.AxisPen.DashCap = System.Drawing.Drawing2D.DashCap.Triangle;
            Graph.AxisPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

            Graph.ConstAxis = Convert.ToBoolean(Reader.ReadLine());
            Graph.SaveProportion = Convert.ToBoolean(Reader.ReadLine());
            Graph.GridPen = new Pen(Color.FromArgb(Convert.ToInt32(Reader.ReadLine())));
            Graph.GridPen.DashStyle = (System.Drawing.Drawing2D.DashStyle)Convert.ToInt32(Reader.ReadLine());
            Graph.DrawGrid = Convert.ToBoolean(Reader.ReadLine());
            Graph.gridXCount = Convert.ToInt32(Reader.ReadLine());
            Graph.gridYCount = Convert.ToInt32(Reader.ReadLine());
            Reader.Close();
        }



        public void LoadForm()
        {
            StreamReader Reader = new StreamReader("GraphSettings.dat");
            btnGraph1Color.BackColor = Color.FromArgb(Convert.ToInt32(Reader.ReadLine()));
            cmbGraph1.SelectedIndex = Convert.ToInt32(Reader.ReadLine());
            btnGraph2Color.BackColor = Color.FromArgb(Convert.ToInt32(Reader.ReadLine()));
            cmbGraph2.SelectedIndex = Convert.ToInt32(Reader.ReadLine());
            btnAxisColor.BackColor = Color.FromArgb(Convert.ToInt32(Reader.ReadLine()));
            cmbAxisStyle.SelectedIndex = Convert.ToInt32(Reader.ReadLine());
            cbConstAxis.Checked = Convert.ToBoolean(Reader.ReadLine());
            cbSaveProp.Checked = Convert.ToBoolean(Reader.ReadLine());
            btnGridColor.BackColor = Color.FromArgb(Convert.ToInt32(Reader.ReadLine()));
            cmbGridStyle.SelectedIndex = Convert.ToInt32(Reader.ReadLine());
            cbShowGrid.Checked = Convert.ToBoolean(Reader.ReadLine());
            udGridXNumber.Value = Convert.ToDecimal(Reader.ReadLine());
            udGridYNumber.Value = Convert.ToDecimal(Reader.ReadLine());
            Reader.Close();
        }

        private void frmGraphicCust_FormClosed(object sender, FormClosedEventArgs e)
        {
            StreamWriter Writer = new StreamWriter("GraphSettings.dat");
            Writer.WriteLine(btnGraph1Color.BackColor.ToArgb());
            Writer.WriteLine(cmbGraph1.SelectedIndex);
            Writer.WriteLine(btnGraph2Color.BackColor.ToArgb());
            Writer.WriteLine(cmbGraph2.SelectedIndex);
            Writer.WriteLine(btnAxisColor.BackColor.ToArgb());
            Writer.WriteLine(cmbAxisStyle.SelectedIndex);
            Writer.WriteLine(cbConstAxis.Checked);
            Writer.WriteLine(cbSaveProp.Checked);
            Writer.WriteLine(btnGridColor.BackColor.ToArgb());
            Writer.WriteLine(cmbGridStyle.SelectedIndex);
            Writer.WriteLine(cbShowGrid.Checked);
            Writer.WriteLine(udGridXNumber.Value);
            Writer.WriteLine(udGridYNumber.Value);
            Writer.Close();
        }


        private void frmGraphicCust_Load(object sender, EventArgs e)
        {
            //dataGridView1.Rows.Add(1);
           /* dataGridView1.Rows[0].HeaderCell.Value = "Исходная";
            dataGridView1.Rows[1].HeaderCell.Value = "Интерполянта";*/

            /*dataGridView1[0, 0].Value = new Button();
            (dataGridView1[0, 0].Value as Button).BackColor = Color.Firebrick;
            (dataGridView1[0, 0].Value as Button).Text = "";
            dataGridView1[0, 1].Value = new Button();
            (dataGridView1[0, 1].Value as Button).BackColor = Color.Teal;
            (dataGridView1[0, 1].Value as Button).Text = "";*/

            /*cmbGraph1.SelectedIndex = 4;
            cmbGraph2.SelectedIndex = 4;
            cmbAxisStyle.SelectedIndex = 2;
            cmbGridStyle.SelectedIndex = 3;*/

            LoadForm();
        }

        private void btnGraph1Color_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
                (sender as Button).BackColor = colorDialog.Color;
        }
    }
}
