using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TuringMachine.Model
{
    class Slide : INotifyPropertyChanged
    {
        private IEnumerable<SlideCell> _cells;
        public Slide(IEnumerable<SlideCell> cells)
        {
            _cells = cells;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
