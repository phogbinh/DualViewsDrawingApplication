namespace DualViewsDrawingModel.CanvasDrawerStates
{
    public interface ICanvasDrawerState
    {
        /// <summary>
        /// Clears the canvas.
        /// </summary>
        void ClearCanvas();

        /// <summary>
        /// Handles the canvas left mouse pressed.
        /// </summary>
        void HandleCanvasLeftMousePressed(Point mousePosition);

        /// <summary>
        /// Handles the canvas left mouse moved.
        /// </summary>
        void HandleCanvasLeftMouseMoved(Point mousePosition);

        /// <summary>
        /// Handles the canvas left mouse released.
        /// </summary>
        void HandleCanvasLeftMouseReleased(Point mousePosition);

        /// <summary>
        /// Draws the specified graphics.
        /// </summary>
        void Draw(IGraphics graphics);
    }
}