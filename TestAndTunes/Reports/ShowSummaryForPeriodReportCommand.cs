using System;
using System.Windows.Input;
using TestAndTunes.Reports;

namespace TestAndTunes
{
    internal class ShowSummaryForPeriodReportCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            var window = new PeriodReportWindow();
            var periodReportWindowViewModel = new PeriodReportWindowViewModel();
            periodReportWindowViewModel.RefreshCommand = new GenerateSummaryForPeriodReportCommand(periodReportWindowViewModel);
            window.DataContext = periodReportWindowViewModel;
            window.ShowDialog();
        }
    }
}