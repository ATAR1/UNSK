using System;
using System.Collections.Generic;
using System.Linq;
using TestAndTunes.DomainModel;
using TestAndTunes.DomainModel.Entities;
using TestAndTunes.Entities;

namespace TestAndTunes.DAL
{
    public class CollectionsRepository
    {
        private JournalDBEntities _ctx;
        
        public CollectionsRepository(JournalDBEntities _ctx)
        {
            this._ctx = _ctx;
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

        internal IEnumerable<SheldueRecord> LoadSheldue()
        {
            return _ctx.SheldueRecords;
        }
    }
}
