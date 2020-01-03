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
            drawingPageCanvasRectangle.Fill = new SolidColorBrush(Colors.SkyBlue);
            _canvas.Children.Add(drawingPageCanvasRectangle);
            Canvas.SetLeft(drawingPageCanvasRectangle, rectangle.X);
            Canvas.SetTop(drawingPageCanvasRectangle, rectangle.Y);
        }

        /// <summary>
        /// Draws the selection border.
        /// </summary>
        public void DrawSelectionBorder(DualViewsDrawingModel.Shapes.Line line)
        {
            Windows.UI.Xaml.Shapes.Line drawingPageCanvasLine = new Windows.UI.Xaml.Shapes.Line();
            drawingPageCanvasLine.X1 = line.X1;
            drawingPageCanvasLine.Y1 = line.Y1;
            drawingPageCanvasLine.X2 = line.X2;
            drawingPageCanvasLine.Y2 = line.Y2;
            drawingPageCanvasLine.Stroke = new SolidColorBrush(Colors.Red);
            drawingPageCanvasLine.StrokeDashArray = GetStrokeDashArray();
            _canvas.Children.Add(drawingPageCanvasLine);
        }

        /// <summary>
        /// Draws the selection border.
        /// </summary>
        public void DrawSelectionBorder(DualViewsDrawingModel.Shapes.Rectangle rectangle)
        {
            Windows.UI.Xaml.Shapes.Rectangle drawingPageCanvasRectangle = new Windows.UI.Xaml.Shapes.Rectangle();
            drawingPageCanvasRectangle.Width = rectangle.Width;
            drawingPageCanvasRectangle.Height = rectangle.Height;
            drawingPageCanvasRectangle.Stroke = new SolidColorBrush(Colors.Red);
            drawingPageCanvasRectangle.StrokeDashArray = GetStrokeDashArray();
            _canvas.Children.Add(drawingPageCanvasRectangle);
            Canvas.SetLeft(drawingPageCanvasRectangle, rectangle.X);
            Canvas.SetTop(drawingPageCanvasRectangle, rectangle.Y);
        }

        /// <summary>
        /// Gets the stroke dash array.
        /// </summary>
        private DoubleCollection GetStrokeDashArray()
        {
            DoubleCollection strokeDashArray = new DoubleCollection();
            strokeDashArray.Add(Definitions.SELECTION_BORDER_DASH_PATTERN_FIRST_VALUE);
            strokeDashArray.Add(Definitions.SELECTION_BORDER_DASH_PATTERN_SECOND_VALUE);
            return strokeDashArray;
        }

        /// <summary>
        /// Draws the selection corner.
        /// </summary>
        public void DrawSelectionCorner(DualViewsDrawingModel.Point point)
        {
            Windows.UI.Xaml.Shapes.Ellipse drawingPageCanvasEllipse = new Windows.UI.Xaml.Shapes.Ellipse();
            drawingPageCanvasEllipse.Width = Definitions.SELECTION_CORNER_DOUBLE_RADIUS;
            drawingPageCanvasEllipse.Height = Definitions.SELECTION_CORNER_DOUBLE_RADIUS;
            drawingPageCanvasEllipse.Stroke = new SolidColorBrush(Colors.Red);
            drawingPageCanvasEllipse.Fill = new SolidColorBrush(Colors.White);
            _canvas.Children.Add(drawingPageCanvasEllipse);
            Canvas.SetLeft(drawingPageCanvasEllipse, point.X - Definitions.SELECTION_CORNER_RADIUS);
            Canvas.SetTop(drawingPageCanvasEllipse, point.Y - Definitions.SELECTION_CORNER_RADIUS);
        }
    }
}
