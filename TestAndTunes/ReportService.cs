using System;
using System.Collections.Generic;
using System.Linq;
using TestAndTunes.Reports;

namespace TestAndTunes
{
    public class ReportService
    {
        private JournalDBEntities _ctx = new JournalDBEntities();

        public TotalsTable GenerateTotals(DateTime date,string shift,string workArea)
        {
            var journalRecords = _ctx.JournalRecords.Where(jr => jr.Date == date && jr.Shift == shift&&jr.WorkArea== workArea).ToList();
            TotalsTable totals = GenerateTotals(journalRecords);
            totals.Caption = workArea;
            return totals;
        }

        public TotalsTable GenerateTotals(DateTime dateBegin, DateTime dateEnd, string workArea)
        {
            var journalRecords = _ctx.JournalRecords.Where(jr => jr.Date >= dateBegin && jr.Date <dateEnd && jr.WorkArea == workArea).ToList();
            TotalsTable totals = GenerateTotals(journalRecords);
            totals.Caption = workArea;
            return totals;
        }

        private TotalsTable GenerateTotals(IEnumerable<JournalRecord> journalRecords)
        {
            TotalsTable totals = new TotalsTable();            
            totals.Tunes = CreateTotalsLine(journalRecords.Where(jr => jr.Operation.Work.OperationGroup == "Настройка"));
            totals.Tests = CreateTotalsLine(journalRecords.Where(jr => jr.Operation.Work.OperationGroup == "Проверка"));
            totals.Repair = CreateTotalsLine(journalRecords.Where(jr => jr.Operation.Work.OperationGroup == "Неисправность"));
            totals.Totals = CreateTotalsLine(journalRecords);
            return totals;
        }

        private TotalsLine CreateTotalsLine(IEnumerable<JournalRecord> journalRecords)
        {
            var result = new TotalsLine();

            result.Quantity = journalRecords.Count();
            var duration = journalRecords.Aggregate(new TimeSpan(), (acc, jr) => acc + jr.Duration);
            result.Duration = duration;
            var normative = journalRecords.Aggregate(new TimeSpan(), (acc, jr) => acc + jr.Normative);
            result.Normative = normative;
            result.Deviation = (duration - normative);
            return result;
        }

        public ICollection<ShiftsReportRecord> GetShiftsReport(DateTime date, DateTime dateEnd)
        {
            return _ctx.JournalRecords.Where(jr => jr.Date >= date && jr.Date < dateEnd)
                .ToList()
                .GroupBy(jr => new { jr.Date, jr.Shift, jr.WorkArea, jr.Operation.Work.OperationGroup })
                .Select(g => new ShiftsReportRecord
                {

                    Date = g.Key.Date,
                    WorkArea = g.Key.WorkArea,
                    RecordHeader = g.Key.OperationGroup,
                    Shift = g.Key.Shift,
                    Quantity = g.Count(),
                    Duration = g.Sum(jr=>Math.Round(jr.Duration.TotalHours,2)),
                    Normative = g.Sum(jr => Math.Round(jr.Normative.TotalHours, 2)),
                    Deviation = g.Sum(jr => Math.Round(jr.Deviation.TotalHours, 2)),

                }
                ).ToList();
        }

        public ICollection<MonthShiftReportRecord> GetMonthShiftsReport(DateTime date, DateTime dateEnd)
        {
            return _ctx.JournalRecords.Where(jr => jr.Date >= date && jr.Date < dateEnd)
                .ToList()
                .GroupBy(jr => new { jr.Shift, jr.WorkArea, jr.Operation.Work.OperationGroup })
                .Select(g => new MonthShiftReportRecord
                {
                    RecordHeader = g.Key.OperationGroup,
                    Month = g.First().Date.ToString("MMMM"),
                    Year = g.First().Date.ToString("yyyy"),
                    Shift = g.Key.Shift,
                    WorkArea = g.Key.WorkArea,
                    Quantity = g.Count(),
                    Duration = g.Sum(jr=>Math.Round(jr.Duration.TotalHours,2)),
                    Normative = g.Sum(jr => Math.Round(jr.Normative.TotalHours, 2)),
                    Deviation = g.Sum(jr => Math.Round(jr.Deviation.TotalHours, 2)),
                }).ToList();
        }
    }
}
