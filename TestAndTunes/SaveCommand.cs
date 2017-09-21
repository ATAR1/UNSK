﻿using System;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace TestAndTunes
{
    internal class SaveCommand : ICommand
    {
        private JournalDBEntities _ctx;
        private UncheckedRecord _uncheckedRecord;
        private ViewModel _viewModel;

        public SaveCommand(UncheckedRecord uncheckedRecord, JournalDBEntities ctx, ViewModel vievModel)
        {
            this._uncheckedRecord = uncheckedRecord;
            this._ctx = ctx;
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
                _ctx.SaveChanges();                
                ((AddCommand)_viewModel.AddCommand).StoredWorkArea = _uncheckedRecord.WorkArea;
                ((AddCommand)_viewModel.AddCommand).StoredDate = _uncheckedRecord.Date.Value;
                ((AddCommand)_viewModel.AddCommand).StoredShift = _uncheckedRecord.Shift;
                _uncheckedRecord.Model = null;
                _viewModel.RefreshJournalRecords();
                _viewModel.RefreshTotals();
            }
        }
    }
}