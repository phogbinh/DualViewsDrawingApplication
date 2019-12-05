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
                _model.HandleCanvasLeftMouseMoved(new Point(pointerPoint.Position.X, pointerPoint.Position.Y));
            }
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
