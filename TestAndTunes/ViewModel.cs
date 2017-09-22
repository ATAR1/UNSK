using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace TestAndTunes
{
    public class ViewModel
    {
        private readonly ICommand _addCommand;
        private readonly ICommand _saveCommand;
        private readonly EditCommand _editCommand;
        private readonly DeleteCommand _deleteCommand;
        private readonly ObservableCollection<Work> _operations = new ObservableCollection<Work>();
        private readonly ObservableCollection<WorkArea> _areas = new ObservableCollection<WorkArea>();
        private readonly ObservableCollection<Employee> _personal = new ObservableCollection<Employee>();
        private readonly ICommand _refreshCommand;
        private JournalRecordViewModel _selectedRecord;
        private ICommand _cancellCommand;
        private MenuModel _menu = new MenuModel();
        private bool onlyForCurrentShift;
        private Service _service;
        private JournalDBEntities _ctx;

        public ViewModel()
        {
            try
            {
                UncheckedRecord = new UncheckedRecord();
                _ctx = new JournalDBEntities();
                _service = new Service(_ctx);
                _saveCommand = new SaveCommand(UncheckedRecord, _ctx, this);
                _deleteCommand = new DeleteCommand(_ctx, this);
                _editCommand = new EditCommand(_ctx, UncheckedRecord);
                JournalRecords = new ObservableCollection<JournalRecordViewModel>();
                _refreshCommand = new RefreshCommand(this);
                _addCommand = new AddCommand(_ctx, UncheckedRecord);
                _cancellCommand = new CancellCommand(_ctx, UncheckedRecord);
                RefreshJournalRecords();
                Shift = new DateShiftVM();
                Shift.PropertyChanged += CurrentShiftChanged;
                RefreshTotals();
                LoadCollections();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void CurrentShiftChanged(object sender, PropertyChangedEventArgs e)
        {
            RefreshTotals(Shift.Date, Shift.Letter);
        }

        public void LoadCollections()
        {
            Areas.Clear();
            Operations.Clear();
            Personal.Clear();
            Shifts.Clear();

            foreach (var area in _service.LoadWorkAreas())
            {
                Areas.Add(area);
            }

            foreach (var operation in _service.LoadWorks())
            {
                Operations.Add(operation);
            }

            foreach (var employee in _service.LoadPersonal())
            {
                Personal.Add(employee);
            }

            foreach (var shift in _service.LoadShiftSet())
            {
                Shifts.Add(shift);
            }
        }

        public void RefreshJournalRecords()
        {
            try
            {
                JournalRecords.Clear();
                var collection = _ctx.JournalRecords.ToList().OrderBy(jr => new Tuple<DateTime, TimeSpan>(jr.Date, jr.Start), new ShiftedTimeComparer()).Select(jr => new JournalRecordViewModel(jr));
                foreach (var viewModel in collection)
                {
                    JournalRecords.Add(viewModel);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void RefreshTotals()
        {
            RefreshTotals(Shift.Date, Shift.Letter);
        }

        public void RefreshTotals(DateTime date, string shift)
        {
            ReportService service = new ReportService();

            Totals.TO1 = service.GenerateTotals(date,shift, "ТО1");
            Totals.TO2 = service.GenerateTotals(date, shift, "ТО2");
            Totals.UOGT = service.GenerateTotals(date, shift, "УОГТ");

        }


        public MenuModel Menu => _menu;

        public Summary Totals { get; set; } = new Summary();

        

        public UncheckedRecord UncheckedRecord { get; private set; }

        public ICommand RefreshCommand => _refreshCommand;

        public ICommand AddCommand => _addCommand; 

        public ICommand SaveCommand => _saveCommand;

        public ICommand EditCommand => _editCommand;

        public ICommand DeleteCommand => _deleteCommand;

        public ICommand CancellCommand => _cancellCommand;

        public ICollection<Work> Operations => _operations;

        public ICollection<WorkArea> Areas => _areas;

        public ICollection<Shift> Shifts { get; private set; } = new ObservableCollection<Shift>();

        public DateShiftVM Shift { get; set; }

        public ICollection<Employee> Personal => _personal;

        public ICollection<JournalRecordViewModel> JournalRecords { get; private set; }
        public JournalRecordViewModel SelectedRecord {
            get
            {
                return _selectedRecord;
            }
            set
            {
                if(_selectedRecord!= value)
                {
                    _selectedRecord = value;
                    _deleteCommand.SetSelectedRecord(value);
                    _editCommand.SetSelectedRecord(value);
                }
            }
        }

        public bool OnlyForCurrentShift
        {
            get
            {
                return onlyForCurrentShift;
            }

            set
            {
                onlyForCurrentShift = value;
            }
        }
    }

    public static class TimeSpanExtensions
    {
        public static string ToShortSignedString(this TimeSpan value)
        {
            string result="";
            if (value < TimeSpan.Zero)
            {
                result = "-";
            }
            result += value.ToString(@"hh\:mm");
            return result;
        }
    }
}
