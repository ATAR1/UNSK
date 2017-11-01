using System;
using Microsoft.Reporting.WinForms;

namespace TestAndTunes.Reports
{
    public interface IReportViewModel
    {
        string ReportEmbeddedResource { get; }

        void FillDataSources(ReportDataSourceCollection dataSources);

        SubreportProcessingEventHandler SubreportProcessing { get; }

        void Load();

        void SetReportParameters(LocalReport localReport);
    }

    public interface IPeriodReportViewModel : IReportViewModel
    {
        DateTime BeginDate { get; set; }
        DateTime EndDate { get; set; }
    }

    public interface IShiftReportViewModel : IReportViewModel
    {
        DateTime Date { get; set; }

        string Shift { get; set; }
    }
}