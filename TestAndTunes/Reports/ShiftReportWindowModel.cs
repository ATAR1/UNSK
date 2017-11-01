﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Reporting.WinForms;
using TestAndTunes.DomainModel;

namespace TestAndTunes.Reports
{
    internal class ShiftReportWindowModel:IReportWindowModel
    {
        private IReportViewModel _report;
        private ShiftService _shiftService = new ShiftService();
        private DateTime _date = DateTime.Today;
        private string _shift;
        

        public ICommand RefreshCommand { get; set; }

        public ICollection<string> Shifts
        {
            get
            {
                return _shiftService.GetAvaliableShifts(Date).ToList();
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
            
            private set
            {
                if(_report!=value)
                {
                    _report = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Report)));
                }
            }
        }

        public SubreportProcessingEventHandler SubreportProcessing { get; private set; }
        
        public event PropertyChangedEventHandler PropertyChanged;

        //public void RefreshReport()
        //{
        //    var report = new ShiftReportViewModel(Date,Shift);
        //    report.Date = Date;
        //    report.Shift = Shift;
        //    report.Load();
        //    SubreportProcessing = report.SubreportProcessing;
        //    Report = report;
        //}
    }
}