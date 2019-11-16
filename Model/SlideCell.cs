using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TuringMachine.Model
{
    /// <summary>
    /// Класс, описывающий один элемент каретки
    /// </summary>
    public class SlideCell : INotifyPropertyChanged
    {
        public int Number { get; set; }
        public char Value { get; set; }
        private bool _isActive = false;
        public bool IsActive
        { 
            get { return _isActive; }
            set { _isActive = value; OnPropertyChanged("IsActive"); }            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}