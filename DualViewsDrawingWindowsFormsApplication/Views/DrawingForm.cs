using DualViewsDrawingModel;
using DualViewsDrawingModel.Shapes;
using DualViewsDrawingWindowsFormsApplication.Views.Utilities;
using System;
using System.Windows.Forms;

namespace DualViewsDrawingWindowsFormsApplication.Views
{
    public partial class DrawingForm : Form
    {
        private DrawingPresentationModel _drawingPresentationModel;
        private Model _model;

        public DrawingForm(DrawingPresentationModel drawingPresentationModelData, Model modelData)
        {
            InitializeComponent();
            _drawingPresentationModel = drawingPresentationModelData;
            _model = modelData;
            this.Disposed += RemoveEvents;
            // Observers
            SubscribeEvents();
            // UI
            _canvas.Resize += (sender, eventArguments) => _model.SetCanvasSize(_canvas.Size.Width, _canvas.Size.Height);
            _canvas.Paint += (sender, eventArguments) => _model.RefreshDrawCanvas(new DrawingFormGraphicsAdapter(eventArguments.Graphics));
            _canvas.MouseDown += HandleCanvasMousePressed;
            _canvas.MouseMove += HandleCanvasMouseMoved;
            _canvas.MouseUp += HandleCanvasMouseReleased;
            _rectangleButton.Click += HandleRectangleButtonClicked;
            _lineButton.Click += HandleLineButtonClicked;
            _clearButton.Click += HandleClearButtonClicked;
            _undoButton.Click += (sender, eventArguments) => _model.Undo();
            _redoButton.Click += (sender, eventArguments) => _model.Redo();
            // Initial UI States
            _drawingPresentationModel.Initialize();
            UpdateUndoRedoButtonEnabledStates();
            _model.Initialize(_canvas.Size.Width, _canvas.Size.Height, ShapeDrawerType.None);
        }

        /// <summary>
        /// Subscribes the events.
        /// </summary>
        private void SubscribeEvents()
        {
            _drawingPresentationModel.ButtonEnabledStatesChanged += UpdateButtonEnabledStates;
            _model.UndoRedoStacksChanged += UpdateUndoRedoButtonEnabledStates;
            _model.CanvasRefreshDrawRequested += HandleCanvasRefreshDrawRequested;
            _model.DrawingEnded += HandleDrawingEnded;
            _model.CanvasCurrentShapeChanged += HandleCanvasCurrentShapeChanged;
        }

        /// <summary>
        /// Removes the events.
        /// </summary>
        private void RemoveEvents(object sender, EventArgs eventArguments)
        {
            _drawingPresentationModel.ButtonEnabledStatesChanged -= UpdateButtonEnabledStates;
            _model.UndoRedoStacksChanged -= UpdateUndoRedoButtonEnabledStates;
            _model.CanvasRefreshDrawRequested -= HandleCanvasRefreshDrawRequested;
            _model.DrawingEnded -= HandleDrawingEnded;
            _model.CanvasCurrentShapeChanged -= HandleCanvasCurrentShapeChanged;
        }

        /// <summary>
        /// Handles the canvas refresh draw requested.
        /// </summary>
        private void HandleCanvasRefreshDrawRequested()
        {
            Invalidate(true); // Triggers canvas Paint event.
        }

        /// <summary>
        /// Handles the drawing ended.
        /// </summary>
        private void HandleDrawingEnded()
        {
            _drawingPresentationModel.HandleDrawingEnded();
            _model.SetCurrentShapeDrawerType(ShapeDrawerType.None);
        }

