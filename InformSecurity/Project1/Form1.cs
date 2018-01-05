using AdvForms;
using ComputerStat;
using System;
using System.IO;
using System.Windows.Forms;

namespace Project1
{
    public partial class Form1 : Form
    {
        string Project1RunPath = "..\\..\\Project1Run\\bin";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            {
                MessageForm Form = new MessageForm();
                Form.Left = Left - Form.Width / 2;
                Form.Top = Top - Form.Height / 2;
                Form.Show();
                Hardware MachineID = new Hardware();
                textBox1.Text = MachineID.InformString.ToString();
                textBox2.Text = MachineID.HashCode;
                Form.Close();

                if (File.Exists("..\\..\\Project1Run\\bin\\Project1Run.exe"))
                {
                    Project1RunPath = "..\\..\\Project1Run\\bin";
                    StreamWriter stream = new StreamWriter("..\\..\\Project1Run\\bin\\licence.dat");
                    stream.Write(MachineID.HashCode);
                    stream.Close();
                }
                else
                {
                    openFileDialog1.Title = "Выберите файл Project1Run.exe";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        Project1RunPath = Path.GetDirectoryName(openFileDialog1.FileName);
                        StreamWriter stream = new StreamWriter(Project1RunPath + "\\licence.dat");
                        stream.Write(MachineID.HashCode);
                        stream.Close();
                    }

                    
                }
                
                textBox3.Text = "Файл licence.dat успешно помещен в директорию с Project1Run.exe";
                MessageBox.Show("Установка успешно завершена");
            }
           /* catch
            {
                textBox3.Text = "";
                MessageBox.Show("Ошибка при установке");                
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(Project1RunPath + "\\licence.dat"))
                    throw new Exception();

                File.Delete(Project1RunPath + "\\licence.dat");
                textBox3.Text = "Файл licence.dat успешно удален из директории с Project1Run.exe";
                MessageBox.Show("Деинсталляция успешно завершена");                
            }
            catch
            {                
                textBox3.Text = "";
                MessageBox.Show("Ошибка при деинтсалляции");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Project1RunPath + "\\Project1Run.exe");
            }
            catch
            {
                MessageBox.Show("Файл " + Project1RunPath + "\\Project1Run.exe не найден");
            }
        }
    }
}
