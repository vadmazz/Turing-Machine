using System;
using System.Windows.Input;

namespace TuringMachine
{
    class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly Func<object, bool> _canExecute;

        private readonly Action<object> _onExecute;

        //---Конструкторы
        public RelayCommand(Action<object> execute)
        {
            _onExecute = execute;
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _canExecute = canExecute;
            _onExecute = execute;
        }        

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null || _canExecute(parameter))
                return true;
            return false;
        }

        public void Execute(object parameter)
        {
            if (_onExecute != null)            
                _onExecute(parameter);            
        }
    }
}