using System;
using System.Linq;
using Microsoft.Reporting.WinForms;
using System.Collections.Generic;
using TestAndTunes.DomainModel.Entities;

namespace TestAndTunes.Reports.ReportModels
{
    public class JournalReportViewModel : IReportViewModel
    {
        private ICollection<ShiftWorkAreaGroup> _groupHeaders;

        private IEnumerable<JournalRecord> _journalRecords;
        private DateTime _beginDate;
        private DateTime _endDate;

        public JournalReportViewModel(DateTime beginDate, DateTime endDate)
        {
            _beginDate = beginDate;
            _endDate = endDate;
        }

        public string ReportEmbeddedResource => "TestAndTunes.Reports.Layouts.Journal.rdlc";



        public Action<LocalReport> SetReportParameters => SetParameters;

        private void SetParameters(LocalReport report)
        {
            report.SetParameters(new ReportParameter("BeginDate", _beginDate.ToString()));
            report.SetParameters(new ReportParameter("EndDate", _endDate.ToString()));
        }

        public SubreportProcessingEventHandler SubreportProcessing => SubReportHandle;

        private void SubReportHandle(object sender, SubreportProcessingEventArgs e)
        {
            var parameters = e.Parameters;
            var date = DateTime.Parse(parameters["Date"].Values[0]);
            var shift = parameters["Shift"].Values[0];
            var workArea = parameters["WorkArea"].Values[0];
            e.DataSources.Add(new ReportDataSource("DataSet1"));
            e.DataSources["DataSet1"].Value = _journalRecords
                .Where(jr => jr.Date == date && jr.Shift == shift && jr.WorkArea == workArea)
                .Select(jr => new Record
                {
                    Defectoscope = jr.DefectoscopeName,
                    Duration = jr.Duration.TotalMinutes,
                    Normative = jr.Normative.TotalMinutes,
                    End = jr.End,
                    Start = jr.Start,
                    Operation = jr.OperationName,
                    Description = jr.Description
                })
                .ToList();

            e.DataSources.Add(new ReportDataSource("DataSet2"));
            e.DataSources["DataSet2"].Value = _journalRecords
                .Where(jr => jr.Date == date && jr.Shift == shift && jr.WorkArea == workArea)
                .GroupBy(jr => jr.Operation.Work.OperationGroup)
                .Select(g => new Summary
                {
                    OperationType = g.Key,
                    Deviation = Math.Round(g.Sum(jr => jr.Deviation.TotalMinutes), 2),
                    Duration = Math.Round(g.Sum(jr => jr.Duration.TotalMinutes), 2),
                    Normative = Math.Round(g.Sum(jr => jr.Normative.TotalMinutes), 2),
                    Quantity = g.Count()
                });
        }
        

        public void FillDataSources(ReportDataSourceCollection dataSources)
        {
            throw new NotImplementedException();
        }
    }
}
