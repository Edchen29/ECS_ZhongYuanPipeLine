using System.Web.Http;
using HHECS;
using HHECS.Model.ApiModel;
using HHECS.Bll;
using HHECS.Model.BllModel;
using System.Configuration;
using HHECS.Model.ApiModel.WCSApiModel;

namespace HHECS.API.Controllers
{
    /// <summary>
    /// 内部使用API，一般用于定时任务处理
    /// </summary>
    public class ECSApiController : ApiController
    {
        [HttpPost]
        public object ECSApiBus(ApiBusModel apiBusModel)
        {
            var a = ConfigurationManager.AppSettings["WarehouseId"];
            if (apiBusModel == null || string.IsNullOrWhiteSpace(apiBusModel.Api))
            {
                return BllResultFactory.Error("未提供API");
            }
            switch (apiBusModel.Api)
            {
                case "test":
                    return AppSession.BllService.GetAllRole();
                default:
                    return BllResultFactory.Error($"未提供{apiBusModel.Api}对应接口的实现");
            }
        }
    }
}