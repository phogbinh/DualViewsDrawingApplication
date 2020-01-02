using DualViewsDrawingModel.Commands;
using DualViewsDrawingModelTest;
using DualViewsDrawingModelTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DualViewsDrawingModel.Test
{
    [TestClass()]
    public class CommandsManagerTest
    {
        private const string MEMBER_VARIABLE_NAME_UNDO_STACK = "_undoStack";
        private const string MEMBER_VARIABLE_NAME_REDO_STACK = "_redoStack";
        private CommandsManager _commandsManager;
        private PrivateObject _target;
        private Stack<ICommand> _undoStack;
        private Stack<ICommand> _redoStack;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize()]
        [DeploymentItem(TestDefinitions.OUTPUT_ITEM_FILE_PATH)]
        public void Initialize()
        {
            _commandsManager = new CommandsManager();
            _target = new PrivateObject(_commandsManager);
            _undoStack = ( Stack<ICommand> )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_UNDO_STACK);
            _redoStack = ( Stack<ICommand> )_target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_REDO_STACK);
        }

        /// <summary>
        /// Tests the commands manager.
        /// </summary>
        [TestMethod()]
        public void TestCommandsManager()
        {
            var commandsManager = new CommandsManager();
            var target = new PrivateObject(commandsManager);
            Assert.IsNotNull(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_UNDO_STACK));
            Assert.IsNotNull(target.GetFieldOrProperty(MEMBER_VARIABLE_NAME_REDO_STACK));
        }

        /// <summary>
        /// Tests the add then execute command.
        /// </summary>
        [TestMethod()]
        public void TestAddThenExecuteCommand()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _commandsManager.AddThenExecuteCommand(null));
            var command = new CommandMock();
            _commandsManager.AddThenExecuteCommand(command);
            Assert.AreSame(command, _undoStack.Pop());
            Assert.AreEqual(_redoStack.Count, 0);
            Assert.IsTrue(command.IsCalledExecute);
        }

        /// <summary>
        /// Tests the add command.
        /// </summary>
        [TestMethod()]
        public void TestAddCommand()
        {
            const string MEMBER_FUNCTION_NAME_ADD_COMMAND = "AddCommand";
            var arguments = new object[] { null };
            TargetInvocationException expectedException = Assert.ThrowsException<TargetInvocationException>(() => _target.Invoke(MEMBER_FUNCTION_NAME_ADD_COMMAND, arguments));
            Assert.IsInstanceOfType(expectedException.InnerException, typeof(ArgumentNullException));
            _redoStack.Push(new CommandMock());
            _redoStack.Push(new CommandMock());
            int count = 0;
            _commandsManager.UndoRedoStacksChanged += () => count++;
            var command = new CommandMock();
            arguments = new object[] { command };
            _target.Invoke(MEMBER_FUNCTION_NAME_ADD_COMMAND, arguments);
            Assert.AreSame(command, _undoStack.Pop());
            Assert.AreEqual(_redoStack.Count, 0);
            Assert.AreEqual(count, 1);
        }

        /// <summary>
        /// Tests the undo.
        /// </summary>
        [TestMethod()]
        public void TestUndo()
        {
            var undoCommand = new CommandMock();
            _undoStack.Push(undoCommand);
            int count = 0;
            _commandsManager.UndoRedoStacksChanged += () => count++;
            _commandsManager.Undo();
            Assert.AreEqual(_undoStack.Count, 0);
            Assert.AreSame(_redoStack.Pop(), undoCommand);
            Assert.IsTrue(undoCommand.IsCalledReverseExecution);
            Assert.AreEqual(count, 1);
        }

        /// <summary>
        /// Tests the redo.
        /// </summary>
        [TestMethod()]
        public void TestRedo()
        {
            var redoCommand = new CommandMock();
            _redoStack.Push(redoCommand);
            int count = 0;
            _commandsManager.UndoRedoStacksChanged += () => count++;
            _commandsManager.Redo();
            Assert.AreEqual(_redoStack.Count, 0);
            Assert.AreSame(_undoStack.Pop(), redoCommand);
            Assert.IsTrue(redoCommand.IsCalledExecute);
            Assert.AreEqual(count, 1);
        }

        /// <summary>
        /// Tests the notify undo redo stacks changed.
        /// </summary>
        [TestMethod()]
        public void TestNotifyUndoRedoStacksChanged()
        {
            const string MEMBER_FUNCTION_NAME_NOTIFY_UNDO_REDO_STACKS_CHANGED = "NotifyUndoRedoStacksChanged";
            int count = 0;
            _commandsManager.UndoRedoStacksChanged += () => count++;
            _target.Invoke(MEMBER_FUNCTION_NAME_NOTIFY_UNDO_REDO_STACKS_CHANGED);
            Assert.AreEqual(count, 1);
            _target.Invoke(MEMBER_FUNCTION_NAME_NOTIFY_UNDO_REDO_STACKS_CHANGED);
            Assert.AreEqual(count, 2);
        }

        /// <summary>
        /// Tests the is empty undo stack.
        /// </summary>
        [TestMethod()]
        public void TestIsEmptyUndoStack()
        {
            Assert.IsTrue(_commandsManager.IsEmptyUndoStack());
            _undoStack.Push(new CommandMock());
            Assert.IsFalse(_commandsManager.IsEmptyUndoStack());
        }

        /// <summary>
        /// Tests the is empty redo stack.
        /// </summary>
        [TestMethod()]
        public void TestIsEmptyRedoStack()
        {
            Assert.IsTrue(_commandsManager.IsEmptyRedoStack());
            _redoStack.Push(new CommandMock());
            Assert.IsFalse(_commandsManager.IsEmptyRedoStack());
        }
    }
}