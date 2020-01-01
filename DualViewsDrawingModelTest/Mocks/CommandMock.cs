using DualViewsDrawingModel.Commands;

namespace DualViewsDrawingModelTest.Mocks
{
    public class CommandMock : ICommand
    {
        public bool IsCalledExecute
        {
            get; set;
        }
        public bool IsCalledUnExecute
        {
            get; set;
        }

        public CommandMock()
        {
            IsCalledExecute = false;
            IsCalledUnExecute = false;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public void Execute()
        {
            IsCalledExecute = true;
        }

        /// <summary>
        /// Un-executes this instance.
        /// </summary>
        public void UnExecute()
        {
            IsCalledUnExecute = true;
        }
    }
}
