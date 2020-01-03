using DualViewsDrawingModel.Shapes;
using System;

namespace DualViewsDrawingModel.ShapeDrawers
{
    public class LineDrawer : ShapeDrawer
    {
        public LineDrawer(Point drawingStartingPointData, Point drawingEndingPointData) : base(drawingStartingPointData, drawingEndingPointData)
        {
            _type = ShapeDrawerType.Line;
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
        /// Gets the close point detector.
        /// </summary>
        public override IClosePointDetector GetClosePointDetector()
        {
            return GetLine();
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
