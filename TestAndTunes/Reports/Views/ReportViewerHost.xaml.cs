using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestAndTunes.Reports.Views
{
    /// <summary>
    /// Логика взаимодействия для ReportViewerHost.xaml
    /// </summary>
    public partial class ReportViewerHost : UserControl
    {
        public ReportViewerHost()
        {
            InitializeComponent();
        }

        private void reportViewerHost_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (reportViewer == null) return;
            var localReport = reportViewer.LocalReport;
            IReportViewModel reportViewModel = (IReportViewModel)e.NewValue;
            if (reportViewModel != null && localReport != null)
            {
                localReport.ReportEmbeddedResource = reportViewModel.ReportEmbeddedResource;
                reportViewModel.FillDataSources(localReport.DataSources);
                reportViewModel.SetReportParameters(localReport);
                if (reportViewModel.SubreportProcessing != null)
                {
                    reportViewer.LocalReport.SubreportProcessing += reportViewModel.SubreportProcessing;
                }
            }
            reportViewer.RefreshReport();
        }
    }
}
