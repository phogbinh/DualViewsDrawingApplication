using DualViewsDrawingModel;
using DualViewsDrawingModel.CanvasDrawerStates;
using DualViewsDrawingModel.Shapes;

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
        public bool IsCalledGetCurrentShapeRectangle
        {
            get; set;
        }
        public bool IsCalledGetCurrentShapeType
        {
            get; set;
        }

        public CanvasDrawerStateMock() : base()
        {
            IsCalledClearCanvas = false;
            IsCalledHandleCanvasLeftMousePressed = false;
            IsCalledHandleCanvasLeftMouseMoved = false;
            IsCalledHandleCanvasLeftMouseReleased = false;
            IsCalledDraw = false;
            IsCalledGetCurrentShapeRectangle = false;
            IsCalledGetCurrentShapeType = false;
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

        /// <summary>
        /// Gets the current shape rectangle.
        /// </summary>
        public Rectangle GetCurrentShapeRectangle()
        {
            IsCalledGetCurrentShapeRectangle = true;
            return null;
        }

        /// <summary>
        /// Gets the type of the current shape.
        /// </summary>
        public ShapeDrawerType GetCurrentShapeType()
        {
            IsCalledGetCurrentShapeType = true;
            return ShapeDrawerType.None;
        }
    }
}
