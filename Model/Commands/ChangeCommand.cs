using System.Collections.ObjectModel;
using System.Linq;

namespace TuringMachine.Model.Commands
{
    class ChangeCommand
    {
        ObservableCollection<AlphabetCell> _cells;
        public void Execute(AlphabetCell executableCell, ObservableCollection<AlphabetCell> cells, Slide slider)
        {
            _cells = cells;
            var action = executableCell.CurrentState.Action[0].ToString();
            if (IsValid(action))
            {
                if (action != ".")
                    slider.Controller.UpdateCellValue(action);
                    //slider.Cells.FirstOrDefault(x => x.IsActive).Value = action;
            }
            else throw new CannotExecuteException("Найден несуществующий элемент алфавита!", executableCell.CurrentState.Action);
        }
        /// <summary>
        /// Проверка есть ли в алфавите символ, на который требуется заменить текущий
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool IsValid(string action)
        {
            foreach (var item in _cells.Select(x => x.Name))
            {
                if (item.Contains(action) || action == ".")
                    return true;
            }
            return false;
        }
    }
}
