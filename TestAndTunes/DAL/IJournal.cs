using TestAndTunes.DomainModel.Entities;

namespace TestAndTunes.DAL
{
    public interface IJournal
    {
        void Add(JournalRecord newRecord);
        void Remove(JournalRecord model);
        void SaveChanges();
        void CancellChanges(JournalRecord model);
    }
}