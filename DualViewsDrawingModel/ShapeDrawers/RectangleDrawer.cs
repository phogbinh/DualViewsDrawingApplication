using DualViewsDrawingModel.Shapes;
using System;
using System.Collections.Generic;

namespace DualViewsDrawingModel.ShapeDrawers
{
    public class RectangleDrawer : ShapeDrawer
    {
        public RectangleDrawer(Point drawingStartingPointData, Point drawingEndingPointData) : base(drawingStartingPointData, drawingEndingPointData)
        {
            _type = ShapeDrawerType.Rectangle;
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
            graphics.Draw(GetRectangle());
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
            graphics.DrawSelectionBorder(GetRectangle());
        }

        /// <summary>
        /// Gets the close point detector.
        /// </summary>
        public override IClosePointDetector GetClosePointDetector()
        {
            return GetRectangle();
        }

        /// <summary>
        /// Gets the corner points.
        /// </summary>
        public override List<Point> GetCornerPoints()
        {
            List<Point> cornerPoints = new List<Point>();
            Rectangle rectangle = GetRectangle();
            cornerPoints.Add(rectangle.GetUpperLeftPoint());
            cornerPoints.Add(rectangle.GetUpperRightPoint());
            cornerPoints.Add(rectangle.GetLowerRightPoint());
            cornerPoints.Add(rectangle.GetLowerLeftPoint());
            return cornerPoints;
        }
    }
}
