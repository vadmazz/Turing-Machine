using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;
using TuringMachine.Model;

namespace TuringMachine.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<SlideCell> Numbers { get; set; } = new ObservableCollection<SlideCell>();

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public MainWindowViewModel()
        {
            for (int i = -10; i <= 10; i++)
            {
                Numbers.Add(new SlideCell { Number = i });
            }            
        }
    }
}