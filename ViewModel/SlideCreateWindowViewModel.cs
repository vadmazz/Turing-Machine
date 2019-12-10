using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace TuringMachine.ViewModel
{
    public class SlideCreateWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string CellsCount { get; set; }
        public string CellsCountTemp { get; set; }
        public bool HaveMinusValues { get; set; }

        public ICommand SubmitCommand { get; set; }

        public SlideCreateWindowViewModel()
        {
            SubmitCommand = new RelayCommand(Submit);
        }

        public void Submit(object parameter)
        {
            if (parameter is Window)
            {
                CellsCount = CellsCountTemp;
                var window = parameter as Window;
                window.Close();
            }
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
