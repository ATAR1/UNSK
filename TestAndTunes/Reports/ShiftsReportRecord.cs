using System;

namespace TestAndTunes.Reports
{
    public class ShiftsReportRecord
    {
        public DateTime Date { get; set; }
        public string WorkArea { get; set; }

        public string Shift { get; set; }

        public string RecordHeader { get; set; }

        public double Deviation { get; internal set; }

        public double Duration { get; internal set; }

        public double Normative { get; internal set; }

        public int Quantity { get; internal set; }        
    }
}
