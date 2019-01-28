using System;
using System.Collections.Generic;
using System.Linq;
using TestAndTunes.DAL;
using TestAndTunes.DAL.JournalRecordsSelectionCriterias;
using TestAndTunes.DomainModel;
using TestAndTunes.DomainModel.Entities;
using TestAndTunes.Entities;
using TestAndTunes.Reports.Models;

namespace TestAndTunes
{
    public class ReportService
    {
        private JournalDBEntities _ctx = new JournalDBEntities();

        public TotalsTable GenerateTotals(DateTime date, string shift, string workArea)
        {
            var journalRecords = _ctx.JournalRecords.Where(jr => jr.Date == date && jr.Shift.Value == shift && jr.WorkArea == workArea&& jr.Operation.Work.OperationGroup != "Сопутствующие").ToList();
            TotalsTable totals = GenerateTotals(journalRecords);
            totals.Caption = workArea;
            return totals;
        }

        public TotalsTable GenerateTotals(DateTime dateBegin, DateTime dateEnd, string workArea)
        {
            var journalRecords = _ctx.JournalRecords.Where(jr => jr.Date >= dateBegin && jr.Date < dateEnd && jr.WorkArea == workArea&& jr.Operation.Work.OperationGroup != "Сопутствующие").ToList();
            TotalsTable totals = GenerateTotals(journalRecords);
            totals.Caption = workArea;
            return totals;
        }

        public IEnumerable<string> GetWorkAreas()
        {
            return _ctx.WorkAreas.Select(wa => wa.Name);
        }

        public ICollection<TestAndTunesReportRecord> GetTunes(DateTime beginDate, DateTime endDate)
        {
            var list = _ctx.JournalRecords
                .Where(jr => jr.Date >= beginDate && jr.Date < endDate && jr.Operation.Work.OperationGroup == "Настройка").ToList();
            List<TestAndTunesReportRecord> result = GetSummaryAndAverage(list);
            return result;
        }

        private List<TestAndTunesReportRecord> GetSummaryAndAverage(List<JournalRecord> list)
        {
            var result = list
                            .GroupBy(jr => new { jr.WorkArea, jr.DefectoscopeName })
                            .Select(g => new TestAndTunesReportRecord
                            {
                                Defectoscope = g.Key.DefectoscopeName,
                                Duration = Math.Round(g.Sum(jr => jr.Duration.TotalHours), 2),
                                LineHeader = "Общее время",
                                WorkArea = g.Key.WorkArea

                            }).ToList();
            result.AddRange(
                list
                .GroupBy(jr => new { jr.WorkArea, jr.DefectoscopeName })
                .Select(g => new TestAndTunesReportRecord
                {
                    Defectoscope = g.Key.DefectoscopeName,
                    Duration = Math.Round(g.Average(jr => jr.Duration.TotalHours), 2),
                    LineHeader = "Среднее время",
                    WorkArea = g.Key.WorkArea

                }).ToList()
                );
            return result;
        }


        public IEnumerable<JournalRecord> GetForTheShift(DateTime date, string shiftLetter)
        {
            return _ctx.JournalRecords.Where(jr => jr.Date == date && jr.Shift.Value == shiftLetter).ToList()
                .OrderBy(jr => jr, new JournalRecordsBeginTimeComparer())
                .ToList();
        }

        public ICollection<TestAndTunesReportRecord> GetTests(DateTime beginDate, DateTime endDate)
        {
            var list = _ctx.JournalRecords
                .Where(jr => jr.Date >= beginDate && jr.Date < endDate && jr.Operation.Work.OperationGroup == "Проверка").ToList();
            List<TestAndTunesReportRecord> result = GetSummaryAndAverage(list);
            return result;
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
            return new JournalRepository(_ctx).GetRecordsForPeriod(date, dateEnd)
                .ToList()
                .GroupBy(jr => new { jr.Date, jr.Shift, jr.WorkArea, jr.Operation.Work.OperationGroup })
                .Select(g => new ShiftsReportRecord
                {

                    Date = g.Key.Date,
                    WorkArea = g.Key.WorkArea,
                    RecordHeader = g.Key.OperationGroup,
                    Shift = g.Key.Shift.Value,
                    Quantity = g.Count(),
                    Duration = g.Sum(jr => Math.Round(jr.Duration.TotalHours, 2)),
                    Normative = g.Sum(jr => Math.Round(jr.Normative.TotalHours, 2)),
                    Deviation = Math.Round(g.Sum(jr => Math.Round(jr.Duration.TotalHours, 2)) - g.Sum(jr => Math.Round(jr.Normative.TotalHours, 2)), 2),

                }
                ).ToList();
        }

        public ICollection<MonthShiftReportRecord> GetMonthShiftsReport(DateTime date, DateTime dateEnd,SetOfCriteriaForSelectingJournalRecords criterias)
        {
            var journalRecords = new JournalRepository(_ctx).GetRecordsForPeriod(date, dateEnd);
            journalRecords = criterias.ApplyAllCriterias(journalRecords);  
            return journalRecords
                .GroupBy(jr => new { jr.Shift, jr.WorkArea, jr.Operation.Work.OperationGroup })
                .Select(g => new MonthShiftReportRecord
                {
                    RecordHeader = g.Key.OperationGroup,
                    Month = g.First().Date.ToString("MMMM"),
                    Year = g.First().Date.ToString("yyyy"),
                    Shift = g.Key.Shift.Value,
                    WorkArea = g.Key.WorkArea,
                    Quantity = g.Count(),
                    Duration = g.Sum(jr => Math.Round(jr.Duration.TotalHours, 2)),
                    Normative = g.Sum(jr => Math.Round(jr.Normative.TotalHours, 2)),
                    Deviation = Math.Round(g.Sum(jr => Math.Round(jr.Duration.TotalHours, 2))- g.Sum(jr => Math.Round(jr.Normative.TotalHours, 2)),2),
                }).ToList();
        }
    }
}
