using DualViewsDrawingModel.CanvasDrawerStates;
using DualViewsDrawingModel.Commands;
using DualViewsDrawingModel.ShapeDrawers;
using DualViewsDrawingModel.Shapes;
using System;

namespace DualViewsDrawingModel
{
    public class CanvasDrawer : IDrawingCommandAgent, IResizingCommandAgent
    {
        public delegate void CanvasRefreshDrawRequestedEventHandler();
        public delegate void DrawingEndedEventHandler();
        public delegate void CurrentShapeChangedEventHandler();
        public CanvasRefreshDrawRequestedEventHandler CanvasRefreshDrawRequested
        {
            get; set;
        }
        public DrawingEndedEventHandler DrawingEnded
        {
            get; set;
        }
        public CurrentShapeChangedEventHandler CurrentShapeChanged
        {
            get; set;
        }
        public ShapeDrawerType CurrentShapeDrawerType
        {
            get
            {
                return _currentShapeDrawerType;
            }
        }
        private const string ERROR_CANVAS_DRAWER_STATE_IS_NULL = "The given canvas drawer state is null.";
        private CommandsManager _commandsManager;
        protected ShapeDrawerType _currentShapeDrawerType;
        private ICanvasDrawerState _currentState;
        private CanvasShapeDrawersHelper _canvasShapeDrawersHelper;

        public CanvasDrawer(CommandsManager commandsManagerData)
        {
            if ( commandsManagerData == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_COMMANDS_MANAGER_IS_NULL);
            }
            _commandsManager = commandsManagerData;
            _canvasShapeDrawersHelper = new CanvasShapeDrawersHelper();
            this.CurrentShapeChanged += NotifyCanvasRefreshDrawRequested;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public virtual void Initialize(ShapeDrawerType shapeDrawerType)
        {
            SetCurrentShapeDrawerType(shapeDrawerType);
            SetCurrentState(new CanvasDrawerPointerState(this));
            NotifyCurrentShapeChanged(); // Only notify after `CanvasDrawerPointerState` is completely created.
            ClearShapeDrawersManager();
        }

        /// <summary>
        /// Clears the shape drawers manager.
        /// </summary>
        public virtual void ClearShapeDrawersManager()
        {
            _canvasShapeDrawersHelper.Clear();
        }

        /// <summary>
        /// Sets the type of the current drawing shape.
        /// </summary>
        public virtual void SetCurrentShapeDrawerType(ShapeDrawerType drawingShapeType)
        {
            if ( !ShapeDrawerTypeHelper.IsValidShapeDrawerType(drawingShapeType) )
            {
                throw new ArgumentException(Definitions.ERROR_SHAPE_DRAWER_TYPE_IS_INVALID);
            }
            _currentShapeDrawerType = drawingShapeType;
        }

        /// <summary>
        /// Clears the canvas.
        /// </summary>
        public virtual void ClearCanvas()
        {
            _currentState.ClearCanvas();
        }

        /// <summary>
        /// Handles the canvas left mouse pressed.
        /// </summary>
        public virtual void HandleCanvasLeftMousePressed(Point mousePosition)
        {
            _currentState.HandleCanvasLeftMousePressed(mousePosition);
        }

        /// <summary>
        /// Handles the canvas left mouse moved.
        /// </summary>
        public virtual void HandleCanvasLeftMouseMoved(Point mousePosition)
        {
            _currentState.HandleCanvasLeftMouseMoved(mousePosition);
        }

        /// <summary>
        /// Handles the canvas left mouse released.
        /// </summary>
        public virtual void HandleCanvasLeftMouseReleased(Point mousePosition)
        {
            _currentState.HandleCanvasLeftMouseReleased(mousePosition);
        }

        /// <summary>
        /// Redraw the canvas.
        /// </summary>
        public virtual void RefreshDrawCanvas(IGraphics graphics)
        {
            if ( graphics == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_GRAPHICS_IS_NULL);
            }
            graphics.ClearAll();
            Draw(graphics);
        }

        /// <summary>
        /// Draws the specified graphics.
        /// </summary>
        private void Draw(IGraphics graphics)
        {
            _canvasShapeDrawersHelper.Draw(graphics);
            _currentState.Draw(graphics);
        }

