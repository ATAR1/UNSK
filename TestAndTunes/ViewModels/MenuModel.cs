using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TestAndTunes.DAL;
using TestAndTunes.Reports;
using TestAndTunes.Routines;

namespace TestAndTunes.ViewModels
{
    public class MenuModel
    {   
        public ObservableCollection<MenuItemModel> MenuItems { get; } = new ObservableCollection<MenuItemModel>();

        public static MenuModel Generate(CollectionsRepository collectionsRepository)
        {
            var menu = new MenuModel();

            var menuItems = new ObservableCollection<MenuItemModel>();
            menu.MenuItems.Add(new MenuItemContainerModel("Отчёты", menuItems));
            menuItems.Add(new MenuItemWithCommandModel("Отчёт за смену", new ShowShiftReportCommand(collectionsRepository.LoadSheldue())));
            menuItems.Add(new MenuItemWithCommandModel("Время настроек и проверок", new ShowTestAndTunesReport()));
            menuItems.Add(new MenuItemWithCommandModel("Отчёт за месяц", new ShowMonthShiftReportCommand()));
            menuItems.Add(new MenuItemWithCommandModel("Отчёт за месяц краткий", new ShowShortMonthCommand()));
            menuItems.Add(new MenuItemWithCommandModel("Посменный отчёт за месяц(подробный)", new ShowShiftsReportCommand()));
            menuItems.Add(new MenuItemWithCommandModel("Посменный отчёт за месяц", new ShowMonthShiftReportCommand()));
            var scndLvlMnu = new ObservableCollection<MenuItemModel>();
            menuItems.Add(new MenuItemContainerModel("Отчёты за период", scndLvlMnu));
            scndLvlMnu.Add(new MenuItemWithCommandModel("Суммарный отчёт за период", new ShowSummaryForPeriodReportCommand()));

            return menu;
        }
    }
}