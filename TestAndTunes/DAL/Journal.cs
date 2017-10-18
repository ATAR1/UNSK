using System.Data.Entity;
using TestAndTunes.DomainModel.Entities;

namespace TestAndTunes.DAL
{
    public class Journal : IJournal
    {
        private JournalDBEntities _ctx;

        public Journal(JournalDBEntities ctx)
        {
            _ctx = ctx;
        }

        public void Add(JournalRecord newRecord)
        {
            _ctx.JournalRecords.Add(newRecord);
        }


        public void Remove(JournalRecord model)
        {
            _ctx.JournalRecords.Remove(model);
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }


        public void CancellChanges(JournalRecord model)
        {
            var entry = _ctx.Entry(model);
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.CurrentValues.SetValues(entry.OriginalValues);
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
            }
        }
    }
}
