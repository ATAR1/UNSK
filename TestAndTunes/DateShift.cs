using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TestAndTunes
{
    public class DateShiftVM:INotifyPropertyChanged
    {
        private DateTime _date = DateTime.Today;
        private string _letter = "А";
        private ShiftService _shiftService = new ShiftService();

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

        public ICollection<string> AvaliableShifts => _shiftService.GetAvaliableShifts(Date).ToArray();
        

        public event PropertyChangedEventHandler PropertyChanged;
    }
}