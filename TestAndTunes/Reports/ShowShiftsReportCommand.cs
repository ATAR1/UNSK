using System;
using System.Collections.Generic;
using System.Windows.Input;
using TestAndTunes.Reports;
using TestAndTunes.Routines;

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
            ICollection<IOption> reportOptions = null;
            var monthReportWindowModel = new MonthReportWindowModel(reportOptions);
            monthReportWindowModel.RefreshCommand = new GenerateShiftsReportCommand(monthReportWindowModel);
            reportWindow.DataContext = monthReportWindowModel;

            reportWindow.ShowDialog();
        }
    }
}