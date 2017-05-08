using System.Collections.Generic;

namespace InstaCrafter.Console.Providers
{
    public interface IDataAccessProvider<T>
    {
        void Add(T item);
        void Update(int userId, T item);
        void Delete(int userId);
        T Get(int id);
        T Get(string name);
        List<T> GetItems();

        bool Exist(T item);
    }
}