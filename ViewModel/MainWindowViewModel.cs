using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TuringMachine.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<int> Numbers { get; set; } = new ObservableCollection<int> { 0,1,2,3,4,5,6};

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public MainWindowViewModel()
        { 
            ///TODO: Пофиксить баг, не изменяется Datagrid
        }
    }
}
