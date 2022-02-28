using System;
using System.Diagnostics;

namespace GraphX.Measure
{
    /// <summary>
    /// Custom PCL implementation of Rect class
    /// </summary>
    [DebuggerDisplay("Size={X} {Y} {Width} {Height}")]
    public struct Rect
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Left => X;
        public double Top => Y;
        public double Bottom { get { if (IsEmpty) return double.NegativeInfinity; return Y + _height; } }
        public double Right { get { if (IsEmpty) return double.NegativeInfinity; return X + _width; } }

        internal double _width;
        public double Width 
        {
            get => _width;
            set {
                if (IsEmpty)
                    throw new InvalidOperationException("Rect_CannotModifyEmptyRect");
                if (value < 0.0)
                    throw new ArgumentException("Size_WidthCannotBeNegative");
                _width = value;
            }
        }
        internal double _height;
        public double Height { get => _height;
            set {
                if (IsEmpty)
                    throw new InvalidOperationException("Rect_CannotModifyEmptyRect");
                if (value < 0.0)
                    throw new ArgumentException("Size_HeightCannotBeNegative");
                _height = value;
            } }

        public Point BottomLeft => new Point(X, Bottom);
        public Point TopLeft => new Point(X, Y);
        public Point TopRight => new Point(Right, Y);
        public Point BottomRight => new Point(Right, Bottom);

        public static Rect Empty { get; }

        public bool IsEmpty => _width < 0.0;

        public Point Location
        {
            get => new Point(X, Y);
            set
            {
                if (IsEmpty)
                {
                    X = 0;
                    Y = 0;
                    return;                    
                }
                X = value.X;
                Y = value.Y;
            }
        }
        public Size Size
        {
            get => IsEmpty ? Size.Empty : new Size(_width, _height);
            set
            {
                if (value.IsEmpty)
                {
                    _width = 0;
                    _height = 0;
                }
                else
                {
                    if (IsEmpty)
                    {
                        _width = 0;
                        _height = 0;
                        return;
                    }
                    _width = value._width;
                    _height = value._height;
                }
            }
        }


        public Rect(Point location, Size size)
        {
            if (size.IsEmpty)
            {
                X = 0;
                Y = 0;
                _width = 0;
                _height = 0;
            }
            else
            {
                X = location.X;
                Y = location.Y;
                _width = size._width;
                _height = size._height;
            }
        }

        public Rect(double x, double y, double width, double height)
        {
            if (width < 0.0 || height < 0.0)
            {
                throw new ArgumentException("Size_WidthAndHeightCannotBeNegative");
            }
            X = x;
            Y = y;
            _width = width;
            _height = height;
        }

        public Rect(Point point1, Point point2)
        {
            X = Math.Min(point1.X, point2.X);
            Y = Math.Min(point1.Y, point2.Y);
            _width = Math.Max(Math.Max(point1.X, point2.X) - X, 0);
            _height = Math.Max(Math.Max(point1.Y, point2.Y) - Y, 0);
        }

        public Rect(Point point, Vector vector)
            : this(point, point + vector)
        {
        }

        public Rect(Size size)
        {
            if (size.IsEmpty)
            {
                X = 0;
                Y = 0;
                _width = 0;
                _height = 0;
            }
            else
            {
                X = Y = 0.0;
                _width = size.Width;
                _height = size.Height;
            }
        }

        #region Custom operator overloads

        public static bool operator ==(Rect value1, Rect value2)
        {
            return value1.Left == value2.Left && value1.Top == value2.Top && value1.Right == value2.Right && value1.Bottom == value2.Bottom;
        }

        public static bool operator !=(Rect rect1, Rect rect2)
        {
            return !(rect1 == rect2);
        }

        #endregion

        public static bool Equals(Rect rect1, Rect rect2)
        {
            if (rect1.IsEmpty)
            {
                return rect2.IsEmpty;
            }
            return rect1.X.Equals(rect2.X) && rect1.Y.Equals(rect2.Y) && rect1.Width.Equals(rect2.Width) && rect1.Height.Equals(rect2.Height);
        }

        public override bool Equals(object o)
        {
            return o is Rect rect && Equals(this, rect);
        }

        public bool Equals(Rect value)
        {
            return Equals(this, value);
        }

        public override int GetHashCode()
        {
            if (IsEmpty)
            {
                return 0;
            }
            return X.GetHashCode() ^ Y.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode();
        }

