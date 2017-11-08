using System;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Reporting.WinForms;

namespace TestAndTunes.Reports
{
    public class PeriodReportWindowViewModel:IReportWindowModel
    {
        private IReportViewModel _report;
        


        public DateTime BeginDate { get; set; } = DateTime.Today;

        public DateTime EndDate { get; set; } = DateTime.Today;


        public ICommand RefreshCommand { get; set; }
        

        public IReportViewModel Report
        {
            get
            {
                return _report;
            }

            set
            {
                _report = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Report)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

       
    }
}