using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace _8Queens
{
    public partial class MainWindow : Window
    {
        public List<List<ChessSquare>> ChessBoard = new List<List<ChessSquare>>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeChessBoard();
            comboBox.ItemsSource = Enum.GetValues(typeof(SearchAlgorithm)).Cast<SearchAlgorithm>();
            comboBox.SelectedIndex = 0;
        }

        private void InitializeChessBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                ChessBoard.Add(new List<ChessSquare>());
                for (int j = 0; j < 8; j++)
                {
                    ChessBoard[i].Add(new ChessSquare(i, j, (i + j) % 2 == 0 || i == 0 && j == 0 ? "#EEEED2" : "#769656"));
                }
            }
            chess.ItemsSource = ChessBoard;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ChessBoard.ForEach(cb => cb.ForEach(c => c.Reset()));
            var watch = new Stopwatch();
            switch ((SearchAlgorithm)comboBox.SelectedItem)
            {
                case SearchAlgorithm.ForwardChecking:
                    watch.Start();
                    await ChessBoard.Search_ForwardChecking((int)slider.Value);
                    watch.Stop();
                    break;
            }
            FinishTimeTB.Text = $"Finished in {watch.ElapsedMilliseconds.ToString()} milliseconds.";
        }

        private void Slider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            delaySliderTB.Text = $"{(int)e.NewValue} milliseconds delay.";
        }
    }

    public enum SearchAlgorithm
    {
        ForwardChecking
    }
}