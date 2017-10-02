using System;

namespace TestAndTunes.Reports
{
    public enum ReportType { DailyMonth, ShiftMonth, Month }
    public class ReportFactory
    {
        public static IReportViewModel CreateReportModel(ReportType _reportType)
        {
            switch (_reportType)
            {
                case ReportType.DailyMonth:
                    return new ShiftsReportViewModel();
                case ReportType.ShiftMonth:
                    return new MonthShiftReportViewModel();
                case ReportType.Month:
                    return new MonthReportViewModel();                
                default: return null;
            }
        }
    }
}