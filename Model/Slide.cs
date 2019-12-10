using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TuringMachine.Model
{
    public class Slide : INotifyPropertyChanged
    {
        private ObservableCollection<SlideCell> _cells;
        public ObservableCollection<SlideCell> Cells
        {
            get { return _cells; }
            set { _cells = value; OnPropertyChanged("Cells"); }
        }

        public SlideControl Controller { get; set; }

        public Slide(int startIndex, int endIndex)
        {
            _cells = new ObservableCollection<SlideCell>();
            for (int i = startIndex; i <= endIndex; i++)
            {
                _cells.Add(new SlideCell { Number = i, Value=" " });
            }
            Controller = new SlideControl(Cells);
        }

        public Slide(int cellsCount)
        {
            _cells = new ObservableCollection<SlideCell>();
            for (int i = 0; i <= cellsCount; i++)
            {
                _cells.Add(new SlideCell { Number = i, Value = " " });
            }
            Controller = new SlideControl(Cells);
        }

        public Slide()
        {
            _cells = new ObservableCollection<SlideCell>();
            for (int i = -10; i <= 10; i++)
            {
                _cells.Add(new SlideCell { Number = i, Value = " " });
            }
            Controller = new SlideControl(Cells);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
