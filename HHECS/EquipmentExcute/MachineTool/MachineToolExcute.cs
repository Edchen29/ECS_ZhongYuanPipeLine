using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Task;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;

namespace HHECS.EquipmentExcute.MachineTool
{
    public abstract class MachineToolExcute
    {  /// <summary>
       /// 用于标记设备的类型
       /// </summary>
        public EquipmentType EquipmentType { get; set; }

        /// <summary>
        /// 用于可用存储设备列表
        /// </summary>
        public List<Equipment> Equipments { get; set; }
        public virtual BllResult Excute(List<Equipment> machineTools, List<Equipment> allEquipments, IPLC plc)
        {
            try
            {
                if (machineTools.Count == 0)
                {
                    return BllResultFactory.Error($"没有连接到【{this.EquipmentType.Code}】设备，所以不执行处理程序。");
                }
                // 如果机床有异常，就不上料，也不下料
                for (var i = machineTools.Count - 1; i >= 0 ; i--)
                {
                    var Abnormal_1 = machineTools[i].EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineToolToECSProps.Abnormal_1.ToString());
                    var Abnormal_2 = machineTools[i].EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineToolToECSProps.Abnormal_2.ToString());
                    if (Abnormal_1.Value == "True" || Abnormal_2.Value == "True")
                    {
                        var Request_Load = machineTools[i].EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineToolToECSProps.Request_Load.ToString());
                        if (Request_Load != null)
                        {
                            Request_Load.Value = "False";
                        }
                        var Request_Blank = machineTools[i].EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineToolToECSProps.Request_Blank.ToString());
                        if (Request_Blank != null)
                        {
                            Request_Blank.Value = "False";
                        }
                    }
                }

                //找出 桁车 未完成的任务
                var stepTraceResult = AppSession.Dal.GetCommonModelByConditionWithZero<StepTrace>($"where status < {StepTraceStatus.任务完成.GetIndexInt()}");
                if (!stepTraceResult.Success)
                {
                    Logger.Log($"查询【{this.EquipmentType.Name}】类型的设备的任务出错，原因：{stepTraceResult.Msg}", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                foreach (var machineTool in machineTools)
                {
                    //处理 上料完成
                    var Task_OK = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineToolToECSProps.Task_OK.ToString());
                    var WCS_Allow_Load = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == EcsToMachineToolPorps.WCS_Allow_Load.ToString());
                    if (Task_OK != null && WCS_Allow_Load != null)
                    {
                        //PLC有请求，但ECS没有，则ECS还没有响应
                        if (Task_OK.Value == "True" && WCS_Allow_Load.Value == "False")
                        {
                            ExcuteArrive(machineTool, allEquipments, stepTraceResult.Data, plc);
                        }
                        //PLC没有请求，但ECS有确认信号， 就清除 ECS确认上料完成信号 
                        if (Task_OK.Value == "False" && WCS_Allow_Load.Value == "True")
                        {
                            SendArriveToPlc(plc, machineTool, false);
                        }
                    }

                    //处理 加工
                    var Request_Product = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineToolToECSProps.Request_Wroking.ToString());
                    var WCS_Allow_Product = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == EcsToMachineToolPorps.WCS_Wroking.ToString());
                    if (Request_Product != null && WCS_Allow_Product != null)
                    {
                        //PLC有请求，但ECS没有，则ECS还没有响应
                        if (Request_Product.Value == "True" && WCS_Allow_Product.Value == "False")
                        {
                            ExcuteProduct(machineTool, allEquipments, stepTraceResult.Data, plc);
                        }
                        //PLC没有请求，但ECS有确认信号， 就清除 ECS确认生产信号 
                        if (Request_Product.Value == "False" && WCS_Allow_Product.Value == "True")
                        {
                            SendProductToPlc(plc, machineTool, false);
                        }
                    }

                    //处理 下料
                    var Request_Blank = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineToolToECSProps.Request_Blank.ToString());
                    if (Request_Blank != null)
                    {
                        //PLC有请求，但ECS没有，则ECS还没有响应
                        if (Request_Blank.Value == "True")
                        {
                            ExcuteRequest(machineTool, allEquipments, stepTraceResult.Data, plc);
                        }
                    }
                }
                return BllResultFactory.Sucess();
            }
            catch (Exception ex)
            {
                Logger.Log($"机器人理过程中出现异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
        }

        /// <summary>
        /// 执行上料完成
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="machineTool"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public abstract BllResult ExcuteArrive(Equipment machineTool, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc);

        /// <summary>
        /// 执行生产请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="machineTool"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public abstract BllResult ExcuteProduct(Equipment machineTool, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc);

        /// <summary>
        /// 执行下料请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="machineTool"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public abstract BllResult ExcuteRequest(Equipment machineTool, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc);

        /// <summary>
        /// 写入或清除  上料完成 
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="robot"></param>
        /// <param name="arrive">是否到达，true为到达,false为清除</param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected BllResult SendArriveToPlc(IPLC plc, Equipment machineTool, bool arrive)
        {
            var operate = arrive ? "写入" : "清除";
            var WCS_Allow_Load = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == EcsToMachineToolPorps.WCS_Allow_Load.ToString());
            WCS_Allow_Load.Value = arrive.ToString();
            BllResult plcResult = plc.Write(WCS_Allow_Load);
            if (plcResult.Success)
            {
                Logger.Log($"{operate}设备【{machineTool.Name}】 ECS确认上料完成 信号成功", LogLevel.Success);
            }
            else
            {
                Logger.Log($"{operate}设备【{machineTool.Name}】 ECS确认上料完成 信号失败，写入PLC失败：原因：{plcResult.Msg}", LogLevel.Error);
            }
            return plcResult;
        }

        /// <summary>
        /// 写入或清除 任务信息
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="robot"></param>
        /// <param name="product">是否生产，true为生产,false为清除</param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected BllResult SendStepTraceToPlc(IPLC plc, Equipment machineTool, bool wcsAllowLoad, string wcsStepTraceId)
        {
            var WCS_Allow_Load = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == EcsToMachineToolPorps.WCS_Allow_Load.ToString());
            var WCS_Step_Trace_Id = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == EcsToMachineToolPorps.WCS_Step_Trace_Id.ToString());
            WCS_Allow_Load.Value = wcsAllowLoad.ToString();
            WCS_Step_Trace_Id.Value = wcsStepTraceId;
            var propsToWriter = new List<EquipmentProp> { WCS_Allow_Load, WCS_Step_Trace_Id };
            return plc.Writes(propsToWriter);
        }

        /// <summary>
        /// 写入或清除 生产请求
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="robot"></param>
        /// <param name="product">是否生产，true为生产,false为清除</param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected BllResult SendProductToPlc(IPLC plc, Equipment machineTool, bool product)
        {
            var operate = product ? "写入" : "清除";
            var WCS_Allow_Product = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == EcsToMachineToolPorps.WCS_Wroking.ToString());
            WCS_Allow_Product.Value = product.ToString();
            BllResult plcResult = plc.Write(WCS_Allow_Product);
            if (plcResult.Success)
            {
                Logger.Log($"{operate}设备【{machineTool.Name}】允许请求生产信号成功", LogLevel.Success);
            }
            else
            {
                Logger.Log($"{operate}设备【{machineTool.Name}】允许请求上料信号失败，写入PLC失败：{plcResult.Msg}", LogLevel.Success);
            }
            return plcResult;
        }





    }
}
