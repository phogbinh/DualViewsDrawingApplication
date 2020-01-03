using DualViewsDrawingModel.Shapes;

namespace DualViewsDrawingModel
{
    public interface IGraphics
    {
        /// <summary>
        /// Clears all.
        /// </summary>
        void ClearAll();

        /// <summary>
        /// Draws the specified line.
        /// </summary>
        void Draw(Line line);

        /// <summary>
        /// Draws the specified rectangle.
        /// </summary>
        void Draw(Rectangle rectangle);

        /// <summary>
        /// Draws the selection border.
        /// </summary>
        void DrawSelectionBorder(Line line);

        /// <summary>
        /// Draws the selection border.
        /// </summary
        void DrawSelectionBorder(Rectangle rectangle);
    }
}
