using System;
using Microsoft.Reporting.WinForms;
using System.Collections.Generic;
using System.Linq;

namespace TestAndTunes.Reports
{
    internal class TestAndTunesReportViewModel : IReportViewModel
    {
        private ICollection<TestAndTunesReportRecord> _testsRecords;
        private ICollection<TestAndTunesReportRecord> _tunesRecords;

        public string ReportEmbeddedResource => "TestAndTunes.Reports.Layouts.TestAndTunesReport.rdlc";

        public void FillDataSources(ReportDataSourceCollection dataSources)
        {
            dataSources.Add(new ReportDataSource("DataSet1"));
            dataSources["DataSet1"].Value = _tunesRecords;
            dataSources.Add(new ReportDataSource("DataSet2"));
            dataSources["DataSet2"].Value = _testsRecords;
        }

        public void Load(DateTime beginDate, DateTime endDate)
        {
            ReportService service = new ReportService();
            _tunesRecords = service.GetTunes(beginDate, endDate).ToArray();
            _testsRecords = service.GetTests(beginDate, endDate).ToArray();
        }
    }
}