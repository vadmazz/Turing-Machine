using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TuringMachine.Model;

namespace TuringMachine.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private Slide _slide;
        public ObservableCollection<SlideCell> Cells => _slide.Cells;

        private Visibility _slideVisibility = Visibility.Visible,Hidden;
        public Visibility SlideVisibility
        {
            get { return _slideVisibility; }
            set { _slideVisibility = value; OnPropertyChanged("SlideVisibility"); }
        }

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
            _slide = new Slide(20);
            MoveRightCommand = new RelayCommand(_slide.Controller.MoveRight);
            MoveLeftCommand = new RelayCommand(_slide.Controller.MoveLeft);
        }
                
    }
}