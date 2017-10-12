using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAndTunes.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAndTunes.DomainModel.Tests
{
    [TestClass()]
    public class ShiftedTimeComparerTests
    {
        [TestMethod()]
        public void CompareTest()
        {
            var comparer = new ShiftedTimeComparer();
            var date = new DateTime().AddDays(1);
            var time1 = TimeSpan.FromHours(1);
            var time2 = TimeSpan.FromHours(10);
            var actual =comparer.Compare(new Tuple<DateTime, TimeSpan>(date, time1), new Tuple<DateTime, TimeSpan>(date, time2));
            Assert.IsTrue(actual > 0);
        }

        [TestMethod()]
        public void CompareTest2()
        {
            var comparer = new ShiftedTimeComparer();
            var date = new DateTime().AddDays(1);
            var time1 = TimeSpan.FromHours(0);
            var time2 = TimeSpan.FromHours(2);
            var actual = comparer.Compare(new Tuple<DateTime, TimeSpan>(date, time1), new Tuple<DateTime, TimeSpan>(date, time2));
            Assert.IsTrue(actual < 0);
        }
    }
}