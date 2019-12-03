namespace DualViewsDrawingModel
{
    public class Model
    {
        public CanvasManager.CanvasRefreshDrawRequestedEventHandler CanvasRefreshDrawRequested
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
        /// Handles the canvas mouse pressed.
        /// </summary>
        public void HandleCanvasMousePressed(Point mousePosition)
        {
            _canvasManager.HandleCanvasMousePressed(mousePosition);
        }

        /// <summary>
        /// Handles the canvas mouse moved.
        /// </summary>
        public void HandleCanvasMouseMoved(Point mousePosition)
        {
            _canvasManager.HandleCanvasMouseMoved(mousePosition);
        }

        /// <summary>
        /// Handles the canvas mouse released.
        /// </summary>
        public void HandleCanvasMouseReleased(Point mousePosition)
        {
            _canvasManager.HandleCanvasMouseReleased(mousePosition);
        }

        /// <summary>
        /// Redraw the canvas.
        /// </summary>
        public void RefreshDrawCanvas(IGraphics graphics)
        {
            _canvasManager.RefreshDrawCanvas(graphics);
        }
    }
}
