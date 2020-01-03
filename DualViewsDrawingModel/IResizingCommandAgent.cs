using DualViewsDrawingModel.ShapeDrawers;

namespace DualViewsDrawingModel
{
    public interface IResizingCommandAgent
    {
        /// <summary>
        /// Resizes the shape.
        /// </summary>
        void ResizeShape(ShapeDrawer shapeDrawer, Point drawingEndingPoint);
    }
}