        public void Offset(Vector offsetVector)
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Rect_CannotCallMethod");
            }
            X += offsetVector.X;
            Y += offsetVector.Y;
        }

        public void Offset(double offsetX, double offsetY)
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Rect_CannotCallMethod");
            }
            X += offsetX;
            Y += offsetY;
        }

        public static Rect Offset(Rect rect, Vector offsetVector)
        {
            rect.Offset(offsetVector.X, offsetVector.Y);
            return rect;
        }

        public static Rect Offset(Rect rect, double offsetX, double offsetY)
        {
            rect.Offset(offsetX, offsetY);
            return rect;
        }

        public bool IntersectsWith(Rect rect)
        {
            if (IsEmpty || rect.IsEmpty)
            {
                return false;
            }
            return rect.Left <= Right && rect.Right >= Left && rect.Top <= Bottom && rect.Bottom >= Top;
        }

        public void Intersect(Rect rect)
        {
            if (!IntersectsWith(rect))
            {
                X = 0;
                Y = 0;
                _width = 0;
                _height = 0;
            }
            else
            {
                var num2 = Math.Max(Left, rect.Left);
                var num = Math.Max(Top, rect.Top);
                _width = Math.Max(Math.Min(Right, rect.Right) - num2, 0.0);
                _height = Math.Max(Math.Min(Bottom, rect.Bottom) - num, 0.0);
                X = num2;
                Y = num;
            }
        }

        public static Rect Intersect(Rect rect1, Rect rect2)
        {
            rect1.Intersect(rect2);
            return rect1;
        }

        public void Union(Rect rect)
        {
            if (IsEmpty)
            {
                X = 0;
                Y = 0;
                _width = 0;
                _height = 0;
            }
            else if (!rect.IsEmpty)
            {
                double num2 = Math.Min(Left, rect.Left);
                double num = Math.Min(Top, rect.Top);
                if (rect.Width == double.PositiveInfinity || Width == double.PositiveInfinity)
                {
                    _width = double.PositiveInfinity;
                }
                else
                {
                    double num4 = Math.Max(Right, rect.Right);
                    _width = Math.Max(num4 - num2, 0.0);
                }
                if (rect.Height == double.PositiveInfinity || Height == double.PositiveInfinity)
                {
                    _height = double.PositiveInfinity;
                }
                else
                {
                    double num3 = Math.Max(Bottom, rect.Bottom);
                    _height = Math.Max(num3 - num, 0.0);
                }
                X = num2;
                Y = num;
            }
        }

        public static Rect Union(Rect rect1, Rect rect2)
        {
            rect1.Union(rect2);
            return rect1;
        }

        public void Union(Point point)
        {
            Union(new Rect(point, point));
        }

        public static Rect Union(Rect rect, Point point)
        {
            rect.Union(new Rect(point, point));
            return rect;
        }

        public bool Contains(Point point) => Contains(point.X, point.Y);

        public bool Contains(double x, double y) => !IsEmpty && ContainsInternal(x, y);

        public bool Contains(Rect rect)
        {
            if (IsEmpty || rect.IsEmpty)
            {
                return false;
            }
            return X <= rect.X && Y <= rect.Y && X + _width >= rect.X + rect._width && Y + _height >= rect.Y + rect._height;
        }

        private bool ContainsInternal(double x, double y)
        {
            return x >= X && x - _width <= X && y >= Y && y - _height <= Y;
        }

        private static Rect CreateEmptyRect()
        {
            return new Rect() { X = double.PositiveInfinity, Y = double.PositiveInfinity, _width = double.NegativeInfinity, _height = double.NegativeInfinity };
        }

        static Rect()
        {
            Empty = CreateEmptyRect();
        }

        public void Inflate(Size size)
        {
            Inflate(size._width, size._height);
        }

        public void Inflate(double width, double height)
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Rect_CannotCallMethod");
            }
            X -= width;
            Y -= height;
            _width += width;
            _width += width;
            _height += height;
            _height += height;
            if (!(_width < 0.0) && !(_height < 0.0)) return;
            X = 0;
            Y = 0;
            _width = 0;
            _height = 0;
        }

        public static Rect Inflate(Rect rect, Size size)
        {
            rect.Inflate(size._width, size._height);
            return rect;
        }


        public static Rect InflateNew(Rect rect, double width, double height)
        {
            var r = new Rect(rect.X, rect.Y, rect._width, rect._height);
            r.Inflate(width, height);
            return r;
        }


        public static Rect Inflate(Rect rect, double width, double height)
        {
            rect.Inflate(width, height);
            return rect;
        }
    }
}
