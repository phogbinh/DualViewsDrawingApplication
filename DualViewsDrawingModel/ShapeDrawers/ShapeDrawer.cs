using DualViewsDrawingModel.Shapes;
using System;
using System.Collections.Generic;

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
        /// Determines whether [is drawing ending point close to point].
        /// </summary>
        public bool IsDrawingEndingPointCloseToPoint(Point point, double pointToShapeDrawerDrawingEndingPointMaximumDistanceSquared)
        {
            return _drawingEndingPoint.IsCloseToPoint(point, pointToShapeDrawerDrawingEndingPointMaximumDistanceSquared);
        }

        /// <summary>
        /// Gets the rectangle.
        /// </summary>
        public virtual Rectangle GetRectangle()
        {
            return new Rectangle(_drawingStartingPoint, _drawingEndingPoint);
        }

        /// <summary>
        /// Draws the selection corners.
        /// </summary>
        protected void DrawSelectionCorners(IGraphics graphics)
        {
            if ( graphics == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_GRAPHICS_IS_NULL);
            }
            foreach ( Point cornerPoint in this.GetCornerPoints() )
            {
                graphics.DrawSelectionCorner(cornerPoint);
            }
        }

        /// <summary>
        /// Draws the specified graphics.
        /// </summary>
        public abstract void Draw(IGraphics graphics);

        /// <summary>
        /// Draws the selection border.
        /// </summary>
        public abstract void DrawSelectionBorder(IGraphics graphics);

        /// <summary>
        /// Gets the close point detector.
        /// </summary>
        public abstract IClosePointDetector GetClosePointDetector();

        /// <summary>
        /// Gets the corner points.
        /// </summary>
        public abstract List<Point> GetCornerPoints();
    }
}
