using System;
using System.Windows.Input;
using TestAndTunes.Reports;

namespace TestAndTunes.ViewModels
{
    public class MenuModel
    {
        private ICommand _showMonthReport = new ShowMonthReportCommand();

        private readonly ICommand _showShiftsReportCommand = new ShowShiftsReportCommand();
        private ICommand _showMonthShiftReport = new ShowMonthShiftReportCommand();
        private ICommand _showTestAndTunesReport = new ShowTestAndTunesReport();
        private ICommand _showShiftReport= new ShowShiftReportCommand();
        private ICommand _showSummaryForPeriodReport = new ShowSummaryForPeriodReportCommand();

        public ICommand ShowMonthReport => _showMonthReport;

        public ICommand ShowShiftsReport => _showShiftsReportCommand;

        public ICommand ShowMonthShiftReport => _showMonthShiftReport;

        public ICommand ShowTestAndTunesReport => _showTestAndTunesReport;

        public ICommand ShowShiftReport => _showShiftReport;

        public ICommand ShowSummaryForPeriodReport => _showSummaryForPeriodReport;

        private class ShowMonthReportCommand : ICommand
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
                monthReportWindowModel.RefreshCommand = new GenerateMonthReportCommand(monthReportWindowModel);
                reportWindow.DataContext = monthReportWindowModel;
                reportWindow.ShowDialog();
            }
        }
    }
}