using DualViewsDrawingModel.ShapeDrawers;

namespace DualViewsDrawingModel
{
    public interface IDrawingCommandAgent
    {
        /// <summary>
        /// Draws the shape on to canvas.
        /// </summary>
        void DrawShapeOnToCanvas(ShapeDrawer shapeDrawer);

        /// <summary>
        /// Removes the shape from canvas.
        /// </summary>
        void RemoveShapeFromCanvas(ShapeDrawer shapeDrawer);
    }
}
