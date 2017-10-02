using System.Linq;
using System.Windows;

namespace TestAndTunes.Reports
{
    /// <summary>
    /// Логика взаимодействия для ShiftsReportWindow.xaml
    /// </summary>
    public partial class ShiftsReportWindow : Window
    {
        

        public ShiftsReportWindow(ReportType reportType)
        {
            InitializeComponent();
            DataContext = new ShiftsReportWindowModel(reportType);            
        }

        

        private void reportViewerHost_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (reportViewer == null) return;
            var localReport = reportViewer.LocalReport;
            IReport reportViewModel = (IReport)e.NewValue;
            if (reportViewModel != null && localReport != null)
            {
                reportViewer.LocalReport.ReportEmbeddedResource = reportViewModel.ReportEmbeddedResource;
                reportViewModel.FillDataSources(localReport.DataSources);
            }
            reportViewer.RefreshReport();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (reportViewer.LocalReport.DataSources[0].Value != null)
            {
                reportViewer.ExportDialog(reportViewer.LocalReport.ListRenderingExtensions().Single(re => re.Name == "WORDOPENXML"));
            }
            else
            {
                MessageBox.Show("Отчёт не сформирован!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (reportViewer.LocalReport.DataSources[0].Value != null)
            {
                reportViewer.PrintDialog();
            }
            else
            {
                MessageBox.Show("Отчёт не сформирован!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
