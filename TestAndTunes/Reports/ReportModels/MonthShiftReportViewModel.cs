using System;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;
using TestAndTunes.DAL.JournalRecordsSelectionCriterias;
using TestAndTunes.Reports.Models;
using TestAndTunes.Routines;

namespace TestAndTunes.Reports
{
    /// <summary>
    /// Модель отчёта "Просстои оборудования за период, по сменам".
    /// </summary>
    class MonthShiftReportViewModel: IReportViewModel
    {
        private static SetOfCriteriaForSelectingJournalRecords criterias;
        static MonthShiftReportViewModel()
        {
            criterias = new SetOfCriteriaForSelectingJournalRecords();
            criterias.AddCriteria(new ExcludeСoncomitant() { IsEnabled = true });
            criterias.AddCriteria(new ExcludeRepair());
        }

        public MonthShiftReportViewModel(DateTime beginDate, DateTime endDate)
        {
            
            Load(beginDate,endDate);            
        }

        Action<LocalReport> IReportViewModel.SetReportParameters { get; }

        string IReportViewModel.ReportEmbeddedResource => "TestAndTunes.Reports.Layouts.ShiftMonthReport.rdlc";

        public ICollection<MonthShiftReportRecord> ReportRecords { get; set; }

        public SubreportProcessingEventHandler SubreportProcessing => null;

        public static IReadOnlyCollection<IOption> Options => criterias.Members;

        public virtual void FillDataSources(ReportDataSourceCollection dataSources)
        {
            dataSources.Add(new ReportDataSource("DataSet1"));
            dataSources["DataSet1"].Value = ReportRecords;
        }
        
        private void Load(DateTime beginDate, DateTime endDate)
        {
            ReportService service = new ReportService();
            ReportRecords = service.GetMonthShiftsReport(beginDate, endDate, criterias);
        }
    }
}
