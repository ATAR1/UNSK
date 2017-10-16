using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TestAndTunes.DomainModel.Entities;

namespace TestAndTunes.DAL
{
    internal class JournalRepository: IJournalRepository
    {
        private JournalDBEntities _ctx;

        public JournalRepository(JournalDBEntities ctx)
        {
            this._ctx = ctx;
        }

        public void Add(JournalRecord newRecord)
        {
            _ctx.JournalRecords.Add(newRecord);
        }

        public void CancellChanges(JournalRecord model)
        {
            var entry = _ctx.Entry(model);
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.CurrentValues.SetValues(entry.OriginalValues);
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
            }
        }

        public List<JournalRecord> GetRecordsByDateAndShift(DateTime date, string letter)
        {
            return _ctx.JournalRecords.Where(jr=>jr.Date== date&&jr.Shift==letter).ToList();
        }

        public List<JournalRecord> GetRecordsStartFrom(DateTime fromTheDate)
        {
            return _ctx.JournalRecords.Where(jr => jr.Date >=fromTheDate).ToList();
        }

        public void Remove(JournalRecord model)
        {
            _ctx.JournalRecords.Remove(model);
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }
    }
}