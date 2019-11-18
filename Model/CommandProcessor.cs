using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using TuringMachine.Model.Commands;

namespace TuringMachine.Model
{
    class CommandProcessor : INotifyPropertyChanged
    {        
        public ObservableCollection<AlphabetCell> AlphabetSymbols{ get; set; }
        private ObservableCollection<State> _states;
        public ObservableCollection<State> States
        {
            get { return _states; }
            set { _states = value; OnPropertyChanged("States"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public CommandProcessor()
        {
            _states = new ObservableCollection<State>();
            AlphabetSymbols = new ObservableCollection<AlphabetCell>();
            AddState();
            AddState();                        
            AddAlphabetSymbol(" ");
        }

        public void AddAlphabetSymbol(string wrap)
        {
            ObservableCollection<State> newStates = new ObservableCollection<State>();
            foreach (var item2 in _states)
            {
                newStates.Add(new State
                {                    
                    Name = item2.Name
                });
            }
            AlphabetSymbols.Clear();
            AlphabetSymbols.Add(new AlphabetCell
            {
                Name = " ",
                States = newStates
            });
            var temp = wrap.ToArray<char>().Distinct();            
            foreach (var item in temp)
            {                
                if (!AlphabetSymbols.Select(x => x.Name).Contains(item.ToString()) && item != " ".ToCharArray()[0])
                {
                    var newCell = new AlphabetCell
                    {
                        Name = item.ToString()
                    };
                    var statesss = new ObservableCollection<State>();
                    foreach (var state in _states)
                    {
                        statesss.Add(new State
                        {
                            Name = state.Name
                        });
                    }
                    newCell.States = statesss;
                    AlphabetSymbols.Add(newCell);
                }
            }            
        }
        public void AddState()
        {
            var lastIndex = States.Count;
            _states.Add(new State
            {
                Name = $"Q{lastIndex + 1}",
            });            
            foreach (var item in AlphabetSymbols)
            {
                item.States.Add(new State
                {
                    Name = $"Q{lastIndex + 2}",
                });
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
        Slide _slider;
        AlphabetCell _executableAlphabetCell;
        ObservableCollection<AlphabetCell> _alphabetCells;
        MoveCommand _moveCommand;
        ChangeCommand _changeCommand;
        SetStateCommand _setStateCommand;
        public bool IsEnd { get; set; }

        public void RegisterSlide(Slide slider, ObservableCollection<AlphabetCell> cells)
        {
            _slider = slider;
            _alphabetCells = cells;
            _moveCommand = new MoveCommand();
            _changeCommand = new ChangeCommand();
            _setStateCommand = new SetStateCommand();
        }

        public void RunStep()
        {
            try
            {
                var currentSlideName = _slider.Cells.FirstOrDefault(z => z.IsActive == true).Value;
                if (_executableAlphabetCell == null)
                {                    
                    _executableAlphabetCell = _alphabetCells.FirstOrDefault(x =>
                        x.Name == currentSlideName);
                    _executableAlphabetCell.IsExecute = true;
                    _executableAlphabetCell.CurrentState = _executableAlphabetCell.States.FirstOrDefault(x => x.Name == "Q1");
                }                
                _changeCommand._cells = _alphabetCells;
                if (!_changeCommand.IsValid(currentSlideName))
                    throw new CannotExecuteException("На каретке имеется символ вне алфавита!", currentSlideName);
                if (_executableAlphabetCell.CurrentState.Action == null)
                    throw new CannotExecuteException("Ожидалось действие!", 
                        $"Элемент алфавита: {_executableAlphabetCell.Name}\nСостояние: {_executableAlphabetCell.CurrentState.Name}");
                var actionText = _executableAlphabetCell.CurrentState.Action;
                if (actionText == ".>.")
                {
                    _executableAlphabetCell = null;
                    IsEnd = true;
                }
                else
                {
                    
                    SlideControl slideController = _slider.Controller;

                    _changeCommand.Execute(_executableAlphabetCell, _alphabetCells, _slider);

                    _moveCommand.Execute(_executableAlphabetCell, _slider);

                    _setStateCommand.Execute(ref _executableAlphabetCell, _alphabetCells, _slider);
                }                              
            }
            catch(CannotExecuteException ex)
            {
                MessageBox.Show(ex.WrongActionText, $"{ex.Message}, вызвано {ex.TargetSite}"); 
            }
        }
        #endregion
    }
}
