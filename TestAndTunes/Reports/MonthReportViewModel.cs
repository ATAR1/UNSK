using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Reporting.WinForms;
using TestAndTunes.Reports;
using TestAndTunes.ViewModels;

namespace TestAndTunes
{
    public class MonthReportViewModel:INotifyPropertyChanged, IPeriodReportViewModel
    {
        private string _layout = "TestAndTunes.Reports.Layouts.MonthReport.rdlc";

        public string ReportEmbeddedResource
        {
            get { return _layout; }
            set { _layout = value; }
        }

        private string _month= "МЕСЯЦ";
        private TotalsTableVM _summaryTO1;
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

        public DateTime BeginDate
        {
            get;

            set;
        }

        public DateTime EndDate
        {
            get;

            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal IEnumerable<MonthReportViewModel> GetLine()
        {
            yield return this;
        }

        public void FillDataSources(ReportDataSourceCollection dataSources)
        {
            dataSources.Add(new ReportDataSource("TO1DataSet"));
            dataSources.Add(new ReportDataSource("TO2DataSet"));
            dataSources.Add(new ReportDataSource("UOGTDataSet"));
            dataSources.Add(new ReportDataSource("ReportDataSet"));

            dataSources["TO1DataSet"].Value =   SummaryPerMonth.GetReportLines();
            dataSources["TO2DataSet"].Value =   SummaryTO2.GetReportLines();
            dataSources["UOGTDataSet"].Value =  SummaryUOGT.GetReportLines();
            dataSources["ReportDataSet"].Value= GetLine();
        }

        private void Load(DateTime beginDate, DateTime endDate)
        {
            ReportService service = new ReportService();
            SummaryPerMonth = new TotalsTableMonthView(service.GenerateTotals(beginDate, endDate, "ТО1"));
            SummaryTO2 = new TotalsTableMonthView(service.GenerateTotals(beginDate, endDate, "ТО2"));
            SummaryUOGT = new TotalsTableMonthView(service.GenerateTotals(beginDate, endDate, "УОГТ"));
            Month = beginDate.ToString("MMMM");
            Year = beginDate.Year; ;
        }

        public void Load()
        {
            Load(BeginDate, EndDate);
        }
    }
}
