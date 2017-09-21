using System;
using System.Windows;
using System.Windows.Input;

namespace TestAndTunes
{
    internal class DeleteCommand : ICommand
    {
        private JournalRecordViewModel _selectedRecord;
        private JournalDBEntities _ctx;
        private ViewModel _viewModel;
        private UncheckedRecord _uncheckedRecord;

        public DeleteCommand(JournalDBEntities _ctx, ViewModel viewModel)
        {
            
            this._ctx = _ctx;
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
                _ctx.JournalRecords.Remove(_selectedRecord.Model);
                _ctx.SaveChanges();
                _viewModel.RefreshJournalRecords();
                _viewModel.RefreshTotals();
                // test 245
            }
        }
    }
}