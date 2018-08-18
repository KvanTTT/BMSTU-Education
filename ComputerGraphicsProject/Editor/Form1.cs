using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Geometry3D;
using Lighting;
using Utils;

namespace Editor
{
    public partial class Form1 : Form
    {
        Color[] Colors = new Color[175];
        Landscape L = new Landscape();

        public Vector ColorToVector(Color C)
        {
            return new Vector(C.R / 255.0, C.G / 255.0, C.B / 255.0);
        }

        public Vector StringToVector(string S)
        {
            int ind = S.IndexOf(';');
            int ind1 = S.IndexOf(';', ind + 1);

            return new Vector(Convert.ToDouble(S.Substring(0, ind)), Convert.ToDouble(S.Substring(ind + 1, ind1 - ind - 1)),
                Convert.ToDouble(S.Substring(ind1 + 1, S.Length - ind1 - 1)));
        }

        public Vector StringToNormVector(string S)
        {
            int ind = S.IndexOf(';');
            int ind1 = S.IndexOf(';', ind + 1);
            double x = Convert.ToDouble(S.Substring(0, ind));
            double y = Convert.ToDouble(S.Substring(ind + 1, ind1 - ind - 1));
            double z = Convert.ToDouble(S.Substring(ind1 + 1, S.Length - ind1 - 1));

            Vector Res = new Vector(x, y, z);
            return Res.Normalize();
        } 


        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                tbSky.Text = folderBrowserDialog1.SelectedPath;           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                textBox1.Text = folderBrowserDialog1.SelectedPath;  
        }



