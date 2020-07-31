using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TestAndTunes.DAL;
using TestAndTunes.DomainModel;
using TestAndTunes.Entities;
using TestAndTunes.Routines;

namespace TestAndTunes.ViewModels
{
    public class MainWindowModel
    {
        private readonly ICommand _addCommand;
        private readonly ICommand _saveCommand;
        private readonly EditCommand _editCommand;
        private readonly DeleteCommand _deleteCommand;
        private readonly ICollection<Work> _operations = new ObservableCollection<Work>();
        private readonly ICollection<WorkArea> _areas = new ObservableCollection<WorkArea>();
        private readonly ICollection<Employee> _personal = new ObservableCollection<Employee>();
        private readonly ICommand _refreshCommand;
        
        private JournalRecordViewModel _selectedRecord;
        private ICommand _cancellCommand;
        private MenuModel _menu;
        private bool onlyForCurrentShift;
        private CollectionsRepository _collectionsRepository;
        private IJournalRepository _journalRepository;

        public MainWindowModel(IJournalRepository journalRepository, CollectionsRepository collectionsRepository, MenuModel menu)
        {
            try
            {
                _menu = menu;
                _journalRepository = journalRepository;
                _collectionsRepository = collectionsRepository;
                UncheckedRecord = new UncheckedRecord(_collectionsRepository.LoadSheldue().ToList());
                _saveCommand = new SaveCommand(UncheckedRecord, _journalRepository, this);
                _deleteCommand = new DeleteCommand(_journalRepository, this);
                _editCommand = new EditCommand(UncheckedRecord);
                JournalRecords = new RangeEnabledObservableCollection<JournalRecordViewModel>();
                _refreshCommand = new RefreshCommand(this);
                _addCommand = new AddCommand(UncheckedRecord);
                _cancellCommand = new CancellCommand(UncheckedRecord);
                RefreshJournalRecords();
                Shift = new DateShiftVM(_collectionsRepository.LoadSheldue().ToList());
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
            if (Shift.Shift==null) return;
            RefreshJournalRecords();
            RefreshTotals();
        }

        public void LoadCollections()
        {
            Areas.Clear();
            Operations.Clear();
            Personal.Clear();
            Shifts.Clear();

            foreach (var area in _collectionsRepository.LoadWorkAreas())
            {
                Areas.Add(area);
            }

            foreach (var operation in _collectionsRepository.LoadWorks())
            {
                Operations.Add(operation);
            }

            foreach (var employee in _collectionsRepository.LoadPersonal())
            {
                Personal.Add(employee);
            }

            foreach (var shift in _collectionsRepository.LoadShiftSet())
            {
                Shifts.Add(shift);
            }
        }

        public void RefreshJournalRecords()
        {
            try
            {
                JournalRecords.Clear();
                List<JournalRecord> list;
                if (OnlySelectedShift)
                {
                    list = _journalRepository.GetRecordsByDateAndShift(Shift.Date, Shift.Shift.Value);
                }
                else
                {
                    list = _journalRepository.GetRecordsStartFrom(FromTheDate);
                }
                var collection = list.OrderBy(jr =>jr, new JournalRecordsBeginTimeComparer()).Select(jr => new JournalRecordViewModel(jr));
                JournalRecords.AddRange(collection);                
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void RefreshTotals()
        {
            if (Shift.Shift==null) MessageBox.Show("Не выбрана смена!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            RefreshTotals(Shift.Date, Shift.Shift.Value);
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

        public RangeEnabledObservableCollection<JournalRecordViewModel> JournalRecords { get; private set; }
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

        public bool OnlySelectedShift
        {
            get
            {
                return onlyForCurrentShift;
            }

            set
            {
                onlyForCurrentShift = value;
                RefreshJournalRecords();
                RefreshTotals();
            }
        }
        public DateTime FromTheDate { get; set; } = DateTime.Today.AddMonths(-1);
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
