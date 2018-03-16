using System;
using System.Collections.Generic;
using System.Linq;
using TestAndTunes.DAL;
using TestAndTunes.DomainModel.Entities;

namespace TestAndTunes.DomainModel
{
    /// <summary>
    /// Содержит операции для работы со сменами
    /// </summary>
    public class ShiftService
    {
        private readonly IEnumerable<SheldueRecord> _sheldue;

        private readonly DateTime _startDate = new DateTime(2017, 1, 12);

        public ShiftService(IEnumerable<SheldueRecord> sheldue)
        {
            _sheldue = sheldue; 
        }
        

        /// <summary>
        /// Вычисляет доступные смены для даты
        /// </summary>
        /// <param name="date">Дата</param>
        /// <returns>Доступные смены(литеры)</returns>
        public IEnumerable<string> GetAvaliableShifts(DateTime date)
        {
            int positionInSheldue = GetPositionInSheldue(date);
            return _sheldue.Where(sr => sr.Group == positionInSheldue).Select(sr => sr.Shift.Value);
        }

        private int GetPositionInSheldue(DateTime date)
        {
            int difference = (int)(date - _startDate).TotalDays;
            var positionInSheldue = (difference % 4 + 4) % 4;
            return positionInSheldue;
        }

        /// <summary>
        /// Вычисляет доступные смены для даты
        /// </summary>
        /// <param name="date">Дата</param>
        /// <returns>Доступные смены</returns>
        public IEnumerable<Shift> GetAvaliableShifts1(DateTime date)
        {
            int positionInSheldue = GetPositionInSheldue(date);
            return _sheldue.Where(sr => sr.Group == positionInSheldue).Select(sr => sr.Shift); 
        }
    }
}