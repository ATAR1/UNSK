using System;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;
using TestAndTunes.Reports.Models;

namespace TestAndTunes.Reports
{
    public class ShiftsReportViewModel: IPeriodReportViewModel
    {
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

        public string ReportEmbeddedResource => "TestAndTunes.Reports.Layouts.DailyMonthReport.rdlc";

        public ICollection<ShiftsReportRecord> ReportRecords { get; set; }

        public void FillDataSources(ReportDataSourceCollection dataSources)
        {
            dataSources.Add(new ReportDataSource("ShiftReportDataSet"));
            dataSources["ShiftReportDataSet"].Value = ReportRecords;
        }

        public void Load()
        {
            Load(BeginDate, EndDate);
        }

        private void Load(DateTime beginDate, DateTime endDate)
        {
            ReportService service = new ReportService();
            ReportRecords = service.GetShiftsReport(beginDate, endDate);
        }
    }


}