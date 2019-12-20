using DualViewsDrawingModel;
using DualViewsDrawingModel.CanvasDrawerStates;

namespace DualViewsDrawingModelTest.Mocks
{
    public class CanvasDrawerStateMock : ICanvasDrawerState
    {
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
        public bool IsCalledDraw
        {
            get; set;
        }

        public CanvasDrawerStateMock()
        {
            IsCalledClearCanvas = false;
            IsCalledHandleCanvasLeftMousePressed = false;
            IsCalledHandleCanvasLeftMouseMoved = false;
            IsCalledHandleCanvasLeftMouseReleased = false;
            IsCalledDraw = false;
        }

        /// <summary>
        /// Clears the canvas.
        /// </summary>
        public void ClearCanvas()
        {
            IsCalledClearCanvas = true;
        }

        /// <summary>
        /// Handles the canvas left mouse pressed.
        /// </summary>
        public void HandleCanvasLeftMousePressed(Point mousePosition)
        {
            IsCalledHandleCanvasLeftMousePressed = true;
        }

        /// <summary>
        /// Handles the canvas left mouse moved.
        /// </summary>
        public void HandleCanvasLeftMouseMoved(Point mousePosition)
        {
            IsCalledHandleCanvasLeftMouseMoved = true;
        }

        /// <summary>
        /// Handles the canvas left mouse released.
        /// </summary>
        public void HandleCanvasLeftMouseReleased(Point mousePosition)
        {
            IsCalledHandleCanvasLeftMouseReleased = true;
        }

        /// <summary>
        /// Draws the specified graphics.
        /// </summary>
        public void Draw(IGraphics graphics)
        {
            IsCalledDraw = true;
        }
    }
}
