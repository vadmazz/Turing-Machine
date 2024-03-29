﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TuringMachine.Model
{
    public class SlideControl
    {
        private ObservableCollection<SlideCell> _cells;
        public SlideControl(ObservableCollection<SlideCell> cells)
        {
            _cells = cells;
            var index = _cells//устанавливаем каретку в нулевой элемент
                .ToList()
                .FirstOrDefault(x => x.Number == 0);
            SetActiveCell(_cells.ToList().IndexOf(index));
            //_cells[0].Value = " ";     
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

        public void AddRight(object parameter)
        {
            var maxCellIndex = _cells
                .Select(x => x.Number)
                .Max();
            _cells.Add(new SlideCell { Number = maxCellIndex + 1, Value = " "});
        }

        public void AddLeft(object parameter)
        {
            var minCellIndex = _cells
                .Select(x => x.Number)
                .Min();
            var cell = new SlideCell { Number = minCellIndex - 1, Value = " " };
            var count = _cells.Count;
            for (int i = count; i >= 0; i--)
            {
                if (i == count)
                {
                    var t = _cells[i - 1];
                    _cells.Add(t);
                }
                if (i == 0)
                {
                    _cells[i] = cell;
                    break;
                }
                _cells[i] = _cells[i - 1];                
            }
        }

        public void UpdateCellValue(string value)
        {
            if (HasOneActive())
            {
                var current = _cells
                    .Where(x => x.IsActive)
                    .Select(x => x)
                    .FirstOrDefault();
                current.Value = value;
                //костыль для обновления ObservableCOllection TODO: FIX IT
                _cells[_cells.IndexOf(current)] = new SlideCell { Number = current.Number, Value = current.Value, IsActive=true };
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
