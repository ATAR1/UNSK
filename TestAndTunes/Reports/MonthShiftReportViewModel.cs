﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.WinForms;

namespace TestAndTunes.Reports
{
    class MonthShiftReportViewModel: IReport
    {
        
        public string ReportEmbeddedResource => "TestAndTunes.Reports.Layouts.ShiftMonthReport.rdlc";

        public ICollection<MonthShiftReportRecord> ReportRecords { get; set; }

        public void FillDataSources(ReportDataSourceCollection dataSources)
        {
            dataSources.Add(new ReportDataSource("DataSet1"));
            dataSources["DataSet1"].Value = ReportRecords;
        }

        public void Load(DateTime beginDate, DateTime endDate)
        {
            ReportService service = new ReportService();
            ReportRecords = service.GetMonthShiftsReport(beginDate, endDate);
        }
    }
}
