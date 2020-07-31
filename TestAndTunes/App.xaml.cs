using System.Windows;
using TestAndTunes.DAL;
using TestAndTunes.Entities;
using TestAndTunes.ViewModels;
using TestAndTunes.Views;

namespace TestAndTunes
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = new MainWindow();
            
            var ctx = new JournalDBEntities();
            var journalRepository = new JournalRepository(ctx);

            var collectionsRepository = new CollectionsRepository(ctx);
            MenuModel menu = MenuModel.Generate(collectionsRepository);

            mainWindow.DataContext = new MainWindowModel(journalRepository,collectionsRepository, menu);
            mainWindow.Show();
        }
    }
}
