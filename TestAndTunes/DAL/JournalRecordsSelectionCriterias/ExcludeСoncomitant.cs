using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAndTunes.DomainModel.Entities;
using TestAndTunes.Entities;

namespace TestAndTunes.DAL.JournalRecordsSelectionCriterias
{
    public class ExcludeСoncomitant : JournalRecordSelectionCriteria
    {
        public override string Description => "Исключить сопутствующие";

        protected override ICollection<JournalRecord> ApplyCore(ICollection<JournalRecord> incomingCollection)
        {
            return incomingCollection.Where(jr => jr.Operation.Work.OperationGroup != "Сопутствующие").ToList();
        }
    }
}
