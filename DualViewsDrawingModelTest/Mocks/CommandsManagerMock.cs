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
        public bool IsCalledIsEmptyUndoStack
        {
            get; set;
        }
        public bool IsCalledIsEmptyRedoStack
        {
            get; set;
        }

        public CommandsManagerMock()
        {
            IsCalledAddThenExecuteCommand = false;
            IsCalledUndo = false;
            IsCalledRedo = false;
            IsCalledIsEmptyUndoStack = false;
            IsCalledIsEmptyRedoStack = false;
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

        /// <summary>
        /// Determines whether [is empty commands undo stack].
        /// </summary>
        public override bool IsEmptyUndoStack()
        {
            IsCalledIsEmptyUndoStack = true;
            return TestDefinitions.DUMP_BOOLEAN;
        }

        /// <summary>
        /// Determines whether [is empty redo stack].
        /// </summary>
        public override bool IsEmptyRedoStack()
        {
            IsCalledIsEmptyRedoStack = true;
            return TestDefinitions.DUMP_BOOLEAN;
        }
    }
}
