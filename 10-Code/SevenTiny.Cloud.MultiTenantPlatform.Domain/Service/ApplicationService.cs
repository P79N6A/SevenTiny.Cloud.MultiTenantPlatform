using SevenTiny.Cloud.MultiTenantPlatform.Domain.Entity;
using SevenTiny.Cloud.MultiTenantPlatform.Domain.Enum;
using SevenTiny.Cloud.MultiTenantPlatform.Domain.ServiceContract;
using System;
using System.Collections.Generic;

namespace SevenTiny.Cloud.MultiTenantPlatform.Domain.Service
{
    public class ApplicationService : IApplicationService
    {
        public ApplicationService(MultiTenantPlatformDbContext multiTenantPlatformDbContext)
        {
            dbContext = multiTenantPlatformDbContext;
        }

        MultiTenantPlatformDbContext dbContext;

        public List<Application> GetEntitiesDeleted()
            => dbContext.QueryList<Application>(t => t.IsDeleted == (int)IsDeleted.Deleted);

        public List<Application> GetEntitiesUnDeleted()
            => dbContext.QueryList<Application>(t => t.IsDeleted == (int)IsDeleted.UnDeleted);

        public bool ExistForSameName(string name)
            => dbContext.QueryExist<Application>(t => t.Name.Equals(name));

        public void Add(Application application)
            => dbContext.Add(application);

        public Application GetById(int id)
            => dbContext.QueryOne<Application>(t => t.Id == id);

        public bool ExistForSameNameAndNotSameId(string name, int id)
            => dbContext.QueryExist<Application>(t => t.Name.Equals(name) && t.Id != id);

        public void Update(Application application)
        {
            var app = GetById(application.Id);
            if (app != null)
            {
                app.Name = application.Name;
                app.Icon = application.Icon;
                app.Group = application.Group;
                app.SortNumber = application.SortNumber;
                app.Description = application.Description;
                app.ModifyBy = -1;
                app.ModifyTime = DateTime.Now;

                dbContext.Update(t => t.Id == application.Id, app);
            }
        }

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

        public Application GetByCode(string code)
            => dbContext.QueryOne<Application>(t => t.Code.Equals(code));
    }
}
