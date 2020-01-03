using DualViewsDrawingModel.Shapes;
using DualViewsDrawingModelTest;
using DualViewsDrawingModelTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DualViewsDrawingModel.ShapeDrawers.Test
{
    [TestClass()]
    public class ShapeDrawerTest
    {
        private const string MEMBER_VARIABLE_NAME_DRAWING_STARTING_POINT = "_drawingStartingPoint";
        private const string MEMBER_VARIABLE_NAME_DRAWING_ENDING_POINT = "_drawingEndingPoint";
        private ShapeDrawerMock _shapeDrawer;
        private PrivateObject _target;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            _shapeDrawer = new ShapeDrawerMock(new Point(), new Point());
            _target = new PrivateObject(_shapeDrawer);
        }

        /// <summary>
        /// Tests the shape drawer.
        /// </summary>
        [TestMethod()]
        public void TestShapeDrawer()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ShapeDrawerMock(null, new Point()));
            Assert.ThrowsException<ArgumentNullException>(() => new ShapeDrawerMock(new Point(), null));
            var drawingStartingPoint = new Point();
            var drawingEndingPoint = new Point();
            var shapeDrawer = new ShapeDrawerMock(drawingStartingPoint, drawingEndingPoint);
            var target = new PrivateObject(shapeDrawer);
            Assert.AreSame(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_DRAWING_STARTING_POINT), drawingStartingPoint);
            Assert.AreSame(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_DRAWING_ENDING_POINT), drawingEndingPoint);
        }

        /// <summary>
        /// Tests the is close to point.
        /// </summary>
        [TestMethod()]
        public void TestIsCloseToPoint()
        {
            var rectangleDrawer = new RectangleDrawer(new Point(1.0, 5.0), new Point(-1.0, 2.0));
            Assert.IsTrue(rectangleDrawer.IsCloseToPoint(new Point(0.5, 3.0), Definitions.MOUSE_POSITION_TO_SELECTION_SHAPE_MAXIMUM_DISTANCE_SQUARED));
            Assert.IsFalse(rectangleDrawer.IsCloseToPoint(new Point(-1.1, 2.0), Definitions.MOUSE_POSITION_TO_SELECTION_SHAPE_MAXIMUM_DISTANCE_SQUARED));
            Assert.IsFalse(rectangleDrawer.IsCloseToPoint(new Point(1.1, 2.0), Definitions.MOUSE_POSITION_TO_SELECTION_SHAPE_MAXIMUM_DISTANCE_SQUARED));
            Assert.IsFalse(rectangleDrawer.IsCloseToPoint(new Point(-1.0, 1.9), Definitions.MOUSE_POSITION_TO_SELECTION_SHAPE_MAXIMUM_DISTANCE_SQUARED));
            Assert.IsFalse(rectangleDrawer.IsCloseToPoint(new Point(-1.0, 5.1), Definitions.MOUSE_POSITION_TO_SELECTION_SHAPE_MAXIMUM_DISTANCE_SQUARED));
            var lineDrawer = new LineDrawer(new Point(1.0, 5.0), new Point(-1.0, 2.0));
            Assert.IsTrue(lineDrawer.IsCloseToPoint(new Point(0.0, 3.0), Definitions.MOUSE_POSITION_TO_SELECTION_SHAPE_MAXIMUM_DISTANCE_SQUARED));
            Assert.IsTrue(lineDrawer.IsCloseToPoint(new Point(0.5, 3.0), Definitions.MOUSE_POSITION_TO_SELECTION_SHAPE_MAXIMUM_DISTANCE_SQUARED));
            Assert.IsFalse(lineDrawer.IsCloseToPoint(new Point(-2.0, 2.0), Definitions.MOUSE_POSITION_TO_SELECTION_SHAPE_MAXIMUM_DISTANCE_SQUARED));
            Assert.IsTrue(lineDrawer.IsCloseToPoint(new Point(0.0, 2.0), Definitions.MOUSE_POSITION_TO_SELECTION_SHAPE_MAXIMUM_DISTANCE_SQUARED));
            Assert.IsFalse(lineDrawer.IsCloseToPoint(new Point(1.0, 6.0), Definitions.MOUSE_POSITION_TO_SELECTION_SHAPE_MAXIMUM_DISTANCE_SQUARED));
            Assert.IsTrue(lineDrawer.IsCloseToPoint(new Point(1.0, 4.0), Definitions.MOUSE_POSITION_TO_SELECTION_SHAPE_MAXIMUM_DISTANCE_SQUARED));
        }

        /// <summary>
        /// Tests the get rectangle.
        /// </summary>
        [TestMethod()]
        public void TestGetRectangle()
        {
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_DRAWING_STARTING_POINT, new Point(1.0, 5.0));
            _shapeDrawer.DrawingEndingPoint = new Point(-1.0, 2.0);
            Rectangle expectedRectangle = _shapeDrawer.GetRectangle();
            Assert.AreEqual(expectedRectangle.X, -1.0);
            Assert.AreEqual(expectedRectangle.Y, 2.0);
            Assert.AreEqual(expectedRectangle.Width, 2.0);
            Assert.AreEqual(expectedRectangle.Height, 3.0);
        }
    }
}