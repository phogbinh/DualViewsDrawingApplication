using DualViewsDrawingModelTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DualViewsDrawingModel.Test
{
    [TestClass()]
    public class PointTest
    {
        private const string MEMBER_VARIABLE_NAME_X = "_x";
        private const string MEMBER_VARIABLE_NAME_Y = "_y";
        private Point _point;
        private PrivateObject _target;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            _point = new Point();
            _target = new PrivateObject(_point);
        }

        /// <summary>
        /// Tests the point default constructor.
        /// </summary>
        [TestMethod()]
        public void TestPointDefaultConstructor()
        {
            var point = new Point();
            var target = new PrivateObject(point);
            Assert.AreEqual(( double )target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_X), 0.0);
            Assert.AreEqual(( double )target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_Y), 0.0);
        }

        /// <summary>
        /// Tests the point two parameters constructor.
        /// </summary>
        [TestMethod()]
        public void TestPointTwoParametersConstructor()
        {
            var point = new Point(-1.1, 2.2);
            var target = new PrivateObject(point);
            Assert.AreEqual(( double )target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_X), -1.1);
            Assert.AreEqual(( double )target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_Y), 2.2);
        }

        /// <summary>
        /// Tests the is inclusive in region.
        /// </summary>
        [TestMethod()]
        public void TestIsInclusiveInRegion()
        {
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_X, 0.0);
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_Y, 5.5);
            Assert.IsTrue(_point.IsInclusiveInRegion(-1.0, 1.0, 5.0, 6.0));
            Assert.IsFalse(_point.IsInclusiveInRegion(0.1, 1.0, 5.0, 6.0));
            Assert.IsFalse(_point.IsInclusiveInRegion(-1.0, 1.0, 5.0, 5.4));
            Assert.IsFalse(_point.IsInclusiveInRegion(-1.0, -0.9, 5.6, 6.0));
        }

        /// <summary>
        /// Tests the resize to be in bound region.
        /// </summary>
        [TestMethod()]
        public void TestResizeToBeInBoundRegion()
        {
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_X, -1.0);
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_Y, 1.0);
            _point.ResizeToBeInBoundRegion(5.0, 5.0, -10.0, -10.0);
            Assert.AreEqual(_point.X, 5.0);
            Assert.AreEqual(_point.Y, -10.0);
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_X, 2.0);
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_Y, 3.0);
            _point.ResizeToBeInBoundRegion(2.0, 10.0, 0.0, 3.0);
            Assert.AreEqual(_point.X, 2.0);
            Assert.AreEqual(_point.Y, 3.0);
        }
    }
}