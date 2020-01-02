using DualViewsDrawingModel.Shapes;
using DualViewsDrawingModelTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DualViewsDrawingModel.ShapeDrawers.Test
{
    public class ShapeDrawerMock : ShapeDrawer
    {
        public ShapeDrawerMock(Point drawingStartingPointData, Point drawingEndingPointData) : base(drawingStartingPointData, drawingEndingPointData)
        {
            /* Body intentionally empty */
        }

        /// <summary>
        /// Draws the specified graphics.
        /// </summary>
        public override void Draw(IGraphics graphics)
        {
            /* Body intentionally empty */
        }
    }
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