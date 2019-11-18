using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TuringMachine.Model;

namespace TuringMachine.View
{
    public class ActionsDataGrid : DataGrid
    {
        public ObservableCollection<string> ColumnHeaders
        {
            get { return GetValue(ColumnHeadersProperty) as ObservableCollection<string>; }
            set { SetValue(ColumnHeadersProperty, value); }
        }        
        public static readonly DependencyProperty ColumnHeadersProperty = System.Windows.DependencyProperty.Register("ColumnHeaders", typeof(ObservableCollection<string>), typeof(ActionsDataGrid), new PropertyMetadata(new PropertyChangedCallback(OnColumnsChanged)));

    static void OnColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as ActionsDataGrid;
            dataGrid.Columns.Clear();
            //Add Person Column
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "  Состояние\nАлфавит", Binding = new Binding("Name") });
            //Add Manufactures Columns
            foreach (var value in dataGrid.ColumnHeaders)
            {//TODO: ВМЕСТО STATES СОЗДАЬТ СВОЙСТВО В aLPHABETCELL ССЫЛКА НА aCTIONS ИЗ STATES
                var column = new DataGridTextColumn() { Header = value, Binding = new Binding("States") { ConverterParameter = value, Converter = new StateConverter()} };
                dataGrid.Columns.Add(column);
            }
        }
    }

    public class StateConverter : IValueConverter
    {
        private IEnumerable<State> _states;        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)//стринг в state
        {
            var states = value as IEnumerable<State>;
            _states = states;
            if (states != null && parameter != null)
            {
                var state = states.FirstOrDefault(s => s.Name == parameter.ToString());
                if (state != null)
                    return state.Action;
                return false;
            }
            return false;            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)//state в стринг
        {
            //throw new NotImplementedException();//TODO: здесь написать метод при клике на ячейку
            var value2 = value as string;            
            if (value != null && parameter != null)
            {
                var state = _states
                    .FirstOrDefault(x => x.Name == parameter.ToString());
                state.Action = value.ToString();
                return _states;
            }
            return null;
        }
    }
}
