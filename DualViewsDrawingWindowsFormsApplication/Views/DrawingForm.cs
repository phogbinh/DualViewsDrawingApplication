using DualViewsDrawingModel;
using DualViewsDrawingWindowsFormsApplication.Views.Utilities;
using System;
using System.Windows.Forms;

namespace DualViewsDrawingWindowsFormsApplication.Views
{
    public partial class DrawingForm : Form
    {
        private Model _model;

        public DrawingForm(Model modelData)
        {
            InitializeComponent();
            _model = modelData;
            this.Disposed += RemoveEvents;
            // Observers
            _model.CanvasRefreshDrawRequested += HandleCanvasRefreshDrawRequested;
            // UI
            _canvas.Resize += (sender, eventArguments) => _model.SetCanvasSize(_canvas.Size.Width, _canvas.Size.Height);
            _canvas.Paint += (sender, eventArguments) => _model.RefreshDrawCanvas(new DrawingFormGraphicsAdapter(eventArguments.Graphics));
            _canvas.MouseDown += HandleCanvasMousePressed;
            _canvas.MouseMove += HandleCanvasMouseMoved;
            _canvas.MouseUp += HandleCanvasMouseReleased;
            _rectangleButton.Click += HandleRectangleButtonClicked;
            _lineButton.Click += HandleLineButtonClicked;
            _clearButton.Click += HandleClearButtonClicked;
            // Initial UI States
            _model.Initialize(_canvas.Size.Width, _canvas.Size.Height, ShapeDrawerType.None);
        }

        /// <summary>
        /// Removes the events.
        /// </summary>
        private void RemoveEvents(object sender, EventArgs eventArguments)
        {
            _model.CanvasRefreshDrawRequested -= HandleCanvasRefreshDrawRequested;
        }

        /// <summary>
        /// Handles the canvas refresh draw requested.
        /// </summary>
        private void HandleCanvasRefreshDrawRequested()
        {
            Invalidate(true); // Triggers canvas Paint event.
        }

        /// <summary>
        /// Handles the canvas mouse pressed.
        /// </summary>
        private void HandleCanvasMousePressed(object sender, MouseEventArgs eventArguments)
        {
            if ( eventArguments.Button == MouseButtons.Left )
            {
                _model.HandleCanvasLeftMousePressed(new Point(eventArguments.X, eventArguments.Y));
            }
        }

        /// <summary>
        /// Handles the canvas mouse moved.
        /// </summary>
        private void HandleCanvasMouseMoved(object sender, MouseEventArgs eventArguments)
        {
            if ( eventArguments.Button == MouseButtons.Left )
            {
                _model.HandleCanvasLeftMouseMoved(new Point(eventArguments.X, eventArguments.Y));
            }
        }

        /// <summary>
        /// Handles the canvas mouse released.
        /// </summary>
        private void HandleCanvasMouseReleased(object sender, MouseEventArgs eventArguments)
        {
            if ( eventArguments.Button == MouseButtons.Left )
            {
                _model.HandleCanvasLeftMouseReleased(new Point(eventArguments.X, eventArguments.Y));
            }
        }

        /// <summary>
        /// Handles the rectangle button clicked.
        /// </summary>
        private void HandleRectangleButtonClicked(object sender, EventArgs eventArguments)
        {
            _model.SetCurrentShapeDrawerType(ShapeDrawerType.Rectangle);
            _lineButton.Enabled = true;
            _rectangleButton.Enabled = false;
        }

        /// <summary>
        /// Handles the line button clicked.
        /// </summary>
        private void HandleLineButtonClicked(object sender, EventArgs eventArguments)
        {
            _model.SetCurrentShapeDrawerType(ShapeDrawerType.Line);
            _lineButton.Enabled = false;
            _rectangleButton.Enabled = true;
        }

        /// <summary>
        /// Handles the clear button clicked.
        /// </summary>
        private void HandleClearButtonClicked(object sender, EventArgs eventArguments)
        {
            _model.ClearCanvas();
            _lineButton.Enabled = true;
            _rectangleButton.Enabled = true;
        }
    }
}
