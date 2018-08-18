using Generators;
using Geometry3D;
using Lighting;
using System;
using System.Drawing;

namespace Editor
{
    public enum GenMethod
    {
        Perlin = 0,
        HillAlg = 1,
    }

    public enum Operation
    {
        Plus = 0,
        Mult = 1,
    }

    public struct Convolution
    {
        public Operation Operation;
        public double Coef;
        //public Convolution() {}
    }

    class Landscape
    {
        GenMethod LandGenMethod;
        Convolution[] Convs;
        bool Smoothing;
        bool Valley;
        bool Island;

        public double[,] Heightmap;
        int[,] Lightmap;
        int[,] Colormap;
        
        public int SizeX, SizeY;
        int LSizeX, LSizeY;
        int CSizeX, CSizeY;

        Vector[,] Points;
        int[] Indexes;
        Vector[] FaceNormals;
        Vector[,] VertexNormals;

        Vector Pos;
        Vector Dimen;

        public Material Ground;


        public Landscape() { }

        // генерирует карту высот с размером SizeX, SizeY, по методу LandGenMethod c коэффициентами Convs, со сглаживанием или нет (Smoothing) с долинизацией (Valley)
        public void GenerateHeightmap(int SizeX, int SizeY, GenMethod LandGenMethod, Convolution[] Convs, bool Smoothing, bool Valley, bool Island)
        {
            this.SizeX = SizeX;
            this.SizeY = SizeY;
            Heightmap = new double[SizeX, SizeY];
            PerlinNoise Noise = new PerlinNoise(256);

            double min =  99999;
            double max = -99999;

            double[,] ar = new double[SizeX, SizeY];

            if (LandGenMethod == GenMethod.Perlin)
            {
                for (int i = 0; i < SizeX; i++)
                    for (int j = 0; j < SizeY; j++)
                    {
                        for (int l = 0; l < Convs.Length; l++)
                        {
                            if (Convs[l].Operation == Operation.Plus)
                                if (Convs[l].Coef != 0)
                                    ar[i, j] += Noise.Generate(i, j, Convs[l].Coef);
                            else
                                if (Convs[l].Coef != 0)
                                    ar[i, j] *= Noise.Generate(i, j, Convs[l].Coef);
                        }

                        if (max < ar[i, j]) max = ar[i, j];
                        if (min > ar[i, j]) min = ar[i, j];
                    }
            }
            else
            {
                Random rand = new Random();
                double theta;
                double distanceX, distanceY;
                double Radius;
                double x, y;
                double t;

                min = 0;
                for (int k = 0; k < Convs[0].Coef; k++)
                {
                    Radius = rand.NextDouble() * (SizeX * Convs[1].Coef);

                    if (Island)
                    {
                        theta = rand.NextDouble() * Math.PI * 2;
                        t = rand.NextDouble();
                        distanceX = t * (SizeX * Convs[2].Coef - Radius);
                        distanceY = t * (SizeY * Convs[2].Coef - Radius);
                        x = SizeX / 2.0 + Math.Cos(theta) * distanceX;
                        y = SizeY / 2.0 + Math.Sin(theta) * distanceY;    
                    }
                    else
                    {
                        x = SizeX * rand.NextDouble();
                        y = SizeY * rand.NextDouble();
                    }

                    for (int i = 0; i < SizeX; i++)
                        for (int j = 0; j < SizeY; j++)
                        {
                            t = Radius * Radius - ((i - x) * (i - x) + (j - y) * (j - y));
                            if (t > 0)
                                ar[i, j] += t;

                            if (max < ar[i, j]) max = ar[i, j];
                            //if (min > ar[i, j]) min = ar[i, j];
                        }
                }
            }


            double coef = 1 / ((max - min));
            for (int i = 1; i < SizeX-1; i++)
                for (int j = 1; j < SizeY-1; j++)
                {
                    if (Smoothing)  
                        Heightmap[i, j] =       (ar[i - 1, j - 1] + ar[i - 1, j] + ar[i - 1, j + 1] +
                                                 ar[i    , j - 1] + ar[i    , j] + ar[i    , j + 1] +
                                                 ar[i + 1, j - 1] + ar[i + 1, j] + ar[i + 1, j + 1] - 9.0*min) / (9.0*(max-min));
                    else
                        Heightmap[i, j] = (ar[i, j] - min) * coef;
                    if (Valley)
                        Heightmap[i, j] = Math.Sqrt(Heightmap[i, j]);
                }
        }

