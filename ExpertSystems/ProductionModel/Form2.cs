using System;
using System.Windows.Forms;

namespace ProductModel
{
    partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private TreeNode MakeTree(MyTreeNode Node)
        {
            int i = 0;
            if (Node.Left == null) i++;
            if (Node.Right == null) i++;
            if (Node.SelfWay == null) i++;

            int n = 3 - i;
            TreeNode[] Children = null;
            if (n > 0) Children = new TreeNode[n];
 
            int k = 0;
            for (int j = 0; j < n; j++)
            {
                for (; Node[k] == null; k++) ;
                Children[j] = MakeTree(Node[k]);
                k++;
            }

            if (n > 0)
                return new TreeNode(Node.Value != null ? Node.Value.ToString() : "NULL", Children);
            else
                return new TreeNode(Node.Value != null ? Node.Value.ToString() : "NULL");
        }
        
        public void ShowTree(MyTreeNode Tree)
        {
            trwResult.Nodes.Add(MakeTree(Tree));
            trwResult.ExpandAll();
            trwResult.Refresh();
            this.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
