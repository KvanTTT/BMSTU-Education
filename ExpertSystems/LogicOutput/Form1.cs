using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        KnowledgeBase KB;

        private void btnAddFact_Click(object sender, EventArgs e)
        {
            string In = tbxFacts.Text;
            ParseFact Parse = new ParseFact(In);
            Fact F = Parse.GetFact();
            if (F != null)
            {
                KB.AddFact(F);
                lvwFacts.Items.Add(In);
                /*if ((In.Length * 5) > cmnFacts.Width)
                    cmnFacts.Width = In.Length * 5;*/
            }
            else
                MessageBox.Show("Невозможно распознать введенный факт", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void btnAddRule_Click(object sender, EventArgs e)
        {
            string In = tbxRules.Text;
            ParseRule Parse = new ParseRule(In);
            Rule R = Parse.GetRule();
            if (R != null)
            {
                KB.AddRule(R);
                lvwRules.Items.Add(In);
              /*  if ((In.Length * 5) > cmnRules.Width)
                    cmnRules.Width = In.Length * 5;*/
            }
            else
                MessageBox.Show("Невозможно распознать введенное правило", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KB = new KnowledgeBase();
        }

        private void btnGoal_Click(object sender, EventArgs e)
        {
            string Goal = tbxGoal.Text;
            ParseFact parse = new ParseFact(Goal);
            Fact F = parse.GetFact();
            if (F == null)
                MessageBox.Show("Ошибка при задании цели", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                Logic L = new Logic(KB);
                string Out;
                if (L.CheckFact(F))
                    Out = "Цель " + Goal + " успешно доказана";
                else
                   Out = "Цель " + Goal + " не доказана";
                lvwResults.Items.Add(Out);
                /*if ((Out.Length * 5) > cmnGoals.Width)
                    cmnGoals.Width = Out.Length * 5;*/
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRuleFromFile_Click(object sender, EventArgs e)
        {
            ofdRules.ShowDialog();
        }

        private void btnFactFromFile_Click(object sender, EventArgs e)
        {
            
            ofdFacts.ShowDialog();
        }

        private void ofdRules_FileOk(object sender, CancelEventArgs e)
        {
            lvwRules.Items.Clear();
            StreamReader SR = new StreamReader(File.OpenRead(ofdRules.FileName));
            while (!SR.EndOfStream)
            {
                string In = SR.ReadLine();
                ParseRule Parse = new ParseRule(In);
                Rule R = Parse.GetRule();
                if (R != null)
                {
                    KB.AddRule(R);
                    lvwRules.Items.Add(In);
                   /* if ((In.Length * 5) > cmnRules.Width)
                        cmnRules.Width = In.Length * 5;*/
                }
            }
        }

        private void ofdFacts_FileOk(object sender, CancelEventArgs e)
        {
            lvwFacts.Items.Clear();
            StreamReader SR = new StreamReader(File.OpenRead(ofdFacts.FileName));
            //int FactCount = Convert.ToInt32(SR.ReadLine());
            while (!SR.EndOfStream)
            {
                string In = SR.ReadLine();
                ParseFact Parse = new ParseFact(In);
                Fact F = Parse.GetFact();
                if (F != null)
                {
                    KB.AddFact(F);
                    lvwFacts.Items.Add(In);
                   /* if ((In.Length * 5) > cmnFacts.Width)
                        cmnFacts.Width = In.Length * 5;*/
                }
            }
        }

        private void btnCheckGoal_Click(object sender, EventArgs e)
        {
            lvwResults.Items.Clear();
            foreach (string Request in lbRequests.Items)
            {
                string Goal = Request;
                ParseFact parse = new ParseFact(Goal);
                Fact F = parse.GetFact();
                if (F == null)
                    MessageBox.Show("Ошибка при задании цели", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    Logic L = new Logic(KB);
                    string Out;
                    if (L.CheckFact(F))
                        Out = "Цель " + Goal + " успешно доказана";
                    else
                        Out = "Цель " + Goal + " не доказана";
                    lvwResults.Items.Add(Out);
                    /*if ((Out.Length * 5) > cmnGoals.Width)
                        cmnGoals.Width = Out.Length * 5;*/
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (ofdRequests.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lbRequests.Items.Clear();
                string[] Requests = File.ReadAllLines(ofdRequests.FileName);
                foreach (string Request in Requests)
                {
                    ParseFact Parse = new ParseFact(Request);
                    Fact F = Parse.GetFact();
                    if (F != null)
                    {
                        KB.AddFact(F);
                        lbRequests.Items.Add(Request);
                    }
                }
            }
        }

        private void btnLoadRequests_Click(object sender, EventArgs e)
        {
            string Goal = tbxGoal.Text;
            ParseFact parse = new ParseFact(Goal);
            Fact F = parse.GetFact();
            if (F == null)
                MessageBox.Show("Ошибка при задании цели", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                Logic L = new Logic(KB);
                string Out;
                if (L.CheckFact(F))
                    Out = "Цель " + Goal + " успешно доказана";
                else
                    Out = "Цель " + Goal + " не доказана";
                lbRequests.Items.Add(Out);
                /*if ((Out.Length * 5) > cmnGoals.Width)
                    cmnGoals.Width = Out.Length * 5;*/
            }
        }
    }
}
