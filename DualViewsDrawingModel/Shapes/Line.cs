using System;

namespace DualViewsDrawingModel.Shapes
{
    public class Line : IClosePointDetector, IIncludingPointDetector
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

        /// <summary>
        /// Determines whether [is close to point].
        /// </summary>
        public bool IsCloseToPoint(Point point, double pointToLineMaximumDistanceSquared)
        {
            Point nearByPoint = GetNearByPoint(point);
            Vector pointToNearByPoint = new Vector(nearByPoint.X, nearByPoint.Y) - new Vector(point.X, point.Y);
            return IsIncludingPoint(nearByPoint) && pointToNearByPoint.LengthSquared <= pointToLineMaximumDistanceSquared;
        }

        /// <summary>
        /// Gets the closest point.
        /// </summary>
        public Point GetNearByPoint(Point point)
        {
            Vector lineHeadToPoint = new Vector(point.X, point.Y) - new Vector(_x1, _y1);
            Vector lineHeadToTail = new Vector(_x2, _y2) - new Vector(_x1, _y1);
            double normalizedDistanceFromLineHeadToClosetPoint = Vector.DotProduct(lineHeadToPoint, lineHeadToTail) / lineHeadToTail.LengthSquared;
            return new Point(_x1 + lineHeadToTail.X * normalizedDistanceFromLineHeadToClosetPoint, _y1 + lineHeadToTail.Y * normalizedDistanceFromLineHeadToClosetPoint);
        }

        /// <summary>
        /// Determines whether [is including point] [the specified point].
        /// </summary>
        public bool IsIncludingPoint(Point point)
        {
            if ( !IsAlignedWithPoint(point, Definitions.DOUBLE_DIFFERENCE) )
            {
                return false;
            }
            Vector pointToLineHead = new Vector(_x1, _y1) - new Vector(point.X, point.Y);
            Vector lineTailToHead = new Vector(_x1, _y1) - new Vector(_x2, _y2);
            double dotProduct = Vector.DotProduct(pointToLineHead, lineTailToHead);
            return Definitions.IsInclusiveInInterval(dotProduct, 0.0, lineTailToHead.LengthSquared);
        }

        /// <summary>
        /// Determines whether [is aligned with point] [the specified point].
        /// </summary>
        public bool IsAlignedWithPoint(Point point, double difference)
        {
            Vector pointToLineHead = new Vector(_x1, _y1) - new Vector(point.X, point.Y);
            Vector lineTailToHead = new Vector(_x1, _y1) - new Vector(_x2, _y2);
            return Math.Abs(Vector.CrossProduct(pointToLineHead, lineTailToHead)) <= difference;
        }
    }
}
