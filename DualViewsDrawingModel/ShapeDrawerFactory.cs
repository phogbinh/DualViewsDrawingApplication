using DualViewsDrawingModel.ShapeDrawers;
using System;

namespace DualViewsDrawingModel
{
    public static class ShapeDrawerFactory
    {
        /// <summary>
        /// Creates the shape drawer.
        /// </summary>
        public static ShapeDrawer CreateShapeDrawer(Point drawingStartingPoint, Point drawingEndingPoint, ShapeDrawerType shapeDrawerType)
        {
            if ( shapeDrawerType == ShapeDrawerType.None )
            {
                throw new ArgumentException(Definitions.ERROR_SHAPE_DRAWER_TYPE_IS_NONE);
            }
            if ( shapeDrawerType == ShapeDrawerType.Line )
            {
                return new LineDrawer(drawingStartingPoint, drawingEndingPoint);
            }
            else if ( shapeDrawerType == ShapeDrawerType.Rectangle )
            {
                return new RectangleDrawer(drawingStartingPoint, drawingEndingPoint);
            }
            else
            {
                throw new ArgumentException(Definitions.ERROR_SHAPE_DRAWER_TYPE_IS_INVALID);
            }
        }
    }
}
