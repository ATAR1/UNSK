using System;
using System.Windows.Input;
using TestAndTunes.ViewModels;

namespace TestAndTunes
{
    internal class RefreshCommand : ICommand
    {
        private MainWindowModel _viewModel;

        public RefreshCommand(MainWindowModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.RefreshJournalRecords();
        }


    }
}