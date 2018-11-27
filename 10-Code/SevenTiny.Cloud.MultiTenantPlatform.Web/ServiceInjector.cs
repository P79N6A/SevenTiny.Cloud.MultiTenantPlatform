using Microsoft.Extensions.DependencyInjection;
using SevenTiny.Cloud.MultiTenantPlatform.Application;
using SevenTiny.Cloud.MultiTenantPlatform.Application.Service;
using SevenTiny.Cloud.MultiTenantPlatform.Application.ServiceContract;
using SevenTiny.Cloud.MultiTenantPlatform.DomainModel.Entities;
using SevenTiny.Cloud.MultiTenantPlatform.DomainModel.Repository;
using SevenTiny.Cloud.MultiTenantPlatform.DomainModel.RepositoryContract;

namespace SevenTiny.Cloud.MultiTenantPlatform.Web
{
    /// <summary>
    /// 依赖注入器
    /// </summary>
    public static class ServiceInjector
    {
        //使用.netcore自带的DI
        public static void InjectWeb(this IServiceCollection services)
        {
            //repository
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IMetaObjectRepository, MetaObjectRepository>();
            services.AddScoped<IMetaFieldRepository, MetaFieldRepository>();
            services.AddScoped<IConditionAggregationRepository, ConditionAggregationRepository>();
            services.AddScoped<IFieldAggregationRepository, FieldAggregationRepository>();
            services.AddScoped<IInterfaceAggregationRepository, InterfaceAggregationRepository>();
            services.AddScoped<IInterfaceFieldRepository, InterfaceFieldRepository>();
            services.AddScoped<IInterfaceSearchConditionRepository, InterfaceSearchConditionRepository>();

            //service
            services.AddScoped<IMetaFieldService, MetaFieldService>();

            //注入Application层的实例
            services.InjectApplication();
        }
    }
}
