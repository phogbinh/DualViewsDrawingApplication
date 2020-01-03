using DualViewsDrawingModel;
using DualViewsDrawingModel.Shapes;
using DualViewsDrawingWindowsUniversalApplication.Views.Utilities;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace DualViewsDrawingWindowsUniversalApplication.Views
{
    public sealed partial class DrawingPage : Page
    {
        private const double CANVAS_DRAWING_REGION_TO_CANVAS_OFFSET = 10.0;
        private DrawingPresentationModel _drawingPresentationModel;
        private Model _model;
        private DrawingPageGraphicsAdapter _graphicsAdapter;

        public DrawingPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs eventArguments)
        {
            base.OnNavigatedTo(eventArguments);
            DrawingPageNavigationEventArgumentsParameter drawingPageNavigationEventArgumentsParameter = ( DrawingPageNavigationEventArgumentsParameter )eventArguments.Parameter;
            Initialize(drawingPageNavigationEventArgumentsParameter.DrawingPresentationModel, drawingPageNavigationEventArgumentsParameter.Model);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize(DrawingPresentationModel drawingPresentationModelData, Model modelData)
        {
            _drawingPresentationModel = drawingPresentationModelData;
            _model = modelData;
            _graphicsAdapter = new DrawingPageGraphicsAdapter(_canvas);
            // Observers
            SubscribeEvents();
            // UI
            SubscribeViewEvents();
            // Initial UI States
            _drawingPresentationModel.Initialize();
            UpdateUndoRedoButtonEnabledStates();
            _canvas.Loaded += (sender, eventArguments) => _model.Initialize(_canvas.ActualWidth, _canvas.ActualHeight, ShapeDrawerType.None); // The actual width and height of the canvas can only be determined after it is completely loaded.
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
        /// Subscribes the view events.
        /// </summary>
        private void SubscribeViewEvents()
        {
            _canvas.SizeChanged += (sender, eventArguments) => _model.SetCanvasSize(_canvas.ActualWidth, _canvas.ActualHeight);
            _canvas.PointerPressed += HandleCanvasMousePressed;
            _canvas.PointerMoved += HandleCanvasMouseMoved;
            _canvas.PointerReleased += HandleCanvasMouseReleased;
            _rectangleButton.Click += HandleRectangleButtonClicked;
            _lineButton.Click += HandleLineButtonClicked;
            _clearButton.Click += HandleClearButtonClicked;
            _undoButton.Click += (sender, eventArguments) => _model.Undo();
            _redoButton.Click += (sender, eventArguments) => _model.Redo();
        }

        /// <summary>
        /// Invoked immediately before the Page is unloaded and is no longer the current source of a parent Frame.
        /// </summary>
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs eventArguments)
        {
            RemoveEvents();
        }

        /// <summary>
        /// Removes the events.
        /// </summary>
        private void RemoveEvents()
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
            _model.RefreshDrawCanvas(_graphicsAdapter);
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
        private void HandleCanvasMousePressed(object sender, PointerRoutedEventArgs eventArguments)
        {
            PointerPoint pointerPoint = eventArguments.GetCurrentPoint(_canvas);
            Point mousePosition = new Point(pointerPoint.Position.X, pointerPoint.Position.Y);
            _model.HandleCanvasLeftMouseReleased(mousePosition); // Forcibly ends the current drawing to counter the bug caused by Windows Universal Application - the pointer moved event is not triggered when the pointer is dragged extremely fast out of the canvas, which leaves the current drawing not being finished if the mouse position obtained from the most recent pointer moved event invocation stays valid inside the canvas drawing region.
            if ( pointerPoint.Properties.IsLeftButtonPressed )
            {
                _model.HandleCanvasLeftMousePressed(mousePosition);
            }
        }

        /// <summary>
        /// Handles the canvas mouse moved.
        /// </summary>
        private void HandleCanvasMouseMoved(object sender, PointerRoutedEventArgs eventArguments)
        {
            PointerPoint pointerPoint = eventArguments.GetCurrentPoint(_canvas);
            if ( pointerPoint.Properties.IsLeftButtonPressed )
            {
                HandleCanvasLeftMouseMoved(new Point(pointerPoint.Position.X, pointerPoint.Position.Y));
            }
        }

        /// <summary>
        /// Handles the canvas left mouse moved.
        /// </summary>
        private void HandleCanvasLeftMouseMoved(Point mousePosition)
        {
            mousePosition.ResizeToBeInBoundRegion(0, _model.CanvasWidth, 0, _model.CanvasHeight); // This mouse position resizing is required to counter the bug of Windows Universal Application - the pointer moved event is sometimes triggered when the pointer is at the edges of the canvas or slightly outside the range of the canvas.
            // Unlike Windows Forms, Windows Universal Application does not trigger the pointer released event when the pointer is outside the range of the canvas.
            // This causes a problem: If the user presses, dragging the mouse outside of the canvas and then releases, no new shape is drawn. Instead, the current drawing shape hint is stuck on the canvas and if the user then presses to start a new drawing, my exception set in the model will be thrown, stating that the previous draw has not finished.
            // My solution to the fore-mentioned problem: If the user presses, dragging the mouse outside of the canvas, the model left mouse released event handler will be manually called to end the current drawing, regardless whether the user releases the mouse or not.
            // A propagated problem to my solution: Since in the Windows Universal Application, the pointer moved event, in most of the time, is not triggered when the pointer is at the edges of the canvas and outside the range of the canvas, I cannot detect whether the mouse is dragged outside of the canvas by checking whether the current position of the mouse is inclusively inside the canvas.
            // My solution to the fore-mentioned propagated problem: Instead of checking whether the current position of the mouse is inclusively inside the canvas, I define a canvas drawing region inside the canvas and impose the mouse position check on this region.
            if ( !mousePosition.IsInclusiveInRegion(CANVAS_DRAWING_REGION_TO_CANVAS_OFFSET, _model.CanvasWidth - CANVAS_DRAWING_REGION_TO_CANVAS_OFFSET, CANVAS_DRAWING_REGION_TO_CANVAS_OFFSET, _model.CanvasHeight - CANVAS_DRAWING_REGION_TO_CANVAS_OFFSET) )
            {
                _model.HandleCanvasLeftMouseReleased(mousePosition);
                return;
            }
            _model.HandleCanvasLeftMouseMoved(mousePosition);
        }

        /// <summary>
        /// Handles the canvas mouse released.
        /// </summary>
        private void HandleCanvasMouseReleased(object sender, PointerRoutedEventArgs eventArguments)
        {
            PointerPoint pointerPoint = eventArguments.GetCurrentPoint(_canvas);
            _model.HandleCanvasLeftMouseReleased(new Point(pointerPoint.Position.X, pointerPoint.Position.Y));
        }

        /// <summary>
        /// Handles the rectangle button clicked.
        /// </summary>
        private void HandleRectangleButtonClicked(object sender, RoutedEventArgs eventArguments)
        {
            _drawingPresentationModel.HandleRectangleButtonClicked();
            _model.SetCurrentShapeDrawerType(ShapeDrawerType.Rectangle);
        }

        /// <summary>
        /// Handles the line button clicked.
        /// </summary>
        private void HandleLineButtonClicked(object sender, RoutedEventArgs eventArguments)
        {
            _drawingPresentationModel.HandleLineButtonClicked();
            _model.SetCurrentShapeDrawerType(ShapeDrawerType.Line);
        }

        /// <summary>
        /// Handles the clear button clicked.
        /// </summary>
        private void HandleClearButtonClicked(object sender, RoutedEventArgs eventArguments)
        {
            _drawingPresentationModel.HandleClearButtonClicked();
            _model.ClearCanvas();
        }

        /// <summary>
        /// Updates the button enabled states.
        /// </summary>
        private void UpdateButtonEnabledStates()
        {
            _rectangleButton.IsEnabled = _drawingPresentationModel.RectangleButtonEnabled;
            _lineButton.IsEnabled = _drawingPresentationModel.LineButtonEnabled;
            _clearButton.IsEnabled = _drawingPresentationModel.ClearButtonEnabled;
        }

        /// <summary>
        /// Updates the undo redo button enabled states.
        /// </summary>
        private void UpdateUndoRedoButtonEnabledStates()
        {
            _undoButton.IsEnabled = !_model.IsEmptyCommandsUndoStack();
            _redoButton.IsEnabled = !_model.IsEmptyCommandsRedoStack();
        }
    }
}
