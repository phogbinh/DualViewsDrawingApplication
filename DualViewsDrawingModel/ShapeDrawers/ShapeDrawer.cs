using DualViewsDrawingModel.Shapes;
using System;

namespace DualViewsDrawingModel.ShapeDrawers
{
    public abstract class ShapeDrawer
    {
        public Point DrawingEndingPoint
        {
            set
            {
                _drawingEndingPoint = value;
            }
        }
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
            _drawingStartingPoint = drawingStartingPointData;
            _drawingEndingPoint = drawingEndingPointData;
        }

        /// <summary>
        /// Determines whether [is including point].
        /// </summary>
        public bool IsIncludingPoint(Point point)
        {
            return GetRectangle().IsIncludingPoint(point);
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
    }
}
