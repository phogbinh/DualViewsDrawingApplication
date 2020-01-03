using DualViewsDrawingModel.Shapes;
using DualViewsDrawingModelTest;
using DualViewsDrawingModelTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace DualViewsDrawingModel.ShapeDrawers.Test
{
    [TestClass()]
    public class ShapeDrawerTest
    {
        private const string MEMBER_VARIABLE_NAME_TYPE = "_type";
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
            Assert.AreEqual(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_TYPE), ShapeDrawerType.None);
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
        /// Tests the is drawing ending point close to point.
        /// </summary>
        [TestMethod()]
        public void TestIsDrawingEndingPointCloseToPoint()
        {
            _shapeDrawer.DrawingEndingPoint = new Point(1.1, 2.2);
            Assert.IsTrue(_shapeDrawer.IsDrawingEndingPointCloseToPoint(new Point(1.1, 2.2), 0.0));
            Assert.IsFalse(_shapeDrawer.IsDrawingEndingPointCloseToPoint(new Point(5.5, 5.5), 8.0));
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

        /// <summary>
        /// Tests the draw selection hint.
        /// </summary>
        [TestMethod()]
        public void TestDrawSelectionHint()
        {
            var lineDrawer = new LineDrawer(new Point(), new Point());
            var graphics = new GraphicsMock();
            lineDrawer.DrawSelectionHint(graphics);
            Assert.IsTrue(graphics.IsCalledDrawSelectionCorner);
            Assert.IsTrue(graphics.IsCalledDrawSelectionBorderLine);
            var rectangleDrawer = new RectangleDrawer(new Point(), new Point());
            graphics = new GraphicsMock();
            rectangleDrawer.DrawSelectionHint(graphics);
            Assert.IsTrue(graphics.IsCalledDrawSelectionCorner);
            Assert.IsTrue(graphics.IsCalledDrawSelectionBorderRectangle);
        }

        /// <summary>
        /// Tests the draw selection corners.
        /// </summary>
        [TestMethod()]
        public void TestDrawSelectionCorners()
        {
            const string MEMBER_FUNCTION_NAME_DRAW_SELECTION_CORNERS = "DrawSelectionCorners";
            var arguments = new object[] { null };
            TargetInvocationException expectedException = Assert.ThrowsException<TargetInvocationException>(() => _target.Invoke(MEMBER_FUNCTION_NAME_DRAW_SELECTION_CORNERS, arguments));
            Assert.IsInstanceOfType(expectedException.InnerException, typeof(ArgumentNullException));
            var lineDrawer = new LineDrawer(new Point(), new Point());
            var target = new PrivateObject(lineDrawer);
            var graphics = new GraphicsMock();
            arguments = new object[] { graphics };
            target.Invoke(MEMBER_FUNCTION_NAME_DRAW_SELECTION_CORNERS, arguments);
            Assert.IsTrue(graphics.IsCalledDrawSelectionCorner);
        }
    }
}