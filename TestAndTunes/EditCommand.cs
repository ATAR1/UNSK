using System;
using System.Windows.Input;
using TestAndTunes.Model;

namespace TestAndTunes
{
    public class EditCommand:ICommand
    {
        private JournalDBEntities _ctx;

        private JournalRecordViewModel _selectedRecord;

        private UncheckedRecord _uncheckedRecord;

        public EditCommand(JournalDBEntities _ctx, UncheckedRecord uncheckedRecord)
        {
            this._ctx = _ctx;
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
        }
    }
}