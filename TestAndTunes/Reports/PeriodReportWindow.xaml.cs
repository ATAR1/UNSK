using Microsoft.Reporting.WinForms;
using System.Windows;

namespace TestAndTunes.Reports
{
    /// <summary>
    /// Логика взаимодействия для PeriodReportWindow.xaml
    /// </summary>
    public partial class PeriodReportWindow : Window
    {
        public PeriodReportWindow()
        {
            InitializeComponent();
            DataContext = new PeriodReportWindowViewModel(reportViewer);
        }

        private void reportViewerHost_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }
    }
}
