using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TuringMachine.Model;

namespace TuringMachine.ViewModel
{
    public class SliderWindowViewModel
    {
        private Slide _slide;
        private ObservableCollection<SlideCell> _cells;
        public ObservableCollection<SlideCell> Cells
        {
            get { return _cells; }
            set { _cells = value; OnPropertyChanged("Cells"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public ICommand MoveRightCommand { get; set; }
        public ICommand MoveLeftCommand { get; set; }      
        public ICommand AddRightCommand { get; private set; }        
        public ICommand AddLeftCommand { get; private set; }       

        private CommandProcessor _commandProcessor;
        private ObservableCollection<AlphabetCell> _alphabetSymbols;

        public ObservableCollection<AlphabetCell> AlphabetSymbols
        {
            get { return _alphabetSymbols; }
            set
            {
                _alphabetSymbols = value;
                OnPropertyChanged("AlphabetSymbols");
            }
        }

        public SliderWindowViewModel(ObservableCollection<AlphabetCell> acells, MainWindowViewModel mw)
        {
            _slide = new Slide(20);
            _commandProcessor = new CommandProcessor();
            Cells = _slide.Cells;
            MoveRightCommand = new RelayCommand(_slide.Controller.MoveRight);
            MoveLeftCommand = new RelayCommand(_slide.Controller.MoveLeft);
            AddRightCommand = new RelayCommand(_slide.Controller.AddRight);
            AddLeftCommand = new RelayCommand(_slide.Controller.AddLeft);
            _alphabetSymbols = acells;
            _commandProcessor.RegisterSlide(_slide, _alphabetSymbols);
            mw.RunHandler += Run;
        }
        private async void Run()
        {
            while (_commandProcessor.IsEnd != true)
            {
                await Task.Delay(1000 / _commandProcessor.Speed);
                _commandProcessor.RunStep();
                
            }
            MessageBox.Show("Машина закончила свою работу");
            _commandProcessor.IsEnd = false;
        }
    }
}