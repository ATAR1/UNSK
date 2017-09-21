using System;
using System.Windows.Input;

namespace TestAndTunes
{
    public class MenuModel
    {
        private ICommand _showMonthReport = new ShowMonthReportCommand();

        public ICommand ShowMonthReport => _showMonthReport;

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