using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace TestAndTunes
{
    public class MonthReportWindowModel : INotifyPropertyChanged, IReportWindowModel
    {
        private MonthReportViewModel _report = new MonthReportViewModel();
        private readonly ICommand _refreshCommand;
        private ICollection<string> _month = CultureInfo.GetCultureInfo("RU-ru").DateTimeFormat.MonthNames.Where(s => !String.IsNullOrWhiteSpace(s)).ToList();

        public event PropertyChangedEventHandler PropertyChanged;

        public ICollection<string> Months => _month;

        public MonthReportWindowModel()
        {
            _refreshCommand = new RefreshReportCommand(this);
        }
        public MonthReportViewModel Report
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


        public int Year { get; set; } = DateTime.Today.Year;

        public string Month { get; set; } = DateTime.Today.ToString("MMMM", CultureInfo.GetCultureInfo("RU-ru"));

        public ICommand RefreshCommand => _refreshCommand; 

        public void RefreshReport()
        {
            DateTime date = default(DateTime);
            if (!DateTime.TryParse($"1 {Month} {Year}", new CultureInfo("ru-Ru"), DateTimeStyles.None, out date))
            {
                MessageBox.Show("Что-то не так с выбраным годом!");
            }

            var dateEnd = date.AddMonths(1);

            ReportService service = new ReportService();

            MonthReportViewModel _report = new MonthReportViewModel();
            _report.SummaryPerMonth = new TotalsTableMonthView(service.GenerateTotals(date, dateEnd, "ТО1"));
            _report.SummaryTO2 = new TotalsTableMonthView(service.GenerateTotals(date, dateEnd, "ТО2"));
            _report.SummaryUOGT = new TotalsTableMonthView(service.GenerateTotals(date, dateEnd, "УОГТ"));
            _report.Month = Month;
            _report.Year = Year;
            Report = _report;
        }
        
    }

    internal class RefreshReportCommand : ICommand
    {
        IReportWindowModel _windowModel;

        public RefreshReportCommand(IReportWindowModel windowModel)
        {
            _windowModel = windowModel;
        }



        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _windowModel.RefreshReport();
        }
    }
}
