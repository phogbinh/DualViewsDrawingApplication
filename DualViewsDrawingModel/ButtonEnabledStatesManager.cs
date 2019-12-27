using System.Collections.Generic;

namespace DualViewsDrawingModel
{
    public class ButtonEnabledStatesManager
    {
        public delegate void ButtonEnabledStatesChangedEventHandler();
        public ButtonEnabledStatesChangedEventHandler ButtonEnabledStatesChanged
        {
            get; set;
        }
        public bool RectangleButtonEnabled
        {
            get
            {
                return _rectangleButtonEnabledState.Value;
            }
        }
        public bool LineButtonEnabled
        {
            get
            {
                return _lineButtonEnabledState.Value;
            }
        }
        public bool ClearButtonEnabled
        {
            get
            {
                return _clearButtonEnabledState.Value;
            }
        }
        private ButtonEnabledState _rectangleButtonEnabledState;
        private ButtonEnabledState _lineButtonEnabledState;
        private ButtonEnabledState _clearButtonEnabledState;
        private List<ButtonEnabledState> _buttonEnabledStates;

        public ButtonEnabledStatesManager()
        {
            _rectangleButtonEnabledState = new ButtonEnabledState();
            _lineButtonEnabledState = new ButtonEnabledState();
            _clearButtonEnabledState = new ButtonEnabledState();
            _buttonEnabledStates = new List<ButtonEnabledState>();
            _buttonEnabledStates.Add(_rectangleButtonEnabledState);
            _buttonEnabledStates.Add(_lineButtonEnabledState);
            _buttonEnabledStates.Add(_clearButtonEnabledState);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public virtual void Initialize()
        {
            SetButtonEnabledStates(true);
            NotifyButtonEnabledStatesChanged();
        }

        /// <summary>
        /// Sets the button enabled states.
        /// </summary>
        private void SetButtonEnabledStates(bool value)
        {
            foreach ( ButtonEnabledState buttonEnabledState in _buttonEnabledStates )
            {
                buttonEnabledState.Value = value;
            }
        }

        /// <summary>
        /// Handles the rectangle button clicked.
        /// </summary>
        public virtual void HandleRectangleButtonClicked()
        {
            SetButtonEnabledStates(true);
            _rectangleButtonEnabledState.Value = false;
            NotifyButtonEnabledStatesChanged();
        }

        /// <summary>
        /// Handles the line button clicked.
        /// </summary>
        public virtual void HandleLineButtonClicked()
        {
            SetButtonEnabledStates(true);
            _lineButtonEnabledState.Value = false;
            NotifyButtonEnabledStatesChanged();
        }

        /// <summary>
        /// Handles the clear button clicked.
        /// </summary>
        public virtual void HandleClearButtonClicked()
        {
            SetButtonEnabledStates(true);
            NotifyButtonEnabledStatesChanged();
        }

        /// <summary>
        /// Notifies the drawing presentation model changed.
        /// </summary>
        private void NotifyButtonEnabledStatesChanged()
        {
            if ( ButtonEnabledStatesChanged != null )
            {
                ButtonEnabledStatesChanged();
            }
        }
    }
}
