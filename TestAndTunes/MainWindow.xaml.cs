using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace TestAndTunes
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Language = System.Windows.Markup.XmlLanguage.GetLanguage(CultureInfo.GetCultureInfo("ru-RU").IetfLanguageTag);
            this.DataContext = new ViewModel();
        }



        private void MaskedTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            var listView = (ListView)sender;
            int count = listView.Items.Count;
            if (count > 0)
            {
                var last = listView.Items[count - 1];
                listView.ScrollIntoView(last);
            }
        }

        private void Grid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        private void ListViewColumnHeaderClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    string header = ((Binding)headerClicked.Column.DisplayMemberBinding).Path.Path as string;
                    Sort(header, direction);

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(lv.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (lv==null||lv.ItemsSource == null) return;
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(lv.ItemsSource);

            dataView.SortDescriptions.Clear();
        }
    }

    public class AutoScrollToLastItemBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            var collection = AssociatedObject.Items.SourceCollection as INotifyCollectionChanged;
            if (collection != null)
                collection.CollectionChanged += collection_CollectionChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            var collection = AssociatedObject.Items.SourceCollection as INotifyCollectionChanged;
            if (collection != null)
                collection.CollectionChanged -= collection_CollectionChanged;
        }

        private void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ScrollToLastItem();
            }
        }

        private void ScrollToLastItem()
        {
            int count = AssociatedObject.Items.Count;
            if (count > 0)
            {
                var last = AssociatedObject.Items[count - 1];
                AssociatedObject.ScrollIntoView(last);
            }
        }
    }
}
