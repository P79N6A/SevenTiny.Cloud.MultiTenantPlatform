using SevenTiny.Cloud.MultiTenantPlatform.Domain.Entity;
using SevenTiny.Cloud.MultiTenantPlatform.Domain.Enum;
using SevenTiny.Cloud.MultiTenantPlatform.Domain.Repository;
using SevenTiny.Cloud.MultiTenantPlatform.Domain.ServiceContract;
using System.Collections.Generic;

namespace SevenTiny.Cloud.MultiTenantPlatform.Domain.Service
{
    public class MetaObjectService : CommonInfoRepository<MetaObject>, IMetaObjectService
    {
        public MetaObjectService(MultiTenantPlatformDbContext multiTenantPlatformDbContext) : base(multiTenantPlatformDbContext)
        {
            dbContext = multiTenantPlatformDbContext;
        }

        MultiTenantPlatformDbContext dbContext;

        public List<MetaObject> GetMetaObjectsUnDeletedByApplicationId(int applicationId)
            => dbContext.QueryList<MetaObject>(t => t.ApplicationId == applicationId && t.IsDeleted == (int)IsDeleted.UnDeleted);
    }
}
