using System.Collections.Generic;
using TestAndTunes.DomainModel.Entities;
using TestAndTunes.Routines;

namespace TestAndTunes.DAL.JournalRecordsSelectionCriterias
{
    public abstract class JournalRecordSelectionCriteria : IOption
    {
        public abstract string Description { get; }

        public bool IsEnabled { get; set; }

        public ICollection<JournalRecord> Apply(ICollection<JournalRecord> incomingCollection)
        {
            if (IsEnabled)
            {
                return ApplyCore(incomingCollection);
            }
            else
            {
                return incomingCollection;
            }
        }

        protected abstract ICollection<JournalRecord> ApplyCore(ICollection<JournalRecord> incomingCollection);
    }
}
