using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TuringMachine.Model;
using TuringMachine.View;

namespace TuringMachine.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private Slide _slide { get; set; }
        private ObservableCollection<SlideCell> _cells;
        public ObservableCollection<SlideCell> Cells
        {
            get { return _cells; }
            set { _cells = value; OnPropertyChanged("Cells"); }
        }
        
        public SlideCreateWindowViewModel _slideVM { get; set; }

        public ICommand MoveRightCommand { get; set; }
        public ICommand MoveLeftCommand { get; set; }        
        public ICommand OpenSlideCreateWindowCommand { get; private set; }        
        public ICommand ChangeSlideCommand { get; private set; }        


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public MainWindowViewModel()
        {
            _slide = new Slide(20);
            Cells = _slide.Cells;
            MoveRightCommand = new RelayCommand(_slide.Controller.MoveRight);
            MoveLeftCommand = new RelayCommand(_slide.Controller.MoveLeft);
            ChangeSlideCommand = new RelayCommand(ChangeSlide);
            OpenSlideCreateWindowCommand = new RelayCommand(OpenSlideCreateWindow);
        }
        
        public void OpenSlideCreateWindow(object parameter)
        {
            var window = new SlideCreateWindow();
            _slideVM = (SlideCreateWindowViewModel)window.DataContext;
            window.Show();
        }

        public void ChangeSlide(object parameter)
        {
            if (_slideVM != null && _slideVM.CellsCount != null)
            {
                if (_slideVM.HaveMinusValues)
                    _slide = new Slide(-int.Parse(_slideVM.CellsCount), int.Parse(_slideVM.CellsCount));
                else
                    _slide = new Slide(int.Parse(_slideVM.CellsCount));
                Cells = _slide.Cells;
                MoveRightCommand = new RelayCommand(_slide.Controller.MoveRight);
                MoveLeftCommand = new RelayCommand(_slide.Controller.MoveLeft);
            }            
        }
    }
}