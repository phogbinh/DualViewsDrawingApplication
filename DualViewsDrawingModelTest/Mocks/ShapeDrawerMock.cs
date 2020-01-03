using DualViewsDrawingModel;
using DualViewsDrawingModel.ShapeDrawers;
using DualViewsDrawingModel.Shapes;
using System.Collections.Generic;

namespace DualViewsDrawingModelTest.Mocks
{
    public class ShapeDrawerMock : ShapeDrawer
    {
        public bool IsCalledGetRectangle
        {
            get; set;
        }
        public bool IsCalledDraw
        {
            get; set;
        }

        public ShapeDrawerMock(Point drawingStartingPointData, Point drawingEndingPointData) : base(drawingStartingPointData, drawingEndingPointData)
        {
            IsCalledGetRectangle = false;
            IsCalledDraw = false;
        }

        /// <summary>
        /// Gets the rectangle.
        /// </summary>
        public override Rectangle GetRectangle()
        {
            IsCalledGetRectangle = true;
            return base.GetRectangle();
        }

        /// <summary>
        /// Draws the specified graphics.
        /// </summary>
        public override void Draw(IGraphics graphics)
        {
            IsCalledDraw = true;
        }

        /// <summary>
        /// Draws the selection border.
        /// </summary>
        public override void DrawSelectionBorder(IGraphics graphics)
        {
            /* Body intentionally empty */
        }

        /// <summary>
        /// Gets the close point detector.
        /// </summary>
        public override IClosePointDetector GetClosePointDetector()
        {
            return null;
        }

        /// <summary>
        /// Gets the corner points.
        /// </summary>
        public override List<Point> GetCornerPoints()
        {
            return null;
        }
    }
}
