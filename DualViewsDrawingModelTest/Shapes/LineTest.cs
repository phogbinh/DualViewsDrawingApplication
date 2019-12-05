using DualViewsDrawingModelTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DualViewsDrawingModel.Shapes.Test
{
    [TestClass()]
    public class LineTest
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
    }
}