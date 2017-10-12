using System;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Reporting.WinForms;

namespace TestAndTunes.Reports
{
    public class PeriodReportWindowViewModel:IReportWindowModel
    {
        private IReportViewModel _report;
        private ICommand _refreshCommand;
        private ReportViewer _reportViewer;

        public PeriodReportWindowViewModel()
        {
            _refreshCommand = new RefreshReportCommand(this);
        }

        public PeriodReportWindowViewModel(ReportViewer reportViewer):this()
        {
            this._reportViewer = reportViewer;
        }


        public DateTime BeginDate { get; set; } = DateTime.Today;

        public DateTime EndDate { get; set; } = DateTime.Today;


        public ICommand RefreshCommand => _refreshCommand;
        

        public IReportViewModel Report
        {
            get
            {
                return _report;
            }

            private set
            {
                _report = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Report)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RefreshReport()
        {
            var report = new MonthReportViewModel()
            {
                BeginDate = BeginDate,
                EndDate = EndDate
            };
            report.ReportEmbeddedResource = "TestAndTunes.Reports.Layouts.SummaryReportForPeriod.rdlc";
            report.Load();
            Report = report;

            if (_reportViewer == null) return;
            var localReport = _reportViewer.LocalReport;
            if ( localReport != null)
            {
                _reportViewer.LocalReport.ReportEmbeddedResource = report.ReportEmbeddedResource;
                localReport.SetParameters(new ReportParameter("BeginDate", BeginDate.ToString()));
                localReport.SetParameters(new ReportParameter("EndDate", EndDate.ToString()));
                report.FillDataSources(localReport.DataSources);
            }
            _reportViewer.RefreshReport();
        }
    }
}