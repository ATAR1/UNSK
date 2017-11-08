﻿using System;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;
using TestAndTunes.Reports.Models;

namespace TestAndTunes.Reports
{
    class MonthShiftReportViewModel: IReportViewModel
    {   
        public MonthShiftReportViewModel(DateTime beginDate, DateTime endDate)
        {
            Load(beginDate,endDate);
        }

        Action<LocalReport> IReportViewModel.SetReportParameters { get; set; }

        public string ReportEmbeddedResource => "TestAndTunes.Reports.Layouts.ShiftMonthReport.rdlc";

        public ICollection<MonthShiftReportRecord> ReportRecords { get; set; }

        public SubreportProcessingEventHandler SubreportProcessing => null;

        public void FillDataSources(ReportDataSourceCollection dataSources)
        {
            dataSources.Add(new ReportDataSource("DataSet1"));
            dataSources["DataSet1"].Value = ReportRecords;
        }
        
        private void Load(DateTime beginDate, DateTime endDate)
        {
            ReportService service = new ReportService();
            ReportRecords = service.GetMonthShiftsReport(beginDate, endDate);
        }
    }
}
