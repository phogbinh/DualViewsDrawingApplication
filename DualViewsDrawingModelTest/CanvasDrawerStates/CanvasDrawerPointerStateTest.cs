using DualViewsDrawingModel.ShapeDrawers;
using DualViewsDrawingModelTest;
using DualViewsDrawingModelTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DualViewsDrawingModel.CanvasDrawerStates.Test
{
    [TestClass()]
    public class CanvasDrawerPointerStateTest
    {
        private const string MEMBER_VARIABLE_NAME_CANVAS_DRAWER = "_canvasDrawer";
        private const string MEMBER_VARIABLE_NAME_CURRENT_SELECTED_SHAPE_SHAPE_DRAWER = "_currentSelectedShapeShapeDrawer";
        private CanvasDrawerMock _canvasDrawer;
        private CanvasDrawerPointerState _canvasDrawerPointerState;
        private PrivateObject _target;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            _canvasDrawer = new CanvasDrawerMock(new CommandsManager());
            _canvasDrawerPointerState = new CanvasDrawerPointerState(_canvasDrawer);
            _target = new PrivateObject(_canvasDrawerPointerState);
        }

        /// <summary>
        /// Tests the state of the canvas drawer pointer.
        /// </summary>
        [TestMethod()]
        public void TestCanvasDrawerPointerState()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new CanvasDrawerPointerState(null));
            var canvasDrawer = new CanvasDrawerMock(new CommandsManager());
            var canvasDrawerPointerState = new CanvasDrawerPointerState(canvasDrawer);
            var target = new PrivateObject(canvasDrawerPointerState);
            Assert.AreSame(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_CANVAS_DRAWER), canvasDrawer);
            Assert.IsNull(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_SELECTED_SHAPE_SHAPE_DRAWER));
        }

        /// <summary>
        /// Tests the clear canvas.
        /// </summary>
        [TestMethod()]
        public void TestClearCanvas()
        {
            _canvasDrawerPointerState.ClearCanvas();
            Assert.IsNull(_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_SELECTED_SHAPE_SHAPE_DRAWER));
            Assert.IsTrue(_canvasDrawer.IsCalledNotifyCurrentShapeChanged);
            Assert.IsTrue(_canvasDrawer.IsCalledClearShapeDrawersManager);
            Assert.IsTrue(_canvasDrawer.IsCalledNotifyCanvasRefreshDrawRequested);
        }

        /// <summary>
        /// Tests the handle canvas left mouse pressed.
        /// </summary>
        [TestMethod()]
        public void TestHandleCanvasLeftMousePressed()
        {
            _canvasDrawer.SetCurrentShapeDrawerType(ShapeDrawerType.None);
            _canvasDrawerPointerState.HandleCanvasLeftMousePressed(new Point());
            Assert.IsTrue(_canvasDrawer.IsCalledGetSelectedShapeShapeDrawer);
            Assert.IsTrue(_canvasDrawer.IsCalledNotifyCurrentShapeChanged);
            _canvasDrawer.SetCurrentShapeDrawerType(ShapeDrawerType.Line);
            _canvasDrawerPointerState.HandleCanvasLeftMousePressed(new Point());
            Assert.IsTrue(_canvasDrawer.IsCalledSetCurrentState);
            Assert.IsInstanceOfType(_canvasDrawer.CurrentState, typeof(CanvasDrawerDrawingState));
            Assert.IsTrue(_canvasDrawer.IsCalledNotifyCurrentShapeChanged);
        }

        /// <summary>
        /// Tests the state of the go to resizing.
        /// </summary>
        [TestMethod()]
        public void TestGoToResizingState()
        {
            const string MEMBER_FUNCTION_NAME_GO_TO_RESIZING_STATE = "GoToResizingState";
            var arguments = new object[] { new ShapeDrawerMock(new Point(), new Point()) };
            _target.Invoke(MEMBER_FUNCTION_NAME_GO_TO_RESIZING_STATE, arguments);
            Assert.IsTrue(_canvasDrawer.IsCalledSetCurrentState);
            Assert.IsInstanceOfType(_canvasDrawer.CurrentState, typeof(CanvasDrawerResizingState));
            Assert.IsTrue(_canvasDrawer.IsCalledNotifyCurrentShapeChanged);
        }

        /// <summary>
        /// Tests the select shape.
        /// </summary>
        [TestMethod()]
        public void TestSelectShape()
        {
            const string MEMBER_FUNCTION_NAME_SELECT_SHAPE = "SelectShape";
            _canvasDrawer.SetCurrentShapeDrawerType(ShapeDrawerType.None);
            var arguments = new object[] { new Point() };
            _target.Invoke(MEMBER_FUNCTION_NAME_SELECT_SHAPE, arguments);
            Assert.IsTrue(_canvasDrawer.IsCalledGetSelectedShapeShapeDrawer);
            Assert.IsTrue(_canvasDrawer.IsCalledNotifyCurrentShapeChanged);
        }

        /// <summary>
        /// Tests the state of the go to drawing.
        /// </summary>
        [TestMethod()]
        public void TestGoToDrawingState()
        {
            const string MEMBER_FUNCTION_NAME_GO_TO_DRAWING_STATE = "GoToDrawingState";
            _canvasDrawer.SetCurrentShapeDrawerType(ShapeDrawerType.Rectangle);
            var arguments = new object[] { new Point() };
            _target.Invoke(MEMBER_FUNCTION_NAME_GO_TO_DRAWING_STATE, arguments);
            Assert.IsTrue(_canvasDrawer.IsCalledSetCurrentState);
            Assert.IsInstanceOfType(_canvasDrawer.CurrentState, typeof(CanvasDrawerDrawingState));
            Assert.IsTrue(_canvasDrawer.IsCalledNotifyCurrentShapeChanged);
        }

        /// <summary>
        /// Tests the handle canvas left mouse moved.
        /// </summary>
        [TestMethod()]
        public void TestHandleCanvasLeftMouseMoved()
        {
            _canvasDrawerPointerState.HandleCanvasLeftMouseMoved(new Point());
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Tests the handle canvas left mouse released.
        /// </summary>
        [TestMethod()]
        public void TestHandleCanvasLeftMouseReleased()
        {
            _canvasDrawerPointerState.HandleCanvasLeftMouseReleased(new Point());
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Tests the draw.
        /// </summary>
        [TestMethod()]
        public void TestDraw()
        {
            _canvasDrawerPointerState.Draw(new GraphicsMock());
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Tests the get current shape rectangle.
        /// </summary>
        [TestMethod()]
        public void TestGetCurrentShapeRectangle()
        {
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_SELECTED_SHAPE_SHAPE_DRAWER, null);
            Assert.IsNull(_canvasDrawerPointerState.GetCurrentShapeRectangle());
            var shapeDrawer = new ShapeDrawerMock(new Point(), new Point());
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_SELECTED_SHAPE_SHAPE_DRAWER, shapeDrawer);
            _canvasDrawerPointerState.GetCurrentShapeRectangle();
            Assert.IsTrue(shapeDrawer.IsCalledGetRectangle);
        }

        /// <summary>
        /// Tests the type of the get current shape.
        /// </summary>
        [TestMethod()]
        public void TestGetCurrentShapeType()
        {
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_SELECTED_SHAPE_SHAPE_DRAWER, null);
            Assert.AreEqual(_canvasDrawerPointerState.GetCurrentShapeType(), ShapeDrawerType.None);
            var lineDrawer = new LineDrawer(new Point(), new Point());
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_SELECTED_SHAPE_SHAPE_DRAWER, lineDrawer);
            Assert.AreEqual(_canvasDrawerPointerState.GetCurrentShapeType(), ShapeDrawerType.Line);
            var rectangleDrawer = new RectangleDrawer(new Point(), new Point());
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_SELECTED_SHAPE_SHAPE_DRAWER, rectangleDrawer);
            Assert.AreEqual(_canvasDrawerPointerState.GetCurrentShapeType(), ShapeDrawerType.Rectangle);
        }
    }
}