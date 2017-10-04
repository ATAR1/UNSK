using System;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;
using System.Linq;

namespace TestAndTunes.Reports
{
    public class ShiftReportViewModel : IShiftReportViewModel
    {
        private ICollection<ShiftWorkAreaGroup> _groupHeaders;
        private IEnumerable<JournalRecord> _lournalRecords;

        public DateTime Date { get;  set; }

        public string ReportEmbeddedResource => "TestAndTunes.Reports.Layouts.ShiftReport.rdlc";

        public string Shift { get;  set; }

        public ICollection<ShiftWorkAreaGroup> GroupHeaders => _groupHeaders;

        public SubreportProcessingEventHandler SubreportProcessing => SubreportProcessingHandler;

        private void SubreportProcessingHandler(object sender, SubreportProcessingEventArgs e)
        {
            var parameters = e.Parameters;
            var date = DateTime.Parse(parameters["Date"].Values[0]);
            var shift = parameters["Shift"].Values[0];
            var workArea = parameters["WorkArea"].Values[0];
            e.DataSources.Add(new ReportDataSource("DataSet1"));
            e.DataSources["DataSet1"].Value = _lournalRecords
                .Where(jr => jr.Date == date && jr.Shift == shift && jr.WorkArea == workArea)
                .Select(jr => new Record
                {
                    Defectoscope = jr.DefectoscopeName,
                    Duration = jr.Duration.TotalMinutes,
                    Normative=jr.Normative.TotalMinutes,
                    End = jr.End,
                    Start=jr.Start,
                    Operation = jr.OperationName
                })
                .ToList();

            e.DataSources.Add(new ReportDataSource("DataSet2"));
            e.DataSources["DataSet2"].Value = _lournalRecords
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
            dataSources.Add(new ReportDataSource("DataSet1"));
            dataSources["DataSet1"].Value = GroupHeaders;
        }
        

        public void Load()
        {
            ReportService service = new ReportService();
            var was = service.GetWorkAreas();
            _groupHeaders = was.Select(wa => new ShiftWorkAreaGroup { Date = Date, Shift = Shift, WorkArea = wa }).ToList();            
            _lournalRecords = service.GetForTheShift(Date, Shift);
        }

        public class Summary
        {
            public string OperationType { get; set; }

            public int Quantity { get; set; }

            public double Duration { get; set; }

            public double Normative { get; set; }

            public double Deviation { get; set; }

            public bool TooLong => Deviation > 0;
        }

        public class Record
        {
            public TimeSpan Start { get; set; }

            public TimeSpan End { get; set; }

            public string Defectoscope { get; set; }

            public string Operation { get; set; }

            public double Duration { get; set; }

            public double Normative { get; set; }

            public double Deviation => Duration - Normative;

            public bool TooLong => Deviation > 0;
        }
        
    }
    public class ShiftWorkAreaGroup
    {
        public DateTime Date { get; set; }

        public string Shift { get; set; }

        public string WorkArea { get; set; }
    }
}