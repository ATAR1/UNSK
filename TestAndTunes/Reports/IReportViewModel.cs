using System;
using Microsoft.Reporting.WinForms;

namespace TestAndTunes.Reports
{
    public interface IReportViewModel
    {
        string ReportEmbeddedResource { get; }

        void FillDataSources(ReportDataSourceCollection dataSources);

        SubreportProcessingEventHandler SubreportProcessing { get; }

        //void Load();

        Action<LocalReport> SetReportParameters { get; set; }
    }

    //public interface IPeriodReportViewModel : IReportViewModel
    //{
    //    DateTime BeginDate { get; }
    //    DateTime EndDate { get; }
    //}

    //public interface IShiftReportViewModel : IReportViewModel
    //{
    //    DateTime Date { get; }

    //    string Shift { get; }
    //}
}