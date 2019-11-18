using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using TuringMachine.Model.Commands;

namespace TuringMachine.Model
{
    class CommandProcessor : INotifyPropertyChanged
    {
        //public Alphabet Alphabet { get; set; }
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
        
        public void RegisterSlide(Slide slider, ObservableCollection<AlphabetCell> cells)
        {
            _slider = slider;
            _alphabetCells = cells;
            var currentSlideCell = _slider.Cells.FirstOrDefault(x => x.IsActive);
            _executableAlphabetCell = _alphabetCells.FirstOrDefault(x => x.Name == currentSlideCell.Value);
            _executableAlphabetCell.IsExecute = true;
            _executableAlphabetCell.CurrentState = _executableAlphabetCell.States.FirstOrDefault(x => x.Name == "Q1");
        }

        public void RunStep()
        {
            var actionText = _executableAlphabetCell.CurrentState.Action;
            //if (actionText.ToCharArray().Length != 3 || actionText.ToCharArray().Length != 4)
            //    throw new CannotExecuteException("Недопустимое количество действий", actionText);            
            SlideControl slideController = _slider.Controller;
            
            MoveCommand moveCommand = new MoveCommand();
            ChangeCommand changeCommand = new ChangeCommand();
            SetStateCommand setStateCommand = new SetStateCommand();
            changeCommand.Execute(_executableAlphabetCell, _alphabetCells, _slider);

            moveCommand.Execute(_executableAlphabetCell, _slider);

            setStateCommand.Execute(_executableAlphabetCell, _alphabetCells, _slider);
            
        }
        #endregion
    }
}
