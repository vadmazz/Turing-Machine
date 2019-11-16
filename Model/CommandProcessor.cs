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
                Name = "Пробел",
                States = _states
            });
        }

        public void AddAlphabetSymbol(string wrap)
        {
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
    }
}
