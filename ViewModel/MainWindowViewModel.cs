using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TuringMachine.Model;
using TuringMachine.View;

namespace TuringMachine.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Properties
        public string Speed { get; set; }
        public string Symbols { get; set; }
        private ObservableCollection<AlphabetCell> _alphabetSymbols;
        public ObservableCollection<AlphabetCell> AlphabetSymbols
        {
            get { return _alphabetSymbols; }
            set { _alphabetSymbols = value;OnPropertyChanged("AlphabetSymbols"); }
        }
        private ObservableCollection<string> _columnHeaders;
        public ObservableCollection<string> ColumnHeaders
        {
            get { return _columnHeaders; }
            set { _columnHeaders = value; OnPropertyChanged("ColumnHeaders"); }
        }
        
        private ObservableCollection<SlideCell> _cells;
        public ObservableCollection<SlideCell> Cells
        {
            get { return _cells; }
            set { _cells = value; OnPropertyChanged("Cells"); }
        }
        #endregion
        
        #region Commands
        public ICommand MoveRightCommand { get; private set; }
        public ICommand MoveLeftCommand { get; private set; }        
        public ICommand OpenSlideCreateWindowCommand { get; private set; }        
        public ICommand ChangeSlideCommand { get; private set; }        
        public ICommand AddRightCommand { get; private set; }        
        public ICommand AddLeftCommand { get; private set; }        
        public ICommand AddAlphabetSymbolCommand { get; private set; }        
        public ICommand AddStateCommand { get; private set; }        
        public ICommand RemoveStateCommand { get; private set; }        
        public ICommand RunStepCommand { get; private set; }        
        public ICommand RunCommand { get; private set; }        
        public ICommand AddActionCommand { get; private set; }        
        public ICommand FasterCommand { get; private set; }        
        public ICommand SlowerCommand { get; private set; }        
        #endregion
        
        private SlideCreateWindowViewModel _slideVm;
        private Slide _slide;
        private CommandProcessor _commandProcessor;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }        

        public MainWindowViewModel()
        {            
            ColumnHeaders = new ObservableCollection<string>();
            _commandProcessor = new CommandProcessor();
            AlphabetSymbols = _commandProcessor.AlphabetSymbols;
            foreach (var item in _commandProcessor.States.Select(x => x.Name))
            {
                ColumnHeaders.Add(item);
            }
            Speed = $"Скорость ({_commandProcessor.Speed})";
            _slide = new Slide(20);
            Cells = _slide.Cells;
            MoveRightCommand = new RelayCommand(_slide.Controller.MoveRight);
            MoveLeftCommand = new RelayCommand(_slide.Controller.MoveLeft);
            AddRightCommand = new RelayCommand(_slide.Controller.AddRight);
            AddLeftCommand = new RelayCommand(_slide.Controller.AddLeft);
            ChangeSlideCommand = new RelayCommand(ChangeSlide);
            OpenSlideCreateWindowCommand = new RelayCommand(OpenSlideCreateWindow);
            AddAlphabetSymbolCommand = new RelayCommand(AddAlphabetSymbol);
            AddStateCommand = new RelayCommand(AddState);
            RemoveStateCommand = new RelayCommand(RemoveState);
            RunStepCommand = new RelayCommand(RunStep);
            RunCommand = new RelayCommand(Run);
            AddActionCommand = new RelayCommand(AddAction);
            FasterCommand = new RelayCommand(Faster);
            SlowerCommand = new RelayCommand(Slower);

            _commandProcessor.RegisterSlide(_slide, _alphabetSymbols);
        }

        private void AddAction(object parameter)
        {
            var msg = parameter as ActionTableMessage;
            _alphabetSymbols[msg.Row].States.FirstOrDefault(x => x.Name == msg.ColumnHeader).Action = msg.Value;            
        }
        
        private async void Run(object parameter)
        {
            while (_commandProcessor.IsEnd != true)
            {
                await Task.Delay(1000 / _commandProcessor.Speed);
                _commandProcessor.RunStep();
                
            }
            MessageBox.Show("Машина закончила свою работу");
            _commandProcessor.IsEnd = false;
        }
        //TODO: Рефакторинг, добавление символов и состояний фикс
        private void Faster(object parameter)
        {
            _commandProcessor.Speed *= 2;
            Speed = $"Скорость ({_commandProcessor.Speed})";
            OnPropertyChanged("Speed");
        }

        private void Slower(object parameter)
        {
            if (_commandProcessor.Speed >= 1)
            {
                _commandProcessor.Speed /= 2;
                Speed = $"Скорость ({_commandProcessor.Speed})";
                OnPropertyChanged("Speed");
            }            
        }

        private void RunStep(object parameter)
        {
            _commandProcessor.RunStep();            
            if (_commandProcessor.IsEnd)
            {
                _commandProcessor.IsEnd = false;
                MessageBox.Show("Машина закончила свою работу");
            }            
        }

        private void OpenSlideCreateWindow(object parameter)
        {
            var window = new SlideCreateWindow();
            _slideVm = (SlideCreateWindowViewModel)window.DataContext;
            window.Show();
        }

        private void AddState(object parameter)
        {           
            _commandProcessor.AddState();
            var c = new ObservableCollection<string>();
            foreach (var item in _commandProcessor.States.Select(x => x.Name))
            {
                c.Add(item);
            }
            ColumnHeaders = c;            
        }

        private void RemoveState(object parameter)
        {
            _commandProcessor.RemoveState();
            var c = new ObservableCollection<string>();
            foreach (var item in AlphabetSymbols[0].States.Select(x => x.Name))
            {
                c.Add(item);
            }
            ColumnHeaders = c;
        }

        private void AddAlphabetSymbol(object parameter)
        {
            _commandProcessor.AddAlphabetSymbol(parameter.ToString());
        }

        private void ChangeSlide(object parameter)
        {
            if (_slideVm != null && _slideVm.CellsCount != null)
            {
                if (_slideVm.HaveMinusValues)
                    _slide = new Slide(-int.Parse(_slideVm.CellsCount), int.Parse(_slideVm.CellsCount));
                else
                    _slide = new Slide(int.Parse(_slideVm.CellsCount));
                Cells = _slide.Cells;
                MoveRightCommand = new RelayCommand(_slide.Controller.MoveRight);
                MoveLeftCommand = new RelayCommand(_slide.Controller.MoveLeft);
                AddRightCommand = new RelayCommand(_slide.Controller.AddRight);
                AddLeftCommand = new RelayCommand(_slide.Controller.AddLeft);
            }            
        }
    }
}