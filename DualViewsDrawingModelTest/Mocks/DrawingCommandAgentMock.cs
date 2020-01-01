using DualViewsDrawingModel;
using DualViewsDrawingModel.ShapeDrawers;

namespace DualViewsDrawingModelTest.Mocks
{
    public class DrawingCommandAgentMock : IDrawingCommandAgent
    {
        public bool IsCalledDrawShapeOnToCanvas
        {
            get; set;
        }
        public bool IsCalledRemoveShapeFromCanvas
        {
            get; set;
        }

        public DrawingCommandAgentMock()
        {
            IsCalledDrawShapeOnToCanvas = false;
            IsCalledRemoveShapeFromCanvas = false;
        }

        /// <summary>
        /// Draws the shape on to canvas.
        /// </summary>
        public void DrawShapeOnToCanvas(ShapeDrawer shapeDrawer)
        {
            IsCalledDrawShapeOnToCanvas = true;
        }

        /// <summary>
        /// Removes the shape from canvas.
        /// </summary>
        public void RemoveShapeFromCanvas(ShapeDrawer shapeDrawer)
        {
            IsCalledRemoveShapeFromCanvas = true;
        }
    }
}
