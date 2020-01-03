using DualViewsDrawingModel.Shapes;
using DualViewsDrawingModelTest;
using DualViewsDrawingModelTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DualViewsDrawingModel.ShapeDrawers.Test
{
    [TestClass()]
    public class RectangleDrawerTest
    {
        private const string MEMBER_VARIABLE_NAME_TYPE = "_type";
        private const string MEMBER_VARIABLE_NAME_DRAWING_STARTING_POINT = "_drawingStartingPoint";
        private const string MEMBER_VARIABLE_NAME_DRAWING_ENDING_POINT = "_drawingEndingPoint";
        private RectangleDrawer _rectangleDrawer;
        private PrivateObject _target;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            _rectangleDrawer = new RectangleDrawer(new Point(), new Point());
            _target = new PrivateObject(_rectangleDrawer);
        }

        /// <summary>
        /// Tests the rectangle drawer.
        /// </summary>
        [TestMethod()]
        public void TestRectangleDrawer()
        {
            var drawingStartingPoint = new Point();
            var drawingEndingPoint = new Point();
            var rectangleDrawer = new RectangleDrawer(drawingStartingPoint, drawingEndingPoint);
            var target = new PrivateObject(rectangleDrawer);
            Assert.AreEqual(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_TYPE), ShapeDrawerType.Rectangle);
            Assert.AreSame(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_DRAWING_STARTING_POINT), drawingStartingPoint);
            Assert.AreSame(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_DRAWING_ENDING_POINT), drawingEndingPoint);
        }

        /// <summary>
        /// Tests the draw.
        /// </summary>
        [TestMethod()]
        public void TestDraw()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _rectangleDrawer.Draw(null));
            var graphics = new GraphicsMock();
            _rectangleDrawer.Draw(graphics);
            Assert.IsTrue(graphics.IsCalledDrawRectangle);
        }

        /// <summary>
        /// Tests the draw selection border.
        /// </summary>
        [TestMethod()]
        public void TestDrawSelectionBorder()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _rectangleDrawer.DrawSelectionBorder(null));
            var graphics = new GraphicsMock();
            _rectangleDrawer.DrawSelectionBorder(graphics);
            Assert.IsTrue(graphics.IsCalledDrawSelectionBorderRectangle);
        }

        /// <summary>
        /// Tests the get close point detector.
        /// </summary>
        [TestMethod()]
        public void TestGetClosePointDetector()
        {
            Assert.IsInstanceOfType(_rectangleDrawer.GetClosePointDetector(), typeof(Rectangle));
        }

        /// <summary>
        /// Tests the get corner points.
        /// </summary>
        [TestMethod()]
        public void TestGetCornerPoints()
        {
            var drawingStartingPoint = new Point(0.0, 0.1);
            var drawingEndingPoint = new Point(1.0, 1.1);
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_DRAWING_STARTING_POINT, drawingStartingPoint);
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_DRAWING_ENDING_POINT, drawingEndingPoint);
            List<Point> expectedCornerPoints = _rectangleDrawer.GetCornerPoints();
            Assert.AreEqual(expectedCornerPoints[ 0 ].X, 0.0);
            Assert.AreEqual(expectedCornerPoints[ 0 ].Y, 0.1);
            Assert.AreEqual(expectedCornerPoints[ 1 ].X, 1.0);
            Assert.AreEqual(expectedCornerPoints[ 1 ].Y, 0.1);
            Assert.AreEqual(expectedCornerPoints[ 2 ].X, 1.0);
            Assert.AreEqual(expectedCornerPoints[ 2 ].Y, 1.1);
            Assert.AreEqual(expectedCornerPoints[ 3 ].X, 0.0);
            Assert.AreEqual(expectedCornerPoints[ 3 ].Y, 1.1);
        }
    }
}