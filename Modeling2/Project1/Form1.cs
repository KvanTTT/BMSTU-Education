using Microsoft.Glee;
using Microsoft.Glee.Drawing;
using System;
using System.Windows.Forms;

namespace Project1
{
    public partial class Form1 : Form
    {
        double[,] DensityProbabsMatrix;

        double[,] ConvertToMarkMatrix(double[,] DensityProbabsMatrix)
        {
            int N = DensityProbabsMatrix.GetLength(0);
            double[,] Result = new double[N, N + 1];
            bool flag = true;

            try
            {
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                        if (DensityProbabsMatrix[i, j] < 0.0)
                        {
                            flag = false;
                            break;
                        }

                if (flag)
                {
                    for (int i = 0; i < N; i++)
                    {
                        for (int j = 0; j < N; j++)
                        {
                            if (DensityProbabsMatrix[j, i] != double.PositiveInfinity)
                                Result[i, j] = DensityProbabsMatrix[j, i];
                            if (i == j)
                            {
                                for (int k = 0; k < N; k++)
                                    if (DensityProbabsMatrix[i, k] != double.PositiveInfinity)
                                        Result[i, j] -= DensityProbabsMatrix[i, k];
                            }

                            Result[i, j] += 1;
                        }

                        Result[i, N] = 1;
                    }
                }
                else
                    throw new Exception("All matrix elements must be greather than 0");
            }
            finally
            {

            }

            return Result;
        }

        double[,] GenerateAdjacencyMatrix(int VertexCount, int LinkCount,
            double LowWeightBound, double HighWeightBound, bool SelfPoint)
        {
            double[,] Result = new double[VertexCount, VertexCount];
            double WeightRange = HighWeightBound - LowWeightBound;
            int Count = 0;
            int i, j;
            Random Rand = new Random();
            for (i = 0; i < VertexCount; i++)
                for (j = 0; j < VertexCount; j++)
                    if (i != j)
                        Result[i, j] = double.PositiveInfinity;
            if (SelfPoint)
            {
                if (LinkCount > VertexCount * VertexCount)
                    LinkCount = VertexCount * VertexCount;
            }
            else
            {
                if (LinkCount > VertexCount * (VertexCount - 1))
                    LinkCount = VertexCount * (VertexCount - 1);
            }
            while (Count < LinkCount)
            {
                i = Rand.Next(VertexCount);
                j = Rand.Next(VertexCount);
                if (i == j && !SelfPoint)
                    continue;
                if (Result[i, j] == double.PositiveInfinity)
                {
                    Result[i, j] = Rand.NextDouble() * WeightRange + LowWeightBound;
                    Count++;
                }
            }
            return Result;
        }

        double[,] FlouydWarshall(double[,] AdjacencyMatrix)
        {
            int N = AdjacencyMatrix.GetLength(0);
            double[,] ResultMatrix = new double[N, N];
            Array.Copy(AdjacencyMatrix, ResultMatrix, N * N);
            for (int k = 0; k < N; k++)
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                        ResultMatrix[i, j] = Math.Min(ResultMatrix[i, j],
                            ResultMatrix[i, k] + ResultMatrix[k, j]);
            return ResultMatrix;
        }

        bool[,] FlouydWarshall(bool[,] AdjacencyMatrix)
        {
            int N = AdjacencyMatrix.GetLength(0);
            bool[,] ResultMatrix = new bool[N, N];
            Array.Copy(AdjacencyMatrix, ResultMatrix, N * N);
            for (int k = 0; k < N; k++)
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                        ResultMatrix[i, j] = ResultMatrix[i, j] || (ResultMatrix[i, k] && ResultMatrix[k, j]);
            return ResultMatrix;
        }

        enum Connectivity
        {
            None,
            Weakly,
            Connected,
            Strongly,
        }

        Connectivity IsConnected(double[,] AdjacencyMatrix)
        {
            Connectivity Result;
            int N = AdjacencyMatrix.GetLength(0);
            bool[,] UndirGraph = new bool[N, N];
            for (int i = 0; i < N; i++)
                for (int j = i; j < N; j++)
                    if (AdjacencyMatrix[i, j] != double.PositiveInfinity || AdjacencyMatrix[j, i] != double.PositiveInfinity)
                    {
                        UndirGraph[i, j] = true;
                        UndirGraph[j, i] = true;
                    }
            bool[,] UndirClosureMatrix = FlouydWarshall(UndirGraph);

            Result = Connectivity.None;
            for (int i = 0; i < N; i++)
                for (int j = i; j < N; j++)
                    if (!UndirClosureMatrix[i, j])
                        return Result;

            Result = Connectivity.Weakly;
            double[,] DirClosureMatrix = FlouydWarshall(AdjacencyMatrix);
            for (int i = 0; i < N; i++)
                for (int j = i; j < N; j++)
                    if (DirClosureMatrix[i, j] == double.PositiveInfinity && DirClosureMatrix[j, i] == double.PositiveInfinity)
                        return Result;

            Result = Connectivity.Connected;
            for (int i = 0; i < N; i++)
                for (int j = i; j < N; j++)
                    if (DirClosureMatrix[i, j] == double.PositiveInfinity || DirClosureMatrix[j, i] == double.PositiveInfinity)
                        return Result;

            Result = Connectivity.Strongly;
            return Result;
        }

