using DualViewsDrawingModel.Shapes;
using System;
using System.Collections.Generic;

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
        /// Draws the selection border.
        /// </summary>
        public override void DrawSelectionBorder(IGraphics graphics)
        {
            if ( graphics == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_GRAPHICS_IS_NULL);
            }
            graphics.DrawSelectionBorder(GetLine());
        }

        /// <summary>
        /// Gets the close point detector.
        /// </summary>
        public override IClosePointDetector GetClosePointDetector()
        {
            return GetLine();
        }

        /// <summary>
        /// Gets the corner points.
        /// </summary>
        public override List<Point> GetCornerPoints()
        {
            List<Point> cornerPoints = new List<Point>();
            cornerPoints.Add(_drawingStartingPoint);
            cornerPoints.Add(_drawingEndingPoint);
            return cornerPoints;
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
