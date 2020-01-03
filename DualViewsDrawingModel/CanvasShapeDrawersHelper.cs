using DualViewsDrawingModel.ShapeDrawers;
using System;
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
            if ( shapeDrawer == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_SHAPE_DRAWER_IS_NULL);
            }
            _shapeDrawers.Add(shapeDrawer);
        }

        /// <summary>
        /// Removes the shape drawer.
        /// </summary>
        public virtual void RemoveShapeDrawer(ShapeDrawer shapeDrawer)
        {
            if ( shapeDrawer == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_SHAPE_DRAWER_IS_NULL);
            }
            _shapeDrawers.Remove(shapeDrawer);
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

        /// <summary>
        /// Gets the most recent drawn shape drawer that is close to point.
        /// </summary>
        public virtual ShapeDrawer GetMostRecentDrawnShapeDrawerThatIsCloseToPoint(Point point, double pointToShapeDrawerMaximumDistanceSquared)
        {
            for ( int index = _shapeDrawers.Count - 1; index >= 0; index-- )
            {
                if ( _shapeDrawers[ index ].IsCloseToPoint(point, pointToShapeDrawerMaximumDistanceSquared) )
                {
                    return _shapeDrawers[ index ];
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the most recent drawn shape drawer whose drawing ending point is close to point.
        /// </summary>
        public ShapeDrawer GetMostRecentDrawnShapeDrawerWhoseDrawingEndingPointIsCloseToPoint(Point point, double pointToShapeDrawerDrawingEndingPointMaximumDistanceSquared)
        {
            for ( int index = _shapeDrawers.Count - 1; index >= 0; index-- )
            {
                if ( _shapeDrawers[ index ].IsDrawingEndingPointCloseToPoint(point, pointToShapeDrawerDrawingEndingPointMaximumDistanceSquared) )
                {
                    return _shapeDrawers[ index ];
                }
            }
            return null;
        }
    }
}
