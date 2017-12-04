using System;
using System.Collections.Generic;
using System.Windows.Input;
using TestAndTunes.Reports;
using TestAndTunes.Routines;

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
            var window = new MonthReportWindow();
            ICollection<IOption> reportOptions = null;
            var monthReportWindowModel = new MonthReportWindowModel(reportOptions);
            monthReportWindowModel.RefreshCommand = new GenerateTestAndTunesReportCommand(monthReportWindowModel);
            window.DataContext = monthReportWindowModel;

            window.ShowDialog();
        }
    }
}