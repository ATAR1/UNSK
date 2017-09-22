using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAndTunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAndTunes.Tests
{
    [TestClass()]
    public class ShiftServiceTests
    {
        private ShiftService _service;

        [TestInitialize]
        public void Init()
        {
            _service = new ShiftService();
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
}