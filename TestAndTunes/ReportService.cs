using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            totals.Tests = CreateTotalsLine(journalRecords.Where(jr => jr.Operation.Work.OperationGroup == "Проверка чувствительности"));
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

        
    }
}
