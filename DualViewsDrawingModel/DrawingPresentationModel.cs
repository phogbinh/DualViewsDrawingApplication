namespace DualViewsDrawingModel
{
    public class DrawingPresentationModel
    {
        public ButtonEnabledStatesManager.ButtonEnabledStatesChangedEventHandler ButtonEnabledStatesChanged
        {
            get
            {
                return _buttonEnabledStatesManager.ButtonEnabledStatesChanged;
            }
            set
            {
                _buttonEnabledStatesManager.ButtonEnabledStatesChanged = value;
            }
        }
        public bool RectangleButtonEnabled
        {
            get
            {
                return _buttonEnabledStatesManager.RectangleButtonEnabled;
            }
        }
        public bool LineButtonEnabled
        {
            get
            {
                return _buttonEnabledStatesManager.LineButtonEnabled;
            }
        }
        public bool ClearButtonEnabled
        {
            get
            {
                return _buttonEnabledStatesManager.ClearButtonEnabled;
            }
        }
        private ButtonEnabledStatesManager _buttonEnabledStatesManager;

        public DrawingPresentationModel()
        {
            _buttonEnabledStatesManager = new ButtonEnabledStatesManager();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            _buttonEnabledStatesManager.Initialize();
        }

        /// <summary>
        /// Handles the rectangle button clicked.
        /// </summary>
        public void HandleRectangleButtonClicked()
        {
            _buttonEnabledStatesManager.HandleRectangleButtonClicked();
        }

        /// <summary>
        /// Handles the line button clicked.
        /// </summary>
        public void HandleLineButtonClicked()
        {
            _buttonEnabledStatesManager.HandleLineButtonClicked();
        }

        /// <summary>
        /// Handles the clear button clicked.
        /// </summary>
        public void HandleClearButtonClicked()
        {
            _buttonEnabledStatesManager.HandleClearButtonClicked();
        }
    }
}
