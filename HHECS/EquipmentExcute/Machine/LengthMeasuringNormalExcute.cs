using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Machine;
using HHECS.Model.Enums.PipeLine;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HHECS.EquipmentExcute.Machine
{
    /// <summary>
    /// 测长处理类
    /// </summary>
    public class LengthMeasuringNormalExcute : MachineExcute
    {   
        /// <summary>
        /// 执行下料请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="lengthMeasuring"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcuteRequest(Equipment lengthMeasuringMachine, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                //接收PLC传来的长度
                var pipeLength = lengthMeasuringMachine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestLength.ToString());
                var lengthConvertResult = int.TryParse(pipeLength.Value, out int length);
                //接收PLC传来的材质
                var pipeMaterial = lengthMeasuringMachine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestMaterial.ToString());
                var materialConvertResult = int.TryParse(pipeMaterial.Value, out int materialType);
                //接收PLC传来的壁厚
                var pipeThickness = lengthMeasuringMachine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestThickness.ToString());
                var thicknessConvertResult = decimal.TryParse(pipeThickness.Value, out decimal thickness);
                //接收PLC传来的内径
                var pipeDiameter = lengthMeasuringMachine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestDiameter.ToString());
                var diameterConvertResult = decimal.TryParse(pipeDiameter.Value, out decimal diameter);

                if (!lengthConvertResult)
                {
                    Logger.Log($"处理站台[{lengthMeasuringMachine.StationId}]的设备[{lengthMeasuringMachine.Name}]下料请求失败，原材料的长度[{pipeLength.Value}]转化为整数失败！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                if (!materialConvertResult)
                {
                    Logger.Log($"处理站台[{lengthMeasuringMachine.StationId}]的设备[{lengthMeasuringMachine.Name}]下料请求失败，原材料的材质[{pipeMaterial.Value}]转化为整数失败！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                if (!thicknessConvertResult)
                {
                    Logger.Log($"处理站台[{lengthMeasuringMachine.StationId}]的设备[{lengthMeasuringMachine.Name}]下料请求失败，原材料的壁厚[{pipeThickness.Value}]转化为整数失败！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                if (!diameterConvertResult)
                {
                    Logger.Log($"处理站台[{lengthMeasuringMachine.StationId}]的设备[{lengthMeasuringMachine.Name}]下料请求失败，原材料的内径[{pipeDiameter.Value}]转化为整数失败！", LogLevel.Error);
                    return BllResultFactory.Error();
                }

                var nextEquipment = allEquipments.FirstOrDefault(t => t.SelfAddress == lengthMeasuringMachine.GoAddress.ToString());

                //对缓存表做判断，小于12条记录说明缓存区没满，可以下料
                var cacheInfo = AppSession.Dal.GetCommonModelByConditionWithZero<StationCache>($" where  stationCode = '{nextEquipment.StationCode}' and status < {StationCacheStatus.使用中.GetIndexInt()}");
                if(!cacheInfo.Success)
                {
                    Logger.Log($"处理站台[{lengthMeasuringMachine.StationId}]的设备[{lengthMeasuringMachine.Name}]下料请求失败，查询缓存区失败，原因：[{cacheInfo.Msg}]", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                if (cacheInfo.Data.Count >= 11)
                {
                    Logger.Log($"处理站台[{lengthMeasuringMachine.StationId}]的设备[{lengthMeasuringMachine.Name}]下料请求失败，缓存区已满，请等待缓存区腾出位置", LogLevel.Warning);
                    return BllResultFactory.Error();
                }
                var requestNumber = lengthMeasuringMachine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestNumber.ToString());

                //生成缓存记录并且插入数据库  
                StationCache stationCache = new StationCache();
                stationCache.StationId = lengthMeasuringMachine.StationId;
                stationCache.StationCode = lengthMeasuringMachine.StationCode;
                stationCache.WcsProductType = materialType;
                stationCache.Thickness = thickness / 10;
                stationCache.Diameter = diameter / 10 ;
                stationCache.MaterialLength = length;
                stationCache.Status = StationCacheStatus.初始.GetIndexInt();
                stationCache.CreateBy = App.User.UserCode; 
                stationCache.CreateTime = DateTime.Now;
                var insertResult = AppSession.Dal.InsertCommonModel<StationCache>(stationCache);
                if (!insertResult.Success)
                {
                    Logger.Log($"处理工位[{lengthMeasuringMachine.StationCode}]的设备[{lengthMeasuringMachine.Name}] 下料请求的时候，插入管材信息到数据库失败，原因：{insertResult.Msg}", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                stationCache.Id = insertResult.Data;
                var sendResult = SendAddressReplyToPlc(lengthMeasuringMachine, plc, MachineMessageFlag.WCS回复允许下料, requestNumber.Value, stationCache.Id.ToString(), "", "0", stationCache.WcsProductType.ToString(), stationCache.MaterialLength.ToString(), stationCache.Diameter.ToString(), stationCache.Thickness.ToString(), lengthMeasuringMachine.GoAddress);
                if (sendResult.Success)
                {
                    Logger.Log($"处理工位[{lengthMeasuringMachine.StationCode}]的设备[{lengthMeasuringMachine.Name}] 下料请求 成功，管子缓存标识[{stationCache.Id}]，管子材质[{stationCache.WcsProductType}]，管子长度[{stationCache.MaterialLength}]，管子直径[{stationCache.Diameter}]，管子壁厚[{stationCache.Thickness}]", LogLevel.Success);
                    return BllResultFactory.Sucess();
                }
                else
                {
                    AppSession.Dal.DeleteCommonModelByIds<StationCache>(new List<int>() { stationCache.Id.Value });
                    Logger.Log($"处理工位[{lengthMeasuringMachine.StationCode}]的设备[{lengthMeasuringMachine.Name}] 下料请求的时候，管子缓存标识[{stationCache.Id}]对应的信息写入PLC失败，原因：{sendResult.Msg}", LogLevel.Error);
                    return BllResultFactory.Error();
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理站台[{lengthMeasuringMachine.StationId}]的设备[{lengthMeasuringMachine.Name}]下料请求时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
        }


    }
}


