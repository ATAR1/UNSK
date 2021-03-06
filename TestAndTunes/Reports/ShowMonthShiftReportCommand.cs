﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using TestAndTunes.Reports;
using TestAndTunes.Routines;

namespace TestAndTunes
{
    internal class ShowMonthShiftReportCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var reportWindow = new MonthReportWindow();
            IReadOnlyCollection<IOption> reportOptions = MonthShiftReportViewModel.Options;
            var monthReportWindowModel = new MonthReportWindowModel(reportOptions);
            monthReportWindowModel.RefreshCommand = new GenerateMonthShiftReportCommand(monthReportWindowModel);
            reportWindow.DataContext = monthReportWindowModel;
            reportWindow.ShowDialog();
        }
    }
}