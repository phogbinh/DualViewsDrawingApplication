using System;

namespace DualViewsDrawingModel.Shapes
{
    public class Rectangle
    {
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }
        public double Width
        {
            get
            {
                return _width;
            }
            set
            {
                if ( value < 0 )
                {
                    throw new ApplicationException(ERROR_WIDTH_CANNOT_BE_SET_TO_NEGATIVE);
                }
                _width = value;
            }
        }
        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                if ( value < 0 )
                {
                    throw new ApplicationException(ERROR_HEIGHT_CANNOT_BE_SET_TO_NEGATIVE);
                }
                _height = value;
            }
        }
        private const string ERROR_WIDTH_CANNOT_BE_SET_TO_NEGATIVE = "Width cannot be set to negative.";
        private const string ERROR_HEIGHT_CANNOT_BE_SET_TO_NEGATIVE = "Height cannot be set to negative.";
        private double _x;
        private double _y;
        private double _width;
        private double _height;

        public Rectangle(Point drawingStartingPoint, Point drawingEndingPoint)
        {
            if ( drawingStartingPoint == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_DRAWING_STARTING_POINT_IS_NULL);
            }
            if ( drawingEndingPoint == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_DRAWING_ENDING_POINT_IS_NULL);
            }
            _x = Math.Min(drawingStartingPoint.X, drawingEndingPoint.X);
            _y = Math.Min(drawingStartingPoint.Y, drawingEndingPoint.Y);
            Width = Math.Abs(drawingStartingPoint.X - drawingEndingPoint.X);
            Height = Math.Abs(drawingStartingPoint.Y - drawingEndingPoint.Y);
        }

        /// <summary>
        /// Determines whether [is including point] [the specified point].
        /// </summary>
        public bool IsIncludingPoint(Point point)
        {
            if ( point == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_POINT_IS_NULL);
            }
            return point.IsInclusiveInRegion(_x, GetLowerRightX(), _y, GetLowerRightY());
        }

        /// <summary>
        /// Gets the lower right x.
        /// </summary>
        public double GetLowerRightX()
        {
            return _x + Width;
        }

        /// <summary>
        /// Gets the lower right y.
        /// </summary>
        public double GetLowerRightY()
        {
            return _y + Height;
        }
    }
}
