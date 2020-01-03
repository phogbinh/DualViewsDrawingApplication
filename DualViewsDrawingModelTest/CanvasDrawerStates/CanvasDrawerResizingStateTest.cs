using DualViewsDrawingModelTest;
using DualViewsDrawingModelTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace DualViewsDrawingModel.CanvasDrawerStates.Test
{
    [TestClass()]
    public class CanvasDrawerResizingStateTest
    {
        private const string MEMBER_VARIABLE_NAME_CANVAS_DRAWER = "_canvasDrawer";
        private const string MEMBER_VARIABLE_NAME_CURRENT_RESIZING_SHAPE_SHAPE_DRAWER = "_currentResizingShapeShapeDrawer";
        private const string MEMBER_VARIABLE_NAME_CURRENT_RESIZING_SHAPE_OLD_DRAWING_ENDING_POINT = "_currentResizingShapeOldDrawingEndingPoint";
        private CanvasDrawerResizingState _canvasDrawerResizingState;
        private ShapeDrawerMock _currentResizingShapeShapeDrawer;
        private PrivateObject _target;
        private CanvasDrawerMock _canvasDrawer;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            _canvasDrawer = new CanvasDrawerMock(new CommandsManager());
            _currentResizingShapeShapeDrawer = new ShapeDrawerMock(new Point(), new Point());
            _canvasDrawerResizingState = new CanvasDrawerResizingState(_canvasDrawer, _currentResizingShapeShapeDrawer);
            _target = new PrivateObject(_canvasDrawerResizingState);
        }

        /// <summary>
        /// Tests the state of the canvas drawer resizing.
        /// </summary>
        [TestMethod()]
        public void TestCanvasDrawerResizingState()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new CanvasDrawerResizingState(null, new ShapeDrawerMock(new Point(), new Point())));
            Assert.ThrowsException<ArgumentNullException>(() => new CanvasDrawerResizingState(new CanvasDrawerMock(new CommandsManager()), null));
            var canvasDrawer = new CanvasDrawerMock(new CommandsManager());
            var currentResizingShapeShapeDrawer = new ShapeDrawerMock(new Point(), new Point());
            var canvasDrawerResizingState = new CanvasDrawerResizingState(canvasDrawer, currentResizingShapeShapeDrawer);
            var target = new PrivateObject(canvasDrawerResizingState);
            Assert.AreSame(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_CANVAS_DRAWER), canvasDrawer);
            Assert.AreSame(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_RESIZING_SHAPE_SHAPE_DRAWER), currentResizingShapeShapeDrawer);
            Assert.AreSame(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_RESIZING_SHAPE_OLD_DRAWING_ENDING_POINT), currentResizingShapeShapeDrawer.DrawingEndingPoint);
        }

        /// <summary>
        /// Tests the clear canvas.
        /// </summary>
        [TestMethod()]
        public void TestClearCanvas()
        {
            Assert.ThrowsException<InvalidOperationException>(() => _canvasDrawerResizingState.ClearCanvas());
        }

        /// <summary>
        /// Tests the handle canvas left mouse pressed.
        /// </summary>
        [TestMethod()]
        public void TestHandleCanvasLeftMousePressed()
        {
            Assert.ThrowsException<InvalidOperationException>(() => _canvasDrawerResizingState.HandleCanvasLeftMousePressed(new Point()));
        }

        /// <summary>
        /// Tests the handle canvas left mouse moved.
        /// </summary>
        [TestMethod()]
        public void TestHandleCanvasLeftMouseMoved()
        {
            var mousePosition = new Point();
            _canvasDrawerResizingState.HandleCanvasLeftMouseMoved(mousePosition);
            Assert.AreSame(_currentResizingShapeShapeDrawer.DrawingEndingPoint, mousePosition);
            Assert.IsTrue(_canvasDrawer.IsCalledNotifyCanvasRefreshDrawRequested);
        }

        /// <summary>
        /// Tests the update current resizing shape.
        /// </summary>
        [TestMethod()]
        public void TestUpdateCurrentResizingShape()
        {
            const string MEMBER_FUNCTION_NAME_UPDATE_CURRENT_RESIZING_SHAPE = "UpdateCurrentResizingShape";
            var arguments = new object[] { null };
            TargetInvocationException expectedException = Assert.ThrowsException<TargetInvocationException>(() => _target.Invoke(MEMBER_FUNCTION_NAME_UPDATE_CURRENT_RESIZING_SHAPE, arguments));
            Assert.IsInstanceOfType(expectedException.InnerException, typeof(ArgumentNullException));
            var mousePosition = new Point();
            arguments = new object[] { mousePosition };
            _target.Invoke(MEMBER_FUNCTION_NAME_UPDATE_CURRENT_RESIZING_SHAPE, arguments);
            Assert.AreSame(_currentResizingShapeShapeDrawer.DrawingEndingPoint, mousePosition);
            Assert.IsTrue(_canvasDrawer.IsCalledNotifyCanvasRefreshDrawRequested);
        }

        /// <summary>
        /// Tests the handle canvas left mouse released.
        /// </summary>
        [TestMethod()]
        public void TestHandleCanvasLeftMouseReleased()
        {
            _canvasDrawerResizingState.HandleCanvasLeftMouseReleased(new Point());
            Assert.IsTrue(_canvasDrawer.IsCalledCreateThenExecuteResizingCommand);
            Assert.IsTrue(_canvasDrawer.IsCalledSetCurrentState);
            Assert.IsInstanceOfType(_canvasDrawer.CurrentState, typeof(CanvasDrawerPointerState));
            Assert.IsTrue(_canvasDrawer.IsCalledNotifyCurrentShapeChanged);
        }

        /// <summary>
        /// Tests the draw.
        /// </summary>
        [TestMethod()]
        public void TestDraw()
        {
            var graphics = new GraphicsMock();
            _canvasDrawerResizingState.Draw(graphics);
            Assert.IsTrue(_currentResizingShapeShapeDrawer.IsCalledDraw);
        }

        /// <summary>
        /// Tests the get current shape rectangle.
        /// </summary>
        [TestMethod()]
        public void TestGetCurrentShapeRectangle()
        {
            Assert.IsNull(_canvasDrawerResizingState.GetCurrentShapeRectangle());
        }

        /// <summary>
        /// Tests the type of the get current shape.
        /// </summary>
        [TestMethod()]
        public void TestGetCurrentShapeType()
        {
            Assert.AreEqual(_canvasDrawerResizingState.GetCurrentShapeType(), ShapeDrawerType.None);
        }
    }
}