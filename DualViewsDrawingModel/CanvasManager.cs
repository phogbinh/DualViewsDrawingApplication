using System;

namespace DualViewsDrawingModel
{
    public class CanvasManager
    {
        public CanvasDrawer.CanvasRefreshDrawRequestedEventHandler CanvasRefreshDrawRequested
        {
            get
            {
                return _canvasDrawer.CanvasRefreshDrawRequested;
            }
            set
            {
                _canvasDrawer.CanvasRefreshDrawRequested = value;
            }
        }
        private const string ERROR_CANVAS_WIDTH_IS_NOT_POSITIVE = "The given canvas width is not positive.";
        private const string ERROR_CANVAS_HEIGHT_IS_NOT_POSITIVE = "The given canvas height is not positive.";
        private const string ERROR_POINT_IS_NULL = "The given point is null.";
        private const string ERROR_MOUSE_POSITION_IS_NOT_INCLUSIVE_IN_CANVAS = "The given mouse position is not inclusively inside the canvas.";
        private double _canvasWidth;
        private double _canvasHeight;
        private CanvasDrawer _canvasDrawer;

        public CanvasManager()
        {
            _canvasDrawer = new CanvasDrawer();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize(double canvasWidth, double canvasHeight, ShapeDrawerType shapeDrawerType)
        {
            SetCanvasSize(canvasWidth, canvasHeight);
            _canvasDrawer.Initialize(shapeDrawerType);
        }

        /// <summary>
        /// Sets the size of the canvas.
        /// </summary>
        public void SetCanvasSize(double canvasWidth, double canvasHeight)
        {
            if ( canvasWidth <= 0 )
            {
                throw new ArgumentException(ERROR_CANVAS_WIDTH_IS_NOT_POSITIVE);
            }
            if ( canvasHeight <= 0 )
            {
                throw new ArgumentException(ERROR_CANVAS_HEIGHT_IS_NOT_POSITIVE);
            }
            _canvasWidth = canvasWidth;
            _canvasHeight = canvasHeight;
        }

        /// <summary>
        /// Sets the type of the current drawing shape.
        /// </summary>
        public void SetCurrentShapeDrawerType(ShapeDrawerType drawingShapeType)
        {
            _canvasDrawer.SetCurrentShapeDrawerType(drawingShapeType);
        }

        /// <summary>
        /// Clears the canvas.
        /// </summary>
        public void ClearCanvas()
        {
            _canvasDrawer.ClearCanvas();
        }

        /// <summary>
        /// Handles the canvas left mouse pressed.
        /// </summary>
        public void HandleCanvasLeftMousePressed(Point mousePosition)
        {
            if ( !IsInclusiveInCanvas(mousePosition) )
            {
                throw new ArgumentException(ERROR_MOUSE_POSITION_IS_NOT_INCLUSIVE_IN_CANVAS);
            }
            _canvasDrawer.HandleCanvasLeftMousePressed(mousePosition);
        }

        /// <summary>
        /// Determines whether [is inclusively in canvas] [the specified point].
        /// </summary>
        private bool IsInclusiveInCanvas(Point point)
        {
            if ( point == null )
            {
                throw new ArgumentNullException(ERROR_POINT_IS_NULL);
            }
            return point.IsInclusiveInRegion(0, _canvasWidth, 0, _canvasHeight);
        }

        /// <summary>
        /// Handles the canvas left mouse moved.
        /// </summary>
        public void HandleCanvasLeftMouseMoved(Point mousePosition)
        {
            if ( !IsInclusiveInCanvas(mousePosition) )
            {
                throw new ArgumentException(ERROR_MOUSE_POSITION_IS_NOT_INCLUSIVE_IN_CANVAS);
            }
            _canvasDrawer.HandleCanvasLeftMouseMoved(mousePosition);
        }

        /// <summary>
        /// Handles the canvas left mouse released.
        /// </summary>
        public void HandleCanvasLeftMouseReleased(Point mousePosition)
        {
            if ( !IsInclusiveInCanvas(mousePosition) )
            {
                throw new ArgumentException(ERROR_MOUSE_POSITION_IS_NOT_INCLUSIVE_IN_CANVAS);
            }
            _canvasDrawer.HandleCanvasLeftMouseReleased(mousePosition);
        }

        /// <summary>
        /// Handles the canvas left mouse action.
        /// </summary>
        private void HandleCanvasLeftMouseAction(Point mousePosition, Action<Point> canvasDrawerHandleCanvasLeftMouseAction)
        {
            if ( !IsInclusiveInCanvas(mousePosition) )
            {
                throw new ArgumentException(ERROR_MOUSE_POSITION_IS_NOT_INCLUSIVE_IN_CANVAS);
            }
            canvasDrawerHandleCanvasLeftMouseAction(mousePosition);
        }

        /// <summary>
        /// Redraw the canvas.
        /// </summary>
        public void RefreshDrawCanvas(IGraphics graphics)
        {
            _canvasDrawer.RefreshDrawCanvas(graphics);
        }

        /// <summary>
        /// Gets the width of the canvas.
        /// </summary>
        public double GetCanvasWidth()
        {
            return _canvasWidth;
        }

        /// <summary>
        /// Gets the height of the canvas.
        /// </summary>
        public double GetCanvasHeight()
        {
            return _canvasHeight;
        }
    }
}
