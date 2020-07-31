using System;
using System.Windows.Input;
using TestAndTunes.DomainModel;
using TestAndTunes.DomainModel.Entities;
using TestAndTunes.ViewModels;

namespace TestAndTunes
{
    public class EditCommand:ICommand
    {
        

        private JournalRecordViewModel _selectedRecord;

        private UncheckedRecord _uncheckedRecord;

        public EditCommand(UncheckedRecord uncheckedRecord)
        {
            _uncheckedRecord = uncheckedRecord;
            _uncheckedRecord.PropertyChanged += (s, a) => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetSelectedRecord(JournalRecordViewModel selectedRecord)
        {
            _selectedRecord = selectedRecord;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _selectedRecord != null&&_uncheckedRecord.Model==null;
        }

        public void Execute(object parameter)
        {
            _uncheckedRecord.Model = _selectedRecord.Model;
            _uncheckedRecord.IsNewRecord = false;
        }
    }
}