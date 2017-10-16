using System;
using System.Collections.Generic;
using TestAndTunes.DomainModel.Entities;

namespace TestAndTunes.DAL
{
    public interface IJournalRepository
    {
        void SaveChanges();
        void Remove(JournalRecord model);
        void Add(JournalRecord newRecord);
        void CancellChanges(JournalRecord model);
        List<JournalRecord> GetRecordsByDateAndShift(DateTime date, string letter);
        List<JournalRecord> GetRecordsStartFrom(DateTime fromTheDate);
    }
}