        // строить трехмерную модель ландшафта с размерами Dimen.X Dimen.Y Dimen.Z
        // Также просчитываются нормали к граням FaceNormals и к вершинам VertexNormals
        public void BuildMesh(Vector Dimen)
        {
            this.Dimen = Dimen;
            double SX = Dimen.X / SizeX;
            double SY = Dimen.Y / SizeY;
            int x, y, k, k1;

            Points = new Vector[SizeX, SizeY];

            for (int i = 0; i < SizeX; i++)
                for (int j = 0; j < SizeY; j++)
                    Points[i, j] = new Vector(i * SX, j * SY, Convert.ToDouble(Heightmap[i, j]) * Dimen.Z);

            FaceNormals = new Vector[(SizeX - 1) * (SizeY - 1) * 2];

            k = 0;
            for (y = 0; y < SizeY-1; y++)
                for (x = 0; x < SizeX-1; x++)
                {
                    FaceNormals[k] = ((Points[x, y + 1] - Points[x, y]) ^ (Points[x + 1, y] - Points[x, y])).Normalize();
                    FaceNormals[k+1] = ((Points[x + 1, y] - Points[x + 1, y + 1]) ^ (Points[x, y + 1] - Points[x + 1, y + 1])).Normalize();
                    k += 2;
                }

            VertexNormals = new Vector[SizeX, SizeY];

            VertexNormals[0, 0] = FaceNormals[0];
            VertexNormals[SizeX - 1, 0] = (FaceNormals[(SizeX - 1) * 2 - 1] + FaceNormals[(SizeX - 1) * 2 - 2]).Normalize();
            VertexNormals[0, SizeY - 1] = (FaceNormals[(SizeX - 1) * (SizeY - 2)] + FaceNormals[(SizeX - 1) * (SizeY - 2) + 1]).Normalize();
            VertexNormals[SizeX-1, SizeY-1] = FaceNormals[(SizeY - 1) * (SizeX - 1) * 2 - 1];

            k1 = 0;
            k = (SizeX - 1) * (SizeY - 2) * 2 + 1;
            for (x = 1; x < SizeX - 1; x++)
            {
                VertexNormals[x, 0] = (FaceNormals[k1] + FaceNormals[k1+1] + FaceNormals[k1+2]).Normalize();
                VertexNormals[x, SizeY - 1] = (FaceNormals[k] + FaceNormals[k + 1] + FaceNormals[k + 2]).Normalize();
                k1 += 2;
                k += 2;
            }

            k1 = 0;
            k = (SizeX - 1) * 2 - 1;
            for (y = 1; y < SizeY - 1; y++)
            {
                VertexNormals[0, y] = (FaceNormals[k1] + FaceNormals[k1 + 1] + FaceNormals[k1 + (SizeX - 1) * 2]).Normalize();
                VertexNormals[SizeX - 1, y] = (FaceNormals[k] + FaceNormals[k + (SizeX - 1) * 2 - 1] + FaceNormals[k + (SizeX - 1) * 2]).Normalize();
                k1 += (SizeX - 1) * 2;
                k += (SizeX - 1) * 2;
            }            

            k = 1;
            for (y = 1; y < SizeY - 1; y++)
            {
                for (x = 1; x < SizeX - 1; x++)
                {
                    VertexNormals[x, y] = (FaceNormals[k] + FaceNormals[k + 1] + FaceNormals[k + 2] + FaceNormals[k + (SizeX - 1) * 2 - 1] +
                                           FaceNormals[k + (SizeX - 1) * 2] + FaceNormals[k + (SizeX - 1) * 2 + 1]).Normalize();
                    k += 2;
                }
                k += 2;
            }
        }

