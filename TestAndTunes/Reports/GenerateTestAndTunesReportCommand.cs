using System;
using System.Windows.Input;
using TestAndTunes.Reports;

namespace TestAndTunes.Reports
{
    internal class GenerateTestAndTunesReportCommand : GenerateMonthReportCommand
    {
        public GenerateTestAndTunesReportCommand(MonthReportWindowModel monthReportWindowModel) : base(monthReportWindowModel)
        {
        }

        protected override IReportViewModel GenerateReport(DateTime beginDate, DateTime endDate)
        {
            return new TestAndTunesReportViewModel(beginDate, endDate);
        }
    }
}