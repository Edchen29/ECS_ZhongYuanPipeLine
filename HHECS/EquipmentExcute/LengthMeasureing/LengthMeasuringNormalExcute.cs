using HHECS.Bll;
using HHECS.EquipmentExcute.LengthMeasureing.LengthMeasuringEnums;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HHECS.EquipmentExcute.LengthMeasureing
{
    /// <summary>
    /// 测长处理类
    /// </summary>
    public class LengthMeasuringNormalExcute : LengthMeasureingExcute
    {   
        /// <summary>
        /// 执行下料请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="lengthMeasuring"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcuteRequest(Equipment lengthMeasuringMachine, List<Equipment> allEquipments, List<Equipment> lthMeasuringCaches, IPLC plc)
        {
            try
            {
                //接收PLC传来的长度
                var pipeLength = lengthMeasuringMachine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MeasuringMachineToECSProps.Length.ToString());
                var lengthConvertResult = int.TryParse(pipeLength.Value, out int length);
                //接收PLC传来的材质
                var pipeMaterial = lengthMeasuringMachine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MeasuringMachineToECSProps.Material.ToString());
                var materialConvertResult = int.TryParse(pipeMaterial.Value, out int materialType);
                //接收PLC传来的壁厚
                var pipeThickness = lengthMeasuringMachine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MeasuringMachineToECSProps.Thickness.ToString());
                var thicknessConvertResult = int.TryParse(pipeThickness.Value, out int thickness);
                //接收PLC传来的内径
                var pipeDiameter = lengthMeasuringMachine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MeasuringMachineToECSProps.Diameter.ToString());
                var diameterConvertResult = int.TryParse(pipeDiameter.Value, out int diameter);

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

                var bevelCcache = allEquipments.FirstOrDefault(t => t.SelfAddress == lengthMeasuringMachine.GoAddress.ToString());

                //对缓存表做判断，小于12条记录说明缓存区没满，可以下料
                var cacheInfo = AppSession.Dal.GetCommonModelByConditionWithZero<StationCache>($" where  stationCode = '{bevelCcache.StationCode}' ");
                if(!cacheInfo.Success)
                {
                    Logger.Log($"处理站台[{lengthMeasuringMachine.StationId}]的设备[{lengthMeasuringMachine.Name}]下料请求失败，查询缓存区失败，原因：[{cacheInfo.Msg}]", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                //测长缓存区只能放11根管子
                if (cacheInfo.Data.Count <= 11)
                {
                    //生成缓存记录并且插入数据库  
                    StationCache stationCache = new StationCache();
                    stationCache.StationId = lengthMeasuringMachine.StationId;
                    stationCache.StationCode = lengthMeasuringMachine.StationCode;
                    stationCache.WcsProductType = materialType;
                    stationCache.Thickness = thickness;
                    stationCache.Diameter = diameter;
                    stationCache.MaterialLength = length;
                    stationCache.Status = 0;
                    AppSession.Dal.InsertCommonModel<StationCache>(stationCache);
                    SendRequestToPlc(plc, lengthMeasuringMachine, true);
                }
                else
                {
                    Logger.Log($"处理站台[{lengthMeasuringMachine.StationId}]的设备[{lengthMeasuringMachine.Name}]下料请求失败，缓存区已满，请等待缓存区腾出位置", LogLevel.Error);
                    return BllResultFactory.Error();
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理站台[{lengthMeasuringMachine.StationId}]的设备[{lengthMeasuringMachine.Name}]下料请求时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
            return BllResultFactory.Sucess();
        }
    }
}


