using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using DataBase;

namespace ClientShell
{  
    public partial class frmMain : Form
    {
        BooksDatabaseClient Client;

        public frmMain()
        {
            if (Screen.PrimaryScreen.WorkingArea.Width <= 1024)
                this.WindowState = FormWindowState.Maximized;      
      
            InitializeComponent();       
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            var form = new frmLogin(out Client, dgvBooks.Invoke);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)             
                Client.BooksDatabase.Get();
            dgvBooks.DataSource = Client.BooksDatabase; 
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {            

        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {   
            if (Client != null)
                Client.Disconnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new frmAddBook();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Client.BooksDatabase.Add(form.AddingBook);
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Remove item?", "Database client", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                var SelRow = dgvBooks.SelectedRows;
                foreach (DataGridViewRow Row in SelRow)
                    Client.BooksDatabase.RemoveAt(Row.Index);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in dgvBooks.SelectedRows)
            {
                var form = new frmAddBook(Client.BooksDatabase[Row.Index]);
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    Client.BooksDatabase[Row.Index] = form.AddingBook;
            }
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
    }

    /// <summary>
    /// Расширения облегчающие работу с элементами управления в многопоточной среде.
    /// </summary>
    public static class ControlExtentions
    {
        /// <summary>
        /// Вызов делегата через control.Invoke, если это необходимо.
        /// </summary>
        /// <param name="control">Элемент управления</param>
        /// <param name="doit">Делегат с некоторым действием</param>
        public static void InvokeIfNeeded(this Control control, Action doit)
        {
            if (control.InvokeRequired)
                control.Invoke(doit);
            else
                doit();
        }

        /// <summary>
        /// Вызов делегата через control.Invoke, если это необходимо.
        /// </summary>
        /// <typeparam name="T">Тип параметра делегата</typeparam>
        /// <param name="control">Элемент управления</param>
        /// <param name="doit">Делегат с некоторым действием</param>
        /// <param name="arg">Аргумент делагата с действием</param>
        public static void InvokeIfNeeded<T>(this Control control, Action<T> doit, T arg)
        {
            if (control.InvokeRequired)
                control.Invoke(doit, arg);
            else
                doit(arg);
        }
    }
}
