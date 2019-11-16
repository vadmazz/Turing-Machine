using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TuringMachine.Model;
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
                AddRightButton.Command = VM.AddRightCommand;
                AddLeftButton.Command = VM.AddLeftCommand;
            }
        }

        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {            
            ResizeSlideGrid();            
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var VM = (MainWindowViewModel)this.DataContext;
            VM.Symbols = AlpText.Text;
            VM.AddAlphabetSymbolCommand.Execute(VM.Symbols); 
        }

        private void AddLeftButton_Click(object sender, RoutedEventArgs e)
        {
            ResizeSlideGrid();
        }
    }
}