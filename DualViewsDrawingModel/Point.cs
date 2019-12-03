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
        private const string ERROR_X_IS_NEGATIVE = "The given x is negative.";
        private const string ERROR_Y_IS_NEGATIVE = "The given y is negative.";
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
        /// Determines whether [is in canvas].
        /// </summary>
        public bool IsInCanvas(double canvasWidth, double canvasHeight)
        {
            return 0 <= _x && _x <= canvasWidth && 0 <= _y && _y <= canvasHeight;
        }
    }
}
