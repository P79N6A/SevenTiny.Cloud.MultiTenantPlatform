using SevenTiny.Cloud.MultiTenantPlatform.Domain.Entity;
using SevenTiny.Cloud.MultiTenantPlatform.Domain.Repository;
using System.Collections.Generic;

namespace SevenTiny.Cloud.MultiTenantPlatform.Domain.ServiceContract
{
    public interface IMetaObjectService : IRepository<MetaObject>
    {
        List<MetaObject> GetMetaObjectsUnDeletedByApplicationId(int applicationId);
    }
}
