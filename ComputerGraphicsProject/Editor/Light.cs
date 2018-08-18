using Geometry3D;

namespace Lighting
{
    class Light
    {
        public Vector Ambient;
        public Vector Diffuse;
        public Vector Mirror;

        public Light() { }
        public Light(Vector Ambient, Vector Diffuse, Vector Mirror) 
        {
            this.Ambient = Ambient;
            this.Diffuse = Diffuse;
            this.Mirror = Mirror;
        }
    }

    class DirLight : Light
    {
        public Vector Dir;

        public DirLight() { }
        public DirLight(Vector Ambient, Vector Diffuse, Vector Mirror, Vector Dir)
            : base(Ambient, Diffuse, Mirror)
        {
            this.Dir = Dir;
        }
    }

    class PointLight : Light
    {
        public Vector Point;
        public double c1, c2, c3;

        public PointLight(Vector Ambient, Vector Diffuse, Vector Mirror, double c1, double c2, double c3, Vector Point)
            : base(Ambient, Diffuse, Mirror)
        {
            this.Point = Point;
            this.c1 = c1;
            this.c2 = c2;
            this.c3 = c3;
        }
    }
}
