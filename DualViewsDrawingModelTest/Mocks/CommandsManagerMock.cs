using DualViewsDrawingModel;
using DualViewsDrawingModel.Commands;

namespace DualViewsDrawingModelTest.Mocks
{
    public class CommandsManagerMock : CommandsManager
    {
        public bool IsCalledAddThenExecuteCommand
        {
            get; set;
        }

        public CommandsManagerMock()
        {
            IsCalledAddThenExecuteCommand = false;
        }

        /// <summary>
        /// Adds then executes command.
        /// </summary>
        public override void AddThenExecuteCommand(ICommand command)
        {
            IsCalledAddThenExecuteCommand = true;
        }
    }
}
