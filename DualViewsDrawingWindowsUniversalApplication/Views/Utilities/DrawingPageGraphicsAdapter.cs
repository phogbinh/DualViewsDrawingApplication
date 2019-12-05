using DualViewsDrawingModel;
using System;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace DualViewsDrawingWindowsUniversalApplication.Views.Utilities
{
    public class DrawingPageGraphicsAdapter : IGraphics
    {
        private const string ERROR_CANVAS_IS_NULL = "The given canvas is null.";
        private Canvas _canvas;

        public DrawingPageGraphicsAdapter(Canvas canvasData)
        {
            if ( canvasData == null )
            {
                throw new ArgumentNullException(ERROR_CANVAS_IS_NULL);
            }
            _canvas = canvasData;
        }

        /// <summary>
        /// Clears all.
        /// </summary>
        public void ClearAll()
        {
            _canvas.Children.Clear();
        }

        /// <summary>
        /// Draws the specified line.
        /// </summary>
        public void Draw(DualViewsDrawingModel.Shapes.Line line)
        {
            Windows.UI.Xaml.Shapes.Line drawingPageCanvasLine = new Windows.UI.Xaml.Shapes.Line();
            drawingPageCanvasLine.X1 = line.X1;
            drawingPageCanvasLine.Y1 = line.Y1;
            drawingPageCanvasLine.X2 = line.X2;
            drawingPageCanvasLine.Y2 = line.Y2;
            drawingPageCanvasLine.Stroke = new SolidColorBrush(Colors.Black);
            _canvas.Children.Add(drawingPageCanvasLine);
        }

        /// <summary>
        /// Draws the specified rectangle.
        /// </summary>
        public void Draw(DualViewsDrawingModel.Shapes.Rectangle rectangle)
        {
            Windows.UI.Xaml.Shapes.Rectangle drawingPageCanvasRectangle = new Windows.UI.Xaml.Shapes.Rectangle();
            drawingPageCanvasRectangle.Width = rectangle.Width;
            drawingPageCanvasRectangle.Height = rectangle.Height;
            drawingPageCanvasRectangle.Stroke = new SolidColorBrush(Colors.Black);
            _canvas.Children.Add(drawingPageCanvasRectangle);
            Canvas.SetLeft(drawingPageCanvasRectangle, rectangle.X);
            Canvas.SetTop(drawingPageCanvasRectangle, rectangle.Y);
        }
    }
}
