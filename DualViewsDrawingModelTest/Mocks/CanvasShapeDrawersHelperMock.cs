using DualViewsDrawingModel;
using DualViewsDrawingModel.ShapeDrawers;

namespace DualViewsDrawingModelTest.Mocks
{
    public class CanvasShapeDrawersHelperMock : CanvasShapeDrawersHelper
    {
        public bool IsCalledAddShapeDrawer
        {
            get; set;
        }
        public bool IsCalledRemoveShapeDrawer
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
        public bool IsCalledGetMostRecentDrawShapeDrawerThatIsCloseToPoint
        {
            get; set;
        }
        public bool IsCalledGetMostRecentDrawShapeDrawerWhoseDrawingEndingPointIsCloseToPoint
        {
            get; set;
        }

        public CanvasShapeDrawersHelperMock() : base()
        {
            IsCalledAddShapeDrawer = false;
            IsCalledRemoveShapeDrawer = false;
            IsCalledClear = false;
            IsCalledDraw = false;
            IsCalledGetMostRecentDrawShapeDrawerThatIsCloseToPoint = false;
            IsCalledGetMostRecentDrawShapeDrawerWhoseDrawingEndingPointIsCloseToPoint = false;
        }

        /// <summary>
        /// Adds the shape drawer.
        /// </summary>
        public override void AddShapeDrawer(ShapeDrawer shapeDrawer)
        {
            IsCalledAddShapeDrawer = true;
        }

        /// <summary>
        /// Removes the shape drawer.
        /// </summary>
        public override void RemoveShapeDrawer(ShapeDrawer shapeDrawer)
        {
            IsCalledRemoveShapeDrawer = true;
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

        /// <summary>
        /// Gets the most recent drawn shape drawer that is close to point.
        /// </summary>
        public override ShapeDrawer GetMostRecentDrawShapeDrawerThatIsCloseToPoint(Point point, double pointToShapeDrawerMaximumDistanceSquared)
        {
            IsCalledGetMostRecentDrawShapeDrawerThatIsCloseToPoint = true;
            return null;
        }

        /// <summary>
        /// Gets the most recent drawn shape drawer whose drawing ending point is close to point.
        /// </summary>
        public override ShapeDrawer GetMostRecentDrawShapeDrawerWhoseDrawingEndingPointIsCloseToPoint(Point point, double pointToShapeDrawerDrawingEndingPointMaximumDistanceSquared)
        {
            IsCalledGetMostRecentDrawShapeDrawerWhoseDrawingEndingPointIsCloseToPoint = true;
            return null;
        }
    }
}
