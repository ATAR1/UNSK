﻿using System;
using System.Collections.Generic;

namespace TestAndTunes.DomainModel
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
        /// <param name="date">Дата</param>
        /// <returns>Доступные смены</returns>
        public IEnumerable<string> GetAvaliableShifts(DateTime date)
        {
            int difference = (int)(date - _startDate).TotalDays;
            var positionInSheldue = (difference % 4 + 4) % 4;
            yield return _sheldue[positionInSheldue, 0];
            yield return _sheldue[positionInSheldue, 1];
        }
    }
}