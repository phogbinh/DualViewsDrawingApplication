using DualViewsDrawingModelTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DualViewsDrawingModel.Shapes.Test
{
    [TestClass()]
    public class LineTest
    {
        private Line _line;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            _line = new Line(new Point(), new Point());
        }

        /// <summary>
        /// Tests the line.
        /// </summary>
        [TestMethod()]
        public void TestLine()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Line(null, new Point()));
            Assert.ThrowsException<ArgumentNullException>(() => new Line(new Point(), null));
            var line = new Line(new Point(1.1, 2.2), new Point(3.3, 4.4));
            Assert.AreEqual(line.X1, 1.1);
            Assert.AreEqual(line.Y1, 2.2);
            Assert.AreEqual(line.X2, 3.3);
            Assert.AreEqual(line.Y2, 4.4);
        }

        /// <summary>
        /// Tests the get closet point.
        /// </summary>
        [TestMethod()]
        public void TestGetClosetPoint()
        {
            _line = new Line(new Point(0.0, 0.0), new Point(5.0, 0.0)); // y = 0
            Point expectedClosetPoint = _line.GetClosetPoint(new Point(0.0, 0.0));
            Assert.AreEqual(expectedClosetPoint.X, 0.0);
            Assert.AreEqual(expectedClosetPoint.Y, 0.0);
            expectedClosetPoint = _line.GetClosetPoint(new Point(5.0, 0.0));
            Assert.AreEqual(expectedClosetPoint.X, 5.0);
            Assert.AreEqual(expectedClosetPoint.Y, 0.0);
            expectedClosetPoint = _line.GetClosetPoint(new Point(-0.1, 0.0));
            Assert.AreEqual(expectedClosetPoint.X, -0.1);
            Assert.AreEqual(expectedClosetPoint.Y, 0.0);
            expectedClosetPoint = _line.GetClosetPoint(new Point(5.1, 0.0));
            Assert.AreEqual(expectedClosetPoint.X, 5.1);
            Assert.AreEqual(expectedClosetPoint.Y, 0.0);
            expectedClosetPoint = _line.GetClosetPoint(new Point(0.0, 0.1));
            Assert.AreEqual(expectedClosetPoint.X, 0.0);
            Assert.AreEqual(expectedClosetPoint.Y, 0.0);
            expectedClosetPoint = _line.GetClosetPoint(new Point(5.0, -0.1));
            Assert.AreEqual(expectedClosetPoint.X, 5.0);
            Assert.AreEqual(expectedClosetPoint.Y, 0.0);
            _line = new Line(new Point(0, 3), new Point(2, -1)); // 2 * x + y = 3
            expectedClosetPoint = _line.GetClosetPoint(new Point(3, 2));
            Assert.AreEqual(expectedClosetPoint.X, 1);
            Assert.AreEqual(expectedClosetPoint.Y, 1);
        }

        /// <summary>
        /// Tests the is including point.
        /// </summary>
        [TestMethod()]
        public void TestIsIncludingPoint()
        {
            _line = new Line(new Point(0.0, 1.0), new Point(5.0, 1.0)); // y = 1
            Assert.IsTrue(_line.IsIncludingPoint(new Point(0.0, 1.0)));
            Assert.IsTrue(_line.IsIncludingPoint(new Point(5.0, 1.0)));
            Assert.IsFalse(_line.IsIncludingPoint(new Point(-0.1, 1.0)));
            Assert.IsFalse(_line.IsIncludingPoint(new Point(5.1, 1.0)));
            Assert.IsFalse(_line.IsIncludingPoint(new Point(0.0, 0.9)));
            Assert.IsFalse(_line.IsIncludingPoint(new Point(0.0, 1.1)));
            _line = new Line(new Point(0, 3), new Point(2, -1)); // 2 * x + y = 3
            Assert.IsTrue(_line.IsIncludingPoint(new Point(1.0, 1.0)));
            Assert.IsFalse(_line.IsIncludingPoint(new Point(1.1, 1.0)));
        }

        /// <summary>
        /// Tests the is aligned with point.
        /// </summary>
        [TestMethod()]
        public void TestIsAlignedWithPoint()
        {
            _line = new Line(new Point(0.0, 1.0), new Point(5.0, 1.0)); // y = 1
            Assert.IsTrue(_line.IsAlignedWithPoint(new Point(0.0, 1.0), Definitions.DOUBLE_EPSILON));
            Assert.IsTrue(_line.IsAlignedWithPoint(new Point(5.0, 1.0), Definitions.DOUBLE_EPSILON));
            Assert.IsTrue(_line.IsAlignedWithPoint(new Point(-0.1, 1.0), Definitions.DOUBLE_EPSILON));
            Assert.IsTrue(_line.IsAlignedWithPoint(new Point(5.1, 1.0), Definitions.DOUBLE_EPSILON));
            Assert.IsFalse(_line.IsAlignedWithPoint(new Point(0.0, 1.1), Definitions.DOUBLE_EPSILON));
            Assert.IsFalse(_line.IsAlignedWithPoint(new Point(0.0, 0.9), Definitions.DOUBLE_EPSILON));
            Assert.IsFalse(_line.IsAlignedWithPoint(new Point(-0.1, 0.9), Definitions.DOUBLE_EPSILON));
            Assert.IsFalse(_line.IsAlignedWithPoint(new Point(0.1, 0.9), Definitions.DOUBLE_EPSILON));
        }
    }
}