using System;
using System.Collections.Generic;
using System.Linq;

namespace TestAndTunes
{
    public class Service
    {
        private JournalDBEntities _ctx;

        public Service()
        {

        }

        public Service(JournalDBEntities _ctx)
        {
            this._ctx = _ctx;
        }

        private IEnumerable<JournalRecordViewModel> GetFrom(DateTime beginDate)
        {
            return _ctx.JournalRecords.Where(jr => jr.Date >= beginDate).ToList()
                .OrderBy(jr => new Tuple<DateTime, TimeSpan>(jr.Date, jr.Start), new ShiftedTimeComparer())
                .Select(jr => new JournalRecordViewModel(jr))
                .ToList();
        }

        private IEnumerable<JournalRecordViewModel> GetForTheShift(DateTime date, string shiftLetter)
        {
            return _ctx.JournalRecords.Where(jr => jr.Date == date && jr.Shift == shiftLetter).ToList()
                .OrderBy(jr => new Tuple<DateTime, TimeSpan>(jr.Date, jr.Start), new ShiftedTimeComparer())
                .Select(jr => new JournalRecordViewModel(jr))
                .ToList();

        }

        internal IEnumerable<WorkArea> LoadWorkAreas()
        {

            return _ctx.WorkAreas;

        }

        internal IEnumerable<Work> LoadWorks()
        {
            return _ctx.Works;

        }

        internal IEnumerable<Employee> LoadPersonal()
        {
            return _ctx.Personal;
        }

        internal IEnumerable<Shift> LoadShiftSet()
        {
            return _ctx.ShiftSet;
        }
    }
}
