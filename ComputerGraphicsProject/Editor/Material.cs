using Geometry3D;

namespace Editor
{
    class Material
    {
        public Vector Ambient;
        public Vector Diffuse;
        public Vector Mirror;

        public Material(Vector Ambient, Vector Diffuse, Vector Mirror)
        {
            this.Ambient = Ambient;
            this.Diffuse = Diffuse;
            this.Mirror = Mirror;
        }
    }
}
