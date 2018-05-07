using System.Collections.ObjectModel;
using TestAndTunes.DAL;

namespace TestAndTunes.ViewModels
{
    public class MenuModel
    {
        public MenuModel()
        {            
            var menuItems = new ObservableCollection<MenuItemModel>();
            MenuItems.Add(new MenuItemModel("Отчёты", menuItems));
            menuItems.Add(new MenuItemModel("Отчёт за смену", new ShowShiftReportCommand(new CollectionsRepository().LoadSheldue())));
            menuItems.Add(new MenuItemModel("Время настроек и проверок", new ShowTestAndTunesReport()));
            menuItems.Add(new MenuItemModel("Отчёт за месяц", new ShowMonthShiftReportCommand()));
            menuItems.Add(new MenuItemModel("Посменный отчёт за месяц(подробный)", new ShowShiftsReportCommand()));
            menuItems.Add(new MenuItemModel("Посменный отчёт за месяц", new ShowMonthShiftReportCommand()));
            menuItems.Add(new MenuItemModel("Справка в БОТ", new ShowShortMonthReportWindowCommand()));
            var scndLvlMnu = new ObservableCollection<MenuItemModel>();
            menuItems.Add(new MenuItemModel("Отчёты за период", scndLvlMnu));
            scndLvlMnu.Add(new MenuItemModel("Суммарный отчёт за период", new ShowSummaryForPeriodReportCommand()));
        }

        public ObservableCollection<MenuItemModel> MenuItems { get; } = new ObservableCollection<MenuItemModel>();

        
    }
}