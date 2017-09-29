using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAndTunes.Reports
{
    public class MonthShiftReportRecord
    {
        public string Month { get; set; }

        public string Year { get; set; }

        public string Shift { get; set; }

        public string WorkArea { get; set; }

        public string RecordHeader { get; set; }

        public double Deviation { get; internal set; }

        public double Duration { get; internal set; }

        public double Normative { get; internal set; }

        public int Quantity { get; internal set; }
    }
}
