using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using TestAndTunes.DomainModel;

namespace TestAndTunes
{
    public class DateShiftVM : INotifyPropertyChanged
    {
        private DateTime _date;
        private string _letter;
        private ShiftService _shiftService = new ShiftService();
        private ICollection<string> _avaliableShifts=new ObservableCollection<string>();

        public DateShiftVM()
        {
            
            _shiftService = new ShiftService();
            Date = DateTime.Today;           
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
                var shift = Letter;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvaliableShifts)));
                if (AvaliableShifts.Contains(Letter)) Letter = shift;
                else Letter = AvaliableShifts.First();
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

        public ICollection<string> AvaliableShifts => _shiftService.GetAvaliableShifts(Date).ToList();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}