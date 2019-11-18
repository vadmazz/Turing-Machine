using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TuringMachine.Model
{
    class AlphabetCell
    {
        public ObservableCollection<State> States { get; set; }
        public State CurrentState { get; set; }
        public string Name { get; set; }  
        public bool IsExecute { get; set; }
    }
}
