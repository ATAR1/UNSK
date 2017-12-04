using System.Collections.Generic;
using TestAndTunes.DomainModel.Entities;

namespace TestAndTunes.DAL.JournalRecordsSelectionCriterias
{
    public class SetOfCriteriaForSelectingJournalRecords
    {
        private List<JournalRecordSelectionCriteria> _list = new List<JournalRecordSelectionCriteria>();
        public void AddCriteria(JournalRecordSelectionCriteria criteria)
        {
            _list.Add(criteria);
        }

        public IReadOnlyCollection<JournalRecordSelectionCriteria> Members => _list;

        public ICollection<JournalRecord> ApplyAllCriterias(ICollection<JournalRecord> incomingCollection)
        {
            var collection = incomingCollection;

            foreach (var criteria in _list)
            {
                collection = criteria.Apply(collection);
            }

            return collection;
        }
    }
}
