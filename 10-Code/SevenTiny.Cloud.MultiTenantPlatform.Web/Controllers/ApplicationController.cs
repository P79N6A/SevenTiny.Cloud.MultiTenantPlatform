﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SevenTiny.Cloud.MultiTenantPlatform.Domain.ServiceContract;
using SevenTiny.Cloud.MultiTenantPlatform.Web.Models;

namespace SevenTiny.Cloud.MultiTenantPlatform.Web.Controllers
{
    public class ApplicationController : ControllerBase
    {
        IApplicationService applicationService;
        IMetaObjectService metaObjectService;

        public ApplicationController(
            IApplicationService _applicationService,
            IMetaObjectService _metaObjectService)
        {
            applicationService = _applicationService;
            metaObjectService = _metaObjectService;
        }

        public IActionResult Setting()
        {
            return View();
        }

        public IActionResult Select()
        {
            var list = applicationService.GetEntitiesUnDeleted();
            return View(list);
        }

        public IActionResult List()
        {
            var list = applicationService.GetEntitiesUnDeleted();
            return View(list);
        }

        public IActionResult DeleteList()
        {
            var list = applicationService.GetEntitiesDeleted();
            return View(list);
        }

        public IActionResult Add()
        {
            var application = new Domain.Entity.Application();
            application.Icon = "cloud";
            return View(ResponseModel.Success(application));
        }

        public IActionResult AddLogic(Domain.Entity.Application application)
        {
            if (string.IsNullOrEmpty(application.Name))
            {
                return View("Add", ResponseModel.Error("Application Name Can Not Be Null！", application));
            }
            if (string.IsNullOrEmpty(application.Code))
            {
                return View("Add", ResponseModel.Error("Application Code Can Not Be Null！", application));
            }
            if (applicationService.ExistForSameName(application.Name))
            {
                return View("Add", ResponseModel.Error("Application Name Has Been Exist！", application));
            }

            applicationService.Add(application);
            return RedirectToAction("List");
        }

        public IActionResult Update(int id)
        {
            var application = applicationService.GetById(id);
            return View(ResponseModel.Success(application));
        }

        public IActionResult UpdateLogic(Domain.Entity.Application application)
        {
            if (application.Id == 0)
            {
                return View("Update", ResponseModel.Error("Application Id Can Not Be Null！", application));
            }
            if (string.IsNullOrEmpty(application.Name))
            {
                return View("Update", ResponseModel.Error("Application Name Can Not Be Null！", application));
            }
            if (string.IsNullOrEmpty(application.Code))
            {
                return View("Update", ResponseModel.Error("Application Code Can Not Be Null！", application));
            }
            if (applicationService.ExistForSameNameAndNotSameId(application.Name, application.Id))
            {
                return View("Add", ResponseModel.Error("Application Name Has Been Exist！", application));
            }

            applicationService.Update(application);
            return RedirectToAction("List");
        }

        public IActionResult LogicDelete(int id)
        {
            applicationService.LogicDelete(id);
            return JsonResultModel.Success("删除成功");
        }

        public IActionResult Recover(int id)
        {
            applicationService.Recover(id);
            return JsonResultModel.Success("恢复成功");
        }

        public IActionResult Detail(string app)
        {
            if (string.IsNullOrEmpty(app))
            {
                return Redirect("/Application/Select");
            }
            var application = applicationService.GetByCode(app);

            HttpContext.Session.SetInt32("ApplicationId", application.Id);
            HttpContext.Session.SetString("ApplicationCode", application.Code);
            ViewData["Application"] = application.Code;

            return View();
        }
    }
}