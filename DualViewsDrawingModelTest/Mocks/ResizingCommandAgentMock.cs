using DualViewsDrawingModel;
using DualViewsDrawingModel.ShapeDrawers;

namespace DualViewsDrawingModelTest.Mocks
{
    public class ResizingCommandAgentMock : IResizingCommandAgent
    {
        public bool IsCalledResizeShape
        {
            get; set;
        }

        public ResizingCommandAgentMock() : base()
        {
            IsCalledResizeShape = false;
        }

        /// <summary>
        /// Resizes the shape.
        /// </summary>
        public void ResizeShape(ShapeDrawer shapeDrawer, Point drawingEndingPoint)
        {
            IsCalledResizeShape = true;
        }
    }
}
