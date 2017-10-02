using System;
using System.Windows.Input;
using TestAndTunes.Reports;

namespace TestAndTunes
{
    internal class ShowTestAndTunesReport : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var window = new MonthReportWindow(ReportType.TestAndTunes);
            window.ShowDialog();
        }
    }
}