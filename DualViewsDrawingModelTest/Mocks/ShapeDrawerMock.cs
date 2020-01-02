using DualViewsDrawingModel;
using DualViewsDrawingModel.ShapeDrawers;

namespace DualViewsDrawingModelTest.Mocks
{
    public class ShapeDrawerMock : ShapeDrawer
    {
        public ShapeDrawerMock(Point drawingStartingPointData, Point drawingEndingPointData) : base(drawingStartingPointData, drawingEndingPointData)
        {
            /* Body intentionally empty */
        }

        /// <summary>
        /// Draws the specified graphics.
        /// </summary>
        public override void Draw(IGraphics graphics)
        {
            /* Body intentionally empty */
        }
    }
}
