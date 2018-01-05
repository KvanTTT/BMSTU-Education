using System.Drawing;
using System.Windows.Forms;

namespace TravellingSalesmanProblem
{
    class Painter
    {
        static int Rad1 = 7;
        static int Rad2 = 4;

        static Color C1 = Color.OrangeRed;
        static Color C2 = Color.Orange;
        static Color C3 = Color.BlueViolet;

        Graphics G;
        Bitmap B;
        PictureBox PB;

        public Painter(Graphics G, Bitmap B, PictureBox PB)
        {
            this.G = G;
            this.B = B;
            this.PB = PB;
        }

        public void PaintCity(int X, int Y)
        {
            G.FillEllipse(new SolidBrush(C1), X - Rad1, Y - Rad1, Rad1 << 1, Rad1 << 1);
            G.FillEllipse(new SolidBrush(C2), X - Rad2, Y - Rad2, Rad2 << 1, Rad2 << 1);
        }

        public void PaintEdge(City C1, City C2)
        {
            G.DrawLine(new Pen(C3), C1.GetX(), C1.GetY(), C2.GetX(), C2.GetY());
        }

        public void Clear()
        {
            G.FillRectangle(new SolidBrush(Color.White), 0, 0, PB.Width, PB.Height);
        }

        public void Refresh()
        {
            PB.Refresh();
        }
    }
}
