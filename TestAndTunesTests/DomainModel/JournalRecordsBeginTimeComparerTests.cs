using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAndTunes.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAndTunes.DomainModel.Entities;
using TestAndTunes.Entities;

namespace TestAndTunes.DomainModel.Tests
{
    [TestClass()]
    public class JournalRecordsBeginTimeComparerTests
    {
        [TestMethod()]
        public void CompareTest()
        {
            var comparer = new JournalRecordsBeginTimeComparer();
            var date = new DateTime().AddDays(1);
            var time1 = TimeSpan.FromHours(1);
            var time2 = TimeSpan.FromHours(10);
            var actual = comparer.Compare(new JournalRecord { Date = date, Start = time1}, new JournalRecord { Date = date, Start = time2 });
            Assert.IsTrue(actual > 0);
            actual = comparer.Compare(new JournalRecord { Date = date, Start = time2 }, new JournalRecord { Date = date, Start = time1 });
            Assert.IsTrue(actual < 0);
            actual = comparer.Compare(new JournalRecord { Date = date, Start = time1 }, new JournalRecord { Date = date, Start = time1 });
            Assert.IsTrue(actual == 0);
        }

        [TestMethod()]
        public void CompareTest2()
        {
            var comparer = new JournalRecordsBeginTimeComparer();
            var date = new DateTime().AddDays(1);
            var time1 = TimeSpan.FromHours(22);
            var time2 = TimeSpan.FromHours(23);
            var actual = comparer.Compare(new JournalRecord { Date = date, Start = time1 }, new JournalRecord { Date = date, Start = time2 });
            Assert.IsTrue(actual < 0);
            actual = comparer.Compare(new JournalRecord { Date = date, Start = time2 }, new JournalRecord { Date = date, Start = time1 });
            Assert.IsTrue(actual > 0);
        }

        [TestMethod()]
        public void CompareTest3()
        {
            var comparer = new JournalRecordsBeginTimeComparer();
            var today = new DateTime().AddDays(1);
            var journalRecordForTodayStartedShift = new JournalRecord { Date = today, Start = TimeSpan.FromHours(9) };
            var journalRecordForTodayStartedShiftToo = new JournalRecord { Date = today, Start = TimeSpan.FromHours(7) };
            var actual = comparer.Compare(journalRecordForTodayStartedShift, journalRecordForTodayStartedShiftToo);
            Assert.IsTrue(actual < 0);
            actual = comparer.Compare(journalRecordForTodayStartedShiftToo, journalRecordForTodayStartedShift);
            Assert.IsTrue(actual > 0);
        }
    }
}