        // Цвет данной точки ландшафта с координатами Pos нормальную Normal добавляется к результирующему Color
        public void AddColor(Vector Pos, Vector Normal, Light Source, Vector AddColor, ref int Color)
        {
            Vector Res = new Vector();
            if (Source is PointLight)
            {
                Vector LightDir = (Pos - (Source as PointLight).Point).Normalize();
                double d2 = (Pos - (Source as PointLight).Point).LengthSqr();
                double CrossProduct = LightDir * Normal;
                if (CrossProduct <= 0)
                    return;
                Res = (CrossProduct * Ground.Diffuse | Source.Diffuse) * 255 /
                        ((Source as PointLight).c1 + (Source as PointLight).c2 * Math.Sqrt(d2) + (Source as PointLight).c3 * d2);              
            }
            else
            {
                double CrossProduct = (Source as DirLight).Dir * Normal;
                if (CrossProduct <= 0)
                    return; 
                Res = (CrossProduct * Ground.Diffuse | Source.Diffuse) * 255;
            }
            Res += (AddColor * 255);
            if (Res.X > 255) Res.X = 255;
            if (Res.Y > 255) Res.Y = 255;
            if (Res.Z > 255) Res.Z = 255;  
            if (Res.X < 0) Res.X = 0;
            if (Res.Y < 0) Res.Y = 0;
            if (Res.Z < 0) Res.Z = 0;  

            Color = Math.Max((byte)Math.Round(Res.Z), Color & 255) + (Math.Max((byte)Math.Round(Res.Y), (Color >> 8) & 255) << 8) + 
                (Math.Max((byte)Math.Round(Res.X), (Color >> 16) & 255) << 16);
        }

        // Цвет данной точки ландшафта с координатами Pos нормальную Normal добавляется к результирующему Color
        public Vector GetColor(Vector Pos, Vector Normal, Light Source)
        {
            Vector Res = new Vector();
            if (Source is PointLight)
            {
                Vector LightDir = (Pos - (Source as PointLight).Point).Normalize();
                double d2 = (Pos - (Source as PointLight).Point).LengthSqr();
                double CrossProduct = LightDir * Normal;
                if (CrossProduct <= 0)
                    return Res;
                Res = (CrossProduct * Ground.Diffuse | Source.Diffuse) /
                        ((Source as PointLight).c1 + (Source as PointLight).c2 * Math.Sqrt(d2) + (Source as PointLight).c3 * d2);            
            }
            else
            {
                double CrossProduct = (Source as DirLight).Dir * Normal;
                if (CrossProduct <= 0)
                    return Res; 
                Res = (CrossProduct * Ground.Diffuse | Source.Diffuse);
            }
            return Res;
        }

        public double GetHeight(double X, double Y)
        {

            int x1 = (int)((X - Pos.X) / (Dimen.X / SizeX));
	        int y1 = (int)((Y - Pos.Y) / (Dimen.Y / SizeY));
            if ((x1 < 0) || (x1 >= SizeX) || (y1 < 0) || (y1 >= SizeY))
                return 1000; //Pos.Y;
            else
            {
                double xt = x1 * SizeX + Pos.X;
                double yt = y1 * SizeY + Pos.Y;
                if ((X - xt) * SizeY + (Y - yt) * SizeX <= SizeX * SizeY)
                {
                    Vector N = (Points[x1 + 1, y1] - Points[x1, y1]) ^ (Points[x1, y1 + 1] - Points[x1, y1]);
                    double D = -N * Points[x1, y1];
                    return (-N.X * X - N.Y * Y - D) / N.Z;
                }
                else
                {
                    int x2 = x1 + 1;
                    int y2 = y1 + 1;
                    Vector N = (Points[x1 + 1, y1] - Points[x2, y2]) ^ (Points[x1, y1 + 1] - Points[x2, y2]);
                    double D = -N * Points[x2, y2];
                    return (-N.X * X - N.Y * Y - D) / N.Z;
                }
            }
        }

