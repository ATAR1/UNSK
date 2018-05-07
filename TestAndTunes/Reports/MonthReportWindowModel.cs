using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TestAndTunes.Routines;

namespace TestAndTunes.Reports
{
    public class MonthReportWindowModel:IReportWindowModel
    {
        private ICollection<string> _months = CultureInfo.GetCultureInfo("RU-ru").DateTimeFormat.MonthNames.Where(s => !String.IsNullOrWhiteSpace(s)).ToList();

        public event PropertyChangedEventHandler PropertyChanged;

        private IReportViewModel _report;
        private readonly ICommand _showOptionsCommand;
        private IEnumerable<IOption> _options;

        public MonthReportWindowModel(IEnumerable<IOption> reportOptions)
        {
            _options = reportOptions;
            _showOptionsCommand = new ShowOptionsCommand(_options);
        }

        public int Year { get; set; } = DateTime.Today.Year;

        public string Month { get; set; } = DateTime.Today.ToString("MMMM", CultureInfo.GetCultureInfo("RU-ru"));
        
        public ICollection<string> Months => _months;
        
        public ICommand RefreshCommand { get; private set; }

        public delegate IReportViewModel RefreshMonthReport(int year, int month, IEnumerable<IOption> reportOptions = null);

        public class RefreshReportCommand:ICommand
        {
            private MonthReportWindowModel _owner;
            private RefreshMonthReport _refreshFunc;
            

            public RefreshReportCommand(RefreshMonthReport refreshFunc, MonthReportWindowModel owner) 
            {
                this._owner = owner;
                this._refreshFunc = refreshFunc;
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter) => true;
            public void Execute(object parameter)
            {
                _owner.Report = _refreshFunc(_owner.Year, 1, _owner._options);
            }
        }

        public RefreshMonthReport RefreshFunc { set { RefreshCommand = new RefreshReportCommand(value, this); } }

        public ICommand ShowOptionsCommand => _showOptionsCommand;

        public IEnumerable<IOption> Options => _options;

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
    }
}