using System;
using System.Collections.Generic;
using System.Windows.Input;
using TestAndTunes.Routines;

namespace TestAndTunes.Reports
{
    internal class ShowOptionsCommand : ICommand
    {
        private IEnumerable<IOption> _options;

        public event EventHandler CanExecuteChanged;
        

        public ShowOptionsCommand(IEnumerable<IOption> options)
        {
            this._options = options;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            OptionsWindow window = new OptionsWindow();
            window.DataContext = _options;
            window.ShowDialog();
        }
    }
}