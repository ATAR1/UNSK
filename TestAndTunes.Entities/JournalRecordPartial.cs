using System;
using System.Linq;

namespace TestAndTunes.Entities
{
    public partial class JournalRecord
    {
        public TimeSpan Duration
        {
            get
            {
                var end = End;
                if (End < Start)
                {
                    end = end.Add(new TimeSpan(24, 0, 0));
                }
                return end - Start;
            }
        }


        public TimeSpan Normative
        {
            get
            {
                return this.Operation.Normatives.Where(n => n.BeginDate < this.Date).DefaultIfEmpty(new Normative { BeginDate = DateTime.MinValue}).OrderBy(n => n.BeginDate).Last().Value;
            }
        }

        public TimeSpan Deviation => Duration - Normative;

        public string OperationGroup => this.Operation.Work.OperationGroup;
    }
}
