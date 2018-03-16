using System;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;
using TestAndTunes.Reports.Models;
using TestAndTunes.Routines;

namespace TestAndTunes.Reports
{
    public class ShiftsReportViewModel: IReportViewModel
    {
        public ShiftsReportViewModel(DateTime beginDate, DateTime endDate)
        {
            Load(beginDate,endDate);
        }
        
        Action<LocalReport> IReportViewModel.SetReportParameters { get;  }

        public string ReportEmbeddedResource => "TestAndTunes.Reports.Layouts.DailyMonthReport.rdlc";

        public ICollection<ShiftsReportRecord> ReportRecords { get; set; }
        

        public SubreportProcessingEventHandler SubreportProcessing => null;

        public void FillDataSources(ReportDataSourceCollection dataSources)
        {
            dataSources.Add(new ReportDataSource("ShiftReportDataSet"));
            dataSources["ShiftReportDataSet"].Value = ReportRecords;
        }
         
        private void Load(DateTime beginDate, DateTime endDate)
        {
            ReportService service = new ReportService();
            ReportRecords = service.GetShiftsReport(beginDate, endDate);
        }
    }


}