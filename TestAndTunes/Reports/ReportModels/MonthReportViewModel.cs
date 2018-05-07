using System;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;
using TestAndTunes.Reports;
using TestAndTunes.Routines;

namespace TestAndTunes
{
    /// <summary>
    /// Модель отчёта "Простои оборудования за месяц"
    /// </summary>
    public class MonthReportViewModel : IReportViewModel
    {
        private string _layout = "TestAndTunes.Reports.Layouts.MonthReport.rdlc";

        public string ReportEmbeddedResource
        {
            get { return _layout; }
            set { _layout = value; }
        }

        private TotalsTableVM _summaryTO1;
        private TotalsTableVM _summaryTO2;
        private TotalsTableVM _summaryUOGT;

        public MonthReportViewModel(DateTime beginDate, DateTime endDate)
        {
            Load(beginDate,endDate);
        }
        
        public SubreportProcessingEventHandler SubreportProcessing => null;

        public Action<LocalReport> SetReportParameters { get; set; }

        public void FillDataSources(ReportDataSourceCollection dataSources)
        {
            dataSources.Add(new ReportDataSource("TO1DataSet"));
            dataSources.Add(new ReportDataSource("TO2DataSet"));
            dataSources.Add(new ReportDataSource("UOGTDataSet"));

            dataSources["TO1DataSet"].Value = _summaryTO1.GetReportLines();
            dataSources["TO2DataSet"].Value = _summaryTO2.GetReportLines();
            dataSources["UOGTDataSet"].Value = _summaryUOGT.GetReportLines();
        }

        private void Load(DateTime beginDate, DateTime endDate)
        {
            ReportService service = new ReportService();
            _summaryTO1 = new TotalsTableMonthView(service.GenerateTotals(beginDate, endDate, "ТО1"));
            _summaryTO2 = new TotalsTableMonthView(service.GenerateTotals(beginDate, endDate, "ТО2"));
            _summaryUOGT = new TotalsTableMonthView(service.GenerateTotals(beginDate, endDate, "УОГТ"));            
        }
        
    }
}
