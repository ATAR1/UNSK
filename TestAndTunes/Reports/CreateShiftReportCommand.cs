using System;
using System.Windows.Input;

namespace TestAndTunes.Reports
{
    internal class CreateShiftReportCommand : ICommand
    {
        private ShiftReportWindowModel _shiftReportWindowModel;

        public CreateShiftReportCommand(ShiftReportWindowModel shiftReportWindowModel)
        {
            _shiftReportWindowModel = shiftReportWindowModel;
        }

        protected ShiftReportWindowModel ShiftReportWindowModel
        {
            get
            {
                return _shiftReportWindowModel;
            }
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            GenerateReport();
        }

        protected virtual void GenerateReport()
        {
            ShiftReportWindowModel.Report = new ShiftReportViewModel(ShiftReportWindowModel.Date, ShiftReportWindowModel.Shift);
        }
    }
}