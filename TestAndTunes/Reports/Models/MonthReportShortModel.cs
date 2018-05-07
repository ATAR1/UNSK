using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAndTunes.Reports.Models
{
    public class MonthReportShortModel
    {
        public DateTime StartDate { get; set; }
        public char ShiftLetter { get; set; }
        public int Quantity { get; set; }
        public double DurationInHour { get; set; }
        public double NormativeInHour { get; set; }
        public double Deviation { get; set; }
        public double DeviationInPercent { get; set; }

    }
}