        private void button3_Click(object sender, EventArgs e)
        {
            int LightCount = 0;
            if (cbLight1.Checked) LightCount++;
            if (cbLight2.Checked) LightCount++;
            if (cbLight3.Checked) LightCount++;
            if (cbLight4.Checked) LightCount++;
            if (cbLight5.Checked) LightCount++;

            Light[] Sources = new Light[LightCount];

            double c1 = Convert.ToDouble(tb_c1.Text);
            double c2 = Convert.ToDouble(tb_c2.Text);
            double c3 = Convert.ToDouble(tb_c3.Text);

            Vector S = new Vector(Convert.ToDouble(tbSizeX.Text), Convert.ToDouble(tbSizeY.Text), Convert.ToDouble(tbSizeZ.Text));

            int i = 0, j;
            if (cbLight1.Checked)
            {
                if (cbPointLight1.Checked)
                    Sources[i] = new PointLight(ColorToVector(btnAmbient1.BackColor), ColorToVector(btnDiffuse1.BackColor),
                        ColorToVector(btnSpecular1.BackColor), c1, c2, c3, StringToVector(tbLight1.Text) | S);
                else
                    Sources[i] = new DirLight(ColorToVector(btnAmbient1.BackColor), ColorToVector(btnDiffuse1.BackColor), 
                        ColorToVector(btnSpecular1.BackColor), StringToNormVector(tbLight1.Text));
                i++;
            }
            if (cbLight2.Checked)
            {
                if (cbPointLight2.Checked)
                    Sources[i] = new PointLight(ColorToVector(btnAmbient2.BackColor), ColorToVector(btnDiffuse2.BackColor),
                        ColorToVector(btnSpecular2.BackColor), c1, c2, c3, StringToVector(tbLight2.Text) | S);
                else
                    Sources[i] = new DirLight(ColorToVector(btnAmbient2.BackColor), ColorToVector(btnDiffuse2.BackColor),
                        ColorToVector(btnSpecular2.BackColor), StringToNormVector(tbLight2.Text));
                i++;
            }
            if (cbLight3.Checked)
            {
                if (cbPointLight3.Checked)
                    Sources[i] = new PointLight(ColorToVector(btnAmbient3.BackColor), ColorToVector(btnDiffuse3.BackColor),
                        ColorToVector(btnSpecular3.BackColor), c1, c2, c3, StringToVector(tbLight3.Text) | S);
                else
                    Sources[i] = new DirLight(ColorToVector(btnAmbient3.BackColor), ColorToVector(btnDiffuse3.BackColor),
                        ColorToVector(btnSpecular3.BackColor), StringToNormVector(tbLight3.Text));
                i++;
            }
            if (cbLight4.Checked)
            {
                if (cbPointLight4.Checked)
                    Sources[i] = new PointLight(ColorToVector(btnAmbient4.BackColor), ColorToVector(btnDiffuse4.BackColor),
                        ColorToVector(btnSpecular4.BackColor), c1, c2, c3, StringToVector(tbLight4.Text) | S);
                else
                    Sources[i] = new DirLight(ColorToVector(btnAmbient4.BackColor), ColorToVector(btnDiffuse4.BackColor),
                        ColorToVector(btnSpecular4.BackColor), StringToNormVector(tbLight4.Text));
                i++;
            }
            if (cbLight5.Checked)
            {
                if (cbPointLight5.Checked)
                    Sources[i] = new PointLight(ColorToVector(btnAmbient5.BackColor), ColorToVector(btnDiffuse5.BackColor),
                        ColorToVector(btnSpecular5.BackColor), c1, c2, c3, StringToVector(tbLight5.Text) | S);
                else
                    Sources[i] = new DirLight(ColorToVector(btnAmbient5.BackColor), ColorToVector(btnDiffuse5.BackColor),
                        ColorToVector(btnSpecular5.BackColor), StringToNormVector(tbLight5.Text));
                i++;
            }

            L.Ground = new Material(ColorToVector(btnAmbient.BackColor), ColorToVector(btnDiffuse.BackColor), ColorToVector(btnSpecular.BackColor));

            float AllTime = 0, t;
            PerfCounter Counter1 = new PerfCounter();

            Counter1.Start();
            Convolution[] Convs = new Convolution[3];
            Convs[0].Operation = Operation.Plus;
            Convs[1].Operation = (Operation)cmbOp2.SelectedIndex;
            Convs[2].Operation = (Operation)cmbOp3.SelectedIndex;
            Convs[0].Coef = Convert.ToDouble(tbCoef1.Text);
            Convs[1].Coef = Convert.ToDouble(tbCoef2.Text);
            Convs[2].Coef = Convert.ToDouble(tbCoef3.Text);
            L.GenerateHeightmap(Convert.ToInt32(tbSizeX.Text), Convert.ToInt32(tbSizeY.Text), (GenMethod)cmbGenMethod.SelectedIndex, Convs, 
                cbSmoothing.Checked, cbValley.Checked, cbIsland.Checked);
            t = Counter1.Finish();
            lblHeightmapTime.Text = Convert.ToString(t);
            AllTime += t;

            L.BuildMesh(new Vector(Convert.ToDouble(tbSizeX.Text), Convert.ToDouble(tbSizeY.Text), Convert.ToDouble(tbSizeZ.Text)));
                        
            Counter1.Start();
            L.BuildLightmap(Sources, 2);
            t = Counter1.Finish();
            lblLightmapTime.Text = Convert.ToString(t);
            AllTime += t;

            Counter1.Start();
            L.GenerateColormap(Colors, 1);
            t = Counter1.Finish();
            lblTextureTime.Text = Convert.ToString(t);
            AllTime += t;

            lblAllTime.Text = Convert.ToString(AllTime);

            string DirName;
            /*if (tbDir.Text[0] == '.')
            {
                DirName = "../../" + tbDir.Text + "/" + tbName.Text;
                Directory.CreateDirectory(DirName);
            }
            else*/
                DirName = tbDir.Text + "/" + tbName.Text;


            Bitmap B1, B2, B3;
           
            L.SaveHeightmap(DirName + "/Heightmap.bmp", out B1);
            L.SaveLightmap(DirName + "/Lightmap.bmp", out B2);
            L.SaveColormap(DirName + "/Texture.bmp", out B3);

            //pictureBox1.Image = B1;
            Bitmap B = new Bitmap(L.SizeX, L.SizeY);
            for (i = 0; i < B.Width; i++)
                for (j = 0; j < B.Height; j++)
                {
                    byte X = Convert.ToByte(L.Heightmap[i, j] * 255); 
                    B.SetPixel(i, j, Color.FromArgb(X, X, X));
                }
            pictureBox1.Image = B;
            pictureBox2.Image = B2;
            pictureBox3.Image = B3;
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            pictureBox3.Refresh();

            SaveSettings("EditorSettings.ini");
        }

