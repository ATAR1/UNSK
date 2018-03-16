using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAndTunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAndTunes.DomainModel;
using TestAndTunes.DomainModel.Entities;
using System.Collections;

namespace TestAndTunes.DomainModel.Tests
{
    [TestClass()]
    public class ShiftServiceTests
    {
        private ShiftService _service;

        [TestInitialize]
        public void Init()
        {
            _service = new ShiftService(new FakeSheldue());
        }

        [TestMethod()]
        public void GetAvaliableShifts_ForDate01012017_MustReturn_VA_Rus()
        {
            var date = new DateTime(2017, 1, 1);
            var actual = _service.GetAvaliableShifts(date).First();
            Assert.AreEqual("В", actual);
            actual = _service.GetAvaliableShifts(date).Last();
            Assert.AreEqual("А", actual);
            Assert.AreEqual(2, _service.GetAvaliableShifts(date).Count());
        }

        [TestMethod]
        public void GetAvaliableShifts_ForDate22092017MustReturn_VA_Rus()
        {
            var date = new DateTime(2017, 9, 22);
            var actual = _service.GetAvaliableShifts(date).First();
            Assert.AreEqual("В", actual);
            actual = _service.GetAvaliableShifts(date).Last();
            Assert.AreEqual("А", actual);
            Assert.AreEqual(2, _service.GetAvaliableShifts(date).Count());
        }
    }

    internal class FakeSheldue : IEnumerable<SheldueRecord>
    {
        public IEnumerator<SheldueRecord> GetEnumerator()
        {
            yield return new SheldueRecord() { Group = 0, Shift = new Shift { Value = "А" } };
            yield return new SheldueRecord() { Group = 0, Shift = new Shift { Value = "Б" } };
            yield return new SheldueRecord() { Group = 1, Shift = new Shift { Value = "В" } };
            yield return new SheldueRecord() { Group = 1, Shift = new Shift { Value = "А" } };
            yield return new SheldueRecord() { Group = 2, Shift = new Shift { Value = "Г" } };
            yield return new SheldueRecord() { Group = 2, Shift = new Shift { Value = "В" } };
            yield return new SheldueRecord() { Group = 3, Shift = new Shift { Value = "Б" } };
            yield return new SheldueRecord() { Group = 3, Shift = new Shift { Value = "Г" } };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}