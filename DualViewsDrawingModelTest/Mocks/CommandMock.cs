using DualViewsDrawingModel.Commands;

namespace DualViewsDrawingModelTest.Mocks
{
    public class CommandMock : ICommand
    {
        public bool IsCalledExecute
        {
            get; set;
        }
        public bool IsCalledReverseExecution
        {
            get; set;
        }

        public CommandMock()
        {
            IsCalledExecute = false;
            IsCalledReverseExecution = false;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public void Execute()
        {
            IsCalledExecute = true;
        }

        /// <summary>
        /// Reverses the execution.
        /// </summary>
        public void ReverseExecution()
        {
            IsCalledReverseExecution = true;
        }
    }
}
