using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TuringMachine.Model
{
    /// <summary>
    /// Класс, описывающий один элемент каретки
    /// </summary>
    class SlideCell
    {
        public int Number { get; set; }
        public string Value { get; set; }
        public Image IsActive { get; set; }
    }
}