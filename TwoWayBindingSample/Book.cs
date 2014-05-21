using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TwoWayBindingSample
{
    public class Book : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private string _Title;

        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if (value == _Title)
                    return;

                _Title = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _Price;

        public decimal Price
        {
            get
            {
                return _Price;
            }
            set
            {
                if (value == _Price)
                    _Price = value;

                _Price = value;
                NotifyPropertyChanged();
            }
        }
    }
}
