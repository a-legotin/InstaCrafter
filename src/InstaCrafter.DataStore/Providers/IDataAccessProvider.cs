using System.Collections.Generic;

namespace InstaCrafter.Providers
{
    public interface IDataAccessProvider<T>
    {
        void Add(T item);
        void Update(int id, T item);
        void Delete(int id);
        T Get(int id);
        T Get(string name);
        List<T> GetItems();

        bool Exist(T item);
    }
}