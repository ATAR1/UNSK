using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TestAndTunes.Reports;

namespace TestAndTunes
{
    internal class RefreshReportCommand : ICommand
    {
        IReportWindowModel _windowModel;

        public RefreshReportCommand(IReportWindowModel windowModel)
        {
            _windowModel = windowModel;
        }



        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _windowModel.RefreshReport();
        }
    }
}
