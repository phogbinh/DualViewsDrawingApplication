using DualViewsDrawingModel;
using DualViewsDrawingModel.Shapes;

namespace DualViewsDrawingModelTest.Mocks
{
    public class GraphicsMock : IGraphics
    {
        public bool IsCalledClearAll
        {
            get; set;
        }
        public bool IsCalledDrawLine
        {
            get; set;
        }
        public bool IsCalledDrawRectangle
        {
            get; set;
        }

        public GraphicsMock() : base()
        {
            IsCalledClearAll = false;
            IsCalledDrawLine = false;
            IsCalledDrawRectangle = false;
        }

        /// <summary>
        /// Clears all.
        /// </summary>
        public void ClearAll()
        {
            IsCalledClearAll = true;
        }

        /// <summary>
        /// Draws the specified line.
        /// </summary>
        public void Draw(Line line)
        {
            IsCalledDrawLine = true;
        }

        /// <summary>
        /// Draws the specified rectangle.
        /// </summary>
        public void Draw(Rectangle rectangle)
        {
            IsCalledDrawRectangle = true;
        }
    }
}
