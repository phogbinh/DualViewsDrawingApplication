using DualViewsDrawingModelTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DualViewsDrawingModel.Test
{
    [TestClass()]
    public class DefinitionsTest
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
        /// Tests the is inclusive in interval.
        /// </summary>
        [TestMethod()]
        public void TestIsInclusiveInInterval()
        {
            Assert.ThrowsException<ArgumentException>(() => Definitions.IsInclusiveInInterval(TestDefinitions.DUMP_DOUBLE, 10, 9));
            Assert.IsTrue(Definitions.IsInclusiveInInterval(0, -1, 1));
            Assert.IsTrue(Definitions.IsInclusiveInInterval(-1, -1, 1));
            Assert.IsTrue(Definitions.IsInclusiveInInterval(1, -1, 1));
            Assert.IsFalse(Definitions.IsInclusiveInInterval(0, 1, 10));
            Assert.IsFalse(Definitions.IsInclusiveInInterval(-10, 0, 1));
        }

        /// <summary>
        /// Tests the resize to be in bound interval.
        /// </summary>
        [TestMethod()]
        public void TestResizeToBeInBoundInterval()
        {
            double value = TestDefinitions.DUMP_DOUBLE;
            Assert.ThrowsException<ArgumentException>(() => Definitions.ResizeToBeInBoundInterval(ref value, 2, 1));
            value = -1;
            Definitions.ResizeToBeInBoundInterval(ref value, 0, 5);
            Assert.AreEqual(value, 0);
            value = 10;
            Definitions.ResizeToBeInBoundInterval(ref value, 9, 9);
            Assert.AreEqual(value, 9);
            value = 5;
            Definitions.ResizeToBeInBoundInterval(ref value, 4, 6);
            Assert.AreEqual(value, 5);
        }
    }
}