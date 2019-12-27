using DualViewsDrawingModel;

namespace DualViewsDrawingWindowsUniversalApplication.Views.Utilities
{
    public class DrawingPageNavigationEventArgumentsParameter
    {
        public DrawingPresentationModel DrawingPresentationModel
        {
            get; set;
        }
        public Model Model
        {
            get; set;
        }

        public DrawingPageNavigationEventArgumentsParameter(DrawingPresentationModel drawingPresentationModelData, Model modelData)
        {
            DrawingPresentationModel = drawingPresentationModelData;
            Model = modelData;
        }
    }
}
