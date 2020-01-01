using DualViewsDrawingModel.CanvasDrawerStates;
using DualViewsDrawingModelTest;
using DualViewsDrawingModelTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DualViewsDrawingModel.Test
{
    [TestClass()]
    public class CanvasDrawerTest
    {
        private const string MEMBER_VARIABLE_NAME_CURRENT_SHAPE_DRAWER_TYPE = "_currentShapeDrawerType";
        private const string MEMBER_VARIABLE_NAME_CURRENT_STATE = "_currentState";
        private const string MEMBER_VARIABLE_NAME_CANVAS_SHAPE_DRAWERS_HELPER = "_canvasShapeDrawersHelper";
        private CanvasDrawer _canvasDrawer;
        private PrivateObject _target;
        private CanvasDrawerStateMock _currentState;
        private CanvasShapeDrawersHelperMock _canvasShapeDrawersHelper;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            _canvasDrawer = new CanvasDrawer();
            _target = new PrivateObject(_canvasDrawer);
            _currentState = new CanvasDrawerStateMock();
            _canvasShapeDrawersHelper = new CanvasShapeDrawersHelperMock();
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_STATE, _currentState);
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_CANVAS_SHAPE_DRAWERS_HELPER, _canvasShapeDrawersHelper);
        }

        /// <summary>
        /// Tests the canvas drawer.
        /// </summary>
        [TestMethod()]
        public void TestCanvasDrawer()
        {
            var canvasDrawer = new CanvasDrawer();
            var target = new PrivateObject(canvasDrawer);
            Assert.IsNotNull(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_CANVAS_SHAPE_DRAWERS_HELPER));
        }

        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestMethod()]
        public void TestInitialize()
        {
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_SHAPE_DRAWER_TYPE, ShapeDrawerType.Line);
            _canvasDrawer.Initialize(ShapeDrawerType.Rectangle);
            Assert.AreEqual(_canvasDrawer.CurrentShapeDrawerType, ShapeDrawerType.Rectangle);
            Assert.IsInstanceOfType(_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_STATE), typeof(CanvasDrawerPointerState));
            Assert.IsTrue(_canvasShapeDrawersHelper.IsCalledClear);
        }

        /// <summary>
        /// Tests the clear shape drawers manager.
        /// </summary>
        [TestMethod()]
        public void TestClearShapeDrawersManager()
        {
            _canvasDrawer.ClearShapeDrawersManager();
            Assert.IsTrue(_canvasShapeDrawersHelper.IsCalledClear);
        }

        /// <summary>
        /// Tests the type of the set current shape drawer.
        /// </summary>
        [TestMethod()]
        public void TestSetCurrentShapeDrawerType()
        {
            Assert.ThrowsException<ArgumentException>(() => _canvasDrawer.SetCurrentShapeDrawerType(( ShapeDrawerType )( -1 )));
            Assert.ThrowsException<ArgumentException>(() => _canvasDrawer.SetCurrentShapeDrawerType(( ShapeDrawerType )3));
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_SHAPE_DRAWER_TYPE, ShapeDrawerType.None);
            _canvasDrawer.SetCurrentShapeDrawerType(ShapeDrawerType.Line);
            Assert.AreEqual(( ShapeDrawerType )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_SHAPE_DRAWER_TYPE), ShapeDrawerType.Line);
        }

        /// <summary>
        /// Tests the clear canvas.
        /// </summary>
        [TestMethod()]
        public void TestClearCanvas()
        {
            _canvasDrawer.ClearCanvas();
            Assert.IsTrue(_currentState.IsCalledClearCanvas);
        }

        /// <summary>
        /// Tests the handle canvas left mouse pressed.
        /// </summary>
        [TestMethod()]
        public void TestHandleCanvasLeftMousePressed()
        {
            _canvasDrawer.HandleCanvasLeftMousePressed(new Point());
            Assert.IsTrue(_currentState.IsCalledHandleCanvasLeftMousePressed);
        }

        /// <summary>
        /// Tests the handle canvas left mouse moved.
        /// </summary>
        [TestMethod()]
        public void TestHandleCanvasLeftMouseMoved()
        {
            _canvasDrawer.HandleCanvasLeftMouseMoved(new Point());
            Assert.IsTrue(_currentState.IsCalledHandleCanvasLeftMouseMoved);
        }

        /// <summary>
        /// Tests the handle canvas left mouse released.
        /// </summary>
        [TestMethod()]
        public void TestHandleCanvasLeftMouseReleased()
        {
            _canvasDrawer.HandleCanvasLeftMouseReleased(new Point());
            Assert.IsTrue(_currentState.IsCalledHandleCanvasLeftMouseReleased);
        }

        /// <summary>
        /// Tests the refresh draw canvas.
        /// </summary>
        [TestMethod()]
        public void TestRefreshDrawCanvas()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _canvasDrawer.RefreshDrawCanvas(null));
            var graphics = new GraphicsMock();
            _canvasDrawer.RefreshDrawCanvas(graphics);
            Assert.IsTrue(graphics.IsCalledClearAll);
            Assert.IsTrue(_canvasShapeDrawersHelper.IsCalledDraw);
            Assert.IsTrue(_currentState.IsCalledDraw);
        }

        /// <summary>
        /// Tests the draw.
        /// </summary>
        [TestMethod()]
        public void TestDraw()
        {
            const string MEMBER_FUNCTION_NAME_DRAW = "Draw";
            var arguments = new object[] { new GraphicsMock() };
            _target.Invoke(MEMBER_FUNCTION_NAME_DRAW, arguments);
            Assert.IsTrue(_canvasShapeDrawersHelper.IsCalledDraw);
            Assert.IsTrue(_currentState.IsCalledDraw);
        }

        /// <summary>
        /// Tests the state of the set current.
        /// </summary>
        [TestMethod()]
        public void TestSetCurrentState()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _canvasDrawer.SetCurrentState(null));
            var currentState = new CanvasDrawerPointerState(_canvasDrawer);
            _canvasDrawer.SetCurrentState(currentState);
            Assert.AreSame(_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_STATE), currentState);
        }

        /// <summary>
        /// Tests the add current shape drawer.
        /// </summary>
        [TestMethod()]
        public void TestAddCurrentShapeDrawer()
        {
            _canvasDrawer.AddCurrentShapeDrawer(new Point(), new Point());
            Assert.IsTrue(_canvasShapeDrawersHelper.IsCalledAddShapeDrawer);
        }

        /// <summary>
        /// Tests the notify canvas refresh draw requested.
        /// </summary>
        [TestMethod()]
        public void TestNotifyCanvasRefreshDrawRequested()
        {
            int count = 0;
            _canvasDrawer.CanvasRefreshDrawRequested += () => count++;
            _canvasDrawer.NotifyCanvasRefreshDrawRequested();
            Assert.AreEqual(count, 1);
            _canvasDrawer.NotifyCanvasRefreshDrawRequested();
            Assert.AreEqual(count, 2);
        }

        /// <summary>
        /// Tests the notify drawing ended.
        /// </summary>
        [TestMethod()]
        public void TestNotifyDrawingEnded()
        {
            int count = 0;
            _canvasDrawer.DrawingEnded += () => count++;
            _canvasDrawer.NotifyDrawingEnded();
            Assert.AreEqual(count, 1);
            _canvasDrawer.NotifyDrawingEnded();
            Assert.AreEqual(count, 2);
        }
    }
}