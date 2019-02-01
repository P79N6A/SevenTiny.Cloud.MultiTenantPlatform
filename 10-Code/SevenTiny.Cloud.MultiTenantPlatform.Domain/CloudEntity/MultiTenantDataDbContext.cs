﻿using SevenTiny.Bantina.Bankinate;
using SevenTiny.Cloud.MultiTenantPlatform.Infrastructure.Configs;

namespace SevenTiny.Cloud.MultiTenantPlatform.Domain.CloudEntity
{
    public class MultiTenantDataDbContext : MongoDbContext<MultiTenantDataDbContext>
    {
        public MultiTenantDataDbContext() : base(ConnectionStringsConfig.Get("mongodb39911"))
        {
            
        }
    }
}
