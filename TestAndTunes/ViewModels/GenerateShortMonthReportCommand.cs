using System;
using System.Windows.Input;
using Microsoft.Reporting.WinForms;
using TestAndTunes.Reports;

namespace TestAndTunes.ViewModels
{
    internal class GenerateShortMonthReportCommand : GenerateMonthReportCommand
    {
        private MonthReportWindowModel monthReportWindowModel;

        public GenerateShortMonthReportCommand(MonthReportWindowModel monthReportWindowModel):base(monthReportWindowModel)
        {
            this.monthReportWindowModel = monthReportWindowModel;
        }

        protected override IReportViewModel GenerateReport(DateTime beginDate, DateTime dateEnd)
        {
            return new ShortMonthReportViewModel(beginDate, dateEnd);
        }
    }

    internal class ShortMonthReportViewModel :  IReportViewModel
    {
        private DateTime beginDate;
        private DateTime dateEnd;

        public ShortMonthReportViewModel(DateTime beginDate, DateTime dateEnd)
        {
            this.beginDate = beginDate;
            this.dateEnd = dateEnd;
        }

        public Action<LocalReport> SetReportParameters => null;

        public SubreportProcessingEventHandler SubreportProcessing => null;

        string IReportViewModel.ReportEmbeddedResource => "TestAndTunes.Reports.Layouts.MonthReportShort.rdlc";
                
        public  void FillDataSources(ReportDataSourceCollection dataSources)
        {
            dataSources.Add(new ReportDataSource("ShortData"));
            dataSources["ShortData"].Value = new ReportService().GetShortMonthReport(beginDate,dateEnd);
        }
    }
}