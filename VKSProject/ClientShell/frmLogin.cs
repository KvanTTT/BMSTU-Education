using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Networking;
using DataBase;

namespace ClientShell
{
    public partial class frmLogin : Form
    {
        string Key =       "jU7Fek0g9Bjeior63Hknf71Nvjdk8Qhe";
        string Vector =    "jfe7Uh83BhFGu34ui789EdUjel89uhqp";
        string NetKey =    "SWhu0CqD2chOgnZzejaEbDmlgIy0EDuj";
        string NetVector = "BX9NevM8zlzNAAkYGy9enVGkKFjNEWZp";
        List<string> Passwords;
        BooksDatabaseClient Client;

        public frmLogin(out BooksDatabaseClient Client, InvokeDelegate Invoke)
        {            
            InitializeComponent();

            Client = null;
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            foreach (IPAddress ipAddress in ipHostInfo.AddressList)
                cmbHost.Items.Add(ipAddress);
            try
            {
                SymmetricCryption Crypt = new SymmetricCryption(Key, Vector);
                string File = Crypt.Decrypt("Settings");
                if (File != "")
                {                    
                    string[] Strings = File.Split(new string[] { " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    int j = 0;
                    int l = Convert.ToInt32(Strings[j++]);
                    Passwords = new List<string>(l);
                    for (int i = 0; i < l; i += 1)
                    {
                        cmbLogin.Items.Add(Strings[j++]);
                        if (Convert.ToBoolean(Strings[j++]))
                            Passwords.Add(Strings[j++]);
                    }
                    cmbLogin.SelectedIndex = Convert.ToInt32(Strings[j++]);
                    cmbHost.SelectedIndex = Convert.ToInt32(Strings[j++]);
                    udPort.Value = Convert.ToInt32(Strings[j++]);
                    Client = new BooksDatabaseClient(ipHostInfo.AddressList[0].ToString(), (ushort)udPort.Value, Invoke);        
                }
            }
            catch
            {
                Client = new BooksDatabaseClient(ipHostInfo.AddressList[0].ToString(), 11000, Invoke);
                Passwords = new List<string>();
            }
            this.Client = Client;
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(cmbLogin.Items.Count);
                sb.Append(Environment.NewLine);
                for (int i = 0; i < cmbLogin.Items.Count; i += 1)
                {
                    sb.Append(cmbLogin.Items[i]);
                    sb.Append(' ');
                    if (Passwords[i] != "")
                    {
                        sb.Append(true);
                        sb.Append(' ');
                        sb.Append(Passwords[i]);
                    }
                    else
                    {
                        sb.Append(false);
                    }
                    sb.Append(Environment.NewLine);
                }
                sb.Append(cmbLogin.SelectedIndex);
                sb.Append(Environment.NewLine);
                sb.Append(cmbHost.SelectedIndex);
                sb.Append(Environment.NewLine);
                sb.Append(udPort.Value);
                sb.Append(Environment.NewLine);

                SymmetricCryption Crypt = new SymmetricCryption(Key, Vector);
                Crypt.Encrypt("Settings", sb.ToString());                
            }
            catch
            {

            }
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SymmetricCryption Crypt = new SymmetricCryption(NetKey, NetVector);
            Client.LogPas = /*Encoding.UTF8.GetString(Crypt.Encrypt(*/cmbLogin.Text + ' ' + tbPassword.Text/*))*/;
            Client.Connect();
            Client.Login();
            System.Threading.Thread.Sleep(1000);
            

            if (!Client.Info.Socket.Connected)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                MessageBox.Show("Connection has not established. Wrong Login or password");
            }
            else
            {
                bool Exists = false;
                foreach (string Login in cmbLogin.Items)
                    if (Login == cmbLogin.Text)
                    {
                        Exists = true;
                        break;
                    }
                if (!Exists)
                {
                    cmbLogin.Items.Add(cmbLogin.Text);
                    cmbLogin.SelectedIndex = cmbLogin.Items.Count - 1;
                    Passwords.Add(tbPassword.Text);
                }    
            }
        }

        private void cmbLogin_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbPassword.Text = Passwords[cmbLogin.SelectedIndex];
            if (tbPassword.Text != "")
                cbSavePassword.Checked = true;
        }

        private void cmbHost_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }


        
    }
}
