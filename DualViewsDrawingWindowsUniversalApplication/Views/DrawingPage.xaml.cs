using DualViewsDrawingModel;
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
            Initialize(( Model )eventArguments.Parameter);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize(Model modelData)
        {
            _model = modelData;
            _graphicsAdapter = new DrawingPageGraphicsAdapter(_canvas);
            // Observers
            _model.CanvasRefreshDrawRequested += HandleCanvasRefreshDrawRequested;
            // UI
            _canvas.SizeChanged += (sender, eventArguments) => _model.SetCanvasSize(_canvas.ActualWidth, _canvas.ActualHeight);
            _canvas.PointerPressed += HandleCanvasMousePressed;
            _canvas.PointerMoved += HandleCanvasMouseMoved;
            _canvas.PointerReleased += HandleCanvasMouseReleased;
            _rectangleButton.Click += HandleRectangleButtonClicked;
            _lineButton.Click += HandleLineButtonClicked;
            _clearButton.Click += HandleClearButtonClicked;
            // Initial UI States
            _canvas.Loaded += (sender, eventArguments) => _model.Initialize(_canvas.ActualWidth, _canvas.ActualHeight, ShapeDrawerType.None); // The actual width and height of the canvas can only be determined after it is completely loaded.
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
            _model.CanvasRefreshDrawRequested -= HandleCanvasRefreshDrawRequested;
        }

        /// <summary>
        /// Handles the canvas refresh draw requested.
        /// </summary>
        private void HandleCanvasRefreshDrawRequested()
        {
            _model.RefreshDrawCanvas(_graphicsAdapter);
        }

        /// <summary>
        /// Handles the canvas mouse pressed.
        /// </summary>
        private void HandleCanvasMousePressed(object sender, PointerRoutedEventArgs eventArguments)
        {
            PointerPoint pointerPoint = eventArguments.GetCurrentPoint(_canvas);
            _model.HandleCanvasLeftMouseReleased(new Point(pointerPoint.Position.X, pointerPoint.Position.Y));
            if ( pointerPoint.Properties.IsLeftButtonPressed )
            {
                _model.HandleCanvasLeftMousePressed(new Point(pointerPoint.Position.X, pointerPoint.Position.Y));
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
            mousePosition.ResizeToBeInBoundRegion(0, _model.GetCanvasWidth(), 0, _model.GetCanvasHeight()); // This mouse position resizing is required to counter the bug of Windows Universal Application - the pointer moved event is sometimes triggered when the pointer is at the edges of the canvas or slightly outside the range of the canvas.
            // Unlike Windows Forms, Windows Universal Application does not trigger the pointer released event when the pointer is outside the range of the canvas.
            // This causes a problem: If the user presses, dragging the mouse outside of the canvas and then releases, no new shape is drawn. Instead, the current drawing shape hint is stuck on the canvas and if the user then presses to start a new drawing, my exception set in the model will be thrown, stating that the previous draw has not finished.
            // My solution to the fore-mentioned problem: If the user presses, dragging the mouse outside of the canvas, the model left mouse released event handler will be manually called to end the current drawing, regardless whether the user releases the mouse or not.
            // A propagated problem to my solution: Since in the Windows Universal Application, the pointer moved event, in most of the time, is not triggered when the pointer is at the edges of the canvas and outside the range of the canvas, I cannot detect whether the mouse is dragged outside of the canvas by checking whether the current position of the mouse is inclusively inside the canvas.
            // My solution to the fore-mentioned propagated problem: Instead of checking whether the current position of the mouse is inclusively inside the canvas, I define a canvas drawing region inside the canvas and impose the mouse position check on this region.
            if ( !mousePosition.IsInclusiveInRegion(CANVAS_DRAWING_REGION_TO_CANVAS_OFFSET, _model.GetCanvasWidth() - CANVAS_DRAWING_REGION_TO_CANVAS_OFFSET, CANVAS_DRAWING_REGION_TO_CANVAS_OFFSET, _model.GetCanvasHeight() - CANVAS_DRAWING_REGION_TO_CANVAS_OFFSET) )
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
            _model.SetCurrentShapeDrawerType(ShapeDrawerType.Rectangle);
            _lineButton.IsEnabled = true;
            _rectangleButton.IsEnabled = false;
        }

        /// <summary>
        /// Handles the line button clicked.
        /// </summary>
        private void HandleLineButtonClicked(object sender, RoutedEventArgs eventArguments)
        {
            _model.SetCurrentShapeDrawerType(ShapeDrawerType.Line);
            _lineButton.IsEnabled = false;
            _rectangleButton.IsEnabled = true;
        }

        /// <summary>
        /// Handles the clear button clicked.
        /// </summary>
        private void HandleClearButtonClicked(object sender, RoutedEventArgs eventArguments)
        {
            _model.ClearCanvas();
            _lineButton.IsEnabled = true;
            _rectangleButton.IsEnabled = true;
        }
    }
}
