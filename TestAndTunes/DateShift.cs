using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace TestAndTunes
{
    public class DateShiftVM : INotifyPropertyChanged
    {
        private DateTime _date;
        private string _letter;
        private ShiftService _shiftService = new ShiftService();
        private ICollection<string> _avaliableShifts;

        public DateShiftVM()
        {
            _date = DateTime.Today;
            _shiftService = new ShiftService();
            _letter = _shiftService.GetAvaliableShifts(Date).ToArray()[0];
            _avaliableShifts = new ObservableCollection<string>();
        }

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
                UpdateAvaliableShiftsList();
            }
        }

        private void UpdateAvaliableShiftsList()
        {
            AvaliableShifts.Clear();
            foreach (var shiftLetter in _shiftService.GetAvaliableShifts(Date))
            {
                AvaliableShifts.Add(shiftLetter);
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

        public ICollection<string> AvaliableShifts
        {
            get
            {
                return _avaliableShifts;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}