using DualViewsDrawingModel;

namespace DualViewsDrawingModelTest.Mocks
{
    public class PointMock : Point
    {
        public bool IsCalledIsInclusiveInRegion
        {
            get; set;
        }

        public PointMock() : base()
        {
            IsCalledIsInclusiveInRegion = false;
        }

        /// <summary>
        /// Determines whether the point is inclusively inside the given region.
        /// </summary>
        public override bool IsInclusiveInRegion(double regionLowerBoundaryX, double regionUpperBoundaryX, double regionLowerBoundaryY, double regionUpperBoundaryY)
        {
            IsCalledIsInclusiveInRegion = true;
            return TestDefinitions.DUMP_BOOLEAN;
        }
    }
}
