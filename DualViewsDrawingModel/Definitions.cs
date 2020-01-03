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
        public const string ERROR_CANVAS_DRAWER_IS_NULL = "The given canvas drawer is null.";
        public const string ERROR_COMMANDS_MANAGER_IS_NULL = "The given commands manager is null.";
        public const string ERROR_POINT_IS_NULL = "The given point is null.";
        public const double DOUBLE_EPSILON = 0.000001;
        public const double MOUSE_POSITION_TO_SELECTION_SHAPE_MAXIMUM_DISTANCE_SQUARED = 4.0;
        public const string CURRENT_SHAPE_INFO_SELECTED_TEXT = "Selected: ";
        public const string OPENING_BRACKET = "(";
        public const string CLOSING_BRACKET = ")";
        public const string COMMA_SPACE = ", ";
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

        /// <summary>
        /// Resizes to be inbound interval.
        /// </summary>
        public static void ResizeToBeInBoundInterval(ref double value, double intervalLowerBoundary, double intervalUpperBoundary)
        {
            if ( intervalLowerBoundary > intervalUpperBoundary )
            {
                throw new ArgumentException(ERROR_INTERVAL_LOWER_BOUNDARY_IS_BIGGER_THAN_UPPER_BOUNDARY);
            }
            value = Math.Min(intervalUpperBoundary, Math.Max(intervalLowerBoundary, value));
        }
    }
}
