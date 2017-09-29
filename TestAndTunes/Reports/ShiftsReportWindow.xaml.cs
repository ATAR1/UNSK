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
using System.Windows.Shapes;

namespace TestAndTunes.Reports
{
    /// <summary>
    /// Логика взаимодействия для ShiftsReportWindow.xaml
    /// </summary>
    public partial class ShiftsReportWindow : Window
    {
        private ReportType _reportType;

        public ShiftsReportWindow(ReportType reportType)
        {
            InitializeComponent();
            DataContext = new ShiftsReportWindowModel(reportType);
            _reportType = reportType;
        }

        private void reportViewer_Load(object sender, EventArgs e)
        {
            
            reportViewer.LocalReport.Refresh();
        }

        private void reportViewerHost_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_reportType == ReportType.Type1)
            {
                reportViewer.LocalReport.ReportEmbeddedResource = "TestAndTunes.Reports.ShiftsReport.rdlc";
                reportViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("ShiftReportDataSet"));
                if (reportViewer == null) return;
                var reportViewModel = reportViewerHost.DataContext as ShiftsReportViewModel;
                var localReport = reportViewer.LocalReport;
                if (reportViewModel != null && localReport != null)
                {
                    localReport.DataSources["ShiftReportDataSet"].Value = ((ShiftsReportViewModel)e.NewValue).ReportRecords;
                }
            }
            else
            {
                reportViewer.LocalReport.ReportEmbeddedResource = "TestAndTunes.Reports.MonthShiftReport.rdlc";
                reportViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1"));
                if (reportViewer == null) return;
                var reportViewModel = reportViewerHost.DataContext as MonthShiftReportViewModel;
                var localReport = reportViewer.LocalReport;
                if (reportViewModel != null && localReport != null)
                {
                    localReport.DataSources["DataSet1"].Value = ((MonthShiftReportViewModel)e.NewValue).ReportRecords;
                }
            }
            reportViewer.RefreshReport();
        }
    }
}
