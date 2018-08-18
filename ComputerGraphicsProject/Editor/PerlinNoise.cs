using System;

namespace Generators
{
    class PerlinNoise
    {
        int TableSize;
        int TableMask;

        struct vec2
        {
            public double x, y;
            public vec2(double x, double y) { this.x = x; this.y = y; }
        };

        vec2[] VectorTable;
        byte[] lut;

        public PerlinNoise(int TableSize)
        {
            this.TableSize = TableSize;
            this.TableMask = TableSize - 1;
            VectorTable = new vec2[TableSize];
            lut = new byte[TableSize];
            setup();
        }

        void setup()
        {
            double step = 6.24 / TableSize;
            double val = 0.0;

            Random rand = new Random();
            for (int i = 0; i < TableSize; ++i)
            {
                VectorTable[i].x = Math.Sin(val);
                VectorTable[i].y = Math.Cos(val);
                val += step;

                lut[i] = (byte)(rand.Next() & TableMask);
            }
        }

        vec2 getVec(int x, int y)
        {
            byte a = lut[x & TableMask];
            byte b = lut[y & TableMask];
            byte val = lut[(a + b) & TableMask];
            return VectorTable[val];
        }

        public double Generate(double x, double y, double scale)
        {
            vec2 pos = new vec2(x * scale, y * scale);

            double X0 = Math.Floor(pos.x);
            double X1 = X0 + 1.0f;
            double Y0 = Math.Floor(pos.y);
            double Y1 = Y0 + 1.0f;

            vec2 v0 = getVec((int)X0, (int)Y0);
            vec2 v1 = getVec((int)X0, (int)Y1);
            vec2 v2 = getVec((int)X1, (int)Y0);
            vec2 v3 = getVec((int)X1, (int)Y1);

            vec2 d0 = new vec2(pos.x - X0, pos.y - Y0);
            vec2 d1 = new vec2(pos.x - X0, pos.y - Y1);
            vec2 d2 = new vec2(pos.x - X1, pos.y - Y0);
            vec2 d3 = new vec2(pos.x - X1, pos.y - Y1);

            double h0 = (d0.x * v0.x) + (d0.y * v0.y);
            double h1 = (d1.x * v1.x) + (d1.y * v1.y);
            double h2 = (d2.x * v2.x) + (d2.y * v2.y);
            double h3 = (d3.x * v3.x) + (d3.y * v3.y);

            double Sx, Sy;

            /*
                Perlin's original equation was faster,
                but produced artifacts in some situations
                Sx = (3*powf(d0.x,2.0f))
                    -(2*powf(d0.x,3.0f));

                Sy = (3*powf(d0.y,2.0f))
                    -(2*powf(d0.y,3.0f));
            */

            // the revised blend equation is 
            // considered more ideal, but is
            // slower to compute
            Sx = (6 * Math.Pow(d0.x, 5.0f))
                - (15 * Math.Pow(d0.x, 4.0f))
                + (10 * Math.Pow(d0.x, 3.0f));

            Sy = (6 * Math.Pow(d0.y, 5.0f))
                - (15 * Math.Pow(d0.y, 4.0f))
                + (10 * Math.Pow(d0.y, 3.0f));


            double avgX0 = h0 + (Sx * (h2 - h0));
            double avgX1 = h1 + (Sx * (h3 - h1));
            double result = avgX0 + (Sy * (avgX1 - avgX0));

            return result;
        }

        public double Generate(int x, int y, double scale)
        {
            return Generate((double)x, (double)y, scale);
        }
    }
}
