﻿using System;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;

namespace TestAndTunes.Reports
{
    public interface IReport
    {
        string ReportEmbeddedResource { get; }
        
        void FillDataSources(ReportDataSourceCollection dataSources);

        void Load(DateTime beginDate, DateTime endDate);
    }
}