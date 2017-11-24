using System.Collections.Generic;

namespace InstaCrafter.Web.DataAccess.Repository
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