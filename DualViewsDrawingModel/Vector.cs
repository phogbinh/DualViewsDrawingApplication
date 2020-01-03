using System;

namespace DualViewsDrawingModel
{
    public class Vector
    {
        public double X
        {
            get
            {
                return _x;
            }
        }
        public double Y
        {
            get
            {
                return _y;
            }
        }
        public double LengthSquared
        {
            get
            {
                return _x * _x + _y * _y;
            }
        }
        private const string ERROR_VECTOR_IS_NULL = "The given vector is null.";
        private double _x;
        private double _y;

        public Vector(double xData, double yData)
        {
            _x = xData;
            _y = yData;
        }

        public static Vector operator -(Vector vector)
        {
            if ( vector == null )
            {
                throw new ArgumentNullException(ERROR_VECTOR_IS_NULL);
            }
            return new Vector(-vector._x, -vector._y);
        }

        public static Vector operator +(Vector leftVector, Vector rightVector)
        {
            if ( leftVector == null )
            {
                throw new ArgumentNullException(ERROR_VECTOR_IS_NULL);
            }
            if ( rightVector == null )
            {
                throw new ArgumentNullException(ERROR_VECTOR_IS_NULL);
            }
            return new Vector(leftVector._x + rightVector._x, leftVector._y + rightVector._y);
        }

        public static Vector operator -(Vector leftVector, Vector rightVector)
        {
            return leftVector + ( -rightVector );
        }

        /// <summary>
        /// Dots the product.
        /// </summary>
        public static double DotProduct(Vector leftVector, Vector rightVector)
        {
            if ( leftVector == null )
            {
                throw new ArgumentNullException(ERROR_VECTOR_IS_NULL);
            }
            if ( rightVector == null )
            {
                throw new ArgumentNullException(ERROR_VECTOR_IS_NULL);
            }
            return leftVector.X * rightVector.X + leftVector.Y * rightVector.Y;
        }

        /// <summary>
        /// Crosses the product.
        /// </summary>
        public static double CrossProduct(Vector leftVector, Vector rightVector)
        {
            if ( leftVector == null )
            {
                throw new ArgumentNullException(ERROR_VECTOR_IS_NULL);
            }
            if ( rightVector == null )
            {
                throw new ArgumentNullException(ERROR_VECTOR_IS_NULL);
            }
            return leftVector.Y * rightVector.X - leftVector.X * rightVector.Y;
        }
    }
}
