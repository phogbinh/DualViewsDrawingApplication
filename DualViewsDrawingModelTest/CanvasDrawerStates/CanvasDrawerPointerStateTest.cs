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
        private CanvasDrawerPointerState _canvasDrawerPointerState;
        private CanvasDrawerMock _canvasDrawer;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            _canvasDrawer = new CanvasDrawerMock(new CommandsManager());
            _canvasDrawerPointerState = new CanvasDrawerPointerState(_canvasDrawer);
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
        }

        /// <summary>
        /// Tests the clear canvas.
        /// </summary>
        [TestMethod()]
        public void TestClearCanvas()
        {
            _canvasDrawerPointerState.ClearCanvas();
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
            Assert.IsTrue(true);
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
    }
}