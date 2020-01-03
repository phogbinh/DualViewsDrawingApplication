using DualViewsDrawingModelTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DualViewsDrawingModel.Test
{
    [TestClass()]
    public class VectorTest
    {
        private Vector _vector;
        private PrivateObject _target;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            _vector = new Vector(TestDefinitions.DUMP_DOUBLE, TestDefinitions.DUMP_DOUBLE);
            _target = new PrivateObject(_vector);
        }

        /// <summary>
        /// Tests the vector.
        /// </summary>
        [TestMethod()]
        public void TestVector()
        {
            var vector = new Vector(-1.1, 2.2);
            Assert.AreEqual(vector.X, -1.1);
            Assert.AreEqual(vector.Y, 2.2);
        }

        /// <summary>
        /// Tests the negation operator overloading.
        /// </summary>
        [TestMethod()]
        public void TestNegationOperatorOverloading()
        {
            Assert.ThrowsException<ArgumentNullException>(() => -( ( Vector )null ));
            Vector expectedVector = -( new Vector(2.0, -6.8) );
            Assert.AreEqual(expectedVector.X, -2.0);
            Assert.AreEqual(expectedVector.Y, 6.8);
        }

        /// <summary>
        /// Tests the addition operator overloading.
        /// </summary>
        [TestMethod()]
        public void TestAdditionOperatorOverloading()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ( Vector )null + new Vector(TestDefinitions.DUMP_DOUBLE, TestDefinitions.DUMP_DOUBLE));
            Assert.ThrowsException<ArgumentNullException>(() => new Vector(TestDefinitions.DUMP_DOUBLE, TestDefinitions.DUMP_DOUBLE) + ( Vector )null);
            Vector expectedVector = new Vector(1.1, 2.2) + new Vector(-2.2, 5.5);
            Assert.AreEqual(expectedVector.X, -1.1);
            Assert.AreEqual(expectedVector.Y, 7.7);
        }

        /// <summary>
        /// Tests the subtraction operator overloading.
        /// </summary>
        [TestMethod()]
        public void TestSubtractionOperatorOverloading()
        {
            Vector expectedVector = new Vector(1.1, 2.2) - new Vector(-2.0, 5.5);
            Assert.AreEqual(expectedVector.X, 3.1);
            Assert.AreEqual(expectedVector.Y, -3.3);
        }

        /// <summary>
        /// Tests the dot product.
        /// </summary>
        [TestMethod()]
        public void TestDotProduct()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Vector.DotProduct(null, new Vector(TestDefinitions.DUMP_DOUBLE, TestDefinitions.DUMP_DOUBLE)));
            Assert.ThrowsException<ArgumentNullException>(() => Vector.DotProduct(new Vector(TestDefinitions.DUMP_DOUBLE, TestDefinitions.DUMP_DOUBLE), null));
            Assert.AreEqual(Vector.DotProduct(new Vector(0.0, 1.1), new Vector(10.0, 1.0)), 1.1);
        }
    }
}