        /// <summary>
        /// Handles the canvas current shape changed.
        /// </summary>
        private void HandleCanvasCurrentShapeChanged()
        {
            ShapeDrawerType currentShapeType = _model.GetCanvasCurrentShapeType();
            Rectangle currentShapeRectangle = _model.GetCanvasCurrentShapeRectangle();
            if ( currentShapeType == ShapeDrawerType.None && currentShapeRectangle == null )
            {
                _currentShapeInfo.Text = "";
            }
            else
            {
                _currentShapeInfo.Text = Definitions.CURRENT_SHAPE_INFO_SELECTED_TEXT + ShapeDrawerTypeHelper.GetString(currentShapeType) + Definitions.OPENING_BRACKET + currentShapeRectangle.X + Definitions.COMMA_SPACE + currentShapeRectangle.Y + Definitions.COMMA_SPACE + currentShapeRectangle.GetLowerRightX() + Definitions.COMMA_SPACE + currentShapeRectangle.X + Definitions.COMMA_SPACE + currentShapeRectangle.GetLowerRightY() + Definitions.CLOSING_BRACKET;
            }
        }

        /// <summary>
        /// Handles the canvas mouse pressed.
        /// </summary>
        private void HandleCanvasMousePressed(object sender, MouseEventArgs eventArguments)
        {
            HandleCanvasMouseAction(eventArguments, (mousePosition) => _model.HandleCanvasLeftMousePressed(mousePosition));
        }

        /// <summary>
        /// Handles the canvas mouse moved.
        /// </summary>
        private void HandleCanvasMouseMoved(object sender, MouseEventArgs eventArguments)
        {
            HandleCanvasMouseAction(eventArguments, (mousePosition) => _model.HandleCanvasLeftMouseMoved(mousePosition));
        }

        /// <summary>
        /// Handles the canvas mouse released.
        /// </summary>
        private void HandleCanvasMouseReleased(object sender, MouseEventArgs eventArguments)
        {
            HandleCanvasMouseAction(eventArguments, (mousePosition) => _model.HandleCanvasLeftMouseReleased(mousePosition));
        }

        /// <summary>
        /// Handles the canvas mouse action.
        /// </summary>
        private void HandleCanvasMouseAction(MouseEventArgs eventArguments, Action<Point> modelHandleCanvasMouseAction)
        {
            if ( eventArguments.Button == MouseButtons.Left )
            {
                Point mousePosition = new Point(eventArguments.X, eventArguments.Y);
                mousePosition.ResizeToBeInBoundRegion(0, _model.CanvasWidth, 0, _model.CanvasHeight);
                modelHandleCanvasMouseAction(mousePosition);
            }
        }

        /// <summary>
        /// Handles the rectangle button clicked.
        /// </summary>
        private void HandleRectangleButtonClicked(object sender, EventArgs eventArguments)
        {
            _drawingPresentationModel.HandleRectangleButtonClicked();
            _model.SetCurrentShapeDrawerType(ShapeDrawerType.Rectangle);
        }

        /// <summary>
        /// Handles the line button clicked.
        /// </summary>
        private void HandleLineButtonClicked(object sender, EventArgs eventArguments)
        {
            _drawingPresentationModel.HandleLineButtonClicked();
            _model.SetCurrentShapeDrawerType(ShapeDrawerType.Line);
        }

        /// <summary>
        /// Handles the clear button clicked.
        /// </summary>
        private void HandleClearButtonClicked(object sender, EventArgs eventArguments)
        {
            _drawingPresentationModel.HandleClearButtonClicked();
            _model.ClearCanvas();
        }

        /// <summary>
        /// Updates the button enabled states.
        /// </summary>
        private void UpdateButtonEnabledStates()
        {
            _rectangleButton.Enabled = _drawingPresentationModel.RectangleButtonEnabled;
            _lineButton.Enabled = _drawingPresentationModel.LineButtonEnabled;
            _clearButton.Enabled = _drawingPresentationModel.ClearButtonEnabled;
        }

        /// <summary>
        /// Updates the undo redo button enabled states.
        /// </summary>
        private void UpdateUndoRedoButtonEnabledStates()
        {
            _undoButton.Enabled = !_model.IsEmptyCommandsUndoStack();
            _redoButton.Enabled = !_model.IsEmptyCommandsRedoStack();
        }
    }
}
