using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TuringMachine.ViewModel;

namespace TuringMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {          
            DataContext = new MainWindowViewModel();
            InitializeComponent();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
            ResizeSlideGrid();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ResizeSlideGrid();
        }

        private void ResizeSlideGrid()
        {
            if (SlideGrid.HasItems)                
                SlideGrid.RowHeight = this.ActualWidth / (SlideGrid.Items.Count);

        }
    }
}