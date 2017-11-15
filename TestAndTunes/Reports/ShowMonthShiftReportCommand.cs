using System;
using System.Windows.Input;
using TestAndTunes.Reports;

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
            var reportWindow = new MonthReportWindow();
            var monthReportWindowModel = new MonthReportWindowModel();
            monthReportWindowModel.RefreshCommand = new GenerateMonthShiftReportCommand(monthReportWindowModel);
            reportWindow.DataContext = monthReportWindowModel;
            reportWindow.ShowDialog();
        }
    }
}