using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProductModel
{
    public partial class Form1 : Form
    {
        SolveMethod Slv = SolveMethod.IntoDepth;
        KnowledgeBase KB = new KnowledgeBase();
        Size PrevSize;
        int SpaceBetween, SpaceRight;

        Form2 ResultForm;

        public Form1()
        {
            InitializeComponent();
        }

        private void AddExpressionToListView(ListView Lv, Expression Expr)
        {
            String str = Expr.ToString();

            Lv.Items.Add(new ListViewItem(str));
        }

        private void AddFact()
        {
            try
            {
                KB.AddFact(Translator.StringToFact(tbFact.Text));
                AddExpressionToListView(lvFactBase, KB.BaseOfFacts[KB.BaseOfFacts.Count - 1].Value);                
                lvFactBase.Refresh();

                tbFact.Clear();
            }
            catch (Exception Exc)
            {
                MessageBox.Show(Exc.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddRule()
        {
            try
            {
                KB.AddRule(Translator.StringsToRule(tbRuleIf.Text, tbRuleThen.Text));
                AddExpressionToListView(lvRuleBase, KB.BaseOfRules[KB.BaseOfRules.Count - 1].Value);
                lvRuleBase.Refresh();

                tbRuleIf.Clear();
                tbRuleThen.Clear();
            }
            catch (Exception Exc)
            {
                MessageBox.Show(Exc.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddFact_Click(object sender, EventArgs e)
        {
            AddFact();
        }

        private void tbFact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                AddFact();
        }

        private void btnClearFactBase_Click(object sender, EventArgs e)
        {
            KB.BaseOfFacts.Clear();
            lvFactBase.Items.Clear();
            lvFactBase.Refresh();
        }

        private void btnDelSelFacts_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection Indicies = lvFactBase.SelectedIndices;
            foreach (int index in Indicies)
                KB.BaseOfFacts[index].MarkToDel();
            Application.DoEvents();
            KB.BaseOfFacts.RemoveAll((cell) => cell.DeleteMark );

            lvFactBase.Items.Clear();
            foreach (EBCell Cell in KB.BaseOfFacts)
                lvFactBase.Items.Add(new ListViewItem(Cell.Value.ToString()));
            lvFactBase.Refresh();
        }

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            AddRule();
        }

        private void Rule_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                if (((TextBox)sender).Name == "tbRuleIf")
                    if (tbRuleThen.Text == String.Empty) tbRuleThen.Focus();
                    else AddRule();
                else
                    if (tbRuleIf.Text == String.Empty) tbRuleIf.Focus();
                    else AddRule();
        }

        private void btnGoal_Click(object sender, EventArgs e)
        {
            try
            {
                MyTreeNode Tree = Solver.AchieveGoal(KB, Translator.StringToRuleExpression(tbGoal.Text), Slv);
                if (Tree == null) MessageBox.Show("Цель не достижима.");
                else
                {
                    ResultForm = new Form2();
                    ResultForm.ShowTree(Tree);
                }
            }
            catch (Exception Exc)
            {
                MessageBox.Show(Exc.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ofdOpen.InitialDirectory = sfdSave.InitialDirectory = Application.StartupPath;
            PrevSize = this.ClientSize;
            SpaceBetween = lvRuleBase.Left - lvFactBase.Left - lvFactBase.Width;
            SpaceRight = this.ClientSize.Width - lvRuleBase.Left - lvRuleBase.Width;
        }

        private void tsmiFile_Open_Click(object sender, EventArgs e)
        {
            KB.Clear();
            lvFactBase.Items.Clear();
            lvRuleBase.Items.Clear();
            lvFactBase.Refresh();
            lvRuleBase.Refresh();

            if (ofdOpen.ShowDialog() == DialogResult.OK)
                try
                {
                    KB = (new FileLoader(ofdOpen.FileName)).Load();
                    foreach (EBCell Cell in KB.BaseOfFacts)
                        AddExpressionToListView(lvFactBase, Cell.Value);
                    foreach (EBCell Cell in KB.BaseOfRules)
                        AddExpressionToListView(lvRuleBase, Cell.Value);
                }
                catch (Exception Exc)
                {
                    MessageBox.Show(Exc.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void btnDelSelRules_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection Indicies = lvRuleBase.SelectedIndices;
            foreach (int index in Indicies)
                KB.BaseOfRules[index].MarkToDel();
            Application.DoEvents();
            KB.BaseOfRules.RemoveAll((cell) => cell.DeleteMark);

            lvRuleBase.Items.Clear();
            foreach (EBCell Cell in KB.BaseOfRules)
                lvRuleBase.Items.Add(new ListViewItem(Cell.Value.ToString()));
            lvRuleBase.Refresh();
        }

        private void btnClearRuleBase_Click(object sender, EventArgs e)
        {
            KB.BaseOfRules.Clear();
            lvRuleBase.Items.Clear();
            lvRuleBase.Refresh();
        }

        private void tsmiFile_SaveAs_Click(object sender, EventArgs e)
        {
            if (sfdSave.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    (new FileSaver(sfdSave.FileName)).Save(KB);
                    MessageBox.Show("База знаний успешно сохранена", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception Exc)
                {
                    MessageBox.Show(Exc.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tsmiFile_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            int delta = (ClientSize.Width - PrevSize.Width) / 2;
            PrevSize = this.ClientSize;

            lvFactBase.Width += delta;
            lvFactBase.Columns[0].Width += delta;
            lvRuleBase.Left = lvFactBase.Left + lvFactBase.Width + SpaceBetween;
            lvRuleBase.Width = this.ClientSize.Width - lvRuleBase.Left - SpaceRight;
            lvRuleBase.Columns[0].Width += delta;
        }

        private void tbGoal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) btnGoal_Click(sender, new EventArgs());
        }

        private void rbReverse_CheckedChanged(object sender, EventArgs e)
        {
            if (rbReverse.Checked) Slv = SolveMethod.IntoDepth;
            if (rbStraight.Checked) Slv = SolveMethod.IntoWidth;
        }
    }
}
