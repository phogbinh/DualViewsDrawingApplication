using DualViewsDrawingModel.ShapeDrawers;
using DualViewsDrawingModelTest;
using DualViewsDrawingModelTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DualViewsDrawingModel.Commands.Test
{
    [TestClass()]
    public class DrawingCommandTest
    {
        private const string MEMBER_VARIABLE_NAME_AGENT = "_agent";
        private const string MEMBER_VARIABLE_NAME_SHAPE_DRAWER = "_shapeDrawer";
        private DrawingCommandAgentMock _agent;
        private DrawingCommand _drawingCommand;
        private PrivateObject _target;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            _agent = new DrawingCommandAgentMock();
            _drawingCommand = new DrawingCommand(_agent, new Point(), new Point(), ShapeDrawerType.Line);
            _target = new PrivateObject(_drawingCommand);
        }

        /// <summary>
        /// Tests the drawing command.
        /// </summary>
        [TestMethod()]
        public void TestDrawingCommand()
        {
            const string SHAPE_DRAWER_MEMBER_VARIABLE_NAME_DRAWING_STARTING_POINT = "_drawingStartingPoint";
            const string SHAPE_DRAWER_MEMBER_VARIABLE_NAME_DRAWING_ENDING_POINT = "_drawingEndingPoint";
            Assert.ThrowsException<ArgumentNullException>(() => new DrawingCommand(null, new Point(), new Point(), ShapeDrawerType.Line));
            var agent = new DrawingCommandAgentMock();
            var drawingStartingPoint = new Point();
            var drawingEndingPoint = new Point();
            var drawingCommand = new DrawingCommand(agent, drawingStartingPoint, drawingEndingPoint, ShapeDrawerType.Line);
            var target = new PrivateObject(drawingCommand);
            Assert.AreSame(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_AGENT), agent);
            ShapeDrawer expectedShapeDrawer = ( ShapeDrawer )target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_SHAPE_DRAWER);
            var expectedShapeDrawerTarget = new PrivateObject(expectedShapeDrawer);
            Assert.AreSame(expectedShapeDrawerTarget.GetFieldOrProperty(SHAPE_DRAWER_MEMBER_VARIABLE_NAME_DRAWING_STARTING_POINT), drawingStartingPoint);
            Assert.AreSame(expectedShapeDrawerTarget.GetFieldOrProperty(SHAPE_DRAWER_MEMBER_VARIABLE_NAME_DRAWING_ENDING_POINT), drawingEndingPoint);
            Assert.IsInstanceOfType(expectedShapeDrawer, typeof(LineDrawer));
        }

        /// <summary>
        /// Tests the execute.
        /// </summary>
        [TestMethod()]
        public void TestExecute()
        {
            _drawingCommand.Execute();
            Assert.IsTrue(_agent.IsCalledDrawShape);
        }

        /// <summary>
        /// Tests the reverse execution.
        /// </summary>
        [TestMethod()]
        public void TestReverseExecution()
        {
            _drawingCommand.ReverseExecution();
            Assert.IsTrue(_agent.IsCalledRemoveShape);
        }
    }
}