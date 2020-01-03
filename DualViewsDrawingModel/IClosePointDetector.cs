namespace DualViewsDrawingModel
{
    public interface IClosePointDetector
    {
        /// <summary>
        /// Determines whether [is close to point].
        /// </summary>
        bool IsCloseToPoint(Point point, double pointToDetectorMaximumDistanceSquared);
    }
}
