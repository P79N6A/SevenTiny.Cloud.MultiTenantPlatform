﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SevenTiny.Cloud.MultiTenantPlatform.DataApi.Models;
using SevenTiny.Cloud.MultiTenantPlatform.Domain.CloudEntity;
using SevenTiny.Cloud.MultiTenantPlatform.Domain.Enum;
using SevenTiny.Cloud.MultiTenantPlatform.Domain.ServiceContract;
using SevenTiny.Cloud.MultiTenantPlatform.TriggerScriptEngine.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SevenTiny.Cloud.MultiTenantPlatform.DataApi.Controllers
{
    [Route("api/UI/[controller]")]
    [ApiController]
    public class TableListController : ControllerBase
    {
        public TableListController(
            IDataAccessService _dataAccessService,
            ISearchConditionService _searchConditionService,
            ISearchConditionAggregationService _conditionAggregationService,
            IInterfaceAggregationService _interfaceAggregationService,
            IFieldBizDataService _fieldBizDataService,
            ITriggerScriptEngineService _triggerScriptEngineService,
            IMetaObjectService _metaObjectService,
            IMetaFieldService _metaFieldService
            )
        {
            dataAccessService = _dataAccessService;
            conditionAggregationService = _conditionAggregationService;
            interfaceAggregationService = _interfaceAggregationService;
            fieldBizDataService = _fieldBizDataService;
            triggerScriptEngineService = _triggerScriptEngineService;
            searchConditionService = _searchConditionService;
            metaObjectService = _metaObjectService;
            metaFieldService = _metaFieldService;
        }

        readonly IDataAccessService dataAccessService;
        readonly IInterfaceAggregationService interfaceAggregationService;
        readonly ISearchConditionAggregationService conditionAggregationService;
        readonly IFieldBizDataService fieldBizDataService;
        readonly ITriggerScriptEngineService triggerScriptEngineService;
        readonly ISearchConditionService searchConditionService;
        readonly IMetaObjectService metaObjectService;
        readonly IMetaFieldService metaFieldService;

        /// <summary>
        /// 这块的逻辑为：
        /// 根据视图找到搜索表单和列表
        /// 依据搜索表单中的
        /// </summary>
        /// <param name="queryArgs"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromQuery]UITableListQueryArgs queryArgs)
        {
            try
            {
                //args check
                if (queryArgs == null)
                {
                    return JsonResultModel.Error($"Parameter invalid:queryArgs = null");
                }

                var checkResult = queryArgs.ArgsCheck();

                if (!checkResult.IsSuccess)
                {
                    return checkResult.ToJsonResultModel();
                }

                //argumentsDic generate
                Dictionary<string, object> argumentsDic = new Dictionary<string, object>();
                foreach (var item in Request.Query)
                {
                    if (!argumentsDic.ContainsKey(item.Key))
                    {
                        argumentsDic.Add(item.Key.ToUpperInvariant(), item.Value);
                    }
                }

                //这里根据视图来匹配搜索条件
                //get filter
                var interfaceAggregation = interfaceAggregationService.GetByMetaObjectIdAndInterfaceAggregationCode("待定");
                if (interfaceAggregation == null)
                {
                    return JsonResultModel.Error($"未能找到接口编码为[{"待定"}]对应的接口信息");
                }

                //分析搜索条件，是否忽略参数校验为true，如果参数没传递则不抛出异常且处理为忽略参数
                var filter = conditionAggregationService.AnalysisConditionToFilterDefinitionByConditionId(interfaceAggregation.MetaObjectId, interfaceAggregation.SearchConditionId, argumentsDic, true);
                //如果参数都没传递或者其他原因导致条件没有，则直接返回全部
                if (filter == null)
                {
                    filter = Builders<BsonDocument>.Filter.Empty;
                }

                filter = triggerScriptEngineService.TableListBefore(interfaceAggregation.MetaObjectId, interfaceAggregation.Code, filter);
                var sort = metaFieldService.GetSortDefinitionBySortFields(interfaceAggregation.MetaObjectId, queryArgs.SortFields);
                var tableListComponent = dataAccessService.GetTableListComponent(interfaceAggregation.MetaObjectId, interfaceAggregation.FieldListId, filter, queryArgs.PageIndex, queryArgs.PageSize, sort, out int totalCount);
                tableListComponent = triggerScriptEngineService.TableListAfter(interfaceAggregation.MetaObjectId, interfaceAggregation.Code, tableListComponent);

                return JsonResultModel.Success("get data list success", tableListComponent);
            }
            catch (ArgumentNullException argNullEx)
            {
                return JsonResultModel.Error(argNullEx.Message);
            }
            catch (ArgumentException argEx)
            {
                return JsonResultModel.Error(argEx.Message);
            }
            catch (Exception ex)
            {
                return JsonResultModel.Error(ex.Message);
            }
        }
    }
}