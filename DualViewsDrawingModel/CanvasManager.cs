using DualViewsDrawingModel.ShapeDrawers;
using System;

namespace DualViewsDrawingModel
{
    public class CanvasManager
    {
        public delegate void CanvasRefreshDrawRequestedEventHandler();
        public CanvasRefreshDrawRequestedEventHandler CanvasRefreshDrawRequested
        {
            get; set;
        }
        private const string ERROR_CANVAS_WIDTH_IS_NOT_POSITIVE = "The given canvas width is not positive.";
        private const string ERROR_CANVAS_HEIGHT_IS_NOT_POSITIVE = "The given canvas height is not positive.";
        private const string ERROR_MOUSE_POSITION_IS_NULL = "The given mouse position is null.";
        private const string ERROR_POINT_IS_NULL = "The given point is null.";
        private const string ERROR_PREVIOUS_DRAW_HAS_NOT_ENDED = "Cannot begin a new draw when the previous draw has not ended.";
        private double _canvasWidth;
        private double _canvasHeight;
        private ShapeDrawerType _currentShapeDrawerType;
        private bool _isDrawing;
        private Point _currentDrawingShapeDrawingStartingPoint;
        private ShapeDrawer _currentDrawingShapeHintShapeDrawer;
        private ShapeDrawersManager _shapeDrawersManager;

        public CanvasManager()
        {
            _shapeDrawersManager = new ShapeDrawersManager();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize(double canvasWidth, double canvasHeight, ShapeDrawerType shapeDrawerType)
        {
            SetCanvasSize(canvasWidth, canvasHeight);
            SetCurrentShapeDrawerType(shapeDrawerType);
            ClearCanvas();
        }

        /// <summary>
        /// Sets the size of the canvas.
        /// </summary>
        public void SetCanvasSize(double canvasWidth, double canvasHeight)
        {
            if ( canvasWidth <= 0 )
            {
                throw new ArgumentException(ERROR_CANVAS_WIDTH_IS_NOT_POSITIVE);
            }
            if ( canvasHeight <= 0 )
            {
                throw new ArgumentException(ERROR_CANVAS_HEIGHT_IS_NOT_POSITIVE);
            }
            _canvasWidth = canvasWidth;
            _canvasHeight = canvasHeight;
        }

        /// <summary>
        /// Sets the type of the current drawing shape.
        /// </summary>
        public void SetCurrentShapeDrawerType(ShapeDrawerType drawingShapeType)
        {
            if ( !ShapeDrawerTypeHelper.IsValidShapeDrawerType(drawingShapeType) )
            {
                throw new ArgumentException(Definitions.ERROR_SHAPE_DRAWER_TYPE_IS_INVALID);
            }
            _currentShapeDrawerType = drawingShapeType;
        }

        /// <summary>
        /// Clears the canvas.
        /// </summary>
        public void ClearCanvas()
        {
            _isDrawing = false;
            _currentDrawingShapeDrawingStartingPoint = null;
            _currentDrawingShapeHintShapeDrawer = null;
            _shapeDrawersManager.Clear();
            NotifyCanvasRefreshDrawRequested();
        }

        /// <summary>
        /// Notifies the canvas refresh draw requested.
        /// </summary>
        private void NotifyCanvasRefreshDrawRequested()
        {
            if ( CanvasRefreshDrawRequested != null )
            {
                CanvasRefreshDrawRequested();
            }
        }

        /// <summary>
        /// Handles the canvas mouse pressed.
        /// </summary>
        public void HandleCanvasMousePressed(Point mousePosition)
        {
            if ( _currentShapeDrawerType == ShapeDrawerType.None )
            {
                return;
            }
            BeginDrawing(mousePosition);
        }

        /// <summary>
        /// Begins the drawing.
        /// </summary>
        private void BeginDrawing(Point mousePosition)
        {
            if ( mousePosition == null )
            {
                throw new ArgumentNullException(ERROR_MOUSE_POSITION_IS_NULL);
            }
            if ( !IsInclusiveInCanvas(mousePosition) )
            {
                // TODO: Throw exception.
                return;
            }
            if ( _isDrawing )
            {
                throw new ApplicationException(ERROR_PREVIOUS_DRAW_HAS_NOT_ENDED);
            }
            _isDrawing = true;
            _currentDrawingShapeDrawingStartingPoint = mousePosition;
            _currentDrawingShapeHintShapeDrawer = _shapeDrawersManager.CreateShapeDrawer(mousePosition, mousePosition, _currentShapeDrawerType);
        }

        /// <summary>
        /// Determines whether [is inclusively in canvas] [the specified point].
        /// </summary>
        private bool IsInclusiveInCanvas(Point point)
        {
            if ( point == null )
            {
                throw new ArgumentNullException(ERROR_POINT_IS_NULL);
            }
            return point.IsInclusiveInRegion(0, _canvasWidth, 0, _canvasHeight);
        }

        /// <summary>
        /// Handles the canvas mouse moved.
        /// </summary>
        public void HandleCanvasMouseMoved(Point mousePosition)
        {
            if ( !_isDrawing )
            {
                return;
            }
            UpdateCurrentDrawingShapeHint(mousePosition);
        }

        /// <summary>
        /// Updates the current drawing shape hint.
        /// </summary>
        private void UpdateCurrentDrawingShapeHint(Point mousePosition)
        {
            if ( mousePosition == null )
            {
                throw new ArgumentNullException(ERROR_MOUSE_POSITION_IS_NULL);
            }
            if ( !IsInclusiveInCanvas(mousePosition) )
            {
                // TODO: Throw exception.
                return;
            }
            _currentDrawingShapeHintShapeDrawer.DrawingEndingPoint = mousePosition;
            NotifyCanvasRefreshDrawRequested();
        }

        /// <summary>
        /// Handles the canvas mouse released.
        /// </summary>
        public void HandleCanvasMouseReleased(Point mousePosition)
        {
            if ( mousePosition == null )
            {
                throw new ArgumentNullException(ERROR_MOUSE_POSITION_IS_NULL);
            }
            if ( !IsInclusiveInCanvas(mousePosition) )
            {
                // TODO: Throw exception.
                return;
            }
            if ( _currentShapeDrawerType == ShapeDrawerType.None )
            {
                return;
            }
            if ( !_isDrawing )
            {
                return;
            }
            _shapeDrawersManager.AddShapeDrawer(_currentDrawingShapeDrawingStartingPoint, mousePosition, _currentShapeDrawerType);
            _isDrawing = false;
            NotifyCanvasRefreshDrawRequested();
        }

        /// <summary>
        /// Redraw the canvas.
        /// </summary>
        public void RefreshDrawCanvas(IGraphics graphics)
        {
            if ( graphics == null )
            {
                throw new ArgumentNullException(Definitions.ERROR_GRAPHICS_IS_NULL);
            }
            graphics.ClearAll();
            Draw(graphics);
        }

        /// <summary>
        /// Draws the specified graphics.
        /// </summary>
        private void Draw(IGraphics graphics)
        {
            _shapeDrawersManager.Draw(graphics);
            if ( _isDrawing )
            {
                _currentDrawingShapeHintShapeDrawer.Draw(graphics);
            }
        }
    }
}
