using System.Collections.Generic;

namespace SevenTiny.Cloud.MultiTenantPlatform.Domain.Repository
{
    public interface IRepository<T> where T : class
    {
        List<T> GetEntitiesDeleted();
        List<T> GetEntitiesUnDeleted();
        void Add(T t);
        T Get(int id);
        void Update(T t);
        void LogicDelete(int id);
        void Recover(int id);
    }
}