        bool RayTriangleIntersect(Vector RayPos, Vector RayDir, Vector P1, Vector P2, Vector P3, 
						  out Vector ResultPoint, out Vector ResultNormal)
        {
            const double EPSILON2 = 0.00001;
	        Vector v1 = P2 - P1;
	        Vector v2 = P3 - P1;
	        Vector pvec = RayDir ^ v2;
	        double det = v1 * pvec;
	        ResultPoint = new Vector();
            ResultNormal = new Vector();
            if ((det < EPSILON2) && (det > -EPSILON2))
	        {
		        return false;
	        }
            double invDet = 1 / det;
	        Vector tvec = RayPos - P1;
            double u = (tvec * pvec) * invDet;
	        if ((u < 0) || (u > 1))
		        return false;
	        else 
	        {
		        Vector qvec = tvec ^ v1;
                double v = (RayDir * qvec) * invDet;
		        if ((v < 0) || (u+v > 1))
			        return false;
                double t = (v2 * qvec) * invDet;
		        if (t > 0)
		        {
			        ResultPoint =  RayPos + RayDir * t;
			        ResultNormal = v1 ^ v2;
                    return true;
		        } 
		        else 
			        return false;
	        }
        }

        int ConvertColor(Vector Color)
        {
            if (Color.X < 0) Color.X = 0;
            if (Color.Y < 0) Color.Y = 0;
            if (Color.Z < 0) Color.Z = 0;
            if (Color.X > 1) Color.X = 1;
            if (Color.Y > 1) Color.Y = 1;
            if (Color.Z > 1) Color.Z = 1;

            return ((int)Math.Round(Color.Z * 255) + ((int)Math.Round(Color.Y * 255) << 8) + ((int)Math.Round(Color.X * 255) << 16));
        }


