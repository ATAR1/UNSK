using System.Windows;

namespace TestAndTunes.Reports
{
    /// <summary>
    /// Логика взаимодействия для ShiftReportWindow.xaml
    /// </summary>
    public partial class ShiftReportWindow : Window
    {
        private ShiftReportWindowModel _shiftReportWindowModel;

        public ShiftReportWindow()
        {
            InitializeComponent();
            _shiftReportWindowModel = new ShiftReportWindowModel();
            this.DataContext = _shiftReportWindowModel;
            

        }

        private void reportViewerHost_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (reportViewer == null) return;
            var localReport = reportViewer.LocalReport;
            IReportViewModel reportViewModel = (IReportViewModel)e.NewValue;
            if (reportViewModel != null && localReport != null)
            {
                reportViewer.LocalReport.ReportEmbeddedResource = reportViewModel.ReportEmbeddedResource;
                reportViewModel.FillDataSources(localReport.DataSources);
                reportViewer.LocalReport.SubreportProcessing += _shiftReportWindowModel.SubreportProcessing;
            }
            reportViewer.RefreshReport();
        }
    }
}