        /// <summary>
        /// Sets the state of the current.
        /// </summary>
        public virtual void SetCurrentState(ICanvasDrawerState value)
        {
            if ( value == null )
            {
                throw new ArgumentNullException(ERROR_CANVAS_DRAWER_STATE_IS_NULL);
            }
            _currentState = value;
        }

        /// <summary>
        /// Notifies the canvas refresh draw requested.
        /// </summary>
        public virtual void NotifyCanvasRefreshDrawRequested()
        {
            if ( CanvasRefreshDrawRequested != null )
            {
                CanvasRefreshDrawRequested();
            }
        }

        /// <summary>
        /// Notifies the drawing ended.
        /// </summary>
        public virtual void NotifyDrawingEnded()
        {
            if ( DrawingEnded != null )
            {
                DrawingEnded();
            }
        }

        /// <summary>
        /// Draws the shape.
        /// </summary>
        public void DrawShape(ShapeDrawer shapeDrawer)
        {
            _canvasShapeDrawersHelper.AddShapeDrawer(shapeDrawer);
            NotifyCanvasRefreshDrawRequested();
        }

        /// <summary>
        /// Removes the shape.
        /// </summary>
        public void RemoveShape(ShapeDrawer shapeDrawer)
        {
            _canvasShapeDrawersHelper.RemoveShapeDrawer(shapeDrawer);
            NotifyCanvasRefreshDrawRequested();
        }

        /// <summary>
        /// Resizes the shape.
        /// </summary>
        public void ResizeShape(ShapeDrawer shapeDrawer, Point drawingEndingPoint)
        {
            shapeDrawer.DrawingEndingPoint = drawingEndingPoint;
            NotifyCanvasRefreshDrawRequested();
        }

        /// <summary>
        /// Creates then executes the drawing command to draw shape using current shape drawer.
        /// </summary>
        public virtual void CreateThenExecuteDrawingCommandToDrawShapeUsingCurrentShapeDrawer(Point drawingStartingPoint, Point drawingEndingPoint)
        {
            _commandsManager.AddThenExecuteCommand(new DrawingCommand(this, drawingStartingPoint, drawingEndingPoint, _currentShapeDrawerType));
        }

        /// <summary>
        /// Creates the then execute resizing command.
        /// </summary>
        public virtual void CreateThenExecuteResizingCommand(ShapeDrawer shapeDrawer, Point oldDrawingEndingPoint, Point newDrawingEndingPoint)
        {
            _commandsManager.AddThenExecuteCommand(new ResizingCommand(this, shapeDrawer, oldDrawingEndingPoint, newDrawingEndingPoint));
        }

        /// <summary>
        /// Gets the selected shape shape drawer.
        /// </summary>
        public virtual ShapeDrawer GetSelectedShapeShapeDrawer(Point leftMousePressedPosition)
        {
            return _canvasShapeDrawersHelper.GetMostRecentDrawShapeDrawerThatIsCloseToPoint(leftMousePressedPosition, Definitions.MOUSE_POSITION_TO_SELECTION_SHAPE_MAXIMUM_DISTANCE_SQUARED);
        }

        /// <summary>
        /// Gets the selected resizing shape drawer.
        /// </summary>
        public virtual ShapeDrawer GetSelectedResizingShapeDrawer(Point leftMousePressedPosition)
        {
            return _canvasShapeDrawersHelper.GetMostRecentDrawShapeDrawerWhoseDrawingEndingPointIsCloseToPoint(leftMousePressedPosition, Definitions.MOUSE_POSITION_TO_SELECTION_SHAPE_MAXIMUM_DISTANCE_SQUARED);
        }

        /// <summary>
        /// Notifies the current shape changed.
        /// </summary>
        public virtual void NotifyCurrentShapeChanged()
        {
            if ( CurrentShapeChanged != null )
            {
                CurrentShapeChanged();
            }
        }

        /// <summary>
        /// Gets the current shape rectangle.
        /// </summary>
        public virtual Rectangle GetCurrentShapeRectangle()
        {
            return _currentState.GetCurrentShapeRectangle();
        }

        /// <summary>
        /// Gets the type of the current shape.
        /// </summary>
        public virtual ShapeDrawerType GetCurrentShapeType()
        {
            return _currentState.GetCurrentShapeType();
        }
    }
}
