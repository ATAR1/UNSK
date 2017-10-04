using System;
using System.Collections.Generic;
using System.Linq;

namespace TestAndTunes
{
    public class Service
    {
        private JournalDBEntities _ctx;
        
        public Service(JournalDBEntities _ctx)
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
    }
}
