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
        }
        public double Y
        {
            get
            {
                return _y;
            }
        }
        public double Width
        {
            get
            {
                return _width;
            }
        }
        public double Height
        {
            get
            {
                return _height;
            }
        }
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
            _width = Math.Abs(drawingStartingPoint.X - drawingEndingPoint.X);
            _height = Math.Abs(drawingStartingPoint.Y - drawingEndingPoint.Y);
        }
    }
}
