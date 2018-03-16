using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using TestAndTunes.DAL;
using TestAndTunes.DomainModel;
using TestAndTunes.DomainModel.Entities;

namespace TestAndTunes
{
    public class DateShiftVM : INotifyPropertyChanged
    {
        private DateTime _date;
        private string _letter;
        private readonly ShiftService _shiftService;
        private ICollection<string> _avaliableShifts=new ObservableCollection<string>();

        public DateShiftVM(IEnumerable<SheldueRecord> sheldue)
        {            
            _shiftService = new ShiftService(sheldue);
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
                var shift = Letter;
                _date = value;
                UpdateAvaliableShiftsCollection(_date);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Date)));
                UpdateSelectedShift(shift);
            }
        }

        private void UpdateSelectedShift(string shift)
        {
            
            if (AvaliableShifts.Any(sr => sr.Value == shift)) Letter = shift;
            else Letter = AvaliableShifts.First().Value;
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



        public ObservableCollection<Shift> AvaliableShifts { get; private set; } = new ObservableCollection<Shift>();

        public event PropertyChangedEventHandler PropertyChanged;

        private void UpdateAvaliableShiftsCollection(DateTime date)
        {
            AvaliableShifts.Clear();
            foreach(var sr in _shiftService.GetAvaliableShifts1(Date))
            {
                AvaliableShifts.Add(sr);
            }            
        }
    }
}