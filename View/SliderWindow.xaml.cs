using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TuringMachine.Model;
using TuringMachine.ViewModel;

namespace TuringMachine.View
{
    public partial class SliderWindow : Window
    {
        public SliderWindow(ObservableCollection<AlphabetCell> acells, MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            DataContext = new SliderWindowViewModel(acells, mainWindowViewModel);
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
//            var VM = (SliderWindowViewModel)this.DataContext;
//            if (VM != null)
//            {
//                           
//                ResizeSlideGrid();
//                MoveRightButton.Command = VM.MoveRightCommand;
//                MoveLeftButton.Command = VM.MoveLeftCommand;
//                AddRightButton.Command = VM.AddRightCommand;
//                AddLeftButton.Command = VM.AddLeftCommand;
//            }            
        }

        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {            
            ResizeSlideGrid();            
        }

        private void AddLeftButton_Click(object sender, RoutedEventArgs e)
        {            
            ResizeSlideGrid();
        }
    }
}