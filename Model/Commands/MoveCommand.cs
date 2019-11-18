using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringMachine.Model.Commands
{
    class MoveCommand
    {       
        public void Execute(AlphabetCell executableCell, Slide slider)
        {
            var action = executableCell.CurrentState.Action[1];
            if (IsValid(action) && executableCell.IsExecute)
            {
                if (action == '<')
                    slider.Controller.MoveLeft(null);
                if (action == '>')
                    slider.Controller.MoveRight(null);
            }
            else throw new CannotExecuteException("Невозможно выполнить команду", executableCell.CurrentState.Action);
        }

        public bool IsValid(char action)
        {
            if (action == '<' || action == '>')
                return true;
            return false;
        }
    }
}
