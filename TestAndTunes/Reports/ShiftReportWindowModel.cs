using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using TestAndTunes.DAL;
using TestAndTunes.DomainModel;
using TestAndTunes.DomainModel.Entities;

namespace TestAndTunes.Reports
{
    internal class ShiftReportWindowModel:IReportWindowModel
    {
        private IReportViewModel _report;
        private readonly ShiftService _shiftService;
        private DateTime _date = DateTime.Today;
        private string _shift;


        public ShiftReportWindowModel(IEnumerable<SheldueRecord> sheldue)
        {
            _shiftService = new ShiftService(sheldue);
        }

        public ICommand RefreshCommand { get; set; }

        public ICollection<Shift> Shifts
        {
            get
            {
                return _shiftService.GetAvaliableShifts1(Date).ToList();
            }
        }

        public string Shift
        {
            get
            {
                return _shift;
            }
            set
            {
                _shift = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Shift)));
            }
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
                var shift = Shift;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Shifts)));
                Shift = shift;
            }
        }

        public IReportViewModel Report
        {
            get
            {
                return _report;
            }
            
            set
            {
                if(_report!=value)
                {
                    _report = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Report)));
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
    }
}