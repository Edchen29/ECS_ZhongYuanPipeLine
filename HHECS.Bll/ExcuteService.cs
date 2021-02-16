using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HHECS.DAL;
using HHECS.Model.BllModel;
using HHECS.Model.Entities;
using HHECS.Model.Enums;

namespace HHECS.Bll
{
    /// <summary>
    /// 与调度相关的服务
    /// </summary>
    public class ExcuteService : BaseService
    {

        /// <summary>
        /// 获取可用站台
        /// todo:注意： 某些情况下，比如可出入站台，需要根据实际情况来判断是否可用，此处需要被重写
        /// </summary>
        /// <param name="tempEquipments"></param>
        /// <returns></returns>
        public List<Equipment> GetAvailableStation(List<Equipment> tempEquipments)
        {
            return tempEquipments.Where(t => t.EquipmentType.Code.Contains("Station")).ToList();
        }

        

        /// <summary>
        /// hack:按需重新实现此方法
        /// 根据堆垛机所在区域和任务目标的portCode，获取堆垛机可以放货的接出站台
        /// </summary>
        /// <param name="destinationArea"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public BllResult<List<Equipment>> GetOutStationByPort(string destinationArea,string portCode,string warehouseCode)
        {
            var b = AppSession.Dal.GetCommonModelByConditionWithZero<PortSRMStationRelative>($" where destinationArea='{destinationArea}' and portCode='{portCode}' and warehouseCode = '{warehouseCode}' and inOutFlag={(int)InOutFlag.出}");
            if (!b.Success )
            {
                return BllResultFactory<List<Equipment>>.Error($"查询出口站台出错：{b.Msg}");
            }
            if (b.Data.Count() == 0)
            {
                return BllResultFactory<List<Equipment>>.Error($"未找到区域：{destinationArea}和出口：{portCode}对应的出口站台设备，映射关系未配置。");
            }
            var a = AppSession.Dal.GetCommonModelByCondition<Equipment>($"where code in @codes",new { codes = b.Data.Select(t=>t.StationCode).ToList()});
            if (!a.Success)
            {
                return BllResultFactory<List<Equipment>>.Error($"未找到区域：{destinationArea}和出口：{portCode}对应的出口站台设备，映射关系配置错误");
            }
            return BllResultFactory<List<Equipment>>.Sucess(a.Data);
        }

        /// <summary>
        /// 当站台回库的时候，根据任务要去巷道和本身拣选台code，获取其去向
        /// </summary>
        /// <param name="destinationArea"></param>
        /// <returns></returns>
        public BllResult<List<Equipment>> GetGoAddressByDestinationArea(string destinationArea, string portCode,string warehouseCode)
        {
            var b = AppSession.Dal.GetCommonModelByConditionWithZero<PortSRMStationRelative>($" where destinationArea='{destinationArea}' and portCode='{portCode}' and warehouseCode = '{warehouseCode}' and inOutFlag={(int)InOutFlag.入}");
            if (!b.Success)
            {
                return BllResultFactory<List<Equipment>>.Error($"查询入口站台出错：{b.Msg}");
            }
            if (b.Data.Count() == 0)
            {
                return BllResultFactory<List<Equipment>>.Error($"未找到目标区域：{destinationArea}和入口：{portCode}对应的入口站台设备，映射关系未配置。");
            }
            var a = AppSession.Dal.GetCommonModelByCondition<Equipment>($"where code in @codes", new { codes = b.Data.Select(t => t.StationCode).ToList() });
            if (!a.Success)
            {
                return BllResultFactory<List<Equipment>>.Error($"未找到目标区域：{destinationArea}和入口：{portCode}对应的入口站台设备，映射关系配置错误");
            }
            return BllResultFactory<List<Equipment>>.Sucess(a.Data);
        }

        /// <summary>
        /// 从配置中获取port编码
        /// 实际这里于要求code与value均为设备的编码
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public BllResult<string> GetPortFromDict(string port)
        {
            var result = AppSession.BllService.GetDictWithDetails(SysConst.Port.ToString());
            if (!result.Success)
            {
                return BllResultFactory<string>.Error($"未配置Port");
            }
            var portDict = result.Data.DictDetails?.FirstOrDefault(t => t.Value == port);
            if (portDict == null)
            {
                return BllResultFactory<string>.Error($"未识别的port编码");
            }
            var a = AppSession.Dal.GetCommonModelByCondition<Equipment>($"where code = '{portDict.Value}'");
            if (!a.Success)
            {
                return BllResultFactory<string>.Error($"Port对应设备未配置:{a.Msg}");
            }
            return BllResultFactory<string>.Sucess(data: a.Data[0].Code);
        }
    }
}
