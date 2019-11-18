using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace TuringMachine.Model.Commands
{
    class SetStateCommand
    {
        
        public void Execute(AlphabetCell executableCell, ObservableCollection<AlphabetCell> cells, Slide slider)
        {
            var action = $"{executableCell.CurrentState.Action[2]}{executableCell.CurrentState.Action[3]}";
            //var states = executableCell.States;
            var currentSlideCell = slider.Cells.FirstOrDefault(x => x.IsActive);            
            var nextExec = cells.FirstOrDefault(x => x.Name == currentSlideCell.Value);
            var states = nextExec.States;
            if (IsValid(action, states))
            {
                //if (action == ".")

                var newcell = slider.Cells.FirstOrDefault(x => x.IsActive);
                executableCell.IsExecute = false;                
                executableCell = cells.FirstOrDefault(x => x.Name == newcell.Value);
                executableCell.IsExecute = true;
                executableCell.CurrentState = executableCell.States.FirstOrDefault(x => x.Name == action);
            }
            else throw new CannotExecuteException("Указано несуществующее состояние!", action);
        }

        public bool IsValid(string action, ObservableCollection<State> states)
        {
            if (states.Select(x => x.Name).Contains(action) || action == ".")
                return true;
            return false;
        }
    }
}
