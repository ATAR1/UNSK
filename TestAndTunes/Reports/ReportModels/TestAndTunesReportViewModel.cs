using System;
using Microsoft.Reporting.WinForms;
using System.Collections.Generic;
using System.Linq;
using TestAndTunes.Reports.Models;
using TestAndTunes.Routines;

namespace TestAndTunes.Reports
{
    internal class TestAndTunesReportViewModel : IReportViewModel
    {
        private ICollection<TestAndTunesReportRecord> _testsRecords;
        private ICollection<TestAndTunesReportRecord> _tunesRecords;

        public TestAndTunesReportViewModel(DateTime beginDate, DateTime endDate)
        {
            Load(beginDate,endDate);
        }
        
        Action<LocalReport> IReportViewModel.SetReportParameters { get; }
        
        public string ReportEmbeddedResource => "TestAndTunes.Reports.Layouts.TestAndTunesReport.rdlc";

        public SubreportProcessingEventHandler SubreportProcessing => null;

        public void FillDataSources(ReportDataSourceCollection dataSources)
        {
            dataSources.Add(new ReportDataSource("DataSet1"));
            dataSources["DataSet1"].Value = _tunesRecords;
            dataSources.Add(new ReportDataSource("DataSet2"));
            dataSources["DataSet2"].Value = _testsRecords;
        }

        
        private void Load(DateTime beginDate, DateTime endDate)
        {
            ReportService service = new ReportService();
            _tunesRecords = service.GetTunes(beginDate, endDate).ToArray();
            _testsRecords = service.GetTests(beginDate, endDate).ToArray();
        }
    }
}