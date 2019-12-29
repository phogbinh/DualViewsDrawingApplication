using DualViewsDrawingModel.ShapeDrawers;
using System;

namespace DualViewsDrawingModel.CanvasDrawerStates
{
    public class CanvasDrawerDrawingState : ICanvasDrawerState
    {
        private const string ERROR_CURRENT_DRAWING_SHAPE_DRAWING_STARTING_POINT_IS_NULL = "The given current drawing shape's drawing starting pointer is null.";
        private const string ERROR_CLEAR_CANVAS_IS_CALLED_IN_CANVAS_DRAWER_DRAWING_STATE = "The function ClearCanvas() is called in Canvas Drawer's Drawing State.";
        private const string ERROR_PREVIOUS_DRAW_HAS_NOT_ENDED = "Cannot begin a new draw when the previous draw has not ended.";
        private const string ERROR_MOUSE_POSITION_IS_NULL = "The given mouse position is null.";
        private CanvasDrawer _canvasDrawer;
        private Point _currentDrawingShapeDrawingStartingPoint;
        private ShapeDrawer _currentDrawingShapeHintShapeDrawer;

        public CanvasDrawerDrawingState(CanvasDrawer canvasDrawerData, Point currentDrawingShapeDrawingStartingPointData)
        {
            if ( canvasDrawerData == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_CANVAS_DRAWER_IS_NULL);
            }
            if ( currentDrawingShapeDrawingStartingPointData == null )
            {
                throw new ArgumentNullException(ERROR_CURRENT_DRAWING_SHAPE_DRAWING_STARTING_POINT_IS_NULL);
            }
            _canvasDrawer = canvasDrawerData;
            _currentDrawingShapeDrawingStartingPoint = currentDrawingShapeDrawingStartingPointData;
            _currentDrawingShapeHintShapeDrawer = ShapeDrawerFactory.CreateShapeDrawer(_currentDrawingShapeDrawingStartingPoint, _currentDrawingShapeDrawingStartingPoint, _canvasDrawer.CurrentShapeDrawerType);
        }

        /// <summary>
        /// Clears the canvas.
        /// </summary>
        public void ClearCanvas()
        {
            throw new InvalidOperationException(ERROR_CLEAR_CANVAS_IS_CALLED_IN_CANVAS_DRAWER_DRAWING_STATE);
        }

        /// <summary>
        /// Handles the canvas left mouse pressed.
        /// </summary>
        public void HandleCanvasLeftMousePressed(Point mousePosition)
        {
            throw new InvalidOperationException(ERROR_PREVIOUS_DRAW_HAS_NOT_ENDED);
        }

        /// <summary>
        /// Handles the canvas left mouse moved.
        /// </summary>
        public void HandleCanvasLeftMouseMoved(Point mousePosition)
        {
            UpdateCurrentDrawingShapeHint(mousePosition);
        }

        /// <summary>
        /// Updates the current drawing shape hint.
        /// </summary>
        private void UpdateCurrentDrawingShapeHint(Point mousePosition)
        {
            if ( mousePosition == null )
            {
                throw new ArgumentNullException(ERROR_MOUSE_POSITION_IS_NULL);
            }
            _currentDrawingShapeHintShapeDrawer.DrawingEndingPoint = mousePosition;
            _canvasDrawer.NotifyCanvasRefreshDrawRequested();
        }

        /// <summary>
        /// Handles the canvas left mouse released.
        /// </summary>
        public void HandleCanvasLeftMouseReleased(Point mousePosition)
        {
            EndDrawing(mousePosition);
            _canvasDrawer.SetCurrentState(new CanvasDrawerPointerState(_canvasDrawer));
        }

        /// <summary>
        /// Ends the drawing.
        /// </summary>
        private void EndDrawing(Point mousePosition)
        {
            _canvasDrawer.AddCurrentShapeDrawer(_currentDrawingShapeDrawingStartingPoint, mousePosition);
            _canvasDrawer.NotifyDrawingEnded();
            _canvasDrawer.NotifyCanvasRefreshDrawRequested();
        }

        /// <summary>
        /// Draws the specified graphics.
        /// </summary>
        public void Draw(IGraphics graphics)
        {
            _currentDrawingShapeHintShapeDrawer.Draw(graphics);
        }
    }
}
