using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAndTunes.Reports
{
    public class ShiftsReportRecord
    {
        public string Date { get; set; }

        public string Shift { get; set; }

        public string WorkArea { get; set; }

        public string RecordHeader { get; set; }

        public string Deviation { get; internal set; }

        public string Duration { get; internal set; }

        public string Normative { get; internal set; }

        public int Quantity { get; internal set; }
    }
}
