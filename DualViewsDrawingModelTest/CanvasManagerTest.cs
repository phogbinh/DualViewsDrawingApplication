using DualViewsDrawingModelTest;
using DualViewsDrawingModelTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace DualViewsDrawingModel.Test
{
    [TestClass()]
    public class CanvasManagerTest
    {
        private const string MEMBER_VARIABLE_NAME_CANVAS_WIDTH = "_canvasWidth";
        private const string MEMBER_VARIABLE_NAME_CANVAS_HEIGHT = "_canvasHeight";
        private const string MEMBER_VARIABLE_NAME_CANVAS_DRAWER = "_canvasDrawer";
        private CanvasManager _canvasManager;
        private PrivateObject _target;
        private CanvasDrawerMock _canvasDrawer;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            _canvasManager = new CanvasManager();
            _target = new PrivateObject(_canvasManager);
            _canvasDrawer = new CanvasDrawerMock();
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_CANVAS_DRAWER, _canvasDrawer);
        }

        /// <summary>
        /// Tests the set property canvas refresh draw requested.
        /// </summary>
        [TestMethod()]
        public void TestSetPropertyCanvasRefreshDrawRequested()
        {
            _canvasManager.CanvasRefreshDrawRequested += () => { };
            Assert.IsNotNull(_canvasManager.CanvasRefreshDrawRequested);
        }

        /// <summary>
        /// Tests the set property drawing ended.
        /// </summary>
        [TestMethod()]
        public void TestSetPropertyDrawingEnded()
        {
            _canvasManager.DrawingEnded += () => { };
            Assert.IsNotNull(_canvasManager.DrawingEnded);
        }

        /// <summary>
        /// Tests the canvas manager.
        /// </summary>
        [TestMethod()]
        public void TestCanvasManager()
        {
            var canvasManager = new CanvasManager();
            var target = new PrivateObject(canvasManager);
            Assert.IsNotNull(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_CANVAS_DRAWER));
        }

        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestMethod()]
        public void TestInitialize()
        {
            _canvasManager.Initialize(0.1, 9.9, ShapeDrawerType.Rectangle);
            Assert.AreEqual(_canvasManager.CanvasWidth, 0.1);
            Assert.AreEqual(_canvasManager.CanvasHeight, 9.9);
            Assert.IsTrue(_canvasDrawer.IsCalledInitialize);
        }

        /// <summary>
        /// Tests the size of the set canvas.
        /// </summary>
        [TestMethod()]
        public void TestSetCanvasSize()
        {
            Assert.ThrowsException<ArgumentException>(() => _canvasManager.SetCanvasSize(0.0, 0.1));
            Assert.ThrowsException<ArgumentException>(() => _canvasManager.SetCanvasSize(0.1, 0.0));
            _canvasManager.SetCanvasSize(1.1, 2.2);
            Assert.AreEqual(_canvasManager.CanvasWidth, 1.1);
            Assert.AreEqual(_canvasManager.CanvasHeight, 2.2);
        }

        /// <summary>
        /// Tests the type of the set current shape drawer.
        /// </summary>
        [TestMethod()]
        public void TestSetCurrentShapeDrawerType()
        {
            _canvasManager.SetCurrentShapeDrawerType(ShapeDrawerType.Line);
            Assert.IsTrue(_canvasDrawer.IsCalledSetCurrentShapeDrawerType);
        }

        /// <summary>
        /// Tests the clear canvas.
        /// </summary>
        [TestMethod()]
        public void TestClearCanvas()
        {
            _canvasManager.ClearCanvas();
            Assert.IsTrue(_canvasDrawer.IsCalledClearCanvas);
        }

        /// <summary>
        /// Tests the handle canvas left mouse pressed.
        /// </summary>
        [TestMethod()]
        public void TestHandleCanvasLeftMousePressed()
        {
            _canvasManager.HandleCanvasLeftMousePressed(new Point());
            Assert.IsTrue(_canvasDrawer.IsCalledHandleCanvasLeftMousePressed);
        }

        /// <summary>
        /// Tests the handle canvas left mouse moved.
        /// </summary>
        [TestMethod()]
        public void TestHandleCanvasLeftMouseMoved()
        {
            _canvasManager.HandleCanvasLeftMouseMoved(new Point());
            Assert.IsTrue(_canvasDrawer.IsCalledHandleCanvasLeftMouseMoved);
        }

        /// <summary>
        /// Tests the handle canvas left mouse released.
        /// </summary>
        [TestMethod()]
        public void TestHandleCanvasLeftMouseReleased()
        {
            _canvasManager.HandleCanvasLeftMouseReleased(new Point());
            Assert.IsTrue(_canvasDrawer.IsCalledHandleCanvasLeftMouseReleased);
        }

        /// <summary>
        /// Tests the handle canvas left mouse action.
        /// </summary>
        [TestMethod()]
        public void TestHandleCanvasLeftMouseAction()
        {
            const string MEMBER_FUNCTION_NAME_HANDLE_CANVAS_LEFT_MOUSE_ACTION = "HandleCanvasLeftMouseAction";
            var arguments = new object[] { new Point(1.1, TestDefinitions.DUMP_DOUBLE), new Action<Point>((mousePositionParameter) => { }) };
            TargetInvocationException expectedException = Assert.ThrowsException<TargetInvocationException>(() => _target.Invoke(MEMBER_FUNCTION_NAME_HANDLE_CANVAS_LEFT_MOUSE_ACTION, arguments));
            Assert.IsInstanceOfType(expectedException.InnerException, typeof(ArgumentException));
            arguments = new object[] { new Point(TestDefinitions.DUMP_DOUBLE, 2.1), new Action<Point>((mousePositionParameter) => { }) };
            expectedException = Assert.ThrowsException<TargetInvocationException>(() => _target.Invoke(MEMBER_FUNCTION_NAME_HANDLE_CANVAS_LEFT_MOUSE_ACTION, arguments));
            Assert.IsInstanceOfType(expectedException.InnerException, typeof(ArgumentException));
            Point point = new Point();
            Point mousePosition = new Point(TestDefinitions.DUMP_DOUBLE, TestDefinitions.DUMP_DOUBLE);
            arguments = new object[] { mousePosition, new Action<Point>((mousePositionParameter) => point = mousePositionParameter) };
            _target.Invoke(MEMBER_FUNCTION_NAME_HANDLE_CANVAS_LEFT_MOUSE_ACTION, arguments);
            Assert.AreSame(point, mousePosition);
        }

        /// <summary>
        /// Tests the is inclusive in canvas.
        /// </summary>
        [TestMethod()]
        public void TestIsInclusiveInCanvas()
        {
            const string MEMBER_FUNCTION_NAME_IS_INCLUSIVE_IN_CANVAS = "IsInclusiveInCanvas";
            var arguments = new object[] { null };
            TargetInvocationException expectedException = Assert.ThrowsException<TargetInvocationException>(() => _target.Invoke(MEMBER_FUNCTION_NAME_IS_INCLUSIVE_IN_CANVAS, arguments));
            Assert.IsInstanceOfType(expectedException.InnerException, typeof(ArgumentNullException));
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_CANVAS_WIDTH, 1.0);
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_CANVAS_HEIGHT, 2.0);
            arguments = new object[] { new Point(1.0, 2.0) };
            Assert.IsTrue(( bool )_target.Invoke(MEMBER_FUNCTION_NAME_IS_INCLUSIVE_IN_CANVAS, arguments));
            arguments = new object[] { new Point(0.0, 0.0) };
            Assert.IsTrue(( bool )_target.Invoke(MEMBER_FUNCTION_NAME_IS_INCLUSIVE_IN_CANVAS, arguments));
            arguments = new object[] { new Point(-0.1, 0.0) };
            Assert.IsFalse(( bool )_target.Invoke(MEMBER_FUNCTION_NAME_IS_INCLUSIVE_IN_CANVAS, arguments));
            arguments = new object[] { new Point(0.0, -0.1) };
            Assert.IsFalse(( bool )_target.Invoke(MEMBER_FUNCTION_NAME_IS_INCLUSIVE_IN_CANVAS, arguments));
            arguments = new object[] { new Point(-0.1, 2.1) };
            Assert.IsFalse(( bool )_target.Invoke(MEMBER_FUNCTION_NAME_IS_INCLUSIVE_IN_CANVAS, arguments));
        }

        /// <summary>
        /// Tests the refresh draw canvas.
        /// </summary>
        [TestMethod()]
        public void TestRefreshDrawCanvas()
        {
            _canvasManager.RefreshDrawCanvas(new GraphicsMock());
            Assert.IsTrue(_canvasDrawer.IsCalledRefreshDrawCanvas);
        }
    }
}