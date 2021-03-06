﻿using System.Collections.Generic;
using SevenTiny.Cloud.MultiTenantPlatform.Domain.Entity;
using SevenTiny.Cloud.MultiTenantPlatform.Domain.ValueObject;

namespace SevenTiny.Cloud.MultiTenantPlatform.Domain.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public Repository(MultiTenantPlatformDbContext multiTenantPlatformDbContext)
        {
            dbContext = multiTenantPlatformDbContext;
        }

        MultiTenantPlatformDbContext dbContext;

        public ResultModel Add(TEntity entity)
        {
            dbContext.Add(entity);
            return ResultModel.Success();
        }

        public ResultModel Add(IList<TEntity> entities)
        {
            dbContext.Add<TEntity>(entities);
            return ResultModel.Success();
        }

        public ResultModel Update(TEntity entity)
        {
            dbContext.Update(entity);
            return ResultModel.Success();
        }

        public ResultModel Delete(TEntity entity)
        {
            dbContext.Delete<TEntity>(entity);
            return ResultModel.Success();
        }
    }
}
