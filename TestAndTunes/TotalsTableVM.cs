using System.Collections.Generic;

namespace TestAndTunes
{
    public abstract class TotalsTableVM
    {
        protected TotalsTable Model;

        protected TotalsTableVM(TotalsTable model)
        {
            Model = model;
        }

        public string Caption => Model.Caption;
        public abstract TotalsLineVM Tunes { get; }
        public abstract TotalsLineVM Tests { get; }
        public abstract TotalsLineVM Repair { get; }
        public abstract TotalsLineVM Totals { get; }

        public IEnumerable<ReportLine> GetReportLines()
        {
            yield return new ReportLine() { Header = "Настройки", Quantity=Tunes.Quantity, Duration=Tunes.Duration, Normative=Tunes.Normative, Deviation=Tunes.Deviation , TooLong = Tunes.TooLong };

            yield return new ReportLine() { Header = "Проверки", Deviation = Tests.Deviation, Normative = Tests.Normative, Duration = Tests.Duration, Quantity = Tests.Quantity, TooLong = Tests.TooLong };

            yield return new ReportLine() { Header = "Неисправность", Deviation = Repair.Deviation, Normative = Repair.Normative, Duration = Repair.Duration, Quantity = Repair.Quantity, TooLong = Repair.TooLong };

            yield return new ReportLine() { Header = "Всего", Deviation = Totals.Deviation, Normative = Totals.Normative, Duration = Totals.Duration, Quantity = Totals.Quantity, TooLong = Totals.TooLong };

            yield break;
        }
    }

    public class TotalsTableMonthView:TotalsTableVM
    {
        private TotalsLineVM _repair;
        private TotalsLineVM _tests;
        private TotalsLineVM _totals;
        private TotalsLineVM _tunes;

        public TotalsTableMonthView(TotalsTable model):base(model)
        {
            _repair = new TotalLineForMonth(model.Repair);
            _tests = new TotalLineForMonth(model.Tests);
            _tunes = new TotalLineForMonth(model.Tunes);
            _totals = new TotalLineForMonth(model.Totals);
        }
        
        public override TotalsLineVM Repair=> _repair;

        public override TotalsLineVM Tests => _tests;

        public override TotalsLineVM Totals => _totals;

        public override TotalsLineVM Tunes => _tunes;
    }
}