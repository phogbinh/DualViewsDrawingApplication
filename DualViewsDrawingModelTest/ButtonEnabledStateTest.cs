using DualViewsDrawingModelTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DualViewsDrawingModel.Test
{
    [TestClass()]
    public class ButtonEnabledStateTest
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
        /// Tests the state of the button enabled.
        /// </summary>
        [TestMethod()]
        public void TestButtonEnabledState()
        {
            var buttonEnabledState = new ButtonEnabledState();
            Assert.IsFalse(buttonEnabledState.Value);
        }
    }
}