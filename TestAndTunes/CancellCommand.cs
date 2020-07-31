using System;
using System.Data.Entity;
using System.Windows.Input;
using TestAndTunes.DAL;
using TestAndTunes.DomainModel;
using TestAndTunes.DomainModel.Entities;
using TestAndTunes.ViewModels;

namespace TestAndTunes
{
    internal class CancellCommand : ICommand
    {
        private UncheckedRecord _uncheckedRecord;

        public CancellCommand( UncheckedRecord uncheckedRecord)
        {
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
            _uncheckedRecord.Model = null;
        }
    }
}