using DualViewsDrawingModelTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DualViewsDrawingModel.Test
{
    [TestClass()]
    public class ButtonEnabledStatesManagerTest
    {
        private const string MEMBER_VARIABLE_NAME_RECTANGLE_BUTTON_ENABLED_STATE = "_rectangleButtonEnabledState";
        private const string MEMBER_VARIABLE_NAME_LINE_BUTTON_ENABLED_STATE = "_lineButtonEnabledState";
        private const string MEMBER_VARIABLE_NAME_CLEAR_BUTTON_ENABLED_STATE = "_clearButtonEnabledState";
        private const string MEMBER_VARIABLE_NAME_BUTTON_ENABLED_STATES = "_buttonEnabledStates";
        private ButtonEnabledStatesManager _buttonEnabledStatesManager;
        private PrivateObject _target;
        private List<ButtonEnabledState> _buttonEnabledStates;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            _buttonEnabledStatesManager = new ButtonEnabledStatesManager();
            _target = new PrivateObject(_buttonEnabledStatesManager);
            _buttonEnabledStates = ( List<ButtonEnabledState> )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_BUTTON_ENABLED_STATES);
        }

        /// <summary>
        /// Tests the button enabled states manager.
        /// </summary>
        [TestMethod()]
        public void TestButtonEnabledStatesManager()
        {
            var buttonEnabledStatesManager = new ButtonEnabledStatesManager();
            var target = new PrivateObject(buttonEnabledStatesManager);
            var buttonEnabledStates = ( List<ButtonEnabledState> )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_BUTTON_ENABLED_STATES);
            Assert.IsNotNull(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_RECTANGLE_BUTTON_ENABLED_STATE));
            Assert.IsNotNull(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_LINE_BUTTON_ENABLED_STATE));
            Assert.IsNotNull(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_CLEAR_BUTTON_ENABLED_STATE));
            Assert.IsNotNull(buttonEnabledStates);
            Assert.AreEqual(buttonEnabledStates.Count, 3);
            Assert.AreSame(buttonEnabledStates[ 0 ], _target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_RECTANGLE_BUTTON_ENABLED_STATE));
            Assert.AreSame(buttonEnabledStates[ 1 ], _target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_LINE_BUTTON_ENABLED_STATE));
            Assert.AreSame(buttonEnabledStates[ 2 ], _target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_CLEAR_BUTTON_ENABLED_STATE));
        }

        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestMethod()]
        public void TestInitialize()
        {
            _buttonEnabledStatesManager.Initialize();
            foreach ( ButtonEnabledState buttonEnabledState in _buttonEnabledStates )
            {
                Assert.IsTrue(buttonEnabledState.Value);
            }
        }

        /// <summary>
        /// Tests the set button enabled states.
        /// </summary>
        [TestMethod()]
        public void TestSetButtonEnabledStates()
        {
            const string MEMBER_FUNCTION_NAME_SET_BUTTON_ENABLED_STATES = "SetButtonEnabledStates";
            var arguments = new object[] { true };
            _target.Invoke(MEMBER_FUNCTION_NAME_SET_BUTTON_ENABLED_STATES, arguments);
            foreach ( ButtonEnabledState buttonEnabledState in _buttonEnabledStates )
            {
                Assert.IsTrue(buttonEnabledState.Value);
            }
            arguments = new object[] { false };
            _target.Invoke(MEMBER_FUNCTION_NAME_SET_BUTTON_ENABLED_STATES, arguments);
            foreach ( ButtonEnabledState buttonEnabledState in _buttonEnabledStates )
            {
                Assert.IsFalse(buttonEnabledState.Value);
            }
        }

        /// <summary>
        /// Tests the handle rectangle button clicked.
        /// </summary>
        [TestMethod()]
        public void TestHandleRectangleButtonClicked()
        {
            _buttonEnabledStatesManager.HandleRectangleButtonClicked();
            Assert.IsFalse(_buttonEnabledStatesManager.RectangleButtonEnabled);
            Assert.IsTrue(_buttonEnabledStatesManager.LineButtonEnabled);
            Assert.IsTrue(_buttonEnabledStatesManager.ClearButtonEnabled);
        }

        /// <summary>
        /// Tests the handle line button clicked.
        /// </summary>
        [TestMethod()]
        public void TestHandleLineButtonClicked()
        {
            _buttonEnabledStatesManager.HandleLineButtonClicked();
            Assert.IsTrue(_buttonEnabledStatesManager.RectangleButtonEnabled);
            Assert.IsFalse(_buttonEnabledStatesManager.LineButtonEnabled);
            Assert.IsTrue(_buttonEnabledStatesManager.ClearButtonEnabled);
        }

        /// <summary>
        /// Tests the handle clear button clicked.
        /// </summary>
        [TestMethod()]
        public void TestHandleClearButtonClicked()
        {
            _buttonEnabledStatesManager.HandleClearButtonClicked();
            foreach ( ButtonEnabledState buttonEnabledState in _buttonEnabledStates )
            {
                Assert.IsTrue(buttonEnabledState.Value);
            }
        }

        /// <summary>
        /// Tests the notify button enabled states changed.
        /// </summary>
        [TestMethod()]
        public void TestNotifyButtonEnabledStatesChanged()
        {
            const string MEMBER_FUNCTION_NAME_NOTIFY_BUTTON_ENABLED_STATES_CHANGED = "NotifyButtonEnabledStatesChanged";
            int count = 0;
            _buttonEnabledStatesManager.ButtonEnabledStatesChanged += () => count++;
            _target.Invoke(MEMBER_FUNCTION_NAME_NOTIFY_BUTTON_ENABLED_STATES_CHANGED);
            Assert.AreEqual(count, 1);
            _target.Invoke(MEMBER_FUNCTION_NAME_NOTIFY_BUTTON_ENABLED_STATES_CHANGED);
            Assert.AreEqual(count, 2);
        }
    }
}