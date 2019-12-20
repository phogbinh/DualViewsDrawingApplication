using DualViewsDrawingModel;

namespace DualViewsDrawingModelTest.Mocks
{
    public class ShapeDrawersManagerMock : ShapeDrawersManager
    {
        public bool IsCalledAddShapeDrawer
        {
            get; set;
        }
        public bool IsCalledClear
        {
            get; set;
        }
        public bool IsCalledDraw
        {
            get; set;
        }

        public ShapeDrawersManagerMock()
        {
            IsCalledAddShapeDrawer = false;
            IsCalledClear = false;
            IsCalledDraw = false;
        }

        /// <summary>
        /// Adds the shape drawer.
        /// </summary>
        public override void AddShapeDrawer(Point drawingStartingPoint, Point drawingEndingPoint, ShapeDrawerType shapeDrawerType)
        {
            IsCalledAddShapeDrawer = true;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public override void Clear()
        {
            IsCalledClear = true;
        }

        /// <summary>
        /// Draws the specified graphics.
        /// </summary>
        public override void Draw(IGraphics graphics)
        {
            IsCalledDraw = true;
        }
    }
}
