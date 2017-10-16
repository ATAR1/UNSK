using System.ComponentModel;
using TestAndTunes.DomainModel.Entities;

namespace TestAndTunes.Views.Fakes
{
    public class SummaryFake:INotifyPropertyChanged
    {
        private TotalsTableFake _to1 = new TotalsTableFake() {Caption="ТО1" }; 
        private TotalsTableFake _to2 = new TotalsTableFake() { Caption = "ТО2" };
        private TotalsTableFake _uogt = new TotalsTableFake() { Caption = "УОГТ" };

        public TotalsTableFake TO1
        {
            get
            {
                return _to1;
            }

            
        }

        public TotalsTableFake TO2
        {
            get
            {
                return _to2;
            }
            
        }

        public TotalsTableFake UOGT
        {
            get
            {
                return _uogt;
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}