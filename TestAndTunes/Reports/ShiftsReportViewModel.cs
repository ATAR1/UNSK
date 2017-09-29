using System.Collections.Generic;

namespace TestAndTunes.Reports
{
    public class ShiftsReportViewModel:IReport
    {
        public ICollection<ShiftsReportRecord> ReportRecords { get; set; }

    }


}