        // построение карты освещенности с учетом Sources источников света. Размер карты SizeX*Size SizeY*Size
        public void BuildLightmap(Light[] Sources, int Size)
        {
            LSizeX = (int)(this.Dimen.X * Size);
            LSizeY = (int)(this.Dimen.Y * Size);
            Lightmap = new int[LSizeX, LSizeY];
            Vector[,] LightmapTemp = new Vector[LSizeX, LSizeY];

            int k;
            int NormInd;
            double dx1 = this.Dimen.X / SizeX;
            double dy1 = this.Dimen.Y / SizeY;
            double dx = dx1 / Size;
            double dy = dy1 / Size;
            for (k = 0; k < Sources.Length; k++)
            {
                /*if (Sources[k] is PointLight)
                {
                    if ((Sources[k] as PointLight).Point.Z < GetHeight((Sources[k] as PointLight).Point.X, (Sources[k] as PointLight).Point.Y))
                        continue;
                }
                /*else
                {
                    if ((Sources[k] as DirLight).Dir * */

                Vector AmbientColor = Ground.Ambient | Sources[k].Ambient;

                NormInd = 0;
                for (int y = 0; y < SizeY - 1; y++)
                {
                    for (int x = 0; x < SizeX - 1; x++)
                    {
                        double xabc = x * dx1;
                        double za = Points[x, y].Z;
                        double zb = Points[x, y + 1].Z;
                        double zc = Points[x, y + 1].Z;
                        double yb = y * dy1 + dy1;

                        Vector V1 = (VertexNormals[x + 1, y] - VertexNormals[x, y]) / Size;
                        Vector V2 = (VertexNormals[x + 1, y] - VertexNormals[x, y + 1]) / Size;
                        Vector V3 = (VertexNormals[x + 1, y + 1] - VertexNormals[x, y + 1]) / Size;
                        Vector Va = VertexNormals[x, y];
                        Vector Vb = VertexNormals[x, y + 1];
                        Vector Vc = VertexNormals[x, y + 1];

                        double dza = (Points[x + 1, y].Z - Points[x, y].Z) / Size;
                        double dzb = (Points[x + 1, y].Z - Points[x, y + 1].Z) / Size;
                        double dzc = (Points[x + 1, y + 1].Z - Points[x, y + 1].Z) / Size;

                        double zd;
                        double dzd;
                        double yd;
                        Vector V4;
                        Vector Vd;


                        /*Vector LightDir;
                        Vector RayPos = (Points[x, y] + Points[x+1, y] + Points[x, y+1] + Points[x+1, y+1]) / 4.0;
                        if (Sources[k] is PointLight)
                            LightDir = (RayPos - (Sources[k] as PointLight).Point).Normalize();
                        else
                            LightDir = (Sources[k] as DirLight).Dir;

                        Vector ResultPoint, ResultNormal;
                        Vector ShadowColor1 = new Vector(), ShadowColor2 = new Vector();
                        bool exit = false;
                        for (int y1 = 0; y1 < SizeY - 1; y1++)
                        {
                            for (int x1 = 0; x1 < SizeX - 1; x1++)
                            {
                                if (RayTriangleIntersect(RayPos, LightDir, Points[x1, y1], Points[x1+1,y1], Points[x1, y1+1], out ResultPoint, out ResultNormal))
                                {
                                    ShadowColor1 = GetColor(RayPos, (VertexNormals[x1, y1] + VertexNormals[x1 + 1, y1] + VertexNormals[x1, y1 + 1] + 
                                        VertexNormals[x1 + 1, y1 + 1]).Normalize(), Sources[k]);
                                    exit = true;
                                }
                                if (RayTriangleIntersect(RayPos, LightDir, Points[x1+1, y1+1], Points[x1+1,y1], Points[x1, y1+1], out ResultPoint, out ResultNormal))
                                {
                                    ShadowColor2 = GetColor(RayPos, (VertexNormals[x1, y1] + VertexNormals[x1 + 1, y1] + VertexNormals[x1, y1 + 1] + 
                                        VertexNormals[x1 + 1, y1 + 1]).Normalize(), Sources[k]);
                                    exit = true;
                                }
                                if (exit)
                                    break;
                            }
                            if (exit)
                                break;
                        }*/

                        Vector ResColor1 = AmbientColor;// -ShadowColor1;
                        Vector ResColor2 = AmbientColor;// -ShadowColor2;


                        for (xabc = x * dx1; xabc < (x + 1) * dx1; xabc += dx)
                        {
                            zd = za;
                            dzd = (zb - za) / (Size - (y + dy1 - yb));
                            V4 = (Vb - Va) / (Size - (y + dy1 - yb));
                            Vd = Va;
                            for (yd = y * dy1; yd < yb; yd += dy)
                            {
                                //LightmapTemp[(int)Math.Round(xabc * Size), (int)Math.Round(yd * Size)] += GetColor(new Vector(xabc, yd, zd), Vd, Sources[k])
                                //    - ResColor1 + AmbientColor;

                                AddColor(new Vector(xabc, yd, zd), Vd, Sources[k], ResColor1, ref Lightmap[(int)Math.Round(xabc * Size), (int)Math.Round(yd * Size)]);

                                //xabc, yd, zd; Vd
                                zd += dzd;
                                Vd += V4;
                            }

                            zd = zb;
                            dzd = (zc - zb) / (Size - (yb - y));
                            V4 = (Vc - Vb) / (Size - (yb - y));
                            Vd = Vb;
                            for (yd = yb; yd < (y + 1) * dy1; yd += dy)
                            {
                                //LightmapTemp[(int)Math.Round(xabc * Size), (int)Math.Round(yd * Size)] += GetColor(new Vector(xabc, yd, zd), Vd, Sources[k])
                                //    - ResColor2 + AmbientColor;

                                AddColor(new Vector(xabc, yd, zd), Vd, Sources[k], ResColor2, ref Lightmap[(int)Math.Round(xabc * Size), (int)Math.Round(yd * Size)]);

                                zd += dzd;
                                Vd += V4;
                            }

                            yb -= dy;
                            za += dza;
                            zb += dzb;
                            zc += dzc;

                            Va += V1;
                            Vb += V2;
                            Vc += V3;
                        }
                        NormInd += 2;
                    }
                    NormInd += 2;
                }   
            }
            /*for (int i = 0; i < LSizeX; i++)
                for (int j = 0; j < LSizeY; j++)
                    Lightmap[i, j] = ConvertColor(LightmapTemp[i, j]);*/
        }

