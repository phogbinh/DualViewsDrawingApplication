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

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            /* Body intentionally empty */
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
    }
}