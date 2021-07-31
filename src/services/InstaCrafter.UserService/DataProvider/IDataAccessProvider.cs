using System.Collections.Generic;

namespace InstaCrafter.UserService.DataProvider
{
    public interface IDataAccessProvider<T>
    {
        void Add(T item);
        void Update(long userId, T item);
        void Delete(long userId);
        T Get(long userId);
        T Get(string userName);
        List<T> GetItems();

        bool Exist(T item);

    }
}