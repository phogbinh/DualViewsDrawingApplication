using DualViewsDrawingModel.ShapeDrawers;
using DualViewsDrawingModelTest;
using DualViewsDrawingModelTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DualViewsDrawingModel.Commands.Test
{
    [TestClass()]
    public class ResizingCommandTest
    {
        private const string MEMBER_VARIABLE_NAME_SHAPE_DRAWER = "_shapeDrawer";
        private const string MEMBER_VARIABLE_NAME_OLD_DRAWING_ENDING_POINT = "_oldDrawingEndingPoint";
        private const string MEMBER_VARIABLE_NAME_NEW_DRAWING_ENDING_POINT = "_newDrawingEndingPoint";
        private ResizingCommand _resizingCommand;
        private PrivateObject _target;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            _resizingCommand = new ResizingCommand(new ShapeDrawerMock(new Point(), new Point()), new Point(), new Point());
            _target = new PrivateObject(_resizingCommand);
        }

        /// <summary>
        /// Tests the resizing command.
        /// </summary>
        [TestMethod()]
        public void TestResizingCommand()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ResizingCommand(null, new Point(), new Point()));
            Assert.ThrowsException<ArgumentNullException>(() => new ResizingCommand(new ShapeDrawerMock(new Point(), new Point()), null, new Point()));
            Assert.ThrowsException<ArgumentNullException>(() => new ResizingCommand(new ShapeDrawerMock(new Point(), new Point()), new Point(), null));
            var shapeDrawer = new ShapeDrawerMock(new Point(), new Point());
            var oldDrawingEndingPoint = new Point();
            var newDrawingEndingPoint = new Point();
            var resizingCommand = new ResizingCommand(shapeDrawer, oldDrawingEndingPoint, newDrawingEndingPoint);
            var target = new PrivateObject(resizingCommand);
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
            var newDrawingEndingPoint = new Point();
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_NEW_DRAWING_ENDING_POINT, newDrawingEndingPoint);
            _resizingCommand.Execute();
            ShapeDrawer expectedShapeDrawer = ( ShapeDrawer )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_SHAPE_DRAWER);
            Assert.AreSame(expectedShapeDrawer.DrawingEndingPoint, newDrawingEndingPoint);
        }

        /// <summary>
        /// Tests the reverse execution.
        /// </summary>
        [TestMethod()]
        public void TestReverseExecution()
        {
            var oldDrawingEndingPoint = new Point();
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_OLD_DRAWING_ENDING_POINT, oldDrawingEndingPoint);
            _resizingCommand.ReverseExecution();
            ShapeDrawer expectedShapeDrawer = ( ShapeDrawer )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_SHAPE_DRAWER);
            Assert.AreSame(expectedShapeDrawer.DrawingEndingPoint, oldDrawingEndingPoint);
        }
    }
}