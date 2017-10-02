using System.Windows.Input;
using TestAndTunes.Reports;

namespace TestAndTunes
{
    public interface IReportWindowModel
    {
        ICommand RefreshCommand { get; }

        IReportViewModel Report { get; }

        void RefreshReport();
    }
}