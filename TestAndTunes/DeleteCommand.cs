using System;
using System.Windows;
using System.Windows.Input;
using TestAndTunes.DAL;
using TestAndTunes.DomainModel;
using TestAndTunes.DomainModel.Entities;
using TestAndTunes.ViewModels;

namespace TestAndTunes
{
    internal class DeleteCommand : ICommand
    {
        private JournalRecordViewModel _selectedRecord;
        private IJournal _journal;
        private MainWindowModel _viewModel;
        private UncheckedRecord _uncheckedRecord;

        public DeleteCommand(IJournal journal, MainWindowModel viewModel)
        {
            
            this._journal = journal;
            _viewModel = viewModel;
            _uncheckedRecord = _viewModel.UncheckedRecord;
            _uncheckedRecord.PropertyChanged += (s, a) => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetSelectedRecord(JournalRecordViewModel selectedRecord)
        {
            this._selectedRecord = selectedRecord;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _selectedRecord != null && _uncheckedRecord.Model == null;
        }

        public void Execute(object parameter)
        {
            if (MessageBox.Show("Удалить выбранную запись?","Внимание!",MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _journal.Remove(_selectedRecord.Model);
                _journal.SaveChanges();
                _viewModel.RefreshJournalRecords();
                _viewModel.RefreshTotals();
            }
        }
    }
}