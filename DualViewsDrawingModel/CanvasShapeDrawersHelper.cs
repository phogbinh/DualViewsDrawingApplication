using DualViewsDrawingModel.ShapeDrawers;
using System.Collections.Generic;

namespace DualViewsDrawingModel
{
    public class CanvasShapeDrawersHelper
    {
        private List<ShapeDrawer> _shapeDrawers;

        public CanvasShapeDrawersHelper()
        {
            _shapeDrawers = new List<ShapeDrawer>();
        }

        /// <summary>
        /// Adds the shape drawer.
        /// </summary>
        public virtual void AddShapeDrawer(ShapeDrawer shapeDrawer)
        {
            _shapeDrawers.Add(shapeDrawer);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public virtual void Clear()
        {
            _shapeDrawers.Clear();
        }

        /// <summary>
        /// Draws the specified graphics.
        /// </summary>
        public virtual void Draw(IGraphics graphics)
        {
            foreach ( ShapeDrawer shapeDrawer in _shapeDrawers )
            {
                shapeDrawer.Draw(graphics);
            }
        }
    }
}
