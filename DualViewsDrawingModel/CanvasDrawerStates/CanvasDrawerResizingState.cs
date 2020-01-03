using DualViewsDrawingModel.ShapeDrawers;
using DualViewsDrawingModel.Shapes;
using System;

namespace DualViewsDrawingModel.CanvasDrawerStates
{
    public class CanvasDrawerResizingState : ICanvasDrawerState
    {
        private const string ERROR_CURRENT_RESIZING_SHAPE_SHAPE_DRAWER_IS_NULL = "The given current resizing shape's shape drawer is null.";
        private const string ERROR_CLEAR_CANVAS_IS_CALLED_IN_CANVAS_DRAWER_RESIZING_STATE = "The function ClearCanvas() is called in Canvas Drawer's Resizing State.";
        private const string ERROR_PREVIOUS_RESIZE_HAS_NOT_ENDED = "Cannot begin a new resize when the previous resize has not ended.";
        private CanvasDrawer _canvasDrawer;
        private ShapeDrawer _currentResizingShapeShapeDrawer;
        private Point _currentResizingShapeOldDrawingEndingPoint;

        public CanvasDrawerResizingState(CanvasDrawer canvasDrawerData, ShapeDrawer currentResizingShapeShapeDrawerData)
        {
            if ( canvasDrawerData == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_CANVAS_DRAWER_IS_NULL);
            }
            if ( currentResizingShapeShapeDrawerData == null )
            {
                throw new ArgumentNullException(ERROR_CURRENT_RESIZING_SHAPE_SHAPE_DRAWER_IS_NULL);
            }
            _canvasDrawer = canvasDrawerData;
            _currentResizingShapeShapeDrawer = currentResizingShapeShapeDrawerData;
            _currentResizingShapeOldDrawingEndingPoint = _currentResizingShapeShapeDrawer.DrawingEndingPoint;
        }

        /// <summary>
        /// Clears the canvas.
        /// </summary>
        public void ClearCanvas()
        {
            throw new InvalidOperationException(ERROR_CLEAR_CANVAS_IS_CALLED_IN_CANVAS_DRAWER_RESIZING_STATE);
        }

        /// <summary>
        /// Handles the canvas left mouse pressed.
        /// </summary>
        public void HandleCanvasLeftMousePressed(Point mousePosition)
        {
            throw new InvalidOperationException(ERROR_PREVIOUS_RESIZE_HAS_NOT_ENDED);
        }

        /// <summary>
        /// Handles the canvas left mouse moved.
        /// </summary>
        public void HandleCanvasLeftMouseMoved(Point mousePosition)
        {
            UpdateCurrentResizingShape(mousePosition);
        }

        /// <summary>
        /// Updates the current resizing shape.
        /// </summary>
        private void UpdateCurrentResizingShape(Point mousePosition)
        {
            if ( mousePosition == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_MOUSE_POSITION_IS_NULL);
            }
            _currentResizingShapeShapeDrawer.DrawingEndingPoint = mousePosition;
            _canvasDrawer.NotifyCanvasRefreshDrawRequested();
        }

        /// <summary>
        /// Handles the canvas left mouse released.
        /// </summary>
        public void HandleCanvasLeftMouseReleased(Point mousePosition)
        {
            _canvasDrawer.CreateThenExecuteResizingCommand(_currentResizingShapeShapeDrawer, _currentResizingShapeOldDrawingEndingPoint, _currentResizingShapeShapeDrawer.DrawingEndingPoint);
            _canvasDrawer.SetCurrentState(new CanvasDrawerPointerState(_canvasDrawer));
            _canvasDrawer.NotifyCurrentShapeChanged(); // Only notify after `CanvasDrawerPointerState` is completely created.
        }

        /// <summary>
        /// Draws the specified graphics.
        /// </summary>
        public void Draw(IGraphics graphics)
        {
            _currentResizingShapeShapeDrawer.Draw(graphics);
        }

        /// <summary>
        /// Gets the current shape rectangle.
        /// </summary>
        public Rectangle GetCurrentShapeRectangle()
        {
            return null;
        }

        /// <summary>
        /// Gets the type of the current shape.
        /// </summary>
        public ShapeDrawerType GetCurrentShapeType()
        {
            return ShapeDrawerType.None;
        }
    }
}
