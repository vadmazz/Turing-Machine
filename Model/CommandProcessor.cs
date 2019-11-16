using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace TuringMachine.Model
{
    class CommandProcessor
    {
        //public Alphabet Alphabet { get; set; }
        public ObservableCollection<AlphabetCell> AlphabetSymbols{ get; set; }
        private ObservableCollection<State> _states;
        private ObservableCollection<SlideCell> _cells;
        public CommandProcessor()
        {
            _states = new ObservableCollection<State>();
            AlphabetSymbols = new ObservableCollection<AlphabetCell>();
            _states = new ObservableCollection<State> { new State
                    {
                        Name="Q1",                        
                    },
                    new State
                    {
                        Name="Q2",                        
                    },                    
                };

            AlphabetSymbols.Add(new AlphabetCell 
            {
                Name = " ",
                States = _states
            });
        }

        public void AddAlphabetSymbol(string wrap)
        {
            AlphabetSymbols.Clear();            
            AlphabetSymbols.Add(new AlphabetCell
            {
                Name = " ",
                States = _states
            });
            var temp = wrap.ToArray<char>().Distinct();            
            foreach (var item in temp)
            {
                if (!AlphabetSymbols.Select(x => x.Name).Contains(item.ToString()) && item != " ".ToCharArray()[0])                    
                    AlphabetSymbols.Add(new AlphabetCell 
                    {
                        Name=item.ToString(),
                        States=_states
                    });
            }            
        }
        public void AddState()
        {
            var lastIndex = AlphabetSymbols[0].States.Count;
            _states.Add(new State
            {
                Name=$"Q{lastIndex + 1}",                
            });            
            foreach(var item in AlphabetSymbols)
            {
                item.States = _states;                
            }
        }
        public void RemoveState()
        {
            var lastIndex = AlphabetSymbols[0].States.Count;
            if (lastIndex > 1)
            {
                _states.Remove(_states[lastIndex - 1]);
                foreach (var item in AlphabetSymbols)
                {
                    item.States = _states;
                }
            }                
        }

        #region Команды обработчика
        Slide _slide;
        AlphabetCell _executableAlphabetCell;
        SlideCell _executableSlideCell;
        enum Command
        {
            ChangeValue,
            Move,
            ChangeState
        }       
        public void RegisterSlide(Slide slide, AlphabetCell executableCell)
        {
            _slide = slide;
            _cells = _slide.Cells;
            _executableAlphabetCell = executableCell;
            _executableAlphabetCell.IsExecute = true;
        }

        public void Execute(string actionText)
        {
            if (actionText.ToCharArray().Length != 3)
                throw new CannotExecuteException("Недопустимое количество действий", actionText);
            var com1 = actionText[0].ToString();
            var com2 = actionText[1].ToString();
            var com3 = $"{actionText[2]}{actionText[3]}";
            if (!IsCommandValid(com1, Command.ChangeValue))
                throw new CannotExecuteException($"Невозможно выполнить команду", com1);
            if (!IsCommandValid(com2, Command.Move))
                throw new CannotExecuteException($"Невозможно выполнить команду", com2);
            if (!IsCommandValid(com3, Command.ChangeState))
                throw new CannotExecuteException($"Невозможно выполнить команду", com3);
            if (_executableSlideCell.Value != _executableAlphabetCell.Name.ToCharArray()[0])
                throw new CannotExecuteException($"Не совпадают значения на каретке и в действии", com3);
            SlideControl slideController = _slide.Controller;
            if (com1 != ".")
                _executableAlphabetCell.Name = com1;
            _executableAlphabetCell.IsExecute = false;//TODO: Дописать Execute()

        }        
        /// <summary>
        /// Проверка на валидность команды
        /// </summary>
        /// <param name="actionText">Текст команды</param>
        /// <param name="commandIndex">Команда </param>
        private bool IsCommandValid(string action, Command command)
        {            
            switch (command)
            {
                case Command.ChangeValue:
                    foreach (var item in AlphabetSymbols.Select(x => x.Name))
                    {
                        if (item.Contains(action) || action == ".")                        
                            return true;                        
                    }
                    return false;                    
                case Command.Move:
                    if (action == "<" || action == ">")
                        return true;
                    return false;
                case Command.ChangeState:
                    if (_states.Select(x => x.Name).Contains(action) || action == ".")
                        return true;
                    return false;                    
            }
            return false;
        }

        private void GoToState(State stateTo, AlphabetCell alphabetSymbol )
        {

        }
        #endregion
    }
}
