using System.Collections.Generic;
using System.Windows.Input;
using TestAndTunes.Reports;

namespace TestAndTunes
{
    public interface IMonthReportWindowModel
    {
        int Year { get; set; }

        string Month { get; set; }

        ICollection<string> Months { get; }

        ICommand RefreshCommand { get; }

        IReport Report { get; }

        void RefreshReport();
    }
}