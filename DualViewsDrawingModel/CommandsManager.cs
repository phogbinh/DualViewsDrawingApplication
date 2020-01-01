using DualViewsDrawingModel.Commands;
using System;
using System.Collections.Generic;

namespace DualViewsDrawingModel
{
    public class CommandsManager
    {
        private const string ERROR_COMMAND_IS_NULL = "The given command is null.";
        private Stack<ICommand> _undoStack;
        private Stack<ICommand> _redoStack;

        public CommandsManager()
        {
            _undoStack = new Stack<ICommand>();
            _redoStack = new Stack<ICommand>();
        }

        /// <summary>
        /// Adds then executes command.
        /// </summary>
        public void AddThenExecuteCommand(ICommand command)
        {
            if ( command == null )
            {
                throw new ArgumentNullException(ERROR_COMMAND_IS_NULL);
            }
            AddCommand(command);
            command.Execute();
        }

        /// <summary>
        /// Adds the command.
        /// </summary>
        private void AddCommand(ICommand command)
        {
            if ( command == null )
            {
                throw new ArgumentNullException(ERROR_COMMAND_IS_NULL);
            }
            _undoStack.Push(command);
            _redoStack.Clear();
        }

        /// <summary>
        /// Undoes this instance.
        /// </summary>
        public void Undo()
        {
            ICommand undoCommand = _undoStack.Pop();
            _redoStack.Push(undoCommand);
            undoCommand.UnExecute();
        }

        /// <summary>
        /// Redoes this instance.
        /// </summary>
        public void Redo()
        {
            ICommand redoCommand = _redoStack.Pop();
            _undoStack.Push(redoCommand);
            redoCommand.Execute();
        }
    }
}
