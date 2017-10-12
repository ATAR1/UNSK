using System;
using TestAndTunes.Model;

namespace TestAndTunes
{
    public class JournalRecordViewModel
    {
        private JournalRecord _jr;

        public JournalRecordViewModel(JournalRecord jr)
        {
            this._jr = jr;
        }

        public DateTime Date => _jr.Date;

        public string Description => _jr.Description;

        public string Employee => _jr.Employee;

        public string End => _jr.End.ToShortSignedString();

        public string Shift => _jr.Shift;

        public string Start => _jr.Start.ToShortSignedString();

        public string Work => _jr.OperationName;

        public string WorkArea => _jr.WorkArea;

        public string Defectoscope => _jr.DefectoscopeName;

        public string Duration => _jr.Duration.ToShortSignedString();

        public string Normative => _jr.Normative.ToShortSignedString();

        public string Deviation => (_jr.Duration-_jr.Normative).ToShortSignedString();

        public bool TooLong => _jr.Duration - _jr.Normative > TimeSpan.Zero;

        public JournalRecord Model => _jr;
    }
}