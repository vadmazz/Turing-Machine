using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace TuringMachine.Model.Commands
{
    class SetStateCommand
    {
        public bool IsEnd { get; set; }
        public void Execute(ref AlphabetCell executableCell, ObservableCollection<AlphabetCell> cells, Slide slider)
        {
            var currentSlideCell = slider.Cells.FirstOrDefault(x => x.IsActive);            
            if (currentSlideCell.Value == null)
                throw new CannotExecuteException("На каретке нет требуемого символа!", executableCell.Name);
            var action = $"{executableCell.CurrentState.Action[2]}{executableCell.CurrentState.Action[3]}";
            if (action == null)
                action = "Q1";
            var nextExec = cells.FirstOrDefault(x => x.Name == currentSlideCell.Value);           
            var states = nextExec.States;
            if (IsValid(action, states))
            {                
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
            if (states.Select(x => x.Name).Contains(action))
                return true;
            return false;
        }
    }
}
