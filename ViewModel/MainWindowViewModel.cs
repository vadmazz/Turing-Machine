using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TuringMachine.Model;

namespace TuringMachine.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<SlideCell> Cells { get; set; } = new ObservableCollection<SlideCell>();
        private SlideControl _slideController;
        public ICommand MoveRightCommand { get; private set; }
        public ICommand MoveLeftCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public MainWindowViewModel()
        {            
            for (int i = -10; i <= 10; i++)//TODO:сделать перемещение катерки клкиками на кнопку
            {
                Cells.Add(new SlideCell { Number = i });                
            }
            _slideController = new SlideControl(Cells);
            MoveRightCommand = new RelayCommand(_slideController.MoveRight);
            MoveLeftCommand = new RelayCommand(_slideController.MoveLeft);
        }   
    }
}