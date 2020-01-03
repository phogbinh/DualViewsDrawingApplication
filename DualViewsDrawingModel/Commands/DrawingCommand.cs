using DualViewsDrawingModel.ShapeDrawers;
using System;

namespace DualViewsDrawingModel.Commands
{
    public class DrawingCommand : ICommand
    {
        private IDrawingCommandAgent _agent;
        private ShapeDrawer _shapeDrawer;

        public DrawingCommand(IDrawingCommandAgent agentData, Point drawingStartingPoint, Point drawingEndingPoint, ShapeDrawerType shapeDrawerType)
        {
            if ( agentData == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_AGENT_IS_NULL);
            }
            _agent = agentData;
            _shapeDrawer = ShapeDrawerFactory.CreateShapeDrawer(drawingStartingPoint, drawingEndingPoint, shapeDrawerType);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public void Execute()
        {
            _agent.DrawShape(_shapeDrawer);
        }

        /// <summary>
        /// Reverses the execution.
        /// </summary>
        public void ReverseExecution()
        {
            _agent.RemoveShape(_shapeDrawer);
        }
    }
}
