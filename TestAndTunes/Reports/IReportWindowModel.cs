using System.ComponentModel;
using System.Windows.Input;
using TestAndTunes.Reports;

namespace TestAndTunes
{
    public interface IReportWindowModel:INotifyPropertyChanged
    {
        ICommand RefreshCommand { get; }

        IReportViewModel Report { get; }

        //void RefreshReport();
    }
}