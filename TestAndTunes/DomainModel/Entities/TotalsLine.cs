using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAndTunes.DomainModel.Entities
{

    public class TotalsLine
    {
        public int Quantity { get; set; }

        public TimeSpan Duration { get; set; }

        public TimeSpan Normative { get; set; }

        public TimeSpan Deviation { get; set; }

        public bool TooLong => Duration > Normative;

    }
}
