using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TestAndTunes.Reports
{
    public class ShowWindowCommand : ICommand
    {
        private readonly Window _window;

        public event EventHandler CanExecuteChanged;

        public ShowWindowCommand(Window window)
        {
            _window = window;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            _window.ShowDialog();
        }
    }
}