        // построение карты теней и смешение ее с картой освещенности
        public void BuildShadowmap(Light[] Lights, int Size)
        {

        }

        // построение "цветовой" карты (или текстуры) на основе массива Colors
        public void GenerateColormap(Color[] Colors, int Size)
        {
            CSizeX = SizeX;
            CSizeY = SizeY;
            Colormap = new int[SizeX, SizeY];
            for (int i = 0; i < SizeX; i++)
                for (int j = 0; j < CSizeY; j++)
                    Colormap[i, j] = Colors[(int)(Heightmap[i, j] * (Colors.Length - 1))].ToArgb();

           /* 
            CSizeX = (int)(this.Dimen.X * Size);
            CSizeY = (int)(this.Dimen.Y * Size);
            
            int NormInd;
            double dx1 = this.Dimen.X / SizeX;
            double dy1 = this.Dimen.Y / SizeY;
            double dx = dx1 / Size;
            double dy = dy1 / Size;
                NormInd = 0;
                for (int y = 0; y < SizeY - 1; y++)
                {
                    for (int x = 0; x < SizeX - 1; x++)
                    {
                        double xabc = x * dx1;
                        double za = Points[x, y].Z;
                        double zb = Points[x, y + 1].Z;
                        double zc = Points[x, y + 1].Z;
                        double yb = y * dy1 + dy1;

                        double dza = (Points[x + 1, y].Z - Points[x, y].Z) / Size;
                        double dzb = (Points[x + 1, y].Z - Points[x, y + 1].Z) / Size;
                        double dzc = (Points[x + 1, y + 1].Z - Points[x, y + 1].Z) / Size;

                        double zd;
                        double dzd;
                        double yd;

                        for (xabc = x * dx1; xabc < (x + 1) * dx1; xabc += dx)
                        {
                            zd = za;
                            dzd = (zb - za) / (Size - (y + dy1 - yb));
                            for (yd = y * dy1; yd < yb; yd += dy)
                            {
                                Colormap[(int)Math.Round(xabc * Size), (int)Math.Round(yd * Size)] = ;

                                zd += dzd;
                            }

                            zd = zb;
                            dzd = (zc - zb) / (Size - (yb - y));
                            for (yd = yb; yd < (y + 1) * dy1; yd += dy)
                            {
                                Colormap[(int)Math.Round(xabc * Size), (int)Math.Round(yd * Size)] = Heightmap[;

                                zd += dzd;
                            }

                            yb -= dy;
                            za += dza;
                            zb += dzb;
                            zc += dzc;
                        }
                        NormInd += 2;
                    }
                    NormInd += 2;
                } */
        }

