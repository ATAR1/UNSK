using System;
using System.Linq;

namespace TestAndTunes.Entities
{
    public partial class JournalRecord:ICloneable
    {
        public TimeSpan Duration => EndDate - StartDate;


        public TimeSpan Normative
        {
            get
            {
                return this.Operation.Normatives.Where(n => n.BeginDate < this.Date).DefaultIfEmpty(new Normative { BeginDate = DateTime.MinValue}).OrderBy(n => n.BeginDate).Last().Value;
            }
        }

        public DateTime StartDate => Start < TimeSpan.FromHours(8) ? Date.AddDays(1) + Start : Date + Start;

        public DateTime EndDate => End <= TimeSpan.FromHours(8) ? Date.AddDays(1) + End : Date + End;

        public TimeSpan Deviation => Duration - Normative;

        public string OperationGroup => this.Operation.Work.OperationGroup;

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public JournalRecord Clone()
        {
            return (JournalRecord)this.MemberwiseClone();
        }
    }
}
