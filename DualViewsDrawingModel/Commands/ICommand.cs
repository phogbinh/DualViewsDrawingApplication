namespace DualViewsDrawingModel.Commands
{
    public interface ICommand
    {
        /// <summary>
        /// Executes this instance.
        /// </summary>
        void Execute();

        /// <summary>
        /// Un-executes this instance.
        /// </summary>
        void UnExecute();
    }
}
