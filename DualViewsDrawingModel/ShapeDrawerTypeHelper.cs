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
        /// <summary>
        /// Determines whether [is valid shape drawer type] [the specified shape drawer type].
        /// </summary>
        public static bool IsValidShapeDrawerType(ShapeDrawerType shapeDrawerType)
        {
            return shapeDrawerType == ShapeDrawerType.None || shapeDrawerType == ShapeDrawerType.Line || shapeDrawerType == ShapeDrawerType.Rectangle;
        }
    }
}
