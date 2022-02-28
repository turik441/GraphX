using System;

namespace GraphX.Measure
{
    public struct Vector
    {
        public double X { get; set; }

        public double Y { get; set; }

        public Vector(double x, double y) { X = x; Y = y; }

        public static Vector Zero { get; } = new Vector();

        #region Overloaded operators

        public static bool operator ==(Vector vector1, Vector vector2)
        {
            return vector1.X == vector2.X && vector1.Y == vector2.Y;
        }

        public static bool operator !=(Vector vector1, Vector vector2)
        {
            return !(vector1 == vector2);
        }



        public static double operator *(Vector vector1, Vector vector2)
        {
            return ((vector1.X * vector2.X) + (vector1.Y * vector2.Y));
        }

        public static Vector operator *(double scalar, Vector vector)
        {
            return new Vector(vector.X * scalar, vector.Y * scalar);
        }

        public static Vector operator *(Vector vector, double scalar)
        {
            return new Vector(vector.X * scalar, vector.Y * scalar);
        }

        public static Vector operator *(int value1, Vector value2)
        {
            return new Vector(value1 * value2.X, value1 * value2.Y);
        }

        public static Vector operator +(Vector value1, Vector value2)
        {
            return new Vector(value1.X + value2.X, value1.Y + value2.Y);        
        }

        public static Vector operator -(Vector value1, Vector value2)
        {
            return new Vector(value1.X - value2.X, value1.Y - value2.Y);
        }

        public static Vector operator /(Vector vector, double scalar)
        {
            return vector * (1.0 / scalar);
        }

        public static Vector operator -(Vector value1)
        {
            return new Vector(-value1.X, -value1.Y);
        }



        
        public static Point operator +(Vector value1, Point value2)
        {
            return new Point(value1.X + value2.X, value1.Y + value2.Y);
        }

       /* public static Vector operator /(Vector value1, Vector value2)
        {
            return new Vector(value1.X / value2.X, value1.Y / value2.Y);
        }*/

        public static Vector operator -(Vector value1, Point value2)
        {
            return new Vector(value1.X - value2.X, value1.Y - value2.Y);
        }


        #endregion

        public static bool Equals(Vector vector1, Vector vector2)
        {
            return (vector1.X.Equals(vector2.X) && vector1.Y.Equals(vector2.Y));
        }

        public override bool Equals(object o)
        {
            if (!(o is Vector))
                return false;
            return Equals(this, (Vector)o);
        }

        public bool Equals(Vector value)
        {
            return Equals(this, value);
        }

        public override int GetHashCode()
        {
            return (X.GetHashCode() ^ Y.GetHashCode());
        }

        public double Length => Math.Sqrt((X * X) + (Y * Y));
        public double LengthSquared => ((X * X) + (Y * Y));

        public void Normalize()
        {
            var v = this / Math.Max(Math.Abs(X), Math.Abs(Y));
            v = this / Length;
            X = v.X;
            Y = v.Y;
        }

        public static double CrossProduct(Vector vector1, Vector vector2)
        {
            return ((vector1.X * vector2.Y) - (vector1.Y * vector2.X));
        }

        public static double AngleBetween(Vector vector1, Vector vector2)
        {
            var y = (vector1.X * vector2.Y) - (vector2.X * vector1.Y);
            var x = (vector1.X * vector2.X) + (vector1.Y * vector2.Y);
            return (Math.Atan2(y, x) * 57.295779513082323);
        }

        public void Negate()
        {
            X = -X;
            Y = -Y;
        }

        public override string ToString()
        {
            return $"{X}:{Y}";
        }

        public Point ToPoint => new Point(X, Y);
    }
}
