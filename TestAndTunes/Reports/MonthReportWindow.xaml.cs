using System.Linq;
using System.Windows;

namespace TestAndTunes.Reports
{
    /// <summary>
    /// Логика взаимодействия для ShiftsReportWindow.xaml
    /// </summary>
    public partial class MonthReportWindow : Window
    {
        

        public MonthReportWindow()
        {
            InitializeComponent();
                        
        }
               

        
        //private void MenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    if (reportViewer.LocalReport.DataSources[0].Value != null)
        //    {
        //        reportViewer.ExportDialog(reportViewer.LocalReport.ListRenderingExtensions().Single(re => re.Name == "WORDOPENXML"));
        //    }
        //    else
        //    {
        //        MessageBox.Show("Отчёт не сформирован!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        //{
        //    if (reportViewer.LocalReport.DataSources[0].Value != null)
        //    {
        //        reportViewer.PrintDialog();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Отчёт не сформирован!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}
    }
}
