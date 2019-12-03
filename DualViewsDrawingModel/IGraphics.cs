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
    }
}
