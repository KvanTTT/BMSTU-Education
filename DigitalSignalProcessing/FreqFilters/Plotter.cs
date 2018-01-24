using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using ZedGraph;

namespace Common
{
    public class PlotableFunction
    {
        static string defaultname = "undefined";
        static System.Drawing.Drawing2D.DashStyle defdashstyle = System.Drawing.Drawing2D.DashStyle.Solid;
        static int defwidth = 1;

        public string Name { get; private set; }
        public Func<double, double> F { get; private set; }
        public System.Drawing.Drawing2D.DashStyle DashStyle { get; private set; }
        public int LineWidth { get; private set; }

        public PlotableFunction(Func<double, double> F)
            : this(F, defaultname, defdashstyle)
        {            
        }

        public PlotableFunction(Func<double, double> F, string FuncName)
            : this(F, FuncName, defdashstyle)
        {
        }

        public PlotableFunction(Func<double, double> F, System.Drawing.Drawing2D.DashStyle dashstyle)
            : this(F, defaultname, dashstyle)
        {
        }

        public PlotableFunction(Func<double, double> F, int linewidth)
            : this(F, defaultname, defdashstyle, linewidth)
        {
        }

        public PlotableFunction(Func<double, double> F, string FuncName, int linewidth)
            : this(F, FuncName, defdashstyle, linewidth)
        {
        }

        public PlotableFunction(Func<double, double> F, System.Drawing.Drawing2D.DashStyle dashstyle, int linewidth)
            : this(F, defaultname, dashstyle, linewidth)
        {
        }

        public PlotableFunction(Func<double, double> F, string FuncName, System.Drawing.Drawing2D.DashStyle dashstyle)
            : this(F, FuncName, dashstyle, defwidth)
        {
        }

        public PlotableFunction(Func<double, double> F, string FuncName, System.Drawing.Drawing2D.DashStyle dashstyle, int linewidth)
        {
            this.Name = FuncName;
            this.F = F;
            this.DashStyle = dashstyle;
            this.LineWidth = linewidth;
        } 
    }

    public class Plotter
    {
        ZedGraphControl zgc;
        double dx;
        Color linecolor = Color.Black;

        Color[] ColorCollection = { Color.Blue, Color.Red, Color.GreenYellow, Color.Ivory, Color.Navy };
        SymbolType[] SymbolTypeCollection = { SymbolType.Circle, SymbolType.Star, SymbolType.Diamond, SymbolType.TriangleDown };

        public Plotter(ZedGraphControl zgc, double dx)
        {
            this.zgc = zgc;
            this.dx = dx;
        }

        public static PlotableFunction ToPlotable(Func<double, double> func)
        {
            return new PlotableFunction(func);
        }

        public static PlotableFunction ToPlotable(Func<double, double> func, string FuncName)
        {
            return new PlotableFunction(func, FuncName);
        }

        public void Plot(PlotableFunction f, double min, double max)
        {
            LineItem ln = new LineItem(f.Name, GetPoints(f, min, max), linecolor, SymbolType.None);
            ln.Line.Style = f.DashStyle;
            ln.Line.Width = f.LineWidth;

            zgc.GraphPane.CurveList.Clear();
            zgc.GraphPane.CurveList.Add(ln);
            zgc.AxisChange();
            zgc.Invalidate();
        }

        public void Plot(IEnumerable<PlotableFunction> fs, double min, double max)
        {            
            LineItem ln;
            zgc.GraphPane.CurveList.Clear();
            int i = 0;
            foreach (PlotableFunction f in fs)
            {
                ln = new LineItem(f.Name, GetPoints(f, min, max), ColorCollection[i], SymbolType.None);
                ln.Line.Style = f.DashStyle;
                ln.Line.Width = f.LineWidth;

                zgc.GraphPane.CurveList.Add(ln);
                i = (i + 1) % ColorCollection.Length;
            }
            zgc.AxisChange();
            zgc.Invalidate();
        }

        private IPointList GetPoints(PlotableFunction f, double min, double max)
        {
            List<double> x = new List<double>();
            List<double> y = new List<double>();

            for (double xi = min; xi < max; xi += dx)
            {
                x.Add(xi);
                y.Add(f.F(xi));
            }

            return new BasicArrayPointList(x.ToArray(), y.ToArray());
        }
    }
}