        public Form1()
        {
            Screen screen = Screen.PrimaryScreen;
            if (screen.WorkingArea.Width <= 1024)
                this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            tbLinkDensity.Text = ((double)udLinkCount.Maximum / (double)udLinkCount.Value).ToString("0.00");
        }

        private void UpdateTable()
        {
            dgvTransits.Columns.Clear();
            dgvTransits.Rows.Clear();
            dgvAnswer.Rows.Clear();
            dgvAnswer.Rows.Add(DensityProbabsMatrix.GetLength(1));
            for (int i = 0; i < DensityProbabsMatrix.GetLength(1); i++)
            {
                dgvTransits.Columns.Add("column" + i.ToString(), "S" + (i + 1).ToString());
                dgvTransits.Columns[dgvTransits.ColumnCount - 1].Width = 30;
            }
            dgvTransits.Rows.Add(DensityProbabsMatrix.GetLength(1));
            for (int i = 0; i < DensityProbabsMatrix.GetLength(1); i++)
            {
                dgvTransits.Rows[i].HeaderCell.Value = "S" + (i + 1).ToString();
                dgvAnswer.Rows[i].HeaderCell.Value = "S" + (i + 1).ToString();
                for (int j = 0; j < DensityProbabsMatrix.GetLength(0); j++)
                    if (DensityProbabsMatrix[j, i] == double.PositiveInfinity)
                        dgvTransits[i, j].Value = "∞";
                    else
                        dgvTransits[i, j].Value = DensityProbabsMatrix[j, i].ToString("0.00");
            }
        }

        private void UpdateAnswerAndGraph(object sender)
        {
            Connectivity Con = IsConnected(DensityProbabsMatrix);

            if (Con == Connectivity.None)
            {
                if (!(sender is Button && (sender as Button).Name == "btnCalcul"))
                    if (!checkBox1.Checked)
                        throw new Exception("Unsuitable graph");
                lblConnect.ForeColor = System.Drawing.Color.Red;
                lblConnect.Text = "Not connected";
            }
            else if (Con == Connectivity.Weakly)
            {
                if (!(sender is Button && (sender as Button).Name == "btnCalcul"))
                    if (!checkBox2.Checked)
                        throw new Exception("Unsuitable graph");
                lblConnect.ForeColor = System.Drawing.Color.Coral;
                lblConnect.Text = "Weakly connected";
            }
            else if (Con == Connectivity.Connected)
            {
                if (!(sender is Button && (sender as Button).Name == "btnCalcul"))
                    if (!checkBox3.Checked)
                        throw new Exception("Unsuitable graph");
                lblConnect.ForeColor = System.Drawing.Color.YellowGreen;
                lblConnect.Text = "Connected";
            }
            else
            {
                if (!(sender is Button && (sender as Button).Name == "btnCalcul"))
                    if (!checkBox4.Checked)
                        throw new Exception("Unsuitable graph");
                lblConnect.ForeColor = System.Drawing.Color.ForestGreen;
                lblConnect.Text = "Strongly connected";
            }

            double[,] MarkMatrix = ConvertToMarkMatrix(DensityProbabsMatrix);

            double[] Answer = Gauss.Solve(MarkMatrix);
            for (int i = 0; i < Answer.Length; i++)
                dgvAnswer[0, i].Value = Answer[i].ToString("0.00");

            //this is abstract.dot of GraphViz
            Graph g = new Graph("graph");
            g.GraphAttr.NodeAttr.Padding = 3;

            for (int i = 0; i < DensityProbabsMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < DensityProbabsMatrix.GetLength(1); j++)
                {
                    if (DensityProbabsMatrix[i, j] != double.PositiveInfinity && DensityProbabsMatrix[i, j] != 0)
                    {
                        Microsoft.Glee.Drawing.Edge edge = g.AddEdge("S" + (i + 1).ToString(), "S" + (j + 1).ToString());
                        edge.Attr.Label = DensityProbabsMatrix[i, j].ToString("0.00");
                    }
                }
            }

