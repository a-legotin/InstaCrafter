using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaCrafter.Models;

namespace InstaCrafter
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
