using DualViewsDrawingModel.ShapeDrawers;
using DualViewsDrawingModelTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DualViewsDrawingModel.Test
{
    [TestClass()]
    public class ShapeDrawerFactoryTest
    {
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
        /// Tests the create shape drawer.
        /// </summary>
        [TestMethod()]
        public void TestCreateShapeDrawer()
        {
            const string SHAPE_DRAWER_MEMBER_VARIABLE_NAME_DRAWING_STARTING_POINT = "_drawingStartingPoint";
            const string SHAPE_DRAWER_MEMBER_VARIABLE_NAME_DRAWING_ENDING_POINT = "_drawingEndingPoint";
            Assert.ThrowsException<ArgumentException>(() => ShapeDrawerFactory.CreateShapeDrawer(new Point(), new Point(), ShapeDrawerType.None));
            Assert.ThrowsException<ArgumentException>(() => ShapeDrawerFactory.CreateShapeDrawer(new Point(), new Point(), ( ShapeDrawerType )( -1 )));
            Assert.ThrowsException<ArgumentException>(() => ShapeDrawerFactory.CreateShapeDrawer(new Point(), new Point(), ( ShapeDrawerType )3));
            var drawingStartingPoint = new Point();
            var drawingEndingPoint = new Point();
            // Create line drawer.
            ShapeDrawer expectedShapeDrawer = ShapeDrawerFactory.CreateShapeDrawer(drawingStartingPoint, drawingEndingPoint, ShapeDrawerType.Line);
            var target = new PrivateObject(expectedShapeDrawer);
            Assert.AreSame(target.GetFieldOrProperty(SHAPE_DRAWER_MEMBER_VARIABLE_NAME_DRAWING_STARTING_POINT), drawingStartingPoint);
            Assert.AreSame(target.GetFieldOrProperty(SHAPE_DRAWER_MEMBER_VARIABLE_NAME_DRAWING_ENDING_POINT), drawingEndingPoint);
            Assert.IsInstanceOfType(expectedShapeDrawer, typeof(LineDrawer));
            // Create rectangle drawer.
            expectedShapeDrawer = ShapeDrawerFactory.CreateShapeDrawer(drawingStartingPoint, drawingEndingPoint, ShapeDrawerType.Rectangle);
            target = new PrivateObject(expectedShapeDrawer);
            Assert.AreSame(target.GetFieldOrProperty(SHAPE_DRAWER_MEMBER_VARIABLE_NAME_DRAWING_STARTING_POINT), drawingStartingPoint);
            Assert.AreSame(target.GetFieldOrProperty(SHAPE_DRAWER_MEMBER_VARIABLE_NAME_DRAWING_ENDING_POINT), drawingEndingPoint);
            Assert.IsInstanceOfType(expectedShapeDrawer, typeof(RectangleDrawer));
        }
    }
}