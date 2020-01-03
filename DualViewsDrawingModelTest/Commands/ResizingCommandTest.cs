using DualViewsDrawingModelTest;
using DualViewsDrawingModelTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DualViewsDrawingModel.Commands.Test
{
    [TestClass()]
    public class ResizingCommandTest
    {
        private const string MEMBER_VARIABLE_NAME_AGENT = "_agent";
        private const string MEMBER_VARIABLE_NAME_SHAPE_DRAWER = "_shapeDrawer";
        private const string MEMBER_VARIABLE_NAME_OLD_DRAWING_ENDING_POINT = "_oldDrawingEndingPoint";
        private const string MEMBER_VARIABLE_NAME_NEW_DRAWING_ENDING_POINT = "_newDrawingEndingPoint";
        private ResizingCommandAgentMock _agent;
        private ResizingCommand _resizingCommand;
        private PrivateObject _target;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            _agent = new ResizingCommandAgentMock();
            _resizingCommand = new ResizingCommand(_agent, new ShapeDrawerMock(new Point(), new Point()), new Point(), new Point());
            _target = new PrivateObject(_resizingCommand);
        }

        /// <summary>
        /// Tests the resizing command.
        /// </summary>
        [TestMethod()]
        public void TestResizingCommand()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ResizingCommand(null, new ShapeDrawerMock(new Point(), new Point()), new Point(), new Point()));
            Assert.ThrowsException<ArgumentNullException>(() => new ResizingCommand(new ResizingCommandAgentMock(), null, new Point(), new Point()));
            Assert.ThrowsException<ArgumentNullException>(() => new ResizingCommand(new ResizingCommandAgentMock(), new ShapeDrawerMock(new Point(), new Point()), null, new Point()));
            Assert.ThrowsException<ArgumentNullException>(() => new ResizingCommand(new ResizingCommandAgentMock(), new ShapeDrawerMock(new Point(), new Point()), new Point(), null));
            var agent = new ResizingCommandAgentMock();
            var shapeDrawer = new ShapeDrawerMock(new Point(), new Point());
            var oldDrawingEndingPoint = new Point();
            var newDrawingEndingPoint = new Point();
            var resizingCommand = new ResizingCommand(agent, shapeDrawer, oldDrawingEndingPoint, newDrawingEndingPoint);
            var target = new PrivateObject(resizingCommand);
            Assert.AreSame(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_AGENT), agent);
            Assert.AreSame(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_SHAPE_DRAWER), shapeDrawer);
            Assert.AreSame(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_OLD_DRAWING_ENDING_POINT), oldDrawingEndingPoint);
            Assert.AreSame(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_NEW_DRAWING_ENDING_POINT), newDrawingEndingPoint);
        }

        /// <summary>
        /// Tests the execute.
        /// </summary>
        [TestMethod()]
        public void TestExecute()
        {
            _resizingCommand.Execute();
            Assert.IsTrue(_agent.IsCalledResizeShape);
        }

        /// <summary>
        /// Tests the reverse execution.
        /// </summary>
        [TestMethod()]
        public void TestReverseExecution()
        {
            _resizingCommand.ReverseExecution();
            Assert.IsTrue(_agent.IsCalledResizeShape);
        }
    }
}