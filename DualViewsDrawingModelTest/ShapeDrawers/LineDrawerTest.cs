using DualViewsDrawingModel.Shapes;
using DualViewsDrawingModelTest;
using DualViewsDrawingModelTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DualViewsDrawingModel.ShapeDrawers.Test
{
    [TestClass()]
    public class LineDrawerTest
    {
        private const string MEMBER_VARIABLE_NAME_DRAWING_STARTING_POINT = "_drawingStartingPoint";
        private const string MEMBER_VARIABLE_NAME_DRAWING_ENDING_POINT = "_drawingEndingPoint";
        private LineDrawer _lineDrawer;
        private PrivateObject _target;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            _lineDrawer = new LineDrawer(new Point(), new Point());
            _target = new PrivateObject(_lineDrawer);
        }

        /// <summary>
        /// Tests the line drawer.
        /// </summary>
        [TestMethod()]
        public void TestLineDrawer()
        {
            var drawingStartingPoint = new Point();
            var drawingEndingPoint = new Point();
            var lineDrawer = new LineDrawer(drawingStartingPoint, drawingEndingPoint);
            var target = new PrivateObject(lineDrawer);
            Assert.AreSame(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_DRAWING_STARTING_POINT), drawingStartingPoint);
            Assert.AreSame(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_DRAWING_ENDING_POINT), drawingEndingPoint);
        }

        /// <summary>
        /// Tests the draw.
        /// </summary>
        [TestMethod()]
        public void TestDraw()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _lineDrawer.Draw(null));
            var graphics = new GraphicsMock();
            _lineDrawer.Draw(graphics);
            Assert.IsTrue(graphics.IsCalledDrawLine);
        }

        /// <summary>
        /// Tests the get close point detector.
        /// </summary>
        [TestMethod()]
        public void TestGetClosePointDetector()
        {
            Assert.IsInstanceOfType(_lineDrawer.GetClosePointDetector(), typeof(Line));
        }

        /// <summary>
        /// Tests the get line.
        /// </summary>
        [TestMethod()]
        public void TestGetLine()
        {
            const string MEMBER_FUNCTION_NAME_GET_LINE = "GetLine";
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_DRAWING_STARTING_POINT, new Point(1.0, 5.0));
            _lineDrawer.DrawingEndingPoint = new Point(-1.0, 2.0);
            Line expectedLine = ( Line )_target.Invoke(MEMBER_FUNCTION_NAME_GET_LINE);
            Assert.AreEqual(expectedLine.X1, 1.0);
            Assert.AreEqual(expectedLine.Y1, 5.0);
            Assert.AreEqual(expectedLine.X2, -1.0);
            Assert.AreEqual(expectedLine.Y2, 2.0);
        }
    }
}