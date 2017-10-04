using System.Collections.Specialized;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
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
