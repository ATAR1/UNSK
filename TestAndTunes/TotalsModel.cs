using System;
using System.ComponentModel;

namespace TestAndTunes
{
    public class TotalsTable
    {
        public string Caption { get; set; } = "test";

        public TotalsLine Tunes { get; set; } = new TotalsLine();

        public TotalsLine Tests { get; set; } = new TotalsLine();

        public TotalsLine Repair { get; set; } = new TotalsLine();

        public TotalsLine Totals { get; set; } = new TotalsLine();

    }

    public class TotalsLine
    {
        public int Quantity { get; set; }

        public TimeSpan Duration { get; set; }

        public TimeSpan Normative { get; set; }

        public TimeSpan Deviation { get; set; }

        public bool TooLong => Duration > Normative;

    }
}