        private void button4_Click(object sender, EventArgs e)
        {
           /* StreamWriter stream = new StreamWriter("../../../Settings.ini");
            if (radioButton1.Checked)
                stream.Write(true);
            else
                stream.Write(false);
            stream.Write(textBox1.Text);
            stream.Write(textBox2.Text);
            stream.Write(tbSky.Text);
            stream.Close();*/

            SaveViewerSettings("ViewerSettings.ini");
            System.Diagnostics.Process.Start("LandViewer.exe");
        }

        public void SetupGradient()
        {
            Color[] BaseColors = new Color[8];
            BaseColors[0] = button7.BackColor;
            BaseColors[1] = button8.BackColor;
            BaseColors[2] = button9.BackColor;
            BaseColors[3] = button10.BackColor;
            BaseColors[4] = button11.BackColor;
            BaseColors[5] = button12.BackColor;
            BaseColors[6] = button13.BackColor;
            BaseColors[7] = button14.BackColor;

            Bitmap Gradient = new Bitmap(pbGradient.Width, pbGradient.Height);
            pbGradient.Image = Gradient;
            Graphics G = Graphics.FromImage(Gradient);
            double k;
            double d = 1.0 / 25;

            for (int i = 0; i < 8 - 1; i++)
            {
                k = 1;
                for (int j = 0; j < 25; j++)
                {
                    Colors[i * 25 + j] = Color.FromArgb(Convert.ToByte(BaseColors[i].R * k + BaseColors[i + 1].R * (1 - k)),
                                         Convert.ToByte(BaseColors[i].G * k + BaseColors[i + 1].G * (1 - k)),
                                         Convert.ToByte(BaseColors[i].B * k + BaseColors[i + 1].B * (1 - k)));
                    G.DrawLine(new Pen(Colors[i * 25 + j]), i * 25 + j, 0, i * 25 + j, pbGradient.Height);
                    k -= d;
                }
            }
        }

        public void SaveViewerSettings(string FileName)
        {
            StreamWriter stream = new StreamWriter(FileName);
            if (radioButton1.Checked)
                stream.WriteLine(textBox1.Text + "/");
            else
                stream.WriteLine(tbDir.Text + "/" + tbName.Text + "/");
            stream.Write(tbSky.Text + "/");
            stream.Close();    
        }

        public void SaveSettings(string FileName)
        {
            StreamWriter stream = new StreamWriter(FileName);

            stream.WriteLine(radioButton1.Checked);
            stream.WriteLine(radioButton2.Checked);
            stream.WriteLine(textBox1.Text);
            stream.WriteLine(tbDir.Text);
            stream.WriteLine(tbName.Text);
            stream.WriteLine(tbSky.Text);
            stream.WriteLine(cmbLandType.SelectedIndex);
            stream.WriteLine(tbSizeX.Text);
            stream.WriteLine(tbSizeY.Text);
            stream.WriteLine(tbSizeZ.Text);
            stream.WriteLine(btnAmbient.BackColor.ToArgb());
            stream.WriteLine(btnDiffuse.BackColor.ToArgb());
            stream.WriteLine(btnSpecular.BackColor.ToArgb());

            stream.WriteLine(button7.BackColor.ToArgb());
            stream.WriteLine(button8.BackColor.ToArgb());
            stream.WriteLine(button9.BackColor.ToArgb());
            stream.WriteLine(button10.BackColor.ToArgb());
            stream.WriteLine(button11.BackColor.ToArgb());
            stream.WriteLine(button12.BackColor.ToArgb());
            stream.WriteLine(button13.BackColor.ToArgb());
            stream.WriteLine(button14.BackColor.ToArgb());

            stream.WriteLine(cbLight1.Checked);
            stream.WriteLine(cbLight2.Checked);
            stream.WriteLine(cbLight3.Checked);
            stream.WriteLine(cbLight4.Checked);
            stream.WriteLine(cbLight5.Checked);

            stream.WriteLine(btnAmbient1.BackColor.ToArgb());
            stream.WriteLine(btnDiffuse1.BackColor.ToArgb());
            stream.WriteLine(btnSpecular1.BackColor.ToArgb());
            stream.WriteLine(btnAmbient2.BackColor.ToArgb());
            stream.WriteLine(btnDiffuse2.BackColor.ToArgb());
            stream.WriteLine(btnSpecular2.BackColor.ToArgb());
            stream.WriteLine(btnAmbient3.BackColor.ToArgb());
            stream.WriteLine(btnDiffuse3.BackColor.ToArgb());
            stream.WriteLine(btnSpecular3.BackColor.ToArgb());
            stream.WriteLine(btnAmbient4.BackColor.ToArgb());
            stream.WriteLine(btnDiffuse4.BackColor.ToArgb());
            stream.WriteLine(btnSpecular4.BackColor.ToArgb());
            stream.WriteLine(btnAmbient5.BackColor.ToArgb());
            stream.WriteLine(btnDiffuse5.BackColor.ToArgb());
            stream.WriteLine(btnSpecular5.BackColor.ToArgb());

            stream.WriteLine(tbLight1.Text);
            stream.WriteLine(tbLight2.Text);
            stream.WriteLine(tbLight3.Text);
            stream.WriteLine(tbLight4.Text);
            stream.WriteLine(tbLight5.Text);

            stream.WriteLine(cbPointLight1.Checked);
            stream.WriteLine(cbPointLight2.Checked);
            stream.WriteLine(cbPointLight3.Checked);
            stream.WriteLine(cbPointLight4.Checked);
            stream.WriteLine(cbPointLight5.Checked);

            stream.WriteLine(tb_c1.Text);
            stream.WriteLine(tb_c2.Text);
            stream.WriteLine(tb_c3.Text);

            stream.WriteLine(cmbGenMethod.SelectedIndex);

            stream.WriteLine(tbCoef1.Text);
            stream.WriteLine(tbCoef2.Text);
            stream.WriteLine(tbCoef3.Text);

            stream.WriteLine(cmbOp1.SelectedIndex);
            stream.WriteLine(cmbOp2.SelectedIndex);
            stream.WriteLine(cmbOp3.SelectedIndex);

            stream.WriteLine(cbIsland.Checked);
            stream.WriteLine(cbValley.Checked);
            stream.WriteLine(cbSmoothing.Checked);

            stream.Close();
        }

        public void LoadSettings(string FileName)
        {
            StreamReader stream = new StreamReader(FileName);

            radioButton1.Checked = Convert.ToBoolean(stream.ReadLine());
            radioButton2.Checked = Convert.ToBoolean(stream.ReadLine());
            textBox1.Text = stream.ReadLine();
            tbDir.Text = stream.ReadLine();
            tbName.Text = stream.ReadLine();
            tbSky.Text = stream.ReadLine();
            cmbLandType.SelectedIndex = Convert.ToInt32(stream.ReadLine());
            tbSizeX.Text = stream.ReadLine();
            tbSizeY.Text = stream.ReadLine();
            tbSizeZ.Text = stream.ReadLine();
            btnAmbient.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            btnDiffuse.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            btnSpecular.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));

            button7.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            button8.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            button9.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            button10.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            button11.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            button12.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            button13.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            button14.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));

            cbLight1.Checked = Convert.ToBoolean(stream.ReadLine());
            cbLight2.Checked = Convert.ToBoolean(stream.ReadLine());
            cbLight3.Checked = Convert.ToBoolean(stream.ReadLine());
            cbLight4.Checked = Convert.ToBoolean(stream.ReadLine());
            cbLight5.Checked = Convert.ToBoolean(stream.ReadLine());

            btnAmbient1.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            btnDiffuse1.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            btnSpecular1.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            btnAmbient2.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            btnDiffuse2.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            btnSpecular2.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            btnAmbient3.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            btnDiffuse3.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            btnSpecular3.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            btnAmbient4.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            btnDiffuse4.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            btnSpecular4.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            btnAmbient5.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            btnDiffuse5.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            btnSpecular5.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));

            tbLight1.Text = stream.ReadLine();
            tbLight2.Text = stream.ReadLine();
            tbLight3.Text = stream.ReadLine();
            tbLight4.Text = stream.ReadLine();
            tbLight5.Text = stream.ReadLine();

            cbPointLight1.Checked = Convert.ToBoolean(stream.ReadLine());
            cbPointLight2.Checked = Convert.ToBoolean(stream.ReadLine());
            cbPointLight3.Checked = Convert.ToBoolean(stream.ReadLine());
            cbPointLight4.Checked = Convert.ToBoolean(stream.ReadLine());
            cbPointLight5.Checked = Convert.ToBoolean(stream.ReadLine());

            tb_c1.Text = stream.ReadLine();
            tb_c2.Text = stream.ReadLine();
            tb_c3.Text = stream.ReadLine();

            cmbGenMethod.SelectedIndex = Convert.ToInt32(stream.ReadLine());

            tbCoef1.Text = stream.ReadLine();
            tbCoef2.Text = stream.ReadLine();
            tbCoef3.Text = stream.ReadLine();

            cmbOp1.SelectedIndex = Convert.ToInt32(stream.ReadLine());
            cmbOp2.SelectedIndex = Convert.ToInt32(stream.ReadLine());
            cmbOp3.SelectedIndex = Convert.ToInt32(stream.ReadLine());

            cbIsland.Checked = Convert.ToBoolean(stream.ReadLine());
            cbValley.Checked = Convert.ToBoolean(stream.ReadLine());
            cbSmoothing.Checked = Convert.ToBoolean(stream.ReadLine());

            SetupGradient();

            stream.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {         
            LoadSettings("EditorSettings.ini");

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

            

            //cmbLandType.SelectedIndex = 0;
            //cmbOp1.SelectedIndex = 0;
            //cmbOp2.SelectedIndex = 0;
            //cmbOp3.SelectedIndex = 0;
            //cmbGenMethod.SelectedIndex = 0;

       

            /*Landscape L = new Landscape();
            Lighting.Light[] Sources = new Lighting.Light[3];

            Sources[0] = new Lighting.DirLight(new Vector(0.8, 0.8, 0.8), new Vector(0.8, 0.8, 0.8), new Vector(0, 0, 0), new Vector(1, 0, -1));
            Sources[1] = new Lighting.DirLight(new Vector(0.8, 0.8, 0.8), new Vector(0.8, 0.8, 0.8), new Vector(0, 0, 0), new Vector(-1, 0, -1));
            Sources[2] = new Lighting.PointLight(new Vector(0.8, 0.8, 0.8), new Vector(0.6, 0.6, 0.6), new Vector(0, 0, 0), 1, 0.0006, 0.0004, new Vector(0, 0, 80));            

            L.GenerateHeightmap(512, 512, 0.03);
            L.BuildMesh();

            L.Ground = new Material(new Vector(0.6, 0.6, 0.6), new Vector(0.4, 0.4, 0.4), new Vector(0, 0, 0));
            L.BuildLightmap(Sources, 2);

            L.SaveHeightmap("../../../Data/Heightmap4.bmp");
            L.SaveLightmap("../../../Data/Lightmap.bmp");*/
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings("EditorSettings.ini");
            SaveViewerSettings("ViewerSettings.ini");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                (sender as Button).BackColor = colorDialog1.Color;
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbLight4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                tbDir.Text = folderBrowserDialog1.SelectedPath;  
        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void tbDir_TextChanged(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                (sender as Button).BackColor = colorDialog1.Color;
                SetupGradient();
            }
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void SaveGeneratorSettings(string FileName)
        {
            StreamWriter stream = new StreamWriter(FileName);

            stream.WriteLine(cmbLandType.SelectedIndex);
            stream.WriteLine(tbSizeX.Text);
            stream.WriteLine(tbSizeY.Text);
            stream.WriteLine(tbSizeZ.Text);
            stream.WriteLine(btnAmbient.BackColor.ToArgb());
            stream.WriteLine(btnDiffuse.BackColor.ToArgb());
            stream.WriteLine(btnSpecular.BackColor.ToArgb());

            stream.WriteLine(button7.BackColor.ToArgb());
            stream.WriteLine(button8.BackColor.ToArgb());
            stream.WriteLine(button9.BackColor.ToArgb());
            stream.WriteLine(button10.BackColor.ToArgb());
            stream.WriteLine(button11.BackColor.ToArgb());
            stream.WriteLine(button12.BackColor.ToArgb());
            stream.WriteLine(button13.BackColor.ToArgb());
            stream.WriteLine(button14.BackColor.ToArgb());

            stream.WriteLine(cmbGenMethod.SelectedIndex);

            stream.WriteLine(tbCoef1.Text);
            stream.WriteLine(tbCoef2.Text);
            stream.WriteLine(tbCoef3.Text);

            stream.WriteLine(cmbOp1.SelectedIndex);
            stream.WriteLine(cmbOp2.SelectedIndex);
            stream.WriteLine(cmbOp3.SelectedIndex);

            stream.WriteLine(cbIsland.Checked);
            stream.WriteLine(cbValley.Checked);
            stream.WriteLine(cbSmoothing.Checked);

            stream.Close();
        }

        public void LoadGeneratorSettings(string FileName)
        {
            StreamReader stream = new StreamReader(FileName);

            cmbLandType.SelectedIndex = Convert.ToInt32(stream.ReadLine());
            tbSizeX.Text = stream.ReadLine();
            tbSizeY.Text = stream.ReadLine();
            tbSizeZ.Text = stream.ReadLine();
            btnAmbient.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            btnDiffuse.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            btnSpecular.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));

            button7.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            button8.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            button9.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            button10.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            button11.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            button12.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            button13.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));
            button14.BackColor = Color.FromArgb(Convert.ToInt32(stream.ReadLine()));

            cmbGenMethod.SelectedIndex = Convert.ToInt32(stream.ReadLine());

            tbCoef1.Text = stream.ReadLine();
            tbCoef2.Text = stream.ReadLine();
            tbCoef3.Text = stream.ReadLine();

            cmbOp1.SelectedIndex = Convert.ToInt32(stream.ReadLine());
            cmbOp2.SelectedIndex = Convert.ToInt32(stream.ReadLine());
            cmbOp3.SelectedIndex = Convert.ToInt32(stream.ReadLine());

            cbIsland.Checked = Convert.ToBoolean(stream.ReadLine());
            cbValley.Checked = Convert.ToBoolean(stream.ReadLine());
            cbSmoothing.Checked = Convert.ToBoolean(stream.ReadLine());

            stream.Close();

            SetupGradient();
        }

        private void cmbLandType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGeneratorSettings(cmbLandType.Text + ".lprm");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveGeneratorSettings(cmbLandType.Text + ".lprm");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            LoadGeneratorSettings(cmbLandType.Text + ".lprm");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Convolution[] Convs = new Convolution[3];
            Convs[0].Operation = Operation.Plus;
            Convs[1].Operation = (Operation)cmbOp2.SelectedIndex;
            Convs[2].Operation = (Operation)cmbOp3.SelectedIndex;
            Convs[0].Coef = Convert.ToDouble(tbCoef1.Text);
            Convs[1].Coef = Convert.ToDouble(tbCoef2.Text);
            Convs[2].Coef = Convert.ToDouble(tbCoef3.Text);
            L.GenerateHeightmap(Convert.ToInt32(tbSizeX.Text), Convert.ToInt32(tbSizeY.Text), (GenMethod)cmbGenMethod.SelectedIndex, Convs,
                cbSmoothing.Checked, cbValley.Checked, cbIsland.Checked);

            L.BuildMesh(new Vector(Convert.ToDouble(tbSizeX.Text), Convert.ToDouble(tbSizeY.Text), Convert.ToDouble(tbSizeZ.Text)));

            int i, j;
            Bitmap B = new Bitmap(L.SizeX, L.SizeY);
            for (i = 0; i < B.Width; i++)
                for (j = 0; j < B.Height; j++)
                {
                    byte X = Convert.ToByte(L.Heightmap[i, j] * 255);
                    B.SetPixel(i, j, Color.FromArgb(X, X, X));
                }
            pictureBox1.Image = B;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //L.BuildLightmap(
        }

    }
}
