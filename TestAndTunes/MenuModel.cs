using System;
using System.Windows.Input;

namespace TestAndTunes
{
    public class MenuModel
    {
        private ICommand _showMonthReport = new ShowMonthReportCommand();

        private readonly ICommand _showShiftsReportCommand = new ShowShiftsReportCommand();
        private ICommand _showMonthShiftReport = new ShowMonthShiftReportCommand();

        public ICommand ShowMonthReport => _showMonthReport;

        public ICommand ShowShiftsReport => _showShiftsReportCommand;

        public ICommand ShowMonthShiftReport => _showMonthShiftReport;

        private class ShowMonthReportCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                MonthReportWindow reportWindow = new MonthReportWindow();
                reportWindow.ShowDialog();
            }
        }
    }
}