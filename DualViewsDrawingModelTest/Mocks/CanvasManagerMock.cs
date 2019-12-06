using DualViewsDrawingModel;

namespace DualViewsDrawingModelTest.Mocks
{
    public class CanvasManagerMock : CanvasManager
    {
        public bool IsCalledInitialize
        {
            get; set;
        }
        public bool IsCalledSetCanvasSize
        {
            get; set;
        }
        public bool IsCalledSetCurrentShapeDrawerType
        {
            get; set;
        }
        public bool IsCalledClearCanvas
        {
            get; set;
        }
        public bool IsCalledHandleCanvasLeftMousePressed
        {
            get; set;
        }
        public bool IsCalledHandleCanvasLeftMouseMoved
        {
            get; set;
        }
        public bool IsCalledHandleCanvasLeftMouseReleased
        {
            get; set;
        }
        public bool IsCalledRefreshDrawCanvas
        {
            get; set;
        }

        public CanvasManagerMock() : base()
        {
            IsCalledClearCanvas = false;
            IsCalledSetCanvasSize = false;
            IsCalledSetCurrentShapeDrawerType = false;
            IsCalledClearCanvas = false;
            IsCalledHandleCanvasLeftMousePressed = false;
            IsCalledHandleCanvasLeftMouseMoved = false;
            IsCalledHandleCanvasLeftMouseReleased = false;
            IsCalledRefreshDrawCanvas = false;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize(double canvasWidth, double canvasHeight, ShapeDrawerType shapeDrawerType)
        {
            IsCalledInitialize = true;
        }

        /// <summary>
        /// Sets the size of the canvas.
        /// </summary>
        public override void SetCanvasSize(double canvasWidth, double canvasHeight)
        {
            IsCalledSetCanvasSize = true;
        }

        /// <summary>
        /// Sets the type of the current shape drawer.
        /// </summary>
        public override void SetCurrentShapeDrawerType(ShapeDrawerType shapeDrawerType)
        {
            IsCalledSetCurrentShapeDrawerType = true;
        }

        /// <summary>
        /// Clears the canvas.
        /// </summary>
        public override void ClearCanvas()
        {
            IsCalledClearCanvas = true;
        }

        /// <summary>
        /// Handles the canvas left mouse pressed.
        /// </summary>
        public override void HandleCanvasLeftMousePressed(Point mousePosition)
        {
            IsCalledHandleCanvasLeftMousePressed = true;
        }

        /// <summary>
        /// Handles the canvas left mouse moved.
        /// </summary>
        public override void HandleCanvasLeftMouseMoved(Point mousePosition)
        {
            IsCalledHandleCanvasLeftMouseMoved = true;
        }

        /// <summary>
        /// Handles the canvas left mouse released.
        /// </summary>
        public override void HandleCanvasLeftMouseReleased(Point mousePosition)
        {
            IsCalledHandleCanvasLeftMouseReleased = true;
        }

        /// <summary>
        /// Redraw the canvas.
        /// </summary>
        public override void RefreshDrawCanvas(IGraphics graphics)
        {
            IsCalledRefreshDrawCanvas = true;
        }
    }
}
