﻿using System;

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
    }
}
