using System;
using TestAndTunes.DomainModel.Entities;

namespace TestAndTunes.Views.Fakes
{
    public class TotalsTableFake
    {
        public string Caption { get; set; } = "test";

        public TotalsLine Tunes { get; set; } = new TotalsLine();

        public TotalsLine Tests { get; set; } = new TotalsLine();

        public TotalsLine Repair { get; set; } = new TotalsLine();

        public TotalsLine Totals { get; set; } = new TotalsLine();

    }

}