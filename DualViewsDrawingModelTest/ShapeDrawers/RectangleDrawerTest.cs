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
        /// Tests the get rectangle.
        /// </summary>
        [TestMethod()]
        public void TestGetRectangle()
        {
            const string MEMBER_FUNCTION_NAME_GET_RECTANGLE = "GetRectangle";
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_DRAWING_STARTING_POINT, new Point(1.0, 5.0));
            _rectangleDrawer.DrawingEndingPoint = new Point(-1.0, 2.0);
            Rectangle expectedRectangle = ( Rectangle )_target.Invoke(MEMBER_FUNCTION_NAME_GET_RECTANGLE);
            Assert.AreEqual(expectedRectangle.X, -1.0);
            Assert.AreEqual(expectedRectangle.Y, 2.0);
            Assert.AreEqual(expectedRectangle.Width, 2.0);
            Assert.AreEqual(expectedRectangle.Height, 3.0);
        }
    }
}