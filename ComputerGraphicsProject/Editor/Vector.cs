using System;

namespace Geometry3D
{
    public struct Vector
    {
        public double X, Y, Z;

        public Vector(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public Vector(Vector rhs)
        {
            X = rhs.X;
            Y = rhs.Y;
            Z = rhs.Z;
        }

        public override string ToString()
        {
            return "( " + X + " , " + Y + " , " + Z + " )";
        }

        public static Vector operator -(Vector vec)
        {
            Vector result = new Vector(-vec.X, -vec.Y, -vec.Z);
            return result;
        }

        public static Vector operator +(Vector lhs, Vector rhs)
        {
            Vector result = new Vector(lhs);
            result.X += rhs.X;
            result.Y += rhs.Y;
            result.Z += rhs.Z;

            return result;
        }

        public static Vector operator -(Vector lhs, Vector rhs)
        {
            Vector result = new Vector(lhs);
            result.X -= rhs.X;
            result.Y -= rhs.Y;
            result.Z -= rhs.Z;

            return result;
        }

        public static Vector operator *(double lhs, Vector rhs)
        {
            return new Vector(lhs * rhs.X, lhs * rhs.Y, lhs * rhs.Z);
        }

        public static Vector operator *(Vector lhs, double rhs)
        {
            return rhs * lhs;
        }

        public static Vector operator /(Vector lhs, double rhs)
        {
            return new Vector(lhs.X / rhs, lhs.Y / rhs, lhs.Z / rhs);
        }

        public static double operator *(Vector lhs, Vector rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z;
        }

        public static Vector operator |(Vector lhs, Vector rhs)
        {
            return new Vector(lhs.X * rhs.X, lhs.Y * rhs.Y, lhs.Z * rhs.Z);
        }

        public static Vector operator ^(Vector lhs, Vector rhs)
        {
            return new Vector(lhs.Y * rhs.Z - lhs.Z * rhs.Y, lhs.Z * rhs.X - lhs.X * rhs.Z, lhs.X * rhs.Y - lhs.Y * rhs.X);
        }

        public static bool operator ==(Vector lhs, Vector rhs)
        {
            return (lhs.X == rhs.X) && (lhs.Y == rhs.Y) && (lhs.Z == rhs.Z);
        }

        public static bool operator !=(Vector lhs, Vector rhs)
        {
            return (lhs.X != rhs.X) || (lhs.Y != rhs.Y) || (lhs.Z != rhs.Z);
        }


        public double Length() 
        { 
            return Math.Sqrt(X * X + Y * Y + Z * Z); 
        }

        public double LengthSqr() 
        { 
            return X * X + Y * Y + Z * Z; 
        }

        public void SelfNormalize() 
        { 
            double L = 1 / Length(); X *= L; Y *= L; Z *= L; 
        }

        public Vector Normalize()
        {
            double L = 1 / Length();
            return new Vector(X * L, Y * L, Z * L);
        }

        public static Vector Normal(Vector v1, Vector v2, Vector v3)
        {
            return (v2 - v1) ^ (v3 - v1);
        }

        public static Vector NormalIdentity(Vector v1, Vector v2, Vector v3)
        {
            return ((v2 - v1) ^ (v3 - v1)).Normalize();
        }
    }


}
