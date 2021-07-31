using System.Collections.Generic;

namespace InstaCrafter.PostService.DataProvider
{
    public interface IDataAccessProvider<T>
    {
        void Add(T item);
        void Update(long postId, T item);
        void Delete(long postId);
        T Get(long postId);
        T Get(string userName);
        List<T> GetItems();

        bool Exist(T item);

    }
}