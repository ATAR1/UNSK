using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAndTunes.DomainModel.Entities;

namespace TestAndTunes.DomainModel
{
    /// <summary>
    /// Сравнивает записи журнала по дате начала смены и времени начала операции
    /// </summary>
    public class JournalRecordsBeginTimeComparer : IComparer<JournalRecord>
    {
        public int Compare(JournalRecord x, JournalRecord y)
        {
            return ToShiftDateTimeByEightHours(x).CompareTo(ToShiftDateTimeByEightHours(y));
        }

        private DateTime ToShiftDateTimeByEightHours(JournalRecord journalRecord)
        {
            DateTime date = journalRecord.Date;
            TimeSpan start = journalRecord.Start;

            var dateTime = start - TimeSpan.FromHours(8);
            return dateTime >= TimeSpan.Zero ? date + dateTime : date + dateTime + TimeSpan.FromHours(24);

        }
    }
}
