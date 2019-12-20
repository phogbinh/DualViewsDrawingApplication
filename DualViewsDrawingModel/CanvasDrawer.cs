using DualViewsDrawingModel.CanvasDrawerStates;
using System;

namespace DualViewsDrawingModel
{
    public class CanvasDrawer
    {
        public delegate void CanvasRefreshDrawRequestedEventHandler();
        public CanvasRefreshDrawRequestedEventHandler CanvasRefreshDrawRequested
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
        private ShapeDrawerType _currentShapeDrawerType;
        private ICanvasDrawerState _currentState;
        private ShapeDrawersManager _shapeDrawersManager;

        public CanvasDrawer()
        {
            _shapeDrawersManager = new ShapeDrawersManager();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize(ShapeDrawerType shapeDrawerType)
        {
            SetCurrentShapeDrawerType(shapeDrawerType);
            SetCurrentState(new CanvasDrawerPointerState(this));
            ClearShapeDrawersManager();
        }

        /// <summary>
        /// Clears the shape drawers manager.
        /// </summary>
        public void ClearShapeDrawersManager()
        {
            _shapeDrawersManager.Clear();
        }

        /// <summary>
        /// Sets the type of the current drawing shape.
        /// </summary>
        public void SetCurrentShapeDrawerType(ShapeDrawerType drawingShapeType)
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
        public void ClearCanvas()
        {
            _currentState.ClearCanvas();
        }

        /// <summary>
        /// Handles the canvas left mouse pressed.
        /// </summary>
        public void HandleCanvasLeftMousePressed(Point mousePosition)
        {
            _currentState.HandleCanvasLeftMousePressed(mousePosition);
        }

        /// <summary>
        /// Handles the canvas left mouse moved.
        /// </summary>
        public void HandleCanvasLeftMouseMoved(Point mousePosition)
        {
            _currentState.HandleCanvasLeftMouseMoved(mousePosition);
        }

        /// <summary>
        /// Handles the canvas left mouse released.
        /// </summary>
        public void HandleCanvasLeftMouseReleased(Point mousePosition)
        {
            _currentState.HandleCanvasLeftMouseReleased(mousePosition);
        }

        /// <summary>
        /// Redraw the canvas.
        /// </summary>
        public void RefreshDrawCanvas(IGraphics graphics)
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
            _shapeDrawersManager.Draw(graphics);
            _currentState.Draw(graphics);
        }

        /// <summary>
        /// Sets the state of the current.
        /// </summary>
        public void SetCurrentState(ICanvasDrawerState value)
        {
            if ( value == null )
            {
                throw new ArgumentNullException(ERROR_CANVAS_DRAWER_STATE_IS_NULL);
            }
            _currentState = value;
        }

        /// <summary>
        /// Adds the current shape drawer.
        /// </summary>
        public void AddCurrentShapeDrawer(Point drawingStartingPoint, Point drawingEndingPoint)
        {
            _shapeDrawersManager.AddShapeDrawer(drawingStartingPoint, drawingEndingPoint, _currentShapeDrawerType);
        }

        /// <summary>
        /// Notifies the canvas refresh draw requested.
        /// </summary>
        public void NotifyCanvasRefreshDrawRequested()
        {
            if ( CanvasRefreshDrawRequested != null )
            {
                CanvasRefreshDrawRequested();
            }
        }
    }
}
