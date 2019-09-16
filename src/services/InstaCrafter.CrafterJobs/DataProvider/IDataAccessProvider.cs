using System.Collections.Generic;

namespace InstaCrafter.CrafterJobs.DataProvider
{
    public interface IDataAccessProvider<T>
    {
        void Add(T item);
        void Update(long id, T item);
        void Delete(long id);
        T Get(int id);
        List<T> GetItems();

        bool Exist(T item);

    }
}