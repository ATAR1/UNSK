using System;

namespace TestAndTunes.DomainModel.Entities
{
    public class TotalsTable
    {
        public string Caption { get; set; } = "test";

        public TotalsLine Tunes { get; set; } = new TotalsLine();

        public TotalsLine Tests { get; set; } = new TotalsLine();

        public TotalsLine Repair { get; set; } = new TotalsLine();

        public TotalsLine Totals { get; set; } = new TotalsLine();

    }

}