using DualViewsDrawingModel.ShapeDrawers;
using DualViewsDrawingModelTest;
using DualViewsDrawingModelTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DualViewsDrawingModel.Test
{
    [TestClass()]
    public class CanvasShapeDrawersHelperTest
    {
        private const string MEMBER_VARIABLE_NAME_SHAPE_DRAWERS = "_shapeDrawers";
        private CanvasShapeDrawersHelper _canvasShapeDrawersHelper;
        private PrivateObject _target;
        private List<ShapeDrawer> _shapeDrawers;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            _canvasShapeDrawersHelper = new CanvasShapeDrawersHelper();
            _target = new PrivateObject(_canvasShapeDrawersHelper);
            _shapeDrawers = ( List<ShapeDrawer> )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_SHAPE_DRAWERS);
        }

        /// <summary>
        /// Tests the shape drawers manager.
        /// </summary>
        [TestMethod()]
        public void TestShapeDrawersManager()
        {
            var canvasShapeDrawersHelper = new CanvasShapeDrawersHelper();
            var target = new PrivateObject(canvasShapeDrawersHelper);
            Assert.IsNotNull(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_SHAPE_DRAWERS));
        }

        /// <summary>
        /// Tests the add shape drawer.
        /// </summary>
        [TestMethod()]
        public void TestAddShapeDrawer()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _canvasShapeDrawersHelper.AddShapeDrawer(null));
            var lineDrawer = new LineDrawer(new Point(), new Point());
            _canvasShapeDrawersHelper.AddShapeDrawer(lineDrawer);
            Assert.AreEqual(_shapeDrawers.Count, 1);
            Assert.AreSame(_shapeDrawers[ 0 ], lineDrawer);
            var rectangleDrawer = new RectangleDrawer(new Point(), new Point());
            _canvasShapeDrawersHelper.AddShapeDrawer(rectangleDrawer);
            Assert.AreEqual(_shapeDrawers.Count, 2);
            Assert.AreSame(_shapeDrawers[ 1 ], rectangleDrawer);
        }

        /// <summary>
        /// Tests the remove shape drawer.
        /// </summary>
        [TestMethod()]
        public void TestRemoveShapeDrawer()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _canvasShapeDrawersHelper.RemoveShapeDrawer(null));
            var lineDrawer = new LineDrawer(new Point(), new Point());
            _shapeDrawers.Add(lineDrawer);
            _canvasShapeDrawersHelper.RemoveShapeDrawer(lineDrawer);
            Assert.IsFalse(_shapeDrawers.Contains(lineDrawer));
        }

        /// <summary>
        /// Tests the clear.
        /// </summary>
        [TestMethod()]
        public void TestClear()
        {
            _shapeDrawers.Add(new LineDrawer(new Point(), new Point()));
            _shapeDrawers.Add(new RectangleDrawer(new Point(), new Point()));
            _canvasShapeDrawersHelper.Clear();
            Assert.AreEqual(_shapeDrawers.Count, 0);
        }

        /// <summary>
        /// Tests the draw.
        /// </summary>
        [TestMethod()]
        public void TestDraw()
        {
            _shapeDrawers.Add(new LineDrawer(new Point(), new Point()));
            _shapeDrawers.Add(new RectangleDrawer(new Point(), new Point()));
            var graphics = new GraphicsMock();
            _canvasShapeDrawersHelper.Draw(graphics);
            Assert.IsTrue(graphics.IsCalledDrawLine);
            Assert.IsTrue(graphics.IsCalledDrawRectangle);
        }

        /// <summary>
        /// Tests the get most recent drawn shape drawer that includes point.
        /// </summary>
        [TestMethod()]
        public void TestGetMostRecentDrawnShapeDrawerThatIncludesPoint()
        {
            var rectangleDrawer = new RectangleDrawer(new Point(0.0, 0.0), new Point(5.0, 5.0));
            var lineDrawer = new LineDrawer(new Point(0.0, 0.0), new Point(5.0, 5.0));
            _shapeDrawers.Add(rectangleDrawer);
            _shapeDrawers.Add(lineDrawer);
            Assert.AreSame(_canvasShapeDrawersHelper.GetMostRecentDrawnShapeDrawerThatIsCloseToPoint(new Point(2.5, 2.5), Definitions.MOUSE_POSITION_TO_SELECTION_SHAPE_MAXIMUM_DISTANCE_SQUARED), lineDrawer);
            Assert.IsNull(_canvasShapeDrawersHelper.GetMostRecentDrawnShapeDrawerThatIsCloseToPoint(new Point(-0.1, 0.0), Definitions.MOUSE_POSITION_TO_SELECTION_SHAPE_MAXIMUM_DISTANCE_SQUARED));
        }
    }
}