using System;
using System.Data.Entity;
using System.Windows.Input;
using TestAndTunes.Model;

namespace TestAndTunes
{
    internal class CancellCommand : ICommand
    {
        private UncheckedRecord _uncheckedRecord;
        private JournalDBEntities _ctx;

        public CancellCommand(JournalDBEntities ctx, UncheckedRecord uncheckedRecord)
        {
            this._ctx = ctx;
            this._uncheckedRecord = uncheckedRecord;
            _uncheckedRecord.PropertyChanged += (s, a) => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _uncheckedRecord.Model != null;
        }

        public void Execute(object parameter)
        {
            var entry = _ctx.Entry(_uncheckedRecord.Model);
            switch(entry.State)
            {
                case EntityState.Modified:
                    entry.CurrentValues.SetValues(entry.OriginalValues);
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
            }
            _uncheckedRecord.Model = null;
        }
    }
}