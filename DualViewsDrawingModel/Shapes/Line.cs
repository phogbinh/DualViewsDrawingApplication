using System;

namespace DualViewsDrawingModel.Shapes
{
    public class Line
    {
        public double X1
        {
            get
            {
                return _x1;
            }
        }
        public double Y1
        {
            get
            {
                return _y1;
            }
        }
        public double X2
        {
            get
            {
                return _x2;
            }
        }
        public double Y2
        {
            get
            {
                return _y2;
            }
        }
        private double _x1;
        private double _y1;
        private double _x2;
        private double _y2;

        public Line(Point drawingStartingPoint, Point drawingEndingPoint)
        {
            if ( drawingStartingPoint == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_DRAWING_STARTING_POINT_IS_NULL);
            }
            if ( drawingEndingPoint == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_DRAWING_ENDING_POINT_IS_NULL);
            }
            _x1 = drawingStartingPoint.X;
            _y1 = drawingStartingPoint.Y;
            _x2 = drawingEndingPoint.X;
            _y2 = drawingEndingPoint.Y;
        }
    }
}