        // сохранение карты высот в файл BMP
        public void SaveHeightmap(string FileName, out Bitmap Result)
        {
            Bitmap img = new Bitmap(SizeX, SizeY, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            System.Drawing.Imaging.BitmapData bmpData =
                img.LockBits(new Rectangle(0, 0, SizeX, SizeY), System.Drawing.Imaging.ImageLockMode.WriteOnly,
                System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            IntPtr ptr = bmpData.Scan0;

            byte[] ar = new byte[SizeX * SizeY];
            for (int j = 0; j < SizeY; j++)
                for (int i = 0; i < SizeX; i++)
                    ar[j * SizeX + i] = Convert.ToByte(Heightmap[i, j] * 255);
            System.Runtime.InteropServices.Marshal.Copy(ar, 0, bmpData.Scan0, SizeX * SizeY);

            img.Save(FileName, System.Drawing.Imaging.ImageFormat.Bmp);
            img.UnlockBits(bmpData);
            
           
            /*StreamReader stream = new StreamReader(FileName, Encoding.Default);
            int L = (int)stream.BaseStream.Length;
            char[] buffer = new char[stream.BaseStream.Length];
            stream.ReadBlock(buffer, 0, L);
            float k = 0;
            for (int i = 0; i < 1024; i += 4)
            {
                buffer[54 + i + 0] = buffer[54 + i + 1] = buffer[54 + i + 2] =  (char)k;
                k += 0.5f;
            }   
            stream.Close();

            StreamWriter stream1 = new StreamWriter(FileName, false, Encoding.Default);
            stream1.Write(buffer);
            stream1.Close();

            img = new Bitmap(FileName);*/

            Result = img;
        }

        // сохранение карты освещенности в файл BMP
        public void SaveLightmap(string FileName, out Bitmap Result)
        {
            Bitmap img = new Bitmap(LSizeX, LSizeY,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            System.Drawing.Imaging.BitmapData bmpData =
                img.LockBits(new Rectangle(0, 0, LSizeX, LSizeY), 
                System.Drawing.Imaging.ImageLockMode.WriteOnly,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            IntPtr ptr = bmpData.Scan0;

            byte[] ar = new byte[LSizeX * LSizeY * 3];
            for (int j = 0; j < LSizeY; j++)
                for (int i = 0; i < LSizeX; i++)
                {
                    ar[(j * LSizeX + i) * 3]     = Convert.ToByte(Lightmap[i, j] & 255);
                    ar[(j * LSizeX + i) * 3 + 1] = Convert.ToByte((Lightmap[i, j] >> 8) & 255);
                    ar[(j * LSizeX + i) * 3 + 2] = Convert.ToByte((Lightmap[i, j] >> 16) & 255);
                }
            System.Runtime.InteropServices.Marshal.Copy(ar, 0, bmpData.Scan0, LSizeX * LSizeY * 3);
             
            img.Save(FileName, System.Drawing.Imaging.ImageFormat.Bmp);
            img.UnlockBits(bmpData);

            Result = img;
        }

        // сохранение текстуры в файл BMP
        public void SaveColormap(string FileName, out Bitmap Result)
        {
            Bitmap img = new Bitmap(CSizeX, CSizeY,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            System.Drawing.Imaging.BitmapData bmpData =
                img.LockBits(new Rectangle(0, 0, CSizeX, CSizeY),
                System.Drawing.Imaging.ImageLockMode.WriteOnly,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            IntPtr ptr = bmpData.Scan0;

            byte[] ar = new byte[CSizeX * CSizeY * 3];
            for (int j = 0; j < CSizeY; j++)
                for (int i = 0; i < CSizeX; i++)
                {
                    ar[(j * CSizeX + i) * 3] = Convert.ToByte(Colormap[i, j] & 255);
                    ar[(j * CSizeX + i) * 3 + 1] = Convert.ToByte((Colormap[i, j] >> 8) & 255);
                    ar[(j * CSizeX + i) * 3 + 2] = Convert.ToByte((Colormap[i, j] >> 16) & 255);
                }
            System.Runtime.InteropServices.Marshal.Copy(ar, 0, bmpData.Scan0, CSizeX * CSizeY * 3);

            img.Save(FileName, System.Drawing.Imaging.ImageFormat.Bmp);
            img.UnlockBits(bmpData);

            Result = img;
        }
    }
}
