using DualViewsDrawingModelTest;
using DualViewsDrawingModelTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DualViewsDrawingModel.Shapes.Test
{
    [TestClass()]
    public class RectangleTest
    {
        private const string MEMBER_VARIABLE_NAME_WIDTH = "_width";
        private const string MEMBER_VARIABLE_NAME_HEIGHT = "_height";
        private Rectangle _rectangle;
        private PrivateObject _target;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            _rectangle = new Rectangle(new Point(), new Point());
            _target = new PrivateObject(_rectangle);
        }

        /// <summary>
        /// Tests the width of the set property.
        /// </summary>
        [TestMethod()]
        public void TestSetPropertyWidth()
        {
            Assert.ThrowsException<ApplicationException>(() => _rectangle.Width = -1);
            _rectangle.Width = 0;
            Assert.AreEqual(( double )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_WIDTH), 0);
            _rectangle.Width = 1.2;
            Assert.AreEqual(( double )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_WIDTH), 1.2);
        }

        /// <summary>
        /// Tests the height of the set property.
        /// </summary>
        [TestMethod()]
        public void TestSetPropertyHeight()
        {
            Assert.ThrowsException<ApplicationException>(() => _rectangle.Height = -1);
            _rectangle.Height = 0;
            Assert.AreEqual(( double )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_HEIGHT), 0);
            _rectangle.Height = 1.2;
            Assert.AreEqual(( double )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_HEIGHT), 1.2);
        }

        /// <summary>
        /// Tests the rectangle.
        /// </summary>
        [TestMethod()]
        public void TestRectangle()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Rectangle(null, new Point()));
            Assert.ThrowsException<ArgumentNullException>(() => new Rectangle(new Point(), null));
            var rectangle = new Rectangle(new Point(0.0, 1.0), new Point(1.0, 2.0));
            Assert.AreEqual(rectangle.X, 0.0);
            Assert.AreEqual(rectangle.Y, 1.0);
            Assert.AreEqual(rectangle.Width, 1.0);
            Assert.AreEqual(rectangle.Height, 1.0);
            rectangle = new Rectangle(new Point(1.0, 2.0), new Point(0.0, 1.0));
            Assert.AreEqual(rectangle.X, 0.0);
            Assert.AreEqual(rectangle.Y, 1.0);
            Assert.AreEqual(rectangle.Width, 1.0);
            Assert.AreEqual(rectangle.Height, 1.0);
            rectangle = new Rectangle(new Point(0.0, 2.0), new Point(1.0, 1.0));
            Assert.AreEqual(rectangle.X, 0.0);
            Assert.AreEqual(rectangle.Y, 1.0);
            Assert.AreEqual(rectangle.Width, 1.0);
            Assert.AreEqual(rectangle.Height, 1.0);
        }

        /// <summary>
        /// Tests the is close to point.
        /// </summary>
        [TestMethod()]
        public void TestIsCloseToPoint()
        {
            PointMock point = new PointMock();
            _rectangle.IsCloseToPoint(point, TestDefinitions.DUMP_DOUBLE);
            Assert.IsTrue(point.IsCalledIsInclusiveInRegion);
        }

        /// <summary>
        /// Tests the is including point.
        /// </summary>
        [TestMethod()]
        public void TestIsIncludingPoint()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _rectangle.IsIncludingPoint(null));
            PointMock point = new PointMock();
            _rectangle.IsIncludingPoint(point);
            Assert.IsTrue(point.IsCalledIsInclusiveInRegion);
        }

        /// <summary>
        /// Tests the get lower right x.
        /// </summary>
        [TestMethod()]
        public void TestGetLowerRightX()
        {
            _rectangle.X = 1;
            _rectangle.Width = 100;
            Assert.AreEqual(_rectangle.GetLowerRightX(), 101);
            _rectangle.X = -1;
            Assert.AreEqual(_rectangle.GetLowerRightX(), 99);
        }

        /// <summary>
        /// Tests the get lower right y.
        /// </summary>
        [TestMethod()]
        public void TestGetLowerRightY()
        {
            _rectangle.Y = 1;
            _rectangle.Height = 100;
            Assert.AreEqual(_rectangle.GetLowerRightY(), 101);
            _rectangle.Y = -1;
            Assert.AreEqual(_rectangle.GetLowerRightY(), 99);
        }
    }
}