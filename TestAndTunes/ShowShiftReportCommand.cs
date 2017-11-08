using System;
using System.Windows.Input;
using TestAndTunes.Reports;

namespace TestAndTunes
{
    internal class ShowShiftReportCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            var window = new ShiftReportWindow();
            var shiftReportWindowModel = new ShiftReportWindowModel();
            shiftReportWindowModel.RefreshCommand = new CreateShiftReportCommand(shiftReportWindowModel);
            window.DataContext = shiftReportWindowModel;
            window.ShowDialog();
        }
    }
}