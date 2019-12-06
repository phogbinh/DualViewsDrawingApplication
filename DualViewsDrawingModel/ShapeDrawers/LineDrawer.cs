using DualViewsDrawingModel.Shapes;
using System;

namespace DualViewsDrawingModel.ShapeDrawers
{
    public class LineDrawer : ShapeDrawer
    {
        public LineDrawer(Point drawingStartingPointData, Point drawingEndingPointData) : base(drawingStartingPointData, drawingEndingPointData)
        {
            /* Body intentionally empty */
        }

        /// <summary>
        /// Draws the specified graphics.
        /// </summary>
        public override void Draw(IGraphics graphics)
        {
            if ( graphics == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_GRAPHICS_IS_NULL);
            }
            graphics.Draw(GetLine());
        }

        /// <summary>
        /// Gets the line.
        /// </summary>
        private Line GetLine()
        {
            return new Line(_drawingStartingPoint, _drawingEndingPoint);
        }
    }
}
