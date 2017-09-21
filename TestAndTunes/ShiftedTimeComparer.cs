using System;
using System.Collections.Generic;

namespace TestAndTunes
{
    internal class ShiftedTimeComparer : IComparer<Tuple<DateTime, TimeSpan>>
    {
        public int Compare(Tuple<DateTime, TimeSpan> x, Tuple<DateTime, TimeSpan> y)
        {
            return GetStartDateTimeForSort(x).CompareTo(GetStartDateTimeForSort(y));
        }

        private DateTime GetStartDateTimeForSort(Tuple<DateTime,TimeSpan> tuple)
        {
            DateTime date = tuple.Item1;
            TimeSpan start = tuple.Item2;
            
            var dateTime = start - TimeSpan.FromHours(8);
            return dateTime >= TimeSpan.FromHours(0) ? date + dateTime : date + dateTime + TimeSpan.FromHours(24);
            
        }
    }
}