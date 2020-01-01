using DualViewsDrawingModel;
using DualViewsDrawingModel.CanvasDrawerStates;
using DualViewsDrawingModel.ShapeDrawers;

namespace DualViewsDrawingModelTest.Mocks
{
    public class CanvasDrawerMock : CanvasDrawer
    {
        public bool IsCalledInitialize
        {
            get; set;
        }
        public bool IsCalledClearShapeDrawersManager
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
        public bool IsCalledSetCurrentState
        {
            get; set;
        }
        public bool IsCalledAddCurrentShapeDrawer
        {
            get; set;
        }
        public bool IsCalledNotifyCanvasRefreshDrawRequested
        {
            get; set;
        }
        public bool IsCalledNotifyDrawingEnded
        {
            get; set;
        }
        public bool IsCalledDrawShape
        {
            get; set;
        }
        public bool IsCalledRemoveShape
        {
            get; set;
        }
        public ICanvasDrawerState CurrentState
        {
            get; set;
        }

        public CanvasDrawerMock(CommandsManager commandsManagerData) : base(commandsManagerData)
        {
            IsCalledInitialize = false;
            IsCalledClearShapeDrawersManager = false;
            IsCalledSetCurrentShapeDrawerType = false;
            IsCalledClearCanvas = false;
            IsCalledHandleCanvasLeftMousePressed = false;
            IsCalledHandleCanvasLeftMouseMoved = false;
            IsCalledHandleCanvasLeftMouseReleased = false;
            IsCalledRefreshDrawCanvas = false;
            IsCalledSetCurrentState = false;
            IsCalledAddCurrentShapeDrawer = false;
            IsCalledNotifyCanvasRefreshDrawRequested = false;
            IsCalledNotifyDrawingEnded = false;
            IsCalledDrawShape = false;
            IsCalledRemoveShape = false;
            CurrentState = null;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize(ShapeDrawerType shapeDrawerType)
        {
            IsCalledInitialize = true;
        }

        /// <summary>
        /// Clears the shape drawers manager.
        /// </summary>
        public override void ClearShapeDrawersManager()
        {
            IsCalledClearShapeDrawersManager = true;
        }

        /// <summary>
        /// Sets the type of the current drawing shape.
        /// </summary>
        public override void SetCurrentShapeDrawerType(ShapeDrawerType drawingShapeType)
        {
            IsCalledSetCurrentShapeDrawerType = true;
            _currentShapeDrawerType = drawingShapeType;
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

        /// <summary>
        /// Sets the state of the current.
        /// </summary>
        public override void SetCurrentState(ICanvasDrawerState value)
        {
            IsCalledSetCurrentState = true;
            CurrentState = value;
        }

        /// <summary>
        /// Adds the current shape drawer.
        /// </summary>
        public override void AddCurrentShapeDrawer(Point drawingStartingPoint, Point drawingEndingPoint)
        {
            IsCalledAddCurrentShapeDrawer = true;
        }

        /// <summary>
        /// Notifies the canvas refresh draw requested.
        /// </summary>
        public override void NotifyCanvasRefreshDrawRequested()
        {
            IsCalledNotifyCanvasRefreshDrawRequested = true;
        }

        /// <summary>
        /// Notifies the canvas refresh draw requested.
        /// </summary>
        public override void NotifyDrawingEnded()
        {
            IsCalledNotifyDrawingEnded = true;
        }

        /// <summary>
        /// Draws the shape.
        /// </summary>
        public override void DrawShape(ShapeDrawer shapeDrawer)
        {
            IsCalledDrawShape = true;
        }

        /// <summary>
        /// Removes the shape.
        /// </summary>
        public override void RemoveShape(ShapeDrawer shapeDrawer)
        {
            IsCalledRemoveShape = true;
        }
    }
}
