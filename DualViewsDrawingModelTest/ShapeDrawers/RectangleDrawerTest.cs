using DualViewsDrawingModel.Shapes;
using DualViewsDrawingModelTest;
using DualViewsDrawingModelTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
        /// Tests the get close point detector.
        /// </summary>
        [TestMethod()]
        public void TestGetClosePointDetector()
        {
            Assert.IsInstanceOfType(_rectangleDrawer.GetClosePointDetector(), typeof(Rectangle));
        }
    }
}