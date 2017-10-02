using System;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;

namespace TestAndTunes.Reports
{
    public class ShiftsReportViewModel:IReportViewModel
    {

        public string ReportEmbeddedResource => "TestAndTunes.Reports.Layouts.DailyMonthReport.rdlc";

        public ICollection<ShiftsReportRecord> ReportRecords { get; set; }

        public void FillDataSources(ReportDataSourceCollection dataSources)
        {
            dataSources.Add(new ReportDataSource("ShiftReportDataSet"));
            dataSources["ShiftReportDataSet"].Value = ReportRecords;
        }

        public void Load(DateTime beginDate, DateTime endDate)
        {
            ReportService service = new ReportService();
            ReportRecords = service.GetShiftsReport(beginDate, endDate);
        }
    }


}