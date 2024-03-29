﻿using System;

namespace TuringMachine.Model
{
    class CannotExecuteException : Exception
    {
        public CannotExecuteException()
            : base()
        {
               
        }
        public string WrongActionText { get; set; }
        public CannotExecuteException(string message, string wrongCommand)
            : base(message)
        {
            WrongActionText = wrongCommand;
        }
    }
}
