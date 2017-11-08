using System;
using System.Windows.Input;
using TestAndTunes.Reports;

namespace TestAndTunes.Reports
{
    internal class GenerateShiftsReportCommand : GenerateMonthReportCommand
    {
        public GenerateShiftsReportCommand(MonthReportWindowModel monthReportWindowModel) : base(monthReportWindowModel)
        {
        }

        protected override IReportViewModel GenerateReport(DateTime beginDate, DateTime endDate)
        {
            return new ShiftsReportViewModel(beginDate, endDate);
        }
    }
}