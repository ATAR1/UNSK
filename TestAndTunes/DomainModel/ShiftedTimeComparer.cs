using System;
using System.Collections.Generic;

namespace TestAndTunes.DomainModel
{
    public class ShiftedTimeComparer : IComparer<Tuple<DateTime, TimeSpan>>
    {
        public int Compare(Tuple<DateTime, TimeSpan> x, Tuple<DateTime, TimeSpan> y)
        {
            return ToShiftDateTimeByEightHours(x).CompareTo(ToShiftDateTimeByEightHours(y));
        }

        private DateTime ToShiftDateTimeByEightHours(Tuple<DateTime,TimeSpan> tuple)
        {
            DateTime date = tuple.Item1;
            TimeSpan start = tuple.Item2;
            
            var dateTime = start - TimeSpan.FromHours(8);
            return dateTime >= TimeSpan.Zero ? date + dateTime : date + dateTime + TimeSpan.FromHours(24);
            
        }
    }
}