using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TuringMachine.Model
{
    class AlphabetCell: ICloneable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public ObservableCollection<State> States { get; set; }
        public State CurrentState { get; set; }
        public string Name { get; set; }
        private bool _isExecute;
        public bool IsExecute
        {
            get { return _isExecute; }
            set { _isExecute = value; OnPropertyChanged("IsExecute"); }
        }
        public object Clone()
        {
            var newStates = new ObservableCollection<State>();
            foreach(var item in States)
            {
                newStates.Add(item.Clone() as State);
            }
            return new AlphabetCell
            {
                Name = this.Name,
                States = newStates,
                CurrentState = this.CurrentState,
                IsExecute = this.IsExecute
            };
        }
    }
}