            if (Con == Connectivity.None)
            {
                for (int i = 0; i < DensityProbabsMatrix.GetLength(0); i++)
                {
                    bool brk = false;
                    for (int j = 0; j < DensityProbabsMatrix.GetLength(0); j++)
                    {
                        if (j != i)
                            if (DensityProbabsMatrix[j, i] != double.PositiveInfinity || DensityProbabsMatrix[i, j] != double.PositiveInfinity)
                            {
                                brk = true;
                                break;
                            }
                    }
                    if (!brk)
                        g.AddNode("S" + (i + 1).ToString());
                }
            }

            //layout the graph and draw it
            gViewer1.Graph = g;
            //this.propertyGrid1.SelectedObject = g;
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            int N = (int)udStateCount.Value;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                {
                    string str = dgvTransits[i, j].Value.ToString();
                    if (str == "∞" || str == "+∞" || str == "infinity" || str == "+infinity" || str == "inf" || str == "+inf")
                        DensityProbabsMatrix[j, i] = double.PositiveInfinity;
                    else
                        DensityProbabsMatrix[j, i] = Convert.ToDouble(dgvTransits[i, j].Value);
                }

            

            UpdateAnswerAndGraph(sender);
        }

        private void udStateCount_ValueChanged(object sender, EventArgs e)
        {
            if (cbSelfPoint.Checked)
                udLinkCount.Maximum = (int)udStateCount.Value * (int)udStateCount.Value;
            else
                udLinkCount.Maximum = (int)udStateCount.Value * ((int)udStateCount.Value - 1);
            int N = (int)udStateCount.Value;
            tbLinkDensity.Text = ((double)udLinkCount.Maximum / (double)udLinkCount.Value).ToString("0.00");
            DensityProbabsMatrix = new double[N, N];
            UpdateTable();
        }   

        private void button2_Click(object sender, EventArgs e)
        {
            Random Rand = new Random();
            bool Error;
            do
            {
                Error = false;
                try
                {
                    if (btnLockVertexCount.ImageIndex != 0)
                        udStateCount.Value = Rand.Next((int)udStateCount.Minimum, (int)udStateCount.Maximum + 1);
                    if (btnLockSelfPoint.ImageIndex != 0)
                        cbSelfPoint.Checked = Convert.ToBoolean(Rand.Next(2));
                    if (btnLockLinkCount.ImageIndex != 0)
                        udLinkCount.Value = Rand.Next((int)udLinkCount.Minimum, (int)udLinkCount.Maximum + 1);
                    DensityProbabsMatrix = GenerateAdjacencyMatrix((int)udStateCount.Value, (int)udLinkCount.Value,
                        0.5, 8, cbSelfPoint.Checked);
                
                    UpdateTable();
                    UpdateAnswerAndGraph(sender);
                }
                catch
                {
                    Error = true;
                }
            }
            while (Error);
        }

        private void btnLockVertexCount_Click(object sender, EventArgs e)
        {
            (sender as Button).ImageIndex = 1 - (sender as Button).ImageIndex;
        }

        private void cbSelfPoint_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSelfPoint.Checked)
                udLinkCount.Maximum = (int)udStateCount.Value * (int)udStateCount.Value;
            else
                udLinkCount.Maximum = (int)udStateCount.Value * ((int)udStateCount.Value - 1);
            tbLinkDensity.Text = ((double)udLinkCount.Maximum / (double)udLinkCount.Value).ToString("0.00");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvTransits.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dgvAnswer.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

            DensityProbabsMatrix = new double[4, 4];
            DensityProbabsMatrix[0, 0] = 0;
            DensityProbabsMatrix[0, 1] = 2;
            DensityProbabsMatrix[0, 2] = double.PositiveInfinity;
            DensityProbabsMatrix[0, 3] = double.PositiveInfinity;

            DensityProbabsMatrix[1, 0] = double.PositiveInfinity;
            DensityProbabsMatrix[1, 1] = 0;
            DensityProbabsMatrix[1, 2] = 1;
            DensityProbabsMatrix[1, 3] = 1;

            DensityProbabsMatrix[2, 0] = 1;
            DensityProbabsMatrix[2, 1] = double.PositiveInfinity;
            DensityProbabsMatrix[2, 2] = 0;
            DensityProbabsMatrix[2, 3] = 1;

            DensityProbabsMatrix[3, 0] = double.PositiveInfinity;
            DensityProbabsMatrix[3, 1] = 2;
            DensityProbabsMatrix[3, 2] = double.PositiveInfinity;
            DensityProbabsMatrix[3, 3] = 0;

            UpdateTable();
            UpdateAnswerAndGraph(btnCalcul);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked || checkBox2.Checked)
                udLinkCount.Minimum = udStateCount.Value - 1;
            if (checkBox4.Checked)
                udLinkCount.Minimum = udStateCount.Value;
        }
    }
}
