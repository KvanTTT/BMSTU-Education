using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataBase;

namespace ClientShell
{
    public partial class frmAddBook : Form
    {
        public Book AddingBook
        {
            private set;
            get;
        }

        Book TempBook;

        public frmAddBook()
        {
            InitializeComponent();
        }

        public frmAddBook(Book B) : this()
        {
            TempBook = B;
            tbName.Text = B.Name;
            tbISBN.Text = B.ISBN;
            tbAuthor.Text = B.Author;
            tbGenre.Text = B.Genre;
            tbPublisher.Text = B.Publisher;
            tbLanguage.Text = B.Language;
            udYear.Value = Convert.ToDecimal(B.Year);
            udPageCount.Value = Convert.ToDecimal(B.PageCount);
            tbDescription.Text = B.Description;
        }

        bool BookEqual()
        {
            return
                TempBook != null &&
                tbName.Text == TempBook.Name &&
                tbISBN.Text == TempBook.ISBN &&
                tbAuthor.Text == TempBook.Author &&
                tbGenre.Text == TempBook.Genre &&
                tbPublisher.Text == TempBook.Publisher &&
                tbLanguage.Text == TempBook.Language &&
                udYear.Value == Convert.ToDecimal(TempBook.Year) &&
                udPageCount.Value == Convert.ToDecimal(TempBook.PageCount) &&
                tbDescription.Text == TempBook.Description;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbName.Text == "")
            {
                MessageBox.Show("Fill name field");
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
            else
                if (tbISBN.Text == "")
                {
                    MessageBox.Show("Fill ISBN field");
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                }
                else
                    if (BookEqual())
                        DialogResult = System.Windows.Forms.DialogResult.Ignore;
                    else
                {
                    AddingBook = new Book(tbName.Text, tbISBN.Text, tbAuthor.Text, tbGenre.Text,
                        tbPublisher.Text, tbLanguage.Text, (int)udYear.Value,
                        (int)udPageCount.Value, tbDescription.Text);
                }
        }
    }
}
