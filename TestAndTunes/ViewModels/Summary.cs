using System.ComponentModel;
using TestAndTunes.DomainModel.Entities;

namespace TestAndTunes.ViewModels
{
    public class Summary:INotifyPropertyChanged
    {
        private TotalsTable _to1;
        private TotalsTable _to2;
        private TotalsTable _uogt;

        public TotalsTable TO1
        {
            get
            {
                return _to1;
            }

            set
            {
                _to1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TO1)));
            }
        }

        public TotalsTable TO2
        {
            get
            {
                return _to2;
            }

            set
            {
                _to2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TO2)));
            }
        }

        public TotalsTable UOGT
        {
            get
            {
                return _uogt;
            }

            set
            {
                _uogt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UOGT)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}