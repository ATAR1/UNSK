using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace TestAndTunes.Reports
{
    public class GenerateMonthReportCommand : ICommand
    {
        private MonthReportWindowModel monthReportWindowModel;

        public GenerateMonthReportCommand(MonthReportWindowModel monthReportWindowModel)
        {
            this.monthReportWindowModel = monthReportWindowModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DateTime beginDate = default(DateTime);
            if (!DateTime.TryParse($"1 {monthReportWindowModel.Month} {monthReportWindowModel.Year}", new CultureInfo("ru-Ru"), DateTimeStyles.None, out beginDate))
            {
                MessageBox.Show("Что-то не так с выбраным годом или месяцем!");
            }
            var dateEnd = beginDate.AddMonths(1);

            monthReportWindowModel.Report = GenerateReport(beginDate, dateEnd);
        }

        protected virtual IReportViewModel GenerateReport(DateTime beginDate, DateTime dateEnd)
        {
            return new MonthReportViewModel(beginDate, dateEnd);
        }
    }

    public class GenerateMonthShiftReportCommand : GenerateMonthReportCommand
    {
        public GenerateMonthShiftReportCommand(MonthReportWindowModel monthReportWindowModel) : base(monthReportWindowModel)
        {
        }

        protected override IReportViewModel GenerateReport(DateTime beginDate, DateTime dateEnd)
        {
            return new MonthShiftReportViewModel(beginDate, dateEnd);
        }
    }
}