using System;
using System.Windows;
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

        private void Window_Activated(object sender, EventArgs e)
        {            
            var VM = (MainWindowViewModel)this.DataContext;
            if (VM != null)
            {
                VM.ChangeSlideCommand.Execute(null);            
                ResizeSlideGrid();
                MoveRightButton.Command = VM.MoveRightCommand;
                MoveLeftButton.Command = VM.MoveLeftCommand;
            }
        }
    }
}