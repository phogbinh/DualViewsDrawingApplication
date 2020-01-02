using DualViewsDrawingModel.ShapeDrawers;
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
                return;
            }
            else
            {
                _canvasDrawer.SetCurrentState(new CanvasDrawerDrawingState(_canvasDrawer, mousePosition));
            }
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
    }
}
