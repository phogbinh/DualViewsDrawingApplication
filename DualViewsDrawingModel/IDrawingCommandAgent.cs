using DualViewsDrawingModel.ShapeDrawers;

namespace DualViewsDrawingModel
{
    public interface IDrawingCommandAgent
    {
        /// <summary>
        /// Draws the shape.
        /// </summary>
        void DrawShape(ShapeDrawer shapeDrawer);

        /// <summary>
        /// Removes the shape.
        /// </summary>
        void RemoveShape(ShapeDrawer shapeDrawer);
    }
}
