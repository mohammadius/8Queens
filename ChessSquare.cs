using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace _8Queens
{
    public class ChessSquare : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Color { get; }
        public int X { get; }
        public int Y { get; }

        private bool _hasQueen;
        private bool _accessible;
        private bool _accessibleView;

        public bool HasQueen
        {
            get => _hasQueen;
            set
            {
                if (_hasQueen == value)
                    return;

                _hasQueen = value;
                OnPropertyChanged();
            }
        }
        public bool Accessible
        {
            get => _accessible;
            set
            {
                if (_accessible == value)
                    return;

                _accessible = value;
                AccessibleView = !value;
                OnPropertyChanged();
            }
        }
        public bool AccessibleView
        {
            get => _accessibleView;
            private set
            {
                if (_accessibleView == value)
                    return;

                _accessibleView = value;
                OnPropertyChanged();
            }
        }

        public ChessSquare(int x, int y, string color)
        {
            Color = color;
            X = x;
            Y = y;
            HasQueen = false;
            Accessible = true;
        }

        public void Reset()
        {
            HasQueen = false;
            Accessible = true;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}