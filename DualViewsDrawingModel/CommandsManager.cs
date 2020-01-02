using DualViewsDrawingModel.Commands;
using System;
using System.Collections.Generic;

namespace DualViewsDrawingModel
{
    public class CommandsManager
    {
        public delegate void UndoRedoStacksChangedEventHandler();
        public UndoRedoStacksChangedEventHandler UndoRedoStacksChanged
        {
            get; set;
        }
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
        public virtual void AddThenExecuteCommand(ICommand command)
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
            NotifyUndoRedoStacksChanged();
        }

        /// <summary>
        /// Undoes this instance.
        /// </summary>
        public virtual void Undo()
        {
            ICommand undoCommand = _undoStack.Pop();
            _redoStack.Push(undoCommand);
            undoCommand.ReverseExecution();
            NotifyUndoRedoStacksChanged();
        }

        /// <summary>
        /// Redoes this instance.
        /// </summary>
        public virtual void Redo()
        {
            ICommand redoCommand = _redoStack.Pop();
            _undoStack.Push(redoCommand);
            redoCommand.Execute();
            NotifyUndoRedoStacksChanged();
        }

        /// <summary>
        /// Notifies the undo redo stacks changed.
        /// </summary>
        private void NotifyUndoRedoStacksChanged()
        {
            if ( UndoRedoStacksChanged != null )
            {
                UndoRedoStacksChanged();
            }
        }

        /// <summary>
        /// Determines whether [is empty commands undo stack].
        /// </summary>
        public virtual bool IsEmptyUndoStack()
        {
            return _undoStack.Count == 0;
        }

        /// <summary>
        /// Determines whether [is empty redo stack].
        /// </summary>
        public virtual bool IsEmptyRedoStack()
        {
            return _redoStack.Count == 0;
        }
    }
}
