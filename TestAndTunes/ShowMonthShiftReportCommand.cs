using System;
using System.Windows.Input;

namespace TestAndTunes
{
    internal class ShowMonthShiftReportCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var reportWindow = new Reports.ShiftsReportWindow(Reports.ReportType.Type2);
            reportWindow.ShowDialog();
        }
    }
}