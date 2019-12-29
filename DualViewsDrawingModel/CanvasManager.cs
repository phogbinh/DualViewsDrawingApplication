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
        public CanvasDrawer.DrawingEndedEventHandler DrawingEnded
        {
            get
            {
                return _canvasDrawer.DrawingEnded;
            }
            set
            {
                _canvasDrawer.DrawingEnded = value;
            }
        }
        public double CanvasWidth
        {
            get
            {
                return _canvasWidth;
            }
        }
        public double CanvasHeight
        {
            get
            {
                return _canvasHeight;
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
        public virtual void Initialize(double canvasWidth, double canvasHeight, ShapeDrawerType shapeDrawerType)
        {
            SetCanvasSize(canvasWidth, canvasHeight);
            _canvasDrawer.Initialize(shapeDrawerType);
        }

        /// <summary>
        /// Sets the size of the canvas.
        /// </summary>
        public virtual void SetCanvasSize(double canvasWidth, double canvasHeight)
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
        public virtual void SetCurrentShapeDrawerType(ShapeDrawerType drawingShapeType)
        {
            _canvasDrawer.SetCurrentShapeDrawerType(drawingShapeType);
        }

        /// <summary>
        /// Clears the canvas.
        /// </summary>
        public virtual void ClearCanvas()
        {
            _canvasDrawer.ClearCanvas();
        }

        /// <summary>
        /// Handles the canvas left mouse pressed.
        /// </summary>
        public virtual void HandleCanvasLeftMousePressed(Point mousePosition)
        {
            HandleCanvasLeftMouseAction(mousePosition, (mousePositionParameter) => _canvasDrawer.HandleCanvasLeftMousePressed(mousePositionParameter));
        }

        /// <summary>
        /// Handles the canvas left mouse moved.
        /// </summary>
        public virtual void HandleCanvasLeftMouseMoved(Point mousePosition)
        {
            HandleCanvasLeftMouseAction(mousePosition, (mousePositionParameter) => _canvasDrawer.HandleCanvasLeftMouseMoved(mousePositionParameter));
        }

        /// <summary>
        /// Handles the canvas left mouse released.
        /// </summary>
        public virtual void HandleCanvasLeftMouseReleased(Point mousePosition)
        {
            HandleCanvasLeftMouseAction(mousePosition, (mousePositionParameter) => _canvasDrawer.HandleCanvasLeftMouseReleased(mousePositionParameter));
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
        /// Redraw the canvas.
        /// </summary>
        public virtual void RefreshDrawCanvas(IGraphics graphics)
        {
            _canvasDrawer.RefreshDrawCanvas(graphics);
        }
    }
}
