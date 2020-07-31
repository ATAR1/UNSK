using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TestAndTunes.DAL;
using TestAndTunes.DomainModel;
using TestAndTunes.DomainModel.Entities;
using TestAndTunes.Entities;

namespace TestAndTunes
{
    public class UncheckedRecord : INotifyPropertyChanged
    {
        private JournalRecord _model;

        public UncheckedRecord(IEnumerable<SheldueRecord> sheldue)
        {
            DateShift = new DateShiftVM(sheldue);
        }

        private void DateShiftPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DateShift.Date))
            {
                _model.Date = DateShift.Date;
            }
            if (e.PropertyName == nameof(DateShift.Shift))
            {
                _model.Shift = DateShift.Shift;
            }
        }

        public bool CheckModel()
        {
            Errors.Clear();
            if (String.IsNullOrWhiteSpace(_model.DefectoscopeName))
            {
                Errors.Add("Не выбран ни один дефектоскоп!");
            }
            if (_model.Shift == null)
            {
                Errors.Add("Не выбрана смена!");
            }
            if (String.IsNullOrWhiteSpace(_model.OperationName))
            {
                Errors.Add("Не выбрана операция!");
            }
            return Errors.Count == 0;

        }

        public DateShiftVM DateShift { get; set; }

        public bool IsNewRecord { get; set; }



        public string Start
        {
            get { return _model?.Start.ToString(@"hh\:mm"); }
            set
            {
                _model.Start = TimeSpan.Parse(value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Start)));
            }
        }

        public string End
        {
            get
            {
                return _model?.End.ToString(@"hh\:mm"); ;
            }
            set
            {
                _model.End = TimeSpan.Parse(value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(End)));
            }
        }


        public string Employee
        {
            get
            {
                return _model?.Employee;
            }
            set
            {
                if (_model.Employee != value)
                {
                    _model.Employee = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Employee)));
                }

            }
        }

        public string Description
        {
            get
            {
                return _model?.Description;
            }
            set
            {
                if (_model.Description != value)
                {
                    _model.Description = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
                }
            }
        }

        public string WorkArea
        {
            get
            {
                return _model?.WorkArea;
            }
            set
            {
                if (_model != null) _model.WorkArea = value;
            }
        }

        public string Defectoscope
        {
            get
            {
                return _model?.DefectoscopeName;
            }
            set
            {
                if (_model != null)
                {
                    _model.DefectoscopeName = value;
                    if (String.IsNullOrEmpty(Operation)) Operation = "Проверка чувствительности";
                }
            }
        }

        public string Operation
        {
            get
            {
                return _model?.OperationName;
            }
            set
            {
                if (_model != null) _model.OperationName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Operation)));
            }
        }



        public JournalRecord Model
        {
            get { return _model; }
            set
            {
                _model = value;
                if (value != null)
                {
                    DateShift.PropertyChanged -= DateShiftPropertyChanged;
                    DateShift.Date = _model.Date;
                    DateShift.Shift = _model.Shift;
                    DateShift.PropertyChanged += DateShiftPropertyChanged;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));

            }
        }

        public bool NotEmpty { get { return _model != null; } }

        public ICollection<string> Errors { get; private set; } = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;


    }
}
