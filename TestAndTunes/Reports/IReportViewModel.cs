using System;
using Microsoft.Reporting.WinForms;
using System.Collections.Generic;
using TestAndTunes.Routines;

namespace TestAndTunes.Reports
{
    public interface IReportViewModel
    {
        string ReportEmbeddedResource { get; }

        void FillDataSources(ReportDataSourceCollection dataSources);

        SubreportProcessingEventHandler SubreportProcessing { get; }

        Action<LocalReport> SetReportParameters { get; }
    }
}