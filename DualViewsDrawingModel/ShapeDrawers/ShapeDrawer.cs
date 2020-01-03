using DualViewsDrawingModel.Shapes;
using System;

namespace DualViewsDrawingModel.ShapeDrawers
{
    public abstract class ShapeDrawer : IClosePointDetector
    {
        public ShapeDrawerType Type
        {
            get
            {
                return _type;
            }
        }
        public Point DrawingEndingPoint
        {
            get
            {
                return _drawingEndingPoint;
            }
            set
            {
                _drawingEndingPoint = value;
            }
        }
        protected ShapeDrawerType _type;
        protected Point _drawingStartingPoint;
        protected Point _drawingEndingPoint;

        public ShapeDrawer(Point drawingStartingPointData, Point drawingEndingPointData)
        {
            if ( drawingStartingPointData == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_DRAWING_STARTING_POINT_IS_NULL);
            }
            if ( drawingEndingPointData == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_DRAWING_ENDING_POINT_IS_NULL);
            }
            _type = ShapeDrawerType.None;
            _drawingStartingPoint = drawingStartingPointData;
            _drawingEndingPoint = drawingEndingPointData;
        }

        /// <summary>
        /// Determines whether [is close to point] [the specified point].
        /// </summary>
        public bool IsCloseToPoint(Point point, double pointToShapeDrawerMaximumDistanceSquared)
        {
            return GetClosePointDetector().IsCloseToPoint(point, pointToShapeDrawerMaximumDistanceSquared);
        }

        /// <summary>
        /// Gets the rectangle.
        /// </summary>
        public virtual Rectangle GetRectangle()
        {
            return new Rectangle(_drawingStartingPoint, _drawingEndingPoint);
        }

        /// <summary>
        /// Draws the specified graphics.
        /// </summary>
        public abstract void Draw(IGraphics graphics);

        /// <summary>
        /// Gets the close point detector.
        /// </summary>
        public abstract IClosePointDetector GetClosePointDetector();
    }
}
