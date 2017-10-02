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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private JournalDBEntities _ctx;

        public Window1(JournalDBEntities ctx)
        {
            InitializeComponent();
            _ctx = ctx;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            doc.Document.ColumnWidth = double.PositiveInfinity;
            doc.Document.PageHeight = 1056;
            doc.Document.PageWidth = 816;
            doc.Document.PagePadding = new Thickness(20);
            
        }

        private void FillTable2()
        {
            var t = _ctx.JournalRecords.Where(jr => jr.Date >= beginPicker.SelectedDate && jr.Date <= endPicker.SelectedDate).Join(_ctx.Works.Where(w=>w.OperationGroup=="Проверка"),jr=>jr.OperationName,w=>w.Name,(jr,w)=>jr).ToList();
            var total = t.GroupBy(t1 => new { t1.WorkArea, t1.DefectoscopeName }, (k, g) => new { WorkArea = k.WorkArea, Name = k.DefectoscopeName, Avg = g.Average(r => r.Duration.TotalMinutes), Sum = g.Sum(r => r.Duration.TotalMinutes) });

            if (total.Any(g => g.Name == "МДТ 6.1" && g.WorkArea == "ТО1")) table2.RowGroups[0].Rows[2].Cells[1].Blocks.Add(new Paragraph(new Run(total.Single(g => g.Name == "МДТ 6.1" && g.WorkArea == "ТО1").Avg.ToString())));
            if (total.Any(g => g.Name == "МДТ 6.1" && g.WorkArea == "ТО1")) table2.RowGroups[0].Rows[3].Cells[1].Blocks.Add(new Paragraph(new Run(total.Single(g => g.Name == "МДТ 6.1" && g.WorkArea == "ТО1").Sum.ToString())));
            if (total.Any(g => g.Name == "Сканер" && g.WorkArea == "ТО1")) table2.RowGroups[0].Rows[2].Cells[2].Blocks.Add(new Paragraph(new Run(total.Single(g => g.Name == "Сканер" && g.WorkArea == "ТО1").Avg.ToString())));
            if (total.Any(g => g.Name == "Сканер" && g.WorkArea == "ТО1")) table2.RowGroups[0].Rows[3].Cells[2].Blocks.Add(new Paragraph(new Run(total.Single(g => g.Name == "Сканер" && g.WorkArea == "ТО1").Sum.ToString())));
            if (total.Any(g => g.Name == "АУЗККТ" && g.WorkArea == "ТО1")) table2.RowGroups[0].Rows[2].Cells[3].Blocks.Add(new Paragraph(new Run(total.Single(g => g.Name == "АУЗККТ" && g.WorkArea == "ТО1").Avg.ToString())));
            if (total.Any(g => g.Name == "АУЗККТ" && g.WorkArea == "ТО1")) table2.RowGroups[0].Rows[3].Cells[3].Blocks.Add(new Paragraph(new Run(total.Single(g => g.Name == "АУЗККТ" && g.WorkArea == "ТО1").Sum.ToString())));
            if (total.Any(g => g.Name == "МДТ 6" && g.WorkArea == "ТО1")) table2.RowGroups[0].Rows[2].Cells[4].Blocks.Add(new Paragraph(new Run(total.Single(g => g.Name == "МДТ 6" && g.WorkArea == "ТО1").Avg.ToString())));
            if (total.Any(g => g.Name == "МДТ 6" && g.WorkArea == "ТО1")) table2.RowGroups[0].Rows[3].Cells[4].Blocks.Add(new Paragraph(new Run(total.Single(g => g.Name == "МДТ 6" && g.WorkArea == "ТО1").Sum.ToString())));
            if (total.Any(g => g.Name == "МДТ 6.2" && g.WorkArea == "ТО2")) table2.RowGroups[0].Rows[2].Cells[5].Blocks.Add(new Paragraph(new Run(total.Single(g => g.Name == "МДТ 6.2" && g.WorkArea == "ТО2").Avg.ToString())));
            if (total.Any(g => g.Name == "МДТ 6.2" && g.WorkArea == "ТО2")) table2.RowGroups[0].Rows[3].Cells[5].Blocks.Add(new Paragraph(new Run(total.Single(g => g.Name == "МДТ 6.2" && g.WorkArea == "ТО2").Sum.ToString())));
            if (total.Any(g => g.Name == "Сканер" && g.WorkArea == "ТО2")) table2.RowGroups[0].Rows[2].Cells[6].Blocks.Add(new Paragraph(new Run(total.Single(g => g.Name == "Сканер" && g.WorkArea == "ТО2").Avg.ToString())));
            if (total.Any(g => g.Name == "Сканер" && g.WorkArea == "ТО2")) table2.RowGroups[0].Rows[3].Cells[6].Blocks.Add(new Paragraph(new Run(total.Single(g => g.Name == "Сканер" && g.WorkArea == "ТО2").Sum.ToString())));
            if (total.Any(g => g.Name == "АУЗККТ" && g.WorkArea == "ТО2")) table2.RowGroups[0].Rows[2].Cells[7].Blocks.Add(new Paragraph(new Run(total.Single(g => g.Name == "АУЗККТ" && g.WorkArea == "ТО2").Avg.ToString())));
            if (total.Any(g => g.Name == "АУЗККТ" && g.WorkArea == "ТО2")) table2.RowGroups[0].Rows[3].Cells[7].Blocks.Add(new Paragraph(new Run(total.Single(g => g.Name == "АУЗККТ" && g.WorkArea == "ТО2").Sum.ToString())));
            if (total.Any(g => g.Name == "МДТ 5.1" && g.WorkArea == "УОГТ")) table2.RowGroups[0].Rows[2].Cells[8].Blocks.Add(new Paragraph(new Run(total.Single(g => g.Name == "МДТ 5.1" && g.WorkArea == "УОГТ").Avg.ToString())));
            if (total.Any(g => g.Name == "МДТ 5.1" && g.WorkArea == "УОГТ")) table2.RowGroups[0].Rows[3].Cells[8].Blocks.Add(new Paragraph(new Run(total.Single(g => g.Name == "МДТ 5.1" && g.WorkArea == "УОГТ").Sum.ToString())));
            if (total.Any(g => g.Name == "МДТ 5.2" && g.WorkArea == "УОГТ")) table2.RowGroups[0].Rows[2].Cells[9].Blocks.Add(new Paragraph(new Run(total.Single(g => g.Name == "МДТ 5.2" && g.WorkArea == "УОГТ").Avg.ToString())));
            if (total.Any(g => g.Name == "МДТ 5.2" && g.WorkArea == "УОГТ")) table2.RowGroups[0].Rows[3].Cells[9].Blocks.Add(new Paragraph(new Run(total.Single(g => g.Name == "МДТ 5.2" && g.WorkArea == "УОГТ").Sum.ToString())));
            table2.RowGroups[0].Rows[4].Cells[1].Blocks.Add(new Paragraph(new Run(total.Where(g => g.WorkArea == "ТО1").Sum(g => g.Sum).ToString())));
            table2.RowGroups[0].Rows[4].Cells[2].Blocks.Add(new Paragraph(new Run(total.Where(g => g.WorkArea == "ТО2").Sum(g => g.Sum).ToString())));
            table2.RowGroups[0].Rows[4].Cells[3].Blocks.Add(new Paragraph(new Run(total.Where(g => g.WorkArea == "УОГТ").Sum(g => g.Sum).ToString())));
        }

        private void FillTable()
        {
            var t = _ctx.JournalRecords.Where(jr => jr.Date >= beginPicker.SelectedDate && jr.Date <= endPicker.SelectedDate).Join(_ctx.Works.Where(w => w.OperationGroup == "Настройка"), jr => jr.OperationName, w => w.Name, (jr, w) => jr).ToList();
            var total = t.GroupBy(t1 => new { t1.WorkArea, t1.DefectoscopeName },(k,g)=>new { WorkArea = k.WorkArea, Name = k.DefectoscopeName, Avg = g.Average(r=>r.Duration.TotalMinutes), Sum = g.Sum(r => r.Duration.TotalMinutes) });

            table.RowGroups[0].Rows[2].Cells[1].Blocks.Add(new Paragraph(new Run(total.SingleOrDefault(g=>g.Name=="МДТ 6.1" && g.WorkArea=="ТО1")?      .Avg.ToString())));
            table.RowGroups[0].Rows[3].Cells[1].Blocks.Add(new Paragraph(new Run(total.SingleOrDefault(g => g.Name == "МДТ 6.1" && g.WorkArea == "ТО1")?.Sum.ToString())));
            table.RowGroups[0].Rows[2].Cells[2].Blocks.Add(new Paragraph(new Run(total.SingleOrDefault(g => g.Name == "Сканер" && g.WorkArea == "ТО1")? .Avg.ToString())));
            table.RowGroups[0].Rows[3].Cells[2].Blocks.Add(new Paragraph(new Run(total.SingleOrDefault(g => g.Name == "Сканер" && g.WorkArea == "ТО1")? .Sum.ToString())));
            table.RowGroups[0].Rows[2].Cells[3].Blocks.Add(new Paragraph(new Run(total.SingleOrDefault(g => g.Name == "АУЗККТ" && g.WorkArea == "ТО1")? .Avg.ToString())));
            table.RowGroups[0].Rows[3].Cells[3].Blocks.Add(new Paragraph(new Run(total.SingleOrDefault(g => g.Name == "АУЗККТ" && g.WorkArea == "ТО1")? .Sum.ToString())));
            table.RowGroups[0].Rows[2].Cells[4].Blocks.Add(new Paragraph(new Run(total.SingleOrDefault(g => g.Name == "МДТ 6" && g.WorkArea == "ТО1")?  .Avg.ToString())));
            table.RowGroups[0].Rows[3].Cells[4].Blocks.Add(new Paragraph(new Run(total.SingleOrDefault(g => g.Name == "МДТ 6" && g.WorkArea == "ТО1")?  .Sum.ToString())));
            table.RowGroups[0].Rows[2].Cells[5].Blocks.Add(new Paragraph(new Run(total.SingleOrDefault(g => g.Name == "МДТ 6.2" && g.WorkArea == "ТО2")?.Avg.ToString())));
            table.RowGroups[0].Rows[3].Cells[5].Blocks.Add(new Paragraph(new Run(total.SingleOrDefault(g => g.Name == "МДТ 6.2" && g.WorkArea == "ТО2")?.Sum.ToString())));
            table.RowGroups[0].Rows[2].Cells[6].Blocks.Add(new Paragraph(new Run(total.SingleOrDefault(g => g.Name == "Сканер" && g.WorkArea == "ТО2")? .Avg.ToString())));
            table.RowGroups[0].Rows[3].Cells[6].Blocks.Add(new Paragraph(new Run(total.SingleOrDefault(g => g.Name == "Сканер" && g.WorkArea == "ТО2")? .Sum.ToString())));
            table.RowGroups[0].Rows[2].Cells[7].Blocks.Add(new Paragraph(new Run(total.SingleOrDefault(g => g.Name == "АУЗККТ" && g.WorkArea == "ТО2")? .Avg.ToString())));
            table.RowGroups[0].Rows[3].Cells[7].Blocks.Add(new Paragraph(new Run(total.SingleOrDefault(g => g.Name == "АУЗККТ" && g.WorkArea == "ТО2")? .Sum.ToString())));
            table.RowGroups[0].Rows[2].Cells[8].Blocks.Add(new Paragraph(new Run(total.SingleOrDefault(g => g.Name == "МДТ 5.1" && g.WorkArea == "УОГТ")?.Avg.ToString())));
            table.RowGroups[0].Rows[3].Cells[8].Blocks.Add(new Paragraph(new Run(total.SingleOrDefault(g => g.Name == "МДТ 5.1" && g.WorkArea == "УОГТ")?.Sum.ToString())));
            table.RowGroups[0].Rows[2].Cells[9].Blocks.Add(new Paragraph(new Run(total.SingleOrDefault(g => g.Name == "МДТ 5.2" && g.WorkArea == "УОГТ")?.Avg.ToString())));
            table.RowGroups[0].Rows[3].Cells[9].Blocks.Add(new Paragraph(new Run(total.SingleOrDefault(g => g.Name == "МДТ 5.2" && g.WorkArea == "УОГТ")?.Sum.ToString())));
            table.RowGroups[0].Rows[4].Cells[1].Blocks.Add(new Paragraph(new Run(total.Where(g => g.WorkArea == "ТО1").Sum(g => g.Sum).ToString())));
            table.RowGroups[0].Rows[4].Cells[2].Blocks.Add(new Paragraph(new Run(total.Where(g => g.WorkArea == "ТО2").Sum(g => g.Sum).ToString())));
            table.RowGroups[0].Rows[4].Cells[3].Blocks.Add(new Paragraph(new Run(total.Where(g => g.WorkArea == "УОГТ").Sum(g => g.Sum).ToString())));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                DocumentPaginator pgntr = ((IDocumentPaginatorSource)doc.Document).DocumentPaginator;
                pd.PrintDocument(pgntr, "Печать отчёта");
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Report"; 
            dlg.DefaultExt = ".ftf"; 
            dlg.Filter = "Text documents (.rtf)|*.rtf";


            bool? result = dlg.ShowDialog();
            if (result == true)
            {                
                var tr = new TextRange(doc.Document.ContentStart, doc.Document.ContentEnd);
                var fileStream = new FileStream(dlg.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                tr.Save(fileStream, DataFormats.Rtf);
                fileStream.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FillTable();
            FillTable2();
            doc.Visibility = Visibility.Visible;
            header.Text = $"Отчёт за период с {beginPicker.SelectedDate.Value.ToString("d")} по {endPicker.SelectedDate.Value.ToString("d")}.";
        }
    }
}
