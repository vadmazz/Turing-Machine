using System.Collections.Generic;
using System.Linq;

namespace TuringMachine.Model
{
    public class SlideControl
    {
        private IEnumerable<SlideCell> _cells;
        public SlideControl(IEnumerable<SlideCell> cells)
        {
            _cells = cells;
            var index = _cells.ToList()//находим нулевой элемент
                .Where(x => x.Number == 0)
                .FirstOrDefault();
            SetActiveCell(_cells.ToList().IndexOf(index));
        }

        /// <summary>
        /// Переопределить активный элемент каретки
        /// </summary>
        /// <param name="_cells">Массив элементов каретки</param>
        /// <param name="index">Индекс активного элемента</param>
        /// <returns></returns>
        public void SetActiveCell(int index)
        {
            if (HasOneActive())
            {
                var active = _cells
                    .First(x => x.IsActive);
                active.IsActive = false;
            }
            _cells.ToList()[index].IsActive = true;
        }

        public void MoveRight(object parameter)
        {
            if (HasOneActive())
            {
                var current = _cells
                    .Where(x => x.IsActive)
                    .Select(x => x)
                    .FirstOrDefault();
                var activeIndex = _cells.ToList().IndexOf(current);
                if (activeIndex < _cells.Count() - 1)
                    SetActiveCell(activeIndex + 1);
            }
        }

        public void MoveLeft(object parameter)
        {
            if (HasOneActive())
            {
                var current = _cells
                    .Where(x => x.IsActive)
                    .Select(x => x)
                    .FirstOrDefault();
                var activeIndex = _cells.ToList().IndexOf(current);
                if (activeIndex > 0)
                    SetActiveCell(activeIndex - 1);
            }
        }

        private bool HasOneActive()
        {
            var query = _cells
                .Where(x => x.IsActive)
                .Select(x => x);
            if (query.Count() == 1 && query.FirstOrDefault() != null)//если только один элемент активен и есть активный
                return true;
            return false;
        }
    }
}
