using System;
using System.Windows.Input;

namespace TestAndTunes
{
    public class MenuModel
    {
        private ICommand _showMonthReport = new ShowMonthReportCommand();

        private readonly ICommand _showShiftsReportCommand = new ShowShiftsReportCommand();
        private ICommand _showMonthShiftReport = new ShowMonthShiftReportCommand();
        private ICommand _showTestAndTunesReport = new ShowTestAndTunesReport();
        private ICommand _showShiftReport= new ShowShiftReportCommand();

        public ICommand ShowMonthReport => _showMonthReport;

        public ICommand ShowShiftsReport => _showShiftsReportCommand;

        public ICommand ShowMonthShiftReport => _showMonthShiftReport;

        public ICommand ShowTestAndTunesReport => _showTestAndTunesReport;

        public ICommand ShowShiftReport => _showShiftReport;
        private class ShowMonthReportCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                var reportWindow = new Reports.MonthReportWindow(Reports.ReportType.Month);
                reportWindow.ShowDialog();
            }
        }
    }
}