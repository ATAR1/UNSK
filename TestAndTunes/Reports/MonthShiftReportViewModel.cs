using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAndTunes.Reports
{
    class MonthShiftReportViewModel: IReport
    {
        public ICollection<MonthShiftReportRecord> ReportRecords { get; set; }
    }
}
