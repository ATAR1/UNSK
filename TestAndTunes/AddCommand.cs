using System;
using System.Windows.Input;
using TestAndTunes.DAL;
using TestAndTunes.DomainModel;
using TestAndTunes.DomainModel.Entities;
using TestAndTunes.ViewModels;

namespace TestAndTunes
{
    internal class AddCommand : ICommand
    {
        private IJournalRepository _repository;
        private UncheckedRecord _uncheckedRecord;

        public AddCommand(IJournalRepository repository, UncheckedRecord uncheckedRecord)
        {
            this._repository = repository;
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
            _repository.Add(newRecord);
        }
    }
}