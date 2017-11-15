using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Input;
using TestAndTunes.Reports;

namespace TestAndTunes.Reports
{
    internal class GenerateSummaryForPeriodReportCommand : ICommand
    {
        private PeriodReportWindowViewModel periodReportWindowViewModel;

        public GenerateSummaryForPeriodReportCommand(PeriodReportWindowViewModel periodReportWindowViewModel)
        {
            this.periodReportWindowViewModel = periodReportWindowViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var report = new MonthReportViewModel(periodReportWindowViewModel.BeginDate, periodReportWindowViewModel.EndDate);
            report.ReportEmbeddedResource = "TestAndTunes.Reports.Layouts.SummaryReportForPeriod.rdlc";
            report.SetReportParameters = (localReport) =>
            {
                localReport.SetParameters(new ReportParameter("BeginDate", periodReportWindowViewModel.BeginDate.ToString()));
                localReport.SetParameters(new ReportParameter("EndDate", periodReportWindowViewModel.EndDate.ToString()));
            };
            periodReportWindowViewModel.Report = report;
        }
    }
}