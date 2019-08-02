using System;
using Microsoft.Reporting.WinForms;
using TestAndTunes.Reports;
using TestAndTunes.Reports.Models;
using System.Collections.Generic;
using TestAndTunes.DAL.JournalRecordsSelectionCriterias;

namespace TestAndTunes.ViewModels
{
    internal class ShortMonthReportViewModel : IReportViewModel
    {
        private static SetOfCriteriaForSelectingJournalRecords criterias;
        static ShortMonthReportViewModel()
        {
            criterias = new SetOfCriteriaForSelectingJournalRecords();
            criterias.AddCriteria(new ExcludeСoncomitant() { IsEnabled = true });
            criterias.AddCriteria(new ExcludeRepair() { IsEnabled = true });
        }

        public ShortMonthReportViewModel(DateTime beginDate, DateTime dateEnd)
        {
            Load(beginDate, dateEnd);
        }

        public string ReportEmbeddedResource => "TestAndTunes.Reports.Layouts.ShortMonthReport.rdlc";

        public Action<LocalReport> SetReportParameters => SetReportParametersAction;

        private void SetReportParametersAction(LocalReport report)
        {
            report.SetParameters(new ReportParameter("Signatory1Position", Properties.Settings.Default.ReportSignatory1Position));
            report.SetParameters(new ReportParameter("Signatory2Position", Properties.Settings.Default.ReportSignatory2Position));
            report.SetParameters(new ReportParameter("Signatory1FullName", Properties.Settings.Default.ReportSignatory1FullName));
            report.SetParameters(new ReportParameter("Signatory2FullName", Properties.Settings.Default.ReportSignatory2FullName));
        }

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
            ReportRecords = service.GetMonthShiftsReport(beginDate, endDate, criterias);
        }
    }
}