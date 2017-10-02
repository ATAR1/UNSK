using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace TestAndTunes.Reports
{
    public class ShiftsReportWindowModel:IReportWindowModel,INotifyPropertyChanged
    {
        private ICollection<string> _month = CultureInfo.GetCultureInfo("RU-ru").DateTimeFormat.MonthNames.Where(s => !String.IsNullOrWhiteSpace(s)).ToList();

        public event PropertyChangedEventHandler PropertyChanged;

        private IReportViewModel _report;

        private readonly ICommand _refreshCommand;
        private ReportType _reportType;

        public ShiftsReportWindowModel(ReportType reportType)
        {
            _refreshCommand = new RefreshReportCommand(this);
            _reportType = reportType;
        }
         
        public int Year { get; set; } = DateTime.Today.Year;

        public string Month { get; set; } = DateTime.Today.ToString("MMMM", CultureInfo.GetCultureInfo("RU-ru"));
        
        public ICollection<string> Months => _month;
        
        public ICommand RefreshCommand => _refreshCommand;

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

        public void RefreshReport()
        {
            DateTime date = default(DateTime);
            if (!DateTime.TryParse($"1 {Month} {Year}", new CultureInfo("ru-Ru"), DateTimeStyles.None, out date))
            {
                MessageBox.Show("Что-то не так с выбраным годом!");
            }

            var dateEnd = date.AddMonths(1);
            var report = ReportFactory.CreateReportModel(_reportType);
            report.Load(date, dateEnd);
            Report = report;
        }
    }
}