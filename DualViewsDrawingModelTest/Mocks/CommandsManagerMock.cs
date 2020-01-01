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
        public bool IsCalledUndo
        {
            get; set;
        }
        public bool IsCalledRedo
        {
            get; set;
        }

        public CommandsManagerMock()
        {
            IsCalledAddThenExecuteCommand = false;
            IsCalledUndo = false;
            IsCalledRedo = false;
        }

        /// <summary>
        /// Adds then executes command.
        /// </summary>
        public override void AddThenExecuteCommand(ICommand command)
        {
            IsCalledAddThenExecuteCommand = true;
        }

        /// <summary>
        /// Undoes this instance.
        /// </summary>
        public override void Undo()
        {
            IsCalledUndo = true;
        }

        /// <summary>
        /// Redoes this instance.
        /// </summary>
        public override void Redo()
        {
            IsCalledRedo = true;
        }
    }
}
