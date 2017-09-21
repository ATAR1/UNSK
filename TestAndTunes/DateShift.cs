using System;
using System.ComponentModel;

namespace TestAndTunes
{
    public class DateShift:INotifyPropertyChanged
    {
        private DateTime _date = DateTime.Today;
        private string _letter = "А";

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Date)));
            }
        }

        public string Letter
        {
            get
            {
                return _letter;
            }
            set
            {
                _letter = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Letter)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}