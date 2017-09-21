using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace TestAndTunes
{
    internal class RefreshCommand : ICommand
    {
        private ViewModel _viewModel;

        public RefreshCommand(ViewModel viewModel)
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