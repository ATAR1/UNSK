using System;

namespace TestAndTunes
{
    public abstract class TotalsLineVM
    {
        protected TotalsLine Model;

        protected TotalsLineVM(TotalsLine model)
        {
            Model = model;
        }

        public int Quantity => Model.Quantity;

        public abstract string Duration { get;  }// => Math.Round(_model.Duration.TotalHours, 2).ToString();

        public abstract string Normative { get; }

        public abstract string Deviation { get; }

        public bool TooLong => Model.TooLong;
        
    }

    public class TotalLineForMonth : TotalsLineVM
    {
        public TotalLineForMonth(TotalsLine model):base(model)
        {

        }

        public override string Deviation
        {
            get
            {
                return Math.Round(Model.Deviation.TotalHours, 2).ToString();
            }
        }

        public override string Duration
        {
            get
            {
                return Math.Round(Model.Duration.TotalHours, 2).ToString();
            }
        }

        public override string Normative
        {
            get
            {
                return Math.Round(Model.Normative.TotalHours, 2).ToString();
            }
        }
    }


}