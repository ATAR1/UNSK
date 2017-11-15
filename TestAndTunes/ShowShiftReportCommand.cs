using System;
using System.Windows.Input;
using TestAndTunes.Reports;

namespace TestAndTunes
{
    /// <summary>
    /// Комманда показать сменный рапорт
    /// </summary>
    internal class ShowShiftReportCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            var window = new ShiftReportWindow();
            window.ShowDialog();
        }
    }
}