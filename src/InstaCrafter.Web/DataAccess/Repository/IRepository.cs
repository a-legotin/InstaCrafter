using System.Collections.Generic;
using InstaBackup.Models;

namespace InstaBackup.DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetByInstaId(string id);
        T GetById(long id);
        void Create(T item);
        void Update(T item);
        void Delete(long id);
        void SaveChanges();
        void CreateOrUpdate(T item);
        bool ExistByInstaId(string id);
    }
}