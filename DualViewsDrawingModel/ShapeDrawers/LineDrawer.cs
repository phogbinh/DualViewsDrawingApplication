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
            graphics.Draw(CreateLine());
        }

        /// <summary>
        /// Creates the line.
        /// </summary>
        private Line CreateLine()
        {
            return new Line(_drawingStartingPoint, _drawingEndingPoint);
        }
    }
}
