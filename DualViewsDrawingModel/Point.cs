namespace DualViewsDrawingModel
{
    public class Point
    {
        public double X
        {
            get
            {
                return _x;
            }
        }
        public double Y
        {
            get
            {
                return _y;
            }
        }
        private const double X_INITIAL_VALUE = 0.0;
        private const double Y_INITIAL_VALUE = 0.0;
        private double _x;
        private double _y;

        public Point()
        {
            _x = X_INITIAL_VALUE;
            _y = Y_INITIAL_VALUE;
        }

        public Point(double xData, double yData)
        {
            _x = xData;
            _y = yData;
        }

        /// <summary>
        /// Determines whether the point is inclusively inside the given region.
        /// </summary>
        public virtual bool IsInclusiveInRegion(double regionLowerBoundaryX, double regionUpperBoundaryX, double regionLowerBoundaryY, double regionUpperBoundaryY)
        {
            return Definitions.IsInclusiveInInterval(_x, regionLowerBoundaryX, regionUpperBoundaryX) && Definitions.IsInclusiveInInterval(_y, regionLowerBoundaryY, regionUpperBoundaryY);
        }

        /// <summary>
        /// Resizes to be inbound region.
        /// </summary>
        public void ResizeToBeInBoundRegion(double regionLowerBoundaryX, double regionUpperBoundaryX, double regionLowerBoundaryY, double regionUpperBoundaryY)
        {
            Definitions.ResizeToBeInBoundInterval(ref _x, regionLowerBoundaryX, regionUpperBoundaryX);
            Definitions.ResizeToBeInBoundInterval(ref _y, regionLowerBoundaryY, regionUpperBoundaryY);
        }
    }
}
