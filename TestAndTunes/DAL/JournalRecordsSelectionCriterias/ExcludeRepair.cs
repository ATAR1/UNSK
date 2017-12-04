using System.Collections.Generic;
using System.Linq;
using TestAndTunes.DomainModel.Entities;

namespace TestAndTunes.DAL.JournalRecordsSelectionCriterias
{
    public class ExcludeRepair : JournalRecordSelectionCriteria
    {
        public override string Description => "Исключить время устранения неисправностей";

        protected override ICollection<JournalRecord> ApplyCore(ICollection<JournalRecord> incomingCollection)
        {
            return incomingCollection.Where(jr => jr.OperationGroup != "Неисправность").ToList();
        }
    }
}
