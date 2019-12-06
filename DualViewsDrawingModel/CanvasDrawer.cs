using DualViewsDrawingModel.ShapeDrawers;
using System;

namespace DualViewsDrawingModel
{
    public class CanvasDrawer
    {
        public delegate void CanvasRefreshDrawRequestedEventHandler();
        public CanvasRefreshDrawRequestedEventHandler CanvasRefreshDrawRequested
        {
            get; set;
        }
        private const string ERROR_MOUSE_POSITION_IS_NULL = "The given mouse position is null.";
        private const string ERROR_POINT_IS_NULL = "The given point is null.";
        private const string ERROR_PREVIOUS_DRAW_HAS_NOT_ENDED = "Cannot begin a new draw when the previous draw has not ended.";
        private ShapeDrawerType _currentShapeDrawerType;
        private bool _isDrawing;
        private Point _currentDrawingShapeDrawingStartingPoint;
        private ShapeDrawer _currentDrawingShapeHintShapeDrawer;
        private ShapeDrawersManager _shapeDrawersManager;

        public CanvasDrawer()
        {
            _shapeDrawersManager = new ShapeDrawersManager();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize(ShapeDrawerType shapeDrawerType)
        {
            SetCurrentShapeDrawerType(shapeDrawerType);
            ClearCanvas();
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
        /// Handles the canvas left mouse pressed.
        /// </summary>
        public void HandleCanvasLeftMousePressed(Point mousePosition)
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
            if ( _isDrawing )
            {
                throw new ApplicationException(ERROR_PREVIOUS_DRAW_HAS_NOT_ENDED);
            }
            _isDrawing = true;
            _currentDrawingShapeDrawingStartingPoint = mousePosition;
            _currentDrawingShapeHintShapeDrawer = _shapeDrawersManager.CreateShapeDrawer(mousePosition, mousePosition, _currentShapeDrawerType);
        }

        /// <summary>
        /// Handles the canvas left mouse moved.
        /// </summary>
        public void HandleCanvasLeftMouseMoved(Point mousePosition)
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
            _currentDrawingShapeHintShapeDrawer.DrawingEndingPoint = mousePosition;
            NotifyCanvasRefreshDrawRequested();
        }

        /// <summary>
        /// Handles the canvas left mouse released.
        /// </summary>
        public void HandleCanvasLeftMouseReleased(Point mousePosition)
        {
            if ( !_isDrawing )
            {
                return;
            }
            EndDrawing(mousePosition);
        }

        /// <summary>
        /// Ends the drawing.
        /// </summary>
        private void EndDrawing(Point mousePosition)
        {
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
