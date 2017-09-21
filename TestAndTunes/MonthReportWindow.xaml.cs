using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TestAndTunes
{
    /// <summary>
    /// Логика взаимодействия для MonthReportWindow.xaml
    /// </summary>
    public partial class MonthReportWindow : Window
    {
        public MonthReportWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (reportViewer.LocalReport.DataSources[0].Value != null)
            {
                reportViewer.PrintDialog();
            }
            else
            {
                MessageBox.Show("Отчёт не сформирован!","Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
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

        private void reportViewer_Load(object sender, EventArgs e)
        {
            ReportService service = new ReportService();

            

            var localReport = reportViewer.LocalReport;
            localReport.DataSources.Add(new ReportDataSource("TO1DataSet"));
            localReport.DataSources.Add(new ReportDataSource("TO2DataSet"));
            localReport.DataSources.Add(new ReportDataSource("UOGTDataSet"));
            localReport.DataSources.Add(new ReportDataSource("ReportDataSet"));
            
            localReport.ReportEmbeddedResource = "TestAndTunes.Report1.rdlc";
            localReport.Refresh();
            
        }

        private void WindowsFormsHost_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (reportViewer == null) return;
            var reportViewModel = reportViewerHost.DataContext as MonthReportViewModel;
            var localReport = reportViewer.LocalReport;
            if (reportViewModel != null&&localReport!=null)
            {
                
                localReport.DataSources["TO1DataSet"].Value = reportViewModel.SummaryPerMonth.GetReportLines();
                localReport.DataSources["TO2DataSet"].Value = reportViewModel.SummaryTO2.GetReportLines();
                localReport.DataSources["UOGTDataSet"].Value = reportViewModel.SummaryUOGT.GetReportLines();
                localReport.DataSources["ReportDataSet"].Value = reportViewModel.GetLine();
            }
            reportViewer.RefreshReport();
        }
    }
}
