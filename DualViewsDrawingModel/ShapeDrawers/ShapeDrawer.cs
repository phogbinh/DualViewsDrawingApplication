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
        /// Draws the specified graphics.
        /// </summary>
        public abstract void Draw(IGraphics graphics);
    }
}
