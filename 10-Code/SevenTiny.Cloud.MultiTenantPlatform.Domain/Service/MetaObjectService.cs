using SevenTiny.Cloud.MultiTenantPlatform.Domain.Entity;
using SevenTiny.Cloud.MultiTenantPlatform.Domain.Enum;
using SevenTiny.Cloud.MultiTenantPlatform.Domain.ServiceContract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SevenTiny.Cloud.MultiTenantPlatform.Domain.Service
{
    public class MetaObjectService : IMetaObjectService
    {
        public MetaObjectService(MultiTenantPlatformDbContext multiTenantPlatformDbContext)
        {
            dbContext = multiTenantPlatformDbContext;
        }

        MultiTenantPlatformDbContext dbContext;

        public void Add(MetaObject t)
            => dbContext.Add(t);

        public MetaObject GetById(int id)
            => dbContext.QueryOne<MetaObject>(t => t.Id == id);

        public List<MetaObject> GetEntitiesDeleted()
            => dbContext.QueryList<MetaObject>(t => t.IsDeleted == (int)IsDeleted.Deleted);

        public List<MetaObject> GetEntitiesUnDeleted()
            => dbContext.QueryList<MetaObject>(t => t.IsDeleted == (int)IsDeleted.UnDeleted);

        public List<MetaObject> GetMetaObjectsUnDeletedByApplicationId(int applicationId)
            => dbContext.QueryList<MetaObject>(t => t.ApplicationId == applicationId && t.IsDeleted == (int)IsDeleted.UnDeleted);

        public void LogicDelete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                entity.IsDeleted = (int)IsDeleted.Deleted;
                dbContext.Update(t => t.Id == id, entity);
            }
        }

        public void Recover(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                entity.IsDeleted = (int)IsDeleted.UnDeleted;
                dbContext.Update(t => t.Id == id, entity);
            }
        }

        public void Update(MetaObject t)
        {
            throw new NotImplementedException();
        }
    }
}
