using System;

namespace DualViewsDrawingModel
{
    public static class Definitions
    {
        public const string ERROR_DRAWING_STARTING_POINT_IS_NULL = "The given drawing starting point is null.";
        public const string ERROR_DRAWING_ENDING_POINT_IS_NULL = "The given drawing ending point is null.";
        public const string ERROR_GRAPHICS_IS_NULL = "The given graphics is null.";
        public const string ERROR_SHAPE_DRAWER_TYPE_IS_INVALID = "The given shape drawer type is invalid.";
        public const string ERROR_SHAPE_DRAWER_TYPE_IS_NONE = "The given shape drawer type is none.";
        private const string ERROR_INTERVAL_LOWER_BOUNDARY_IS_BIGGER_THAN_UPPER_BOUNDARY = "The given interval lower boundary is bigger than the interval bigger boundary";

        /// <summary>
        /// Return true if value is in [ interval lower boundary, interval upper boundary ].
        /// </summary>
        public static bool IsInclusiveInInterval(double value, double intervalLowerBoundary, double intervalUpperBoundary)
        {
            if ( intervalLowerBoundary > intervalUpperBoundary )
            {
                throw new ArgumentException(ERROR_INTERVAL_LOWER_BOUNDARY_IS_BIGGER_THAN_UPPER_BOUNDARY);
            }
            return intervalLowerBoundary <= value && value <= intervalUpperBoundary;
        }
    }
}
