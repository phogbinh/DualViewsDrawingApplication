﻿using DualViewsDrawingModel;
using DualViewsDrawingModel.ShapeDrawers;
using DualViewsDrawingModel.Shapes;

namespace DualViewsDrawingModelTest.Mocks
{
    public class ShapeDrawerMock : ShapeDrawer
    {
        public bool IsCalledDraw
        {
            get; set;
        }
        public bool IsCalledGetRectangle
        {
            get; set;
        }

        public ShapeDrawerMock(Point drawingStartingPointData, Point drawingEndingPointData) : base(drawingStartingPointData, drawingEndingPointData)
        {
            IsCalledDraw = false;
            IsCalledGetRectangle = false;
        }

        /// <summary>
        /// Draws the specified graphics.
        /// </summary>
        public override void Draw(IGraphics graphics)
        {
            IsCalledDraw = true;
        }

        /// <summary>
        /// Gets the close point detector.
        /// </summary>
        public override IClosePointDetector GetClosePointDetector()
        {
            return null;
        }

        /// <summary>
        /// Gets the rectangle.
        /// </summary>
        public override Rectangle GetRectangle()
        {
            IsCalledGetRectangle = true;
            return base.GetRectangle();
        }
    }
}
