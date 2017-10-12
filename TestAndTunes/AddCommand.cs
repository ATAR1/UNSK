using System;
using System.Windows.Input;
using TestAndTunes.DomainModel;
using TestAndTunes.DomainModel.Entities;

namespace TestAndTunes
{
    internal class AddCommand : ICommand
    {
        private JournalDBEntities _ctx;
        private UncheckedRecord _uncheckedRecord;

        public AddCommand(JournalDBEntities ctx, UncheckedRecord uncheckedRecord)
        {
            this._ctx = ctx;
            _uncheckedRecord = uncheckedRecord;
            _uncheckedRecord.PropertyChanged += (s, args) => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public string StoredWorkArea { get; set; }

        public DateTime StoredDate { get; set; } = DateTime.Today;
        public string StoredShift { get;  set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _uncheckedRecord.Model == null;
        }

        public void Execute(object parameter)
        {
            JournalRecord newRecord = new JournalRecord() { Date=StoredDate, Shift =StoredShift, WorkArea = StoredWorkArea };
            _uncheckedRecord.Model = newRecord;
            _ctx.JournalRecords.Add(newRecord);
        }
    }
}