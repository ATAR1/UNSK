using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TestAndTunes.DomainModel.Entities;
using TestAndTunes.Entities;

namespace TestAndTunes.DAL
{
    internal class JournalRepository: IJournalRepository
    {
        private JournalDBEntities _ctx;

        public JournalRepository(JournalDBEntities ctx)
        {
            this._ctx = ctx;
        }
        
        public List<JournalRecord> GetRecordsByDateAndShift(DateTime date, string letter)
        {
            return _ctx.JournalRecords.Where(jr=>jr.Date== date&&jr.Shift.Value==letter).ToList();
        }

        public List<JournalRecord> GetRecordsStartFrom(DateTime fromTheDate)
        {
            return _ctx.JournalRecords.Where(jr => jr.Date >=fromTheDate).ToList();
        }

        public ICollection<JournalRecord> GetRecordsForPeriod(DateTime beginDate, DateTime endDate)
        {
            return _ctx.JournalRecords.Where(jr => jr.Date >= beginDate && jr.Date < endDate).ToList();
        }
    }
}