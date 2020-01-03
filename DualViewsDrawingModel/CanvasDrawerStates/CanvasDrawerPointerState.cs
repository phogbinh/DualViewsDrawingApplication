using DualViewsDrawingModel.ShapeDrawers;
using DualViewsDrawingModel.Shapes;
using System;

namespace DualViewsDrawingModel.CanvasDrawerStates
{
    public class CanvasDrawerPointerState : ICanvasDrawerState
    {
        private CanvasDrawer _canvasDrawer;
        private ShapeDrawer _currentSelectedShapeShapeDrawer;

        public CanvasDrawerPointerState(CanvasDrawer canvasDrawerData)
        {
            if ( canvasDrawerData == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_CANVAS_DRAWER_IS_NULL);
            }
            _canvasDrawer = canvasDrawerData;
            _currentSelectedShapeShapeDrawer = null;
        }

        /// <summary>
        /// Clears the canvas.
        /// </summary>
        public void ClearCanvas()
        {
            _currentSelectedShapeShapeDrawer = null;
            _canvasDrawer.NotifyCurrentShapeChanged();
            _canvasDrawer.ClearShapeDrawersManager();
            _canvasDrawer.NotifyCanvasRefreshDrawRequested();
        }

        /// <summary>
        /// Handles the canvas left mouse pressed.
        /// </summary>
        public void HandleCanvasLeftMousePressed(Point mousePosition)
        {
            if ( _canvasDrawer.CurrentShapeDrawerType == ShapeDrawerType.None )
            {
                GoToResizingStateOrSelectShape(_canvasDrawer.GetSelectedResizingShapeDrawer(mousePosition), mousePosition);
            }
            else
            {
                GoToDrawingState(mousePosition);
            }
        }

        /// <summary>
        /// Goes to resizing state or select shape.
        /// </summary>
        private void GoToResizingStateOrSelectShape(ShapeDrawer selectedResizingShapeDrawer, Point mousePosition)
        {
            if ( selectedResizingShapeDrawer != null )
            {
                GoToResizingState(selectedResizingShapeDrawer);
            }
            else
            {
                SelectShape(mousePosition);
            }
        }

        /// <summary>
        /// Goes the state of to resizing.
        /// </summary>
        private void GoToResizingState(ShapeDrawer selectedResizingShapeDrawer)
        {
            _canvasDrawer.SetCurrentState(new CanvasDrawerResizingState(_canvasDrawer, selectedResizingShapeDrawer));
            _canvasDrawer.NotifyCurrentShapeChanged(); // Only notify after `CanvasDrawerResizingState` is completely created.
        }

        /// <summary>
        /// Selects the shape.
        /// </summary>
        private void SelectShape(Point mousePosition)
        {
            _currentSelectedShapeShapeDrawer = _canvasDrawer.GetSelectedShapeShapeDrawer(mousePosition);
            _canvasDrawer.NotifyCurrentShapeChanged();
        }

        /// <summary>
        /// Goes the state of to drawing.
        /// </summary>
        private void GoToDrawingState(Point mousePosition)
        {
            _canvasDrawer.SetCurrentState(new CanvasDrawerDrawingState(_canvasDrawer, mousePosition));
            _canvasDrawer.NotifyCurrentShapeChanged(); // Only notify after `CanvasDrawerDrawingState` is completely created.
        }

        /// <summary>
        /// Handles the canvas left mouse moved.
        /// </summary>
        public void HandleCanvasLeftMouseMoved(Point mousePosition)
        {
            /* Body intentionally empty */
        }

        /// <summary>
        /// Handles the canvas left mouse released.
        /// </summary>
        public void HandleCanvasLeftMouseReleased(Point mousePosition)
        {
            /* Body intentionally empty */
        }

        /// <summary>
        /// Draws the specified graphics.
        /// </summary>
        public void Draw(IGraphics graphics)
        {
            /* Body intentionally empty */
        }

        /// <summary>
        /// Gets the current shape rectangle.
        /// </summary>
        public Rectangle GetCurrentShapeRectangle()
        {
            if ( _currentSelectedShapeShapeDrawer == null )
            {
                return null;
            }
            return _currentSelectedShapeShapeDrawer.GetRectangle();
        }

        /// <summary>
        /// Gets the type of the current shape.
        /// </summary>
        public ShapeDrawerType GetCurrentShapeType()
        {
            if ( _currentSelectedShapeShapeDrawer == null )
            {
                return ShapeDrawerType.None;
            }
            return _currentSelectedShapeShapeDrawer.Type;
        }
    }
}
