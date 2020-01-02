using DualViewsDrawingModel;
using DualViewsDrawingModel.ShapeDrawers;

namespace DualViewsDrawingModelTest.Mocks
{
    public class DrawingCommandAgentMock : IDrawingCommandAgent
    {
        public bool IsCalledDrawShape
        {
            get; set;
        }
        public bool IsCalledRemoveShape
        {
            get; set;
        }

        public DrawingCommandAgentMock() : base()
        {
            IsCalledDrawShape = false;
            IsCalledRemoveShape = false;
        }

        /// <summary>
        /// Draws the shape.
        /// </summary>
        public void DrawShape(ShapeDrawer shapeDrawer)
        {
            IsCalledDrawShape = true;
        }

        /// <summary>
        /// Removes the shape.
        /// </summary>
        public void RemoveShape(ShapeDrawer shapeDrawer)
        {
            IsCalledRemoveShape = true;
        }
    }
}
