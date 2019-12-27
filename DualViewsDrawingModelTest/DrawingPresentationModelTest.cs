using DualViewsDrawingModelTest;
using DualViewsDrawingModelTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DualViewsDrawingModel.Test
{
    [TestClass()]
    public class DrawingPresentationModelTest
    {
        private const string MEMBER_VARIABLE_NAME_BUTTON_ENABLED_STATES_MANAGER = "_buttonEnabledStatesManager";
        private DrawingPresentationModel _drawingPresentationModel;
        private PrivateObject _target;
        private ButtonEnabledStatesManagerMock _buttonEnabledStatesManager;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            _drawingPresentationModel = new DrawingPresentationModel();
            _target = new PrivateObject(_drawingPresentationModel);
            _buttonEnabledStatesManager = new ButtonEnabledStatesManagerMock();
            _target.SetFieldOrProperty(MEMBER_VARIABLE_NAME_BUTTON_ENABLED_STATES_MANAGER, _buttonEnabledStatesManager);
        }

        /// <summary>
        /// Tests the get property button enabled states changed.
        /// </summary>
        [TestMethod()]
        public void TestGetPropertyButtonEnabledStatesChanged()
        {
            Assert.AreSame(_drawingPresentationModel.ButtonEnabledStatesChanged, _buttonEnabledStatesManager.ButtonEnabledStatesChanged);
        }

        /// <summary>
        /// Tests the set property button enabled states changed.
        /// </summary>
        [TestMethod()]
        public void TestSetPropertyButtonEnabledStatesChanged()
        {
            _drawingPresentationModel.ButtonEnabledStatesChanged = null;
            Assert.IsNull(_drawingPresentationModel.ButtonEnabledStatesChanged);
        }

        /// <summary>
        /// Tests the get property rectangle button enabled.
        /// </summary>
        [TestMethod()]
        public void TestGetPropertyRectangleButtonEnabled()
        {
            Assert.AreEqual(_drawingPresentationModel.RectangleButtonEnabled, _buttonEnabledStatesManager.RectangleButtonEnabled);
        }

        /// <summary>
        /// Tests the get property line button enabled.
        /// </summary>
        [TestMethod()]
        public void TestGetPropertyLineButtonEnabled()
        {
            Assert.AreEqual(_drawingPresentationModel.LineButtonEnabled, _buttonEnabledStatesManager.LineButtonEnabled);
        }

        /// <summary>
        /// Tests the get property clear button enabled.
        /// </summary>
        [TestMethod()]
        public void TestGetPropertyClearButtonEnabled()
        {
            Assert.AreEqual(_drawingPresentationModel.ClearButtonEnabled, _buttonEnabledStatesManager.ClearButtonEnabled);
        }

        /// <summary>
        /// Tests the drawing presentation model.
        /// </summary>
        [TestMethod()]
        public void TestDrawingPresentationModel()
        {
            var drawingPresentationModel = new DrawingPresentationModel();
            var target = new PrivateObject(drawingPresentationModel);
            Assert.IsNotNull(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_BUTTON_ENABLED_STATES_MANAGER));
        }

        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestMethod()]
        public void TestInitialize()
        {
            _drawingPresentationModel.Initialize();
            Assert.IsTrue(_buttonEnabledStatesManager.IsCalledInitialize);
        }

        /// <summary>
        /// Tests the handle rectangle button clicked.
        /// </summary>
        [TestMethod()]
        public void TestHandleRectangleButtonClicked()
        {
            _drawingPresentationModel.HandleRectangleButtonClicked();
            Assert.IsTrue(_buttonEnabledStatesManager.IsCalledHandleRectangleButtonClicked);
        }

        /// <summary>
        /// Tests the handle line button clicked.
        /// </summary>
        [TestMethod()]
        public void TestHandleLineButtonClicked()
        {
            _drawingPresentationModel.HandleLineButtonClicked();
            Assert.IsTrue(_buttonEnabledStatesManager.IsCalledHandleLineButtonClicked);
        }

        /// <summary>
        /// Tests the handle clear button clicked.
        /// </summary>
        [TestMethod()]
        public void TestHandleClearButtonClicked()
        {
            _drawingPresentationModel.HandleClearButtonClicked();
            Assert.IsTrue(_buttonEnabledStatesManager.IsCalledHandleClearButtonClicked);
        }
    }
}