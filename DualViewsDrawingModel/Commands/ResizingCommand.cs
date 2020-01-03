using DualViewsDrawingModel.ShapeDrawers;
using System;

namespace DualViewsDrawingModel.Commands
{
    public class ResizingCommand : ICommand
    {
        private ShapeDrawer _shapeDrawer;
        private Point _oldDrawingEndingPoint;
        private Point _newDrawingEndingPoint;

        public ResizingCommand(ShapeDrawer shapeDrawerData, Point oldDrawingEndingPointData, Point newDrawingEndingPointData)
        {
            if ( shapeDrawerData == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_SHAPE_DRAWER_IS_NULL);
            }
            if ( oldDrawingEndingPointData == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_POINT_IS_NULL);
            }
            if ( newDrawingEndingPointData == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_POINT_IS_NULL);
            }
            _shapeDrawer = shapeDrawerData;
            _oldDrawingEndingPoint = oldDrawingEndingPointData;
            _newDrawingEndingPoint = newDrawingEndingPointData;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public void Execute()
        {
            _shapeDrawer.DrawingEndingPoint = _newDrawingEndingPoint;
        }

        /// <summary>
        /// Reverses the execution.
        /// </summary>
        public void ReverseExecution()
        {
            _shapeDrawer.DrawingEndingPoint = _oldDrawingEndingPoint;
        }
    }
}
