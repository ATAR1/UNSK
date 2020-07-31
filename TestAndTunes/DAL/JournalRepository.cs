using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TestAndTunes.DomainModel.Entities;
using TestAndTunes.Entities;

namespace TestAndTunes.DAL
{
    internal class JournalRepository : IJournalRepository
    {
        private JournalDBEntities _ctx;

        public JournalRepository(JournalDBEntities ctx)
        {
            this._ctx = ctx;
        }

        public List<JournalRecord> GetRecordsByDateAndShift(DateTime date, string letter)
        {
            return _ctx.JournalRecords.Where(jr => jr.Date == date && jr.Shift.Value == letter).ToList();
        }

        public List<JournalRecord> GetRecordsStartFrom(DateTime fromTheDate)
        {
            return _ctx.JournalRecords.Where(jr => jr.Date >= fromTheDate).ToList();
        }

        public ICollection<JournalRecord> GetRecordsForPeriod(DateTime beginDate, DateTime endDate)
        {
            return _ctx.JournalRecords.Where(jr => jr.Date >= beginDate && jr.Date < endDate).ToList();
        }

        public void Add(JournalRecord newRecord)
        {
            _ctx.JournalRecords.Add(newRecord);
            _ctx.SaveChanges();
        }

        public void Remove(JournalRecord model)
        {
            _ctx.JournalRecords.Remove(model);
            _ctx.SaveChanges();
        }

        public void Save(JournalRecord oldRecord, JournalRecord newRecord)
        {
            var entity = _ctx.JournalRecords.Single(jr => jr.Id == oldRecord.Id);
            entity.Date = newRecord.Date;
            entity.DefectoscopeName = newRecord.DefectoscopeName;
            entity.Description = newRecord.Description;
            entity.Employee = newRecord.Employee;
            entity.End = newRecord.End;
            entity.OperationName = newRecord.OperationName;
            entity.ShiftValue = newRecord.ShiftValue;
            entity.Start = newRecord.Start;
            entity.WorkArea = newRecord.WorkArea;
            _ctx.SaveChanges();

        }
    }
}