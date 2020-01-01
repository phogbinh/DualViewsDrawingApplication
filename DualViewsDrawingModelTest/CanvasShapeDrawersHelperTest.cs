using DualViewsDrawingModel.ShapeDrawers;
using DualViewsDrawingModelTest;
using DualViewsDrawingModelTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}