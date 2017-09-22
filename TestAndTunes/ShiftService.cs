using System;
using System.Collections.Generic;

namespace TestAndTunes
{
    /// <summary>
    /// Содержит операции для работы со сменами
    /// </summary>
    public class ShiftService
    {
        private readonly string[,] _sheldue = { { "А", "Б" }, { "В", "А" }, { "Г", "В" }, { "Б", "Г" } };

        private readonly DateTime _startDate = new DateTime(2017, 1, 12);


        /// <summary>
        /// Вычисляет доступные смены для даты
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IEnumerable<string> GetAvaliableShifts(DateTime date)
        {
            int different = (int)(date - _startDate).TotalDays;
            var positionInSheldue = (different % 4 + 4) % 4;
            yield return _sheldue[positionInSheldue, 0];
            yield return _sheldue[positionInSheldue, 1];
        }
    }
}