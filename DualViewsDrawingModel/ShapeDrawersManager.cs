using DualViewsDrawingModel.ShapeDrawers;
using System;
using System.Collections.Generic;

namespace DualViewsDrawingModel
{
    public class ShapeDrawersManager
    {
        private List<ShapeDrawer> _shapeDrawers;

        public ShapeDrawersManager()
        {
            _shapeDrawers = new List<ShapeDrawer>();
        }

        /// <summary>
        /// Adds the shape drawer.
        /// </summary>
        public virtual void AddShapeDrawer(Point drawingStartingPoint, Point drawingEndingPoint, ShapeDrawerType shapeDrawerType)
        {
            _shapeDrawers.Add(CreateShapeDrawer(drawingStartingPoint, drawingEndingPoint, shapeDrawerType));
        }

        /// <summary>
        /// Creates the shape drawer.
        /// </summary>
        public virtual ShapeDrawer CreateShapeDrawer(Point drawingStartingPoint, Point drawingEndingPoint, ShapeDrawerType shapeDrawerType)
        {
            return ShapeDrawerFactory.CreateShapeDrawer(drawingStartingPoint, drawingEndingPoint, shapeDrawerType);
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
