using DualViewsDrawingModel;
using System;
using System.Drawing;

namespace DualViewsDrawingWindowsFormsApplication.Views.Utilities
{
    public class DrawingFormGraphicsAdapter : IGraphics
    {
        private Graphics _graphics;

        public DrawingFormGraphicsAdapter(Graphics graphicsData)
        {
            if ( graphicsData == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_GRAPHICS_IS_NULL);
            }
            _graphics = graphicsData;
        }

        /// <summary>
        /// Clears all.
        /// </summary>
        public void ClearAll()
        {
            /* The canvas is automatically cleared when OnPaint() is invoked. */
        }

        /// <summary>
        /// Draws the specified line.
        /// </summary>
        public void Draw(DualViewsDrawingModel.Shapes.Line line)
        {
            _graphics.DrawLine(Pens.Black, ( float )line.X1, ( float )line.Y1, ( float )line.X2, ( float )line.Y2);
        }

        /// <summary>
        /// Draws the specified rectangle.
        /// </summary>
        public void Draw(DualViewsDrawingModel.Shapes.Rectangle rectangle)
        {
            _graphics.DrawRectangle(Pens.Black, ( float )rectangle.X, ( float )rectangle.Y, ( float )rectangle.Width, ( float )rectangle.Height);
        }
    }
}
