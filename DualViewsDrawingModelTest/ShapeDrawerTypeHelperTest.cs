using DualViewsDrawingModelTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DualViewsDrawingModel.Test
{
    [TestClass()]
    public class ShapeDrawerTypeHelperTest
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
        /// Tests the type of the is valid shape drawer.
        /// </summary>
        [TestMethod()]
        public void TestIsValidShapeDrawerType()
        {
            Assert.IsTrue(ShapeDrawerTypeHelper.IsValidShapeDrawerType(ShapeDrawerType.None));
            Assert.IsTrue(ShapeDrawerTypeHelper.IsValidShapeDrawerType(ShapeDrawerType.Line));
            Assert.IsTrue(ShapeDrawerTypeHelper.IsValidShapeDrawerType(ShapeDrawerType.Rectangle));
            Assert.IsFalse(ShapeDrawerTypeHelper.IsValidShapeDrawerType(( ShapeDrawerType )( -1 )));
            Assert.IsFalse(ShapeDrawerTypeHelper.IsValidShapeDrawerType(( ShapeDrawerType )3));
        }

        /// <summary>
        /// Tests the get string.
        /// </summary>
        [TestMethod()]
        public void TestGetString()
        {
            Assert.AreEqual(ShapeDrawerTypeHelper.GetString(ShapeDrawerType.None), "");
            Assert.AreEqual(ShapeDrawerTypeHelper.GetString(ShapeDrawerType.Line), "Line");
            Assert.AreEqual(ShapeDrawerTypeHelper.GetString(ShapeDrawerType.Rectangle), "Rectangle");
            Assert.ThrowsException<ArgumentException>(() => ( ShapeDrawerTypeHelper.GetString(( ShapeDrawerType )( -1 )) ));
            Assert.ThrowsException<ArgumentException>(() => ( ShapeDrawerTypeHelper.GetString(( ShapeDrawerType )3) ));
        }
    }
}