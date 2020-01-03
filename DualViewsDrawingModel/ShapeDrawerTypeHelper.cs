using System;

namespace DualViewsDrawingModel
{
    public enum ShapeDrawerType
    {
        None = 0,
        Line,
        Rectangle
    }
    public static class ShapeDrawerTypeHelper
    {
        private const string SHAPE_DRAWER_TYPE_NONE = "";
        private const string SHAPE_DRAWER_TYPE_LINE = "Line";
        private const string SHAPE_DRAWER_TYPE_RECTANGLE = "Rectangle";

        /// <summary>
        /// Determines whether [is valid shape drawer type] [the specified shape drawer type].
        /// </summary>
        public static bool IsValidShapeDrawerType(ShapeDrawerType shapeDrawerType)
        {
            return shapeDrawerType == ShapeDrawerType.None || shapeDrawerType == ShapeDrawerType.Line || shapeDrawerType == ShapeDrawerType.Rectangle;
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        public static string GetString(ShapeDrawerType shapeDrawerType)
        {
            if ( shapeDrawerType == ShapeDrawerType.None )
            {
                return SHAPE_DRAWER_TYPE_NONE;
            }
            else if ( shapeDrawerType == ShapeDrawerType.Line )
            {
                return SHAPE_DRAWER_TYPE_LINE;
            }
            else if ( shapeDrawerType == ShapeDrawerType.Rectangle )
            {
                return SHAPE_DRAWER_TYPE_RECTANGLE;
            }
            else
            {
                throw new ArgumentException(Definitions.ERROR_SHAPE_DRAWER_TYPE_IS_INVALID);
            }
        }
    }
}
