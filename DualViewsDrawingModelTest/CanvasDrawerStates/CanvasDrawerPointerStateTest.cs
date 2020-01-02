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
            Assert.IsTrue(canvasDrawer.IsCalledNotifyCurrentShapeChanged);
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
    }
}