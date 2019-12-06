namespace DualViewsDrawingModel
{
    public class Model
    {
        public CanvasDrawer.CanvasRefreshDrawRequestedEventHandler CanvasRefreshDrawRequested
        {
            get
            {
                return _canvasManager.CanvasRefreshDrawRequested;
            }
            set
            {
                _canvasManager.CanvasRefreshDrawRequested = value;
            }
        }
        private CanvasManager _canvasManager;

        public Model()
        {
            _canvasManager = new CanvasManager();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize(double canvasWidth, double canvasHeight, ShapeDrawerType shapeDrawerType)
        {
            _canvasManager.Initialize(canvasWidth, canvasHeight, shapeDrawerType);
        }

        /// <summary>
        /// Sets the size of the canvas.
        /// </summary>
        public void SetCanvasSize(double canvasWidth, double canvasHeight)
        {
            _canvasManager.SetCanvasSize(canvasWidth, canvasHeight);
        }

        /// <summary>
        /// Sets the type of the current shape drawer.
        /// </summary>
        public void SetCurrentShapeDrawerType(ShapeDrawerType shapeDrawerType)
        {
            _canvasManager.SetCurrentShapeDrawerType(shapeDrawerType);
        }

        /// <summary>
        /// Clears the canvas.
        /// </summary>
        public void ClearCanvas()
        {
            _canvasManager.ClearCanvas();
        }

        /// <summary>
        /// Handles the canvas left mouse pressed.
        /// </summary>
        public void HandleCanvasLeftMousePressed(Point mousePosition)
        {
            _canvasManager.HandleCanvasLeftMousePressed(mousePosition);
        }

        /// <summary>
        /// Handles the canvas left mouse moved.
        /// </summary>
        public void HandleCanvasLeftMouseMoved(Point mousePosition)
        {
            _canvasManager.HandleCanvasLeftMouseMoved(mousePosition);
        }

        /// <summary>
        /// Handles the canvas left mouse released.
        /// </summary>
        public void HandleCanvasLeftMouseReleased(Point mousePosition)
        {
            _canvasManager.HandleCanvasLeftMouseReleased(mousePosition);
        }

        /// <summary>
        /// Redraw the canvas.
        /// </summary>
        public void RefreshDrawCanvas(IGraphics graphics)
        {
            _canvasManager.RefreshDrawCanvas(graphics);
        }

        /// <summary>
        /// Gets the width of the canvas.
        /// </summary>
        public double GetCanvasWidth()
        {
            return _canvasManager.GetCanvasWidth();
        }

        /// <summary>
        /// Gets the height of the canvas.
        /// </summary>
        public double GetCanvasHeight()
        {
            return _canvasManager.GetCanvasHeight();
        }
    }
}
