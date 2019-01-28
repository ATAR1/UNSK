using System;
using System.Collections.Generic;
using System.Windows.Input;
using TestAndTunes.DomainModel.Entities;
using TestAndTunes.Entities;
using TestAndTunes.Reports;

namespace TestAndTunes
{
    /// <summary>
    /// Комманда показать сменный рапорт
    /// </summary>
    internal class ShowShiftReportCommand : ICommand
    {
        private readonly IEnumerable<SheldueRecord> _sheldue;

        public ShowShiftReportCommand(IEnumerable<SheldueRecord> sheldue)
        {
            _sheldue = sheldue;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            var window = new ShiftReportWindow();
            var shiftReportWindowModel = new ShiftReportWindowModel(_sheldue);
            shiftReportWindowModel.RefreshCommand = new CreateShiftReportCommand(shiftReportWindowModel);
            window.DataContext = shiftReportWindowModel;
            window.ShowDialog();
        }
    }
}