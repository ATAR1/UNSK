using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TestAndTunes.ViewModels
{
    public class MenuItemWithCommandModel:MenuItemModel
    {
        private ICommand _command;        

        public MenuItemWithCommandModel(string header, ICommand command):base(header)
        {
            _command = command;            
        }

        


        public ICommand Command => _command;
        
    }
}