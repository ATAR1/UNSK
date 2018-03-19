using System;
using System.Windows.Input;
using TestAndTunes.DAL;
using TestAndTunes.DomainModel;
using TestAndTunes.DomainModel.Entities;
using TestAndTunes.ViewModels;

namespace TestAndTunes
{
    public class SaveCommand : ICommand
    {
        private IJournal _journal;
        private UncheckedRecord _uncheckedRecord;
        private MainWindowModel _viewModel;

        public SaveCommand(UncheckedRecord uncheckedRecord, IJournal journal, MainWindowModel vievModel)
        {
            this._uncheckedRecord = uncheckedRecord;
            this._journal = journal;
            _viewModel = vievModel;
            _uncheckedRecord.PropertyChanged += (s, a) => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _uncheckedRecord.Model != null;
        }

        public void Execute(object parameter)
        {            
            if (_uncheckedRecord.CheckModel())
            {
                _journal.SaveChanges();                
                ((AddCommand)_viewModel.AddCommand).StoredWorkArea = _uncheckedRecord.WorkArea;
                ((AddCommand)_viewModel.AddCommand).StoredDate = _uncheckedRecord.DateShift.Date;
                ((AddCommand)_viewModel.AddCommand).StoredShift = _uncheckedRecord.DateShift.Shift;
                _uncheckedRecord.Model = null;
                _viewModel.RefreshJournalRecords();
                _viewModel.RefreshTotals();
            }
        }
    }
}