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
            Assert.ThrowsException<ArgumentException>(() => Definitions.IsInclusiveInInterval(TestDefinitions.DUMP_DOUBLE, 9.1, 9.0));
            Assert.IsTrue(Definitions.IsInclusiveInInterval(0.0, -1.0, 1.0));
            Assert.IsTrue(Definitions.IsInclusiveInInterval(-1.0, -1.0, 1.0));
            Assert.IsTrue(Definitions.IsInclusiveInInterval(1.0, -1.0, 1.0));
            Assert.IsFalse(Definitions.IsInclusiveInInterval(0.0, 1.0, 10.0));
            Assert.IsFalse(Definitions.IsInclusiveInInterval(-10.0, 0.0, 1.0));
        }

        /// <summary>
        /// Tests the resize to be in bound interval.
        /// </summary>
        [TestMethod()]
        public void TestResizeToBeInBoundInterval()
        {
            double value = TestDefinitions.DUMP_DOUBLE;
            Assert.ThrowsException<ArgumentException>(() => Definitions.ResizeToBeInBoundInterval(ref value, 1.1, 1.0));
            value = -1.0;
            Definitions.ResizeToBeInBoundInterval(ref value, 0.0, 5.0);
            Assert.AreEqual(value, 0.0);
            value = 10.0;
            Definitions.ResizeToBeInBoundInterval(ref value, 9.0, 9.0);
            Assert.AreEqual(value, 9.0);
            value = 5.0;
            Definitions.ResizeToBeInBoundInterval(ref value, 4.0, 6.0);
            Assert.AreEqual(value, 5.0);
        }
    }
}