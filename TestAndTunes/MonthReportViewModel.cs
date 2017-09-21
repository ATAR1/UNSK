using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAndTunes
{
    public class MonthReportViewModel:INotifyPropertyChanged
    {
        private string _month= "МЕСЯЦ";
        private TotalsTableVM _summaryTO1 = new TotalsTableMonthView( new TotalsTable
        {
            Caption = "Участок",
            Repair = new TotalsLine { Deviation = TimeSpan.FromMinutes(1) },
            Tests = new TotalsLine { Deviation = TimeSpan.FromMinutes(1) },
            Tunes = new TotalsLine { Deviation = TimeSpan.FromMinutes(1) },
            Totals = new TotalsLine { Deviation = TimeSpan.FromMinutes(1) }
        });
        private TotalsTableVM _summaryTO2;
        private TotalsTableVM _summaryUOGT;
        private int _year = 9999;

        public string Month
        {
            get
            {
                return _month;
            }
            set
            {
                _month = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ReportHeader)));
            }
        }

        public string ReportHeader => $"Простои оборудования УНСК за {Month} {Year} года.";

        public TotalsTableVM SummaryPerMonth
        {
            get { return _summaryTO1; }
            set
            {
                _summaryTO1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SummaryPerMonth)));
            }
        }


        public TotalsTableVM SummaryTO2
        {
            get
            {
                return _summaryTO2;
            }
            set
            {
                _summaryTO2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SummaryTO2)));
            }
        }


        public TotalsTableVM SummaryUOGT
        {
            get
            {
                return _summaryUOGT;
            }
            set
            {
                _summaryUOGT = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SummaryUOGT)));
            }
        }


        public int Year
        {
            get { return _year; }
            set
            {
                _year = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ReportHeader)));
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        internal IEnumerable<MonthReportViewModel> GetLine()
        {
            yield return this;
        }
    }
}
