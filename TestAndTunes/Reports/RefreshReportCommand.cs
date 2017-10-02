using System;
using System.Windows.Input;

namespace TestAndTunes
{
    internal class RefreshReportCommand : ICommand
    {
        IReportWindowModel _windowModel;

        public RefreshReportCommand(IReportWindowModel windowModel)
        {
            _windowModel = windowModel;
        }



        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _windowModel.RefreshReport();
        }
    }
}
