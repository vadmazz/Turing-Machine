using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
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
            {
                var column = new DataGridTextColumn() { Header = value, Binding = new Binding("States") { ConverterParameter = value, Converter = new StateConverter()} };                                
                dataGrid.Columns.Add(column);
            }
        }
        public static DataGridCell GetCell(DataGrid dg, int row, int column)
        {
            DataGridRow rowContainer = GetRow(dg, row);

            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);

                // try to get the cell but it may possibly be virtualized
                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                if (cell == null)
                {
                    // now try to bring into view and retreive the cell
                    dg.ScrollIntoView(rowContainer, dg.Columns[column]);
                    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                }
                return cell;
            }
            return null;
        }
        static public DataGridRow GetRow(DataGrid dg, int index)
        {
            DataGridRow row = (DataGridRow)dg.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                // may be virtualized, bring into view and try again
                dg.ScrollIntoView(dg.Items[index]);
                row = (DataGridRow)dg.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
    }

    public class StateConverter : IValueConverter
    {
        public IEnumerable<State> _states;        
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
