using System;
using System.Windows.Input;
using TestAndTunes.Reports;

namespace TestAndTunes
{
    internal class ShowShiftsReportCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var reportWindow = new Reports.MonthReportWindow();
            var monthReportWindowModel = new MonthReportWindowModel();
            monthReportWindowModel.RefreshCommand = new GenerateShiftsReportCommand(monthReportWindowModel);
            reportWindow.DataContext = monthReportWindowModel;

            reportWindow.ShowDialog();
        }
    }
}