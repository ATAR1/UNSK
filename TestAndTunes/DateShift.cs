using System;
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
        private ICollection<string> _avaliableShifts=new List<string>();

        public DateShiftVM()
        {
            
            _shiftService = new ShiftService();
            Date = DateTime.Today;
            Letter = _shiftService.GetAvaliableShifts(Date).ToArray()[0];            
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
                var shift = _letter;
                UpdateAvaliableShiftsList();
                _letter = shift;
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