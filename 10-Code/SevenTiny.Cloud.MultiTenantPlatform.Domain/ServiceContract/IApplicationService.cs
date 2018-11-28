using SevenTiny.Cloud.MultiTenantPlatform.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SevenTiny.Cloud.MultiTenantPlatform.Domain.ServiceContract
{
    public interface IApplicationService
    {
        List<Application> GetApplicationsDeleted();
        List<Application> GetApplicationsUnDeleted();
        bool ExistForSameName(string name);
        bool ExistForSameNameAndNotSameId(string name, int id);
        void Add(Application application);
        Application GetById(int id);
        void Update(Application application);
        void LogicDelete(int id);
        void Recover(int id);
    }
}
