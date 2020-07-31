using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TestAndTunes.ViewModels
{
    internal class MenuItemContainerModel : MenuItemModel
    {
        public ObservableCollection<MenuItemModel> MenuItems { get; }
        private string v;

        public MenuItemContainerModel(string header, ObservableCollection<MenuItemModel> scndLvlMnu):base(header)
        {
            this.v = header;
            this.MenuItems = scndLvlMnu;
        }
    }
}