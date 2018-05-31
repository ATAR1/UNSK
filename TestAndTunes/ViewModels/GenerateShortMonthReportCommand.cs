using System;
using TestAndTunes.Reports;

namespace TestAndTunes.ViewModels
{
    internal class GenerateShortMonthReportCommand : GenerateMonthReportCommand
    {
        public GenerateShortMonthReportCommand(MonthReportWindowModel monthReportWindowModel) : base(monthReportWindowModel)
        {
        }

        protected override IReportViewModel GenerateReport(DateTime beginDate, DateTime dateEnd)
        {
            return new ShortMonthReportViewModel(beginDate, dateEnd);
        }
    }
}