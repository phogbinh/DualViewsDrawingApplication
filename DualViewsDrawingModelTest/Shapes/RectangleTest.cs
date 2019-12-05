using Microsoft.VisualStudio.TestTools.UnitTesting;
using DualViewsDrawingModel.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DualViewsDrawingModelTest;

namespace DualViewsDrawingModel.Shapes.Test
{
    [TestClass()]
    public class RectangleTest
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
    }
}