using DualViewsDrawingModel.ShapeDrawers;
using DualViewsDrawingModelTest;
using DualViewsDrawingModelTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DualViewsDrawingModel.Test
{
    [TestClass()]
    public class CanvasDrawerTest
    {
        private const string MEMBER_VARIABLE_NAME_CURRENT_SHAPE_DRAWER_TYPE = "_currentShapeDrawerType";
        private const string MEMBER_VARIABLE_NAME_IS_DRAWING = "_isDrawing";
        private const string MEMBER_VARIABLE_NAME_CURRENT_DRAWING_SHAPE_DRAWING_STARTING_POINT = "_currentDrawingShapeDrawingStartingPoint";
        private const string MEMBER_VARIABLE_NAME_CURRENT_DRAWING_SHAPE_HINT_SHAPE_DRAWER = "_currentDrawingShapeHintShapeDrawer";
        private const string MEMBER_VARIABLE_NAME_SHAPE_DRAWERS_MANAGER = "_shapeDrawersManager";
        private CanvasDrawer _canvasDrawer;
        private PrivateObject _target;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            _canvasDrawer = new CanvasDrawer();
            _target = new PrivateObject(_canvasDrawer);
        }

        /// <summary>
        /// Sets the type of the current shape drawer.
        /// </summary>
        private void SetCurrentShapeDrawerType(ShapeDrawerType value)
        {
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_SHAPE_DRAWER_TYPE, value);
        }

        /// <summary>
        /// Sets the is drawing.
        /// </summary>
        private void SetIsDrawing(bool value)
        {
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_IS_DRAWING, value);
        }

        /// <summary>
        /// Sets the current drawing shape drawing starting point.
        /// </summary>
        private void SetCurrentDrawingShapeDrawingStartingPoint(Point value)
        {
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_DRAWING_SHAPE_DRAWING_STARTING_POINT, value);
        }

        /// <summary>
        /// Sets the current drawing shape hint shape drawer.
        /// </summary>
        private void SetCurrentDrawingShapeHintShapeDrawer(ShapeDrawer value)
        {
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_DRAWING_SHAPE_HINT_SHAPE_DRAWER, value);
        }

        /// <summary>
        /// Sets the shape drawers manager.
        /// </summary>
        private void SetShapeDrawersManager(ShapeDrawersManager value)
        {
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_SHAPE_DRAWERS_MANAGER, value);
        }

        /// <summary>
        /// Tests the canvas drawer.
        /// </summary>
        [TestMethod()]
        public void TestCanvasDrawer()
        {
            var canvasDrawer = new CanvasDrawer();
            var target = new PrivateObject(canvasDrawer);
            Assert.IsNotNull(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_SHAPE_DRAWERS_MANAGER));
        }

        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestMethod()]
        public void TestInitialize()
        {
            PrivateObject shapeDrawersManagerTarget = PrepareTestClearCanvas();
            int count = 0;
            _canvasDrawer.CanvasRefreshDrawRequested += () => count++;
            SetCurrentShapeDrawerType(ShapeDrawerType.Line);
            _canvasDrawer.Initialize(ShapeDrawerType.Rectangle);
            AssertTestClearCanvas(shapeDrawersManagerTarget);
            Assert.AreEqual(count, 1);
            Assert.AreEqual(( ShapeDrawerType )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_SHAPE_DRAWER_TYPE), ShapeDrawerType.Rectangle); // The only different assert from that of TestClearCanvas.
        }

        /// <summary>
        /// Tests the type of the set current shape drawer.
        /// </summary>
        [TestMethod()]
        public void TestSetCurrentShapeDrawerType()
        {
            Assert.ThrowsException<ArgumentException>(() => _canvasDrawer.SetCurrentShapeDrawerType(( ShapeDrawerType )( -1 )));
            Assert.ThrowsException<ArgumentException>(() => _canvasDrawer.SetCurrentShapeDrawerType(( ShapeDrawerType )3));
            SetCurrentShapeDrawerType(ShapeDrawerType.None);
            _canvasDrawer.SetCurrentShapeDrawerType(ShapeDrawerType.Line);
            Assert.AreEqual(( ShapeDrawerType )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_SHAPE_DRAWER_TYPE), ShapeDrawerType.Line);
        }

        /// <summary>
        /// Tests the clear canvas.
        /// </summary>
        [TestMethod()]
        public void TestClearCanvas()
        {
            PrivateObject shapeDrawersManagerTarget = PrepareTestClearCanvas();
            int count = 0;
            _canvasDrawer.CanvasRefreshDrawRequested += () => count++;
            _canvasDrawer.ClearCanvas();
            AssertTestClearCanvas(shapeDrawersManagerTarget);
            Assert.AreEqual(count, 1);
        }

        /// <summary>
        /// Prepares the test clear canvas.
        /// </summary>
        private PrivateObject PrepareTestClearCanvas()
        {
            SetIsDrawing(true);
            SetCurrentDrawingShapeDrawingStartingPoint(new Point());
            SetCurrentDrawingShapeHintShapeDrawer(new LineDrawer(new Point(), new Point()));
            var shapeDrawersManager = new ShapeDrawersManager();
            shapeDrawersManager.AddShapeDrawer(new Point(), new Point(), ShapeDrawerType.Rectangle);
            SetShapeDrawersManager(shapeDrawersManager);
            return new PrivateObject(shapeDrawersManager);
        }

        /// <summary>
        /// Asserts the test clear canvas.
        /// </summary>
        private void AssertTestClearCanvas(PrivateObject shapeDrawersManagerTarget)
        {
            const string SHAPE_DRAWERS_MANAGER_MEMBER_VARIABLE_NAME_SHAPE_DRAWERS = "_shapeDrawers";
            Assert.IsFalse(( bool )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_IS_DRAWING));
            Assert.IsNull(( Point )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_DRAWING_SHAPE_DRAWING_STARTING_POINT));
            Assert.IsNull(( ShapeDrawer )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_DRAWING_SHAPE_HINT_SHAPE_DRAWER));
            Assert.AreEqual(( ( List<ShapeDrawer> )shapeDrawersManagerTarget.GetFieldOrProperty(SHAPE_DRAWERS_MANAGER_MEMBER_VARIABLE_NAME_SHAPE_DRAWERS) ).Count, 0);
        }

        /// <summary>
        /// Tests the notify canvas refresh draw requested.
        /// </summary>
        [TestMethod()]
        public void TestNotifyCanvasRefreshDrawRequested()
        {
            const string MEMBER_FUNCTION_NAME_NOTIFY_CANVAS_REFRESH_DRAW_REQUESTED = "NotifyCanvasRefreshDrawRequested";
            int count = 0;
            _canvasDrawer.CanvasRefreshDrawRequested += () => count++;
            _target.Invoke(MEMBER_FUNCTION_NAME_NOTIFY_CANVAS_REFRESH_DRAW_REQUESTED);
            Assert.AreEqual(count, 1);
            _target.Invoke(MEMBER_FUNCTION_NAME_NOTIFY_CANVAS_REFRESH_DRAW_REQUESTED);
            Assert.AreEqual(count, 2);
        }

        /// <summary>
        /// Tests the handle canvas left mouse pressed.
        /// </summary>
        [TestMethod()]
        public void TestHandleCanvasLeftMousePressed()
        {
            SetIsDrawing(false);
            SetCurrentShapeDrawerType(ShapeDrawerType.None);
            _canvasDrawer.HandleCanvasLeftMousePressed(new Point());
            Assert.IsFalse(( bool )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_IS_DRAWING));
            Point mousePosition = new Point();
            PrepareTestBeginDrawing();
            _canvasDrawer.HandleCanvasLeftMousePressed(mousePosition);
            AssertTestBeginDrawing(mousePosition);
        }

        /// <summary>
        /// Tests the begin drawing.
        /// </summary>
        [TestMethod()]
        public void TestBeginDrawing()
        {
            const string MEMBER_FUNCTION_NAME_BEGIN_DRAWING = "BeginDrawing";
            var arguments = new object[] { null };
            TargetInvocationException expectedException = Assert.ThrowsException<TargetInvocationException>(() => _target.Invoke(MEMBER_FUNCTION_NAME_BEGIN_DRAWING, arguments));
            Assert.IsInstanceOfType(expectedException.InnerException, typeof(ArgumentNullException));
            SetIsDrawing(true);
            arguments = new object[] { new Point() };
            expectedException = Assert.ThrowsException<TargetInvocationException>(() => _target.Invoke(MEMBER_FUNCTION_NAME_BEGIN_DRAWING, arguments));
            Assert.IsInstanceOfType(expectedException.InnerException, typeof(ApplicationException));
            Point mousePosition = new Point();
            PrepareTestBeginDrawing();
            arguments = new object[] { mousePosition };
            _target.Invoke(MEMBER_FUNCTION_NAME_BEGIN_DRAWING, arguments);
            AssertTestBeginDrawing(mousePosition);
        }

        /// <summary>
        /// Prepares the test begin drawing.
        /// </summary>
        private void PrepareTestBeginDrawing()
        {
            SetIsDrawing(false);
            SetCurrentShapeDrawerType(ShapeDrawerType.Rectangle);
        }

        /// <summary>
        /// Asserts the test begin drawing.
        /// </summary>
        private void AssertTestBeginDrawing(Point mousePosition)
        {
            Assert.IsTrue(( bool )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_IS_DRAWING));
            Assert.AreSame(_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_DRAWING_SHAPE_DRAWING_STARTING_POINT), mousePosition);
            Assert.IsInstanceOfType(_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_DRAWING_SHAPE_HINT_SHAPE_DRAWER), typeof(RectangleDrawer));
        }

        /// <summary>
        /// Tests the handle canvas left mouse moved.
        /// </summary>
        [TestMethod()]
        public void TestHandleCanvasLeftMouseMoved()
        {
            var oldDrawingEndingPoint = new Point();
            SetCurrentDrawingShapeHintShapeDrawer(new RectangleDrawer(new Point(), oldDrawingEndingPoint));
            SetIsDrawing(false);
            var mousePosition = new Point();
            _canvasDrawer.HandleCanvasLeftMouseMoved(mousePosition);
            AssertCurrentDrawingShapeHintShapeDrawerDrawingEndingPoint(oldDrawingEndingPoint);
            SetIsDrawing(true);
            int count = 0;
            _canvasDrawer.CanvasRefreshDrawRequested += () => count++;
            _canvasDrawer.HandleCanvasLeftMouseMoved(mousePosition);
            AssertCurrentDrawingShapeHintShapeDrawerDrawingEndingPoint(mousePosition);
            Assert.AreEqual(count, 1);
        }

        /// <summary>
        /// Tests the update current drawing shape hint.
        /// </summary>
        [TestMethod()]
        public void TestUpdateCurrentDrawingShapeHint()
        {
            const string MEMBER_FUNCTION_NAME_UPDATE_CURRENT_DRAWING_SHAPE_HINT = "UpdateCurrentDrawingShapeHint";

            var arguments = new object[] { null };
            TargetInvocationException expectedException = Assert.ThrowsException<TargetInvocationException>(() => _target.Invoke(MEMBER_FUNCTION_NAME_UPDATE_CURRENT_DRAWING_SHAPE_HINT, arguments));
            Assert.IsInstanceOfType(expectedException.InnerException, typeof(ArgumentNullException));
            SetCurrentDrawingShapeHintShapeDrawer(new RectangleDrawer(new Point(), new Point()));
            var mousePosition = new Point();
            int count = 0;
            _canvasDrawer.CanvasRefreshDrawRequested += () => count++;
            arguments = new object[] { mousePosition };
            _target.Invoke(MEMBER_FUNCTION_NAME_UPDATE_CURRENT_DRAWING_SHAPE_HINT, arguments);
            AssertCurrentDrawingShapeHintShapeDrawerDrawingEndingPoint(mousePosition);
            Assert.AreEqual(count, 1);
        }

        /// <summary>
        /// Asserts the current drawing shape hint shape drawer drawing ending point.
        /// </summary>
        private void AssertCurrentDrawingShapeHintShapeDrawerDrawingEndingPoint(Point mousePosition)
        {
            const string SHAPE_DRAWER_MEMBER_VARIABLE_NAME_DRAWING_ENDING_POINT = "_drawingEndingPoint";
            ShapeDrawer expectedCurrentDrawingShapeHintShapeDrawer = ( ShapeDrawer )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_CURRENT_DRAWING_SHAPE_HINT_SHAPE_DRAWER);
            var target = new PrivateObject(expectedCurrentDrawingShapeHintShapeDrawer);
            Assert.AreSame(target.GetFieldOrProperty(SHAPE_DRAWER_MEMBER_VARIABLE_NAME_DRAWING_ENDING_POINT), mousePosition);
        }

        /// <summary>
        /// Tests the handle canvas left mouse released.
        /// </summary>
        [TestMethod()]
        public void TestHandleCanvasLeftMouseReleased()
        {
            // Negative test.
            const string SHAPE_DRAWERS_MANAGER_MEMBER_VARIABLE_NAME_SHAPE_DRAWERS = "_shapeDrawers";
            SetIsDrawing(false);
            var shapeDrawersManager = new ShapeDrawersManager();
            var shapeDrawersManagerTarget = new PrivateObject(shapeDrawersManager);
            shapeDrawersManager.AddShapeDrawer(new Point(), new Point(), ShapeDrawerType.Rectangle);
            SetShapeDrawersManager(shapeDrawersManager);
            _canvasDrawer.HandleCanvasLeftMouseReleased(new Point());
            Assert.AreEqual(( ( List<ShapeDrawer> )shapeDrawersManagerTarget.GetFieldOrProperty(SHAPE_DRAWERS_MANAGER_MEMBER_VARIABLE_NAME_SHAPE_DRAWERS) ).Count, 1);
            // Positive test.
            shapeDrawersManagerTarget = PrepareTestEndDrawing();
            int count = 0;
            _canvasDrawer.CanvasRefreshDrawRequested += () => count++;
            _canvasDrawer.HandleCanvasLeftMouseReleased(new Point());
            AssertTestEndDrawing(shapeDrawersManagerTarget);
            Assert.AreEqual(count, 1);
        }

        /// <summary>
        /// Tests the end drawing.
        /// </summary>
        [TestMethod()]
        public void TestEndDrawing()
        {
            const string MEMBER_FUNCTION_NAME_END_DRAWING = "EndDrawing";
            PrivateObject shapeDrawersManagerTarget = PrepareTestEndDrawing();
            int count = 0;
            _canvasDrawer.CanvasRefreshDrawRequested += () => count++;
            var arguments = new object[] { new Point() };
            _target.Invoke(MEMBER_FUNCTION_NAME_END_DRAWING, arguments);
            AssertTestEndDrawing(shapeDrawersManagerTarget);
            Assert.AreEqual(count, 1);
        }

        /// <summary>
        /// Prepares the test end drawing.
        /// </summary>
        private PrivateObject PrepareTestEndDrawing()
        {
            var shapeDrawersManager = new ShapeDrawersManager();
            shapeDrawersManager.AddShapeDrawer(new Point(), new Point(), ShapeDrawerType.Rectangle);
            SetShapeDrawersManager(shapeDrawersManager);
            SetCurrentDrawingShapeDrawingStartingPoint(new Point());
            SetCurrentShapeDrawerType(ShapeDrawerType.Line);
            SetIsDrawing(true);
            return new PrivateObject(shapeDrawersManager);
        }

        /// <summary>
        /// Asserts the test end drawing.
        /// </summary>
        private void AssertTestEndDrawing(PrivateObject shapeDrawersManagerTarget)
        {
            const string SHAPE_DRAWERS_MANAGER_MEMBER_VARIABLE_NAME_SHAPE_DRAWERS = "_shapeDrawers";
            List<ShapeDrawer> expectedShapeDrawers = ( List<ShapeDrawer> )shapeDrawersManagerTarget.GetFieldOrProperty(SHAPE_DRAWERS_MANAGER_MEMBER_VARIABLE_NAME_SHAPE_DRAWERS);
            Assert.AreEqual(expectedShapeDrawers.Count, 2);
            Assert.IsInstanceOfType(expectedShapeDrawers[ 1 ], typeof(LineDrawer));
            Assert.IsFalse(( bool )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_IS_DRAWING));
        }

        /// <summary>
        /// Tests the refresh draw canvas.
        /// </summary>
        [TestMethod()]
        public void TestRefreshDrawCanvas()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _canvasDrawer.RefreshDrawCanvas(null));
            PrepareTestDraw();
            var graphics = new GraphicsMock();
            _canvasDrawer.RefreshDrawCanvas(graphics);
            Assert.IsTrue(graphics.IsCalledClearAll);
            AssertTestDraw(graphics);
        }

        /// <summary>
        /// Tests the draw.
        /// </summary>
        [TestMethod()]
        public void TestDraw()
        {
            const string MEMBER_FUNCTION_NAME_DRAW = "Draw";
            PrepareTestDraw();
            var graphics = new GraphicsMock();
            var arguments = new object[] { graphics };
            _target.Invoke(MEMBER_FUNCTION_NAME_DRAW, arguments);
            AssertTestDraw(graphics);
        }

        /// <summary>
        /// Prepares the test draw.
        /// </summary>
        private void PrepareTestDraw()
        {
            SetIsDrawing(true);
            var shapeDrawersManager = new ShapeDrawersManager();
            shapeDrawersManager.AddShapeDrawer(new Point(), new Point(), ShapeDrawerType.Rectangle);
            SetShapeDrawersManager(shapeDrawersManager);
            SetCurrentDrawingShapeHintShapeDrawer(new LineDrawer(new Point(), new Point()));
        }

        /// <summary>
        /// Asserts the test draw.
        /// </summary>
        private void AssertTestDraw(GraphicsMock graphics)
        {
            Assert.IsTrue(graphics.IsCalledDrawLine);
            Assert.IsTrue(graphics.IsCalledDrawRectangle);
        }
    }
}