﻿using System;
using System.Collections.Generic;
using TestAndTunes.DomainModel.Entities;
using TestAndTunes.Entities;

namespace TestAndTunes.DAL
{
    public interface IJournalRepository
    {
        List<JournalRecord> GetRecordsByDateAndShift(DateTime date, string letter);
        List<JournalRecord> GetRecordsStartFrom(DateTime fromTheDate);
        void Add(JournalRecord newRecord);
        void Remove(JournalRecord model);
        void Save(JournalRecord oldRecord, JournalRecord newRecord);
    }
}