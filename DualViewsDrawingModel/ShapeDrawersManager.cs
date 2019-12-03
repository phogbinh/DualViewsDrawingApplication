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
        public void AddShapeDrawer(Point drawingStartingPoint, Point drawingEndingPoint, ShapeDrawerType shapeDrawerType)
        {
            _shapeDrawers.Add(CreateShapeDrawer(drawingStartingPoint, drawingEndingPoint, shapeDrawerType));
        }

        /// <summary>
        /// Creates the shape drawer.
        /// </summary>
        public ShapeDrawer CreateShapeDrawer(Point drawingStartingPoint, Point drawingEndingPoint, ShapeDrawerType shapeDrawerType)
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

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            _shapeDrawers.Clear();
        }

        /// <summary>
        /// Draws the specified graphics.
        /// </summary>
        public void Draw(IGraphics graphics)
        {
            foreach ( ShapeDrawer shapeDrawer in _shapeDrawers )
            {
                shapeDrawer.Draw(graphics);
            }
        }
    }
}
