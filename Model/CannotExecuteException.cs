using System;

namespace TuringMachine.Model
{
    class CannotExecuteException : Exception
    {
        public string WrongActionText { get; }
        public CannotExecuteException(string message, string wrongCommand)
            : base(message)
        {
            WrongActionText = wrongCommand;
        }
    }
}