using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TestAndTunes.ViewModels
{
    public class MenuItemModel
    {
        private ICommand _command;        

        public MenuItemModel(string header, ICommand command)
        {
            _command = command;
            Header = header;
        }

        public ObservableCollection<MenuItemModel> MenuItems { get; }

        public MenuItemModel(string header, ObservableCollection<MenuItemModel> menuItems)
        {
            Header = header;
            MenuItems = menuItems;
        }

        public string Header { get; }

        public ICommand Command => _command;
        
    }
}