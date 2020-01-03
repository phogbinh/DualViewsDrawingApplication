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
            _graphics.FillRectangle(Brushes.SkyBlue, ( float )rectangle.X, ( float )rectangle.Y, ( float )rectangle.Width, ( float )rectangle.Height);
            _graphics.DrawRectangle(Pens.Black, ( float )rectangle.X, ( float )rectangle.Y, ( float )rectangle.Width, ( float )rectangle.Height);
        }

        /// <summary>
        /// Draws the selection border.
        /// </summary>
        public void DrawSelectionBorder(DualViewsDrawingModel.Shapes.Line line)
        {
            _graphics.DrawLine(GetSelectionBorderDashPen(), ( float )line.X1, ( float )line.Y1, ( float )line.X2, ( float )line.Y2);
        }

        /// <summary>
        /// Draws the selection border.
        /// </summary>
        public void DrawSelectionBorder(DualViewsDrawingModel.Shapes.Rectangle rectangle)
        {
            _graphics.DrawRectangle(GetSelectionBorderDashPen(), ( float )rectangle.X, ( float )rectangle.Y, ( float )rectangle.Width, ( float )rectangle.Height);
        }

        /// <summary>
        /// Gets the selection border dash pen.
        /// </summary>
        private Pen GetSelectionBorderDashPen()
        {
            Pen pen = new Pen(Color.Red);
            pen.DashPattern = new float[] { ( float )Definitions.SELECTION_BORDER_DASH_PATTERN_FIRST_VALUE, ( float )Definitions.SELECTION_BORDER_DASH_PATTERN_SECOND_VALUE };
            return pen;
        }

        /// <summary>
        /// Draws the selection corner.
        /// </summary>
        public void DrawSelectionCorner(DualViewsDrawingModel.Point point)
        {
            RectangleF rectangle = new RectangleF(( float )point.X - ( float )Definitions.SELECTION_CORNER_RADIUS, ( float )point.Y - ( float )Definitions.SELECTION_CORNER_RADIUS, ( float )Definitions.SELECTION_CORNER_DOUBLE_RADIUS, ( float )Definitions.SELECTION_CORNER_DOUBLE_RADIUS);
            _graphics.DrawEllipse(Pens.Red, rectangle);
            _graphics.FillEllipse(Brushes.White, rectangle);
        }
    }
}
