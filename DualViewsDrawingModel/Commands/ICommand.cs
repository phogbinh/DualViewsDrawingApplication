namespace DualViewsDrawingModel.Commands
{
    public interface ICommand
    {
        /// <summary>
        /// Executes this instance.
        /// </summary>
        void Execute();

        /// <summary>
        /// Reverses the execution.
        /// </summary>
        void ReverseExecution();
    }
}
