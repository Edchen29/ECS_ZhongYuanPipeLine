using HHECS.EquipmentExcute.Car.CarEnums;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Car;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;

namespace HHECS.EquipmentExcute
{
    /// <summary>
    /// 穿梭车处理类
    /// </summary>
    public abstract class CarExcute
    {
        /// <summary>
        /// 对应的设备类型
        /// </summary>
        public EquipmentType EquipmentType { get; set; }

        /// <summary>
        /// 用于可用存储设备列表
        /// </summary>
        public List<Equipment> Equipments { get; set; }

        /// <summary>
        /// 穿梭车处理逻辑
        /// </summary>
        /// <param name="cars"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public abstract BllResult Excute(List<Equipment> cars, List<Equipment> allEquipments, IPLC plc);

        /// <summary>
        /// 发送穿梭车的信息
        /// </summary>
        /// <param name="car">穿梭车</param>
        /// <param name="plc">plc</param>
        /// <param name="carActionType">小车执行的动作类型</param>
        /// <param name="carRow">行</param>
        /// <param name="carColumn">列</param>
        /// <param name="carLayer">层</param>
        /// <param name="taskHeaderId">任务-关联WCS的TaskHeader表格</param>
        /// <param name="taskCarId">子任务</param>
        /// <returns></returns>
        public BllResult SendTaskToCar(Equipment car, IPLC plc, CarActionType carActionType, string carRow, string carColumn, string carLayer, string taskHeaderId, string taskCarId)
        {
            try
            {
                List<EquipmentProp> propsToWriter = new List<EquipmentProp>();
                var props = car.EquipmentProps;
                var action = props.Find(t => t.EquipmentTypeTemplateCode == "wcsActionType");
                action.Value = carActionType.GetIndexString();
                var taskRow = props.Find(t => t.EquipmentTypeTemplateCode == "wcsRow");
                taskRow.Value = carRow;
                var taskLine = props.Find(t => t.EquipmentTypeTemplateCode == "wcsLine");
                taskLine.Value = carColumn;
                var taskLayer = props.Find(t => t.EquipmentTypeTemplateCode == "wcsLayer");
                taskLayer.Value = carLayer;
                var taskHeader = props.Find(t => t.EquipmentTypeTemplateCode == "wcsTaskHeaderId");
                taskHeader.Value = taskHeaderId;
                var taskCar = props.Find(t => t.EquipmentTypeTemplateCode == "wcsTaskCarId");
                taskCar.Value = taskCarId;
                var switchEnable = props.Find(t => t.EquipmentTypeTemplateCode == "wcsSwitch");
                switchEnable.Value = "1";
                propsToWriter.AddRange(new List<EquipmentProp>() {  action, taskHeader, taskCar, taskRow, taskLine, taskLayer, switchEnable });
                //return S7Helper.PlcSplitWrite(plc, propsToWriter, 20);
                return plc.Writes(propsToWriter);
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error(ex.Message);
            }
        }

        /// <summary>
        /// 发送小车任务，行：1.打标  2.坡口短缓存架  3.坡口长缓存架 4.坡口短缓存架  5.坡口机   6.组队输送架
        /// </summary>
        /// <param name="car"></param>
        /// <param name="plc"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public BllResult SendTaskToCar(Equipment car, CarTask task, IPLC plc)
        {
            try
            {
                List<EquipmentProp> propsToWriter = new List<EquipmentProp>();
                var props = car.EquipmentProps;
                var wcsConfirmTaskFinish = props.Find(t => t.EquipmentTypeTemplateCode == CarProps.wcsConfirmTaskFinish.ToString());
                wcsConfirmTaskFinish.Value = "0";
                var wcsActionType = props.Find(t => t.EquipmentTypeTemplateCode == CarProps.wcsActionType.ToString());
                wcsActionType.Value = task.Type.ToString();
                var wcsStartRow = props.Find(t => t.EquipmentTypeTemplateCode == CarProps.wcsStartRow.ToString());
                wcsStartRow.Value = task.FromLocation.ToString();
                var wcsDestinationRow = props.Find(t => t.EquipmentTypeTemplateCode == CarProps.wcsDestinationRow.ToString());
                wcsDestinationRow.Value = task.ToLocation.ToString();
                var wcsTaskHeaderId = props.Find(t => t.EquipmentTypeTemplateCode == CarProps.wcsTaskHeaderId.ToString());
                wcsTaskHeaderId.Value = task.StepTraceId.ToString();
                var wcsTaskCarId = props.Find(t => t.EquipmentTypeTemplateCode == CarProps.wcsTaskCarId.ToString());
                wcsTaskCarId.Value = task.Id.ToString();
                var wcsSwitch = props.Find(t => t.EquipmentTypeTemplateCode == CarProps.wcsSwitch.ToString());
                wcsSwitch.Value = "1";
                propsToWriter.AddRange(new List<EquipmentProp>() { wcsConfirmTaskFinish, wcsStartRow, wcsDestinationRow, wcsTaskHeaderId, wcsTaskCarId, wcsActionType, wcsSwitch });
                //return S7Helper.PlcSplitWrite(plc, propsToWriter, 20);
                return plc.Writes(propsToWriter);
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error(ex.Message);
            }
        }


        public BllResult SendCleanTaskToCar(Equipment car, CarTask task, IPLC plc)
        {
            try
            {
                List<EquipmentProp> propsToWriter = new List<EquipmentProp>();
                var props = car.EquipmentProps;
                var action = props.Find(t => t.EquipmentTypeTemplateCode == "wcsActionType");
                action.Value = "0";
                var taskRow = props.Find(t => t.EquipmentTypeTemplateCode == "wcsTaskRow");
                taskRow.Value = "0";
                //var taskLine = props.Find(t => t.EquipmentTypeTemplateCode == "wcsTaskLine");
                //taskLine.Value = "0";
                //var taskLayer = props.Find(t => t.EquipmentTypeTemplateCode == "wcsTaskLayer");
                //taskLayer.Value = "0";
                var taskHeader = props.Find(t => t.EquipmentTypeTemplateCode == "wcsTaskHeaderId");
                taskHeader.Value = "0";
                var taskCar = props.Find(t => t.EquipmentTypeTemplateCode == "wcsTaskCarId");
                taskCar.Value = "0";
                var switchEnable = props.Find(t => t.EquipmentTypeTemplateCode == "wcsSwitch");
                switchEnable.Value = "0";
                var hasPallet = props.Find(t => t.EquipmentTypeTemplateCode == "hasPallet");
                hasPallet.Value = "1";
                propsToWriter.AddRange(new List<EquipmentProp>() { action, taskRow, taskHeader, taskCar, switchEnable , hasPallet });
                //return S7Helper.PlcSplitWrite(plc, propsToWriter, 20);
                return plc.Writes(propsToWriter);
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error(ex.Message);
            }
        }

        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="car"></param>
        /// <param name="plc"></param>
        public BllResult Heartbeat(Equipment car, IPLC plc)
        {
            try
            {
                var prop = car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "WCSHeartBeat");
                if (prop.Value == "1")
                {
                    prop.Value = "0";
                }
                else
                {
                    prop.Value = "1";
                }
                return plc.Writes(new List<EquipmentProp>() { prop }); 
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error(ex.Message);
            }
        }

        /// <summary>
        /// 输入过账确认信号,根据过账确认信号清空穿梭车的指令完成状态和输出任务号
        /// </summary>
        /// <param name="car"></param>
        /// <param name="plc"></param>
        public BllResult sendConfirmTaskFinish(Equipment car, IPLC plc)
        {
            try
            {
                List<EquipmentProp> propsToWriter = new List<EquipmentProp>();
                var props = car.EquipmentProps;
                var wcsConfirmTaskFinish = props.Find(t => t.EquipmentTypeTemplateCode == CarProps.wcsConfirmTaskFinish.ToString());
                wcsConfirmTaskFinish.Value = "1";
                var wcsActionType = props.Find(t => t.EquipmentTypeTemplateCode == CarProps.wcsActionType.ToString());
                wcsActionType.Value = "0";
                var wcsStartRow = props.Find(t => t.EquipmentTypeTemplateCode == CarProps.wcsStartRow.ToString());
                wcsStartRow.Value = "0";
                var wcsDestinationRow = props.Find(t => t.EquipmentTypeTemplateCode == CarProps.wcsDestinationRow.ToString());
                wcsDestinationRow.Value = "0";
                var wcsTaskHeaderId = props.Find(t => t.EquipmentTypeTemplateCode == CarProps.wcsTaskHeaderId.ToString());
                wcsTaskHeaderId.Value = "0";
                var wcsTaskCarId = props.Find(t => t.EquipmentTypeTemplateCode == CarProps.wcsTaskCarId.ToString());
                wcsTaskCarId.Value = "0";
                var wcsSwitch = props.Find(t => t.EquipmentTypeTemplateCode == CarProps.wcsSwitch.ToString());
                wcsSwitch.Value = "0";
                propsToWriter.AddRange(new List<EquipmentProp>() { wcsConfirmTaskFinish, wcsActionType, wcsStartRow, wcsDestinationRow, wcsTaskHeaderId, wcsTaskCarId, wcsSwitch });
                //return S7Helper.PlcSplitWrite(plc, propsToWriter, 20);
                return plc.Writes(propsToWriter);
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error(ex.Message);
            }
        }

        /// <summary>
        /// 输入开启信号
        /// </summary>
        /// <param name="car"></param>
        /// <param name="plc"></param>
        public BllResult sendWcsSwitch(Equipment car, IPLC plc, string wcsSwitchValue)
        {
            try
            {
                var wcsSwitch = car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == CarProps.wcsSwitch.ToString());
                if (wcsSwitch.Value != wcsSwitchValue)
                {
                    wcsSwitch.Value = wcsSwitchValue;
                    var result = plc.Writes(new List<EquipmentProp>() { wcsSwitch });
                    return result;
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error(ex.Message);
            }
            return BllResultFactory.Sucess();            
        }



        /// <summary>
        /// 通用条件验证   设备准备OK  无错误  控制模式为wcs
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public BllResult Validate(Equipment car)
        {
            //穿梭车准备就绪  ready    
            if (car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "ready").Value == CarReadyStatus.Ready.GetIndexString()
                    && car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "carError").Value =="0"
                    && car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "controlMode").Value == CarControlMode.自动.GetIndexString())
            {
                return BllResultFactory.Sucess();
            }
            else
            {
                return BllResultFactory.Error();
            }
        }

        /// <summary>
        /// 获取穿梭车的位置
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public CarLocation getCarLocation(Equipment car)
        {
            CarLocation location= new CarLocation();
            location.row = car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "row").Value;
            location.line = car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "line").Value;
            location.layer = car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "layer").Value;
            //location.location = car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "location").Value;
            location.carNo = car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "carNo").Value;
            location.controlMode= car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "controlMode").Value;
            return location;
        }


        /// <summary>
        /// 发送穿梭车的信息
        /// </summary>
        /// <param name="car">穿梭车</param>
        /// <param name="plc">plc</param>
        /// <param name="carActionType">小车执行的动作类型</param>
        /// <param name="carRow">行</param>
        /// <param name="carColumn">列</param>
        /// <param name="carLayer">层</param>
        /// <param name="taskHeaderId">任务-关联WCS的TaskHeader表格</param>
        /// <param name="taskCarId">子任务</param>
        /// <returns></returns>
        public BllResult SendManualTaskToCar(Equipment car, IPLC plc, CarActionType carActionType, string carRow, string carColumn, string carLayer, string taskHeaderId, string taskCarId)
        {
            try
            {
                List<EquipmentProp> propsToWriter = new List<EquipmentProp>();
                var props = car.EquipmentProps;
                var action = props.Find(t => t.EquipmentTypeTemplateCode == "wcsActionType");
                action.Value = carActionType.GetIndexString();
                var taskRow = props.Find(t => t.EquipmentTypeTemplateCode == "wcsTaskRow");
                taskRow.Value = carRow;
                var taskLine = props.Find(t => t.EquipmentTypeTemplateCode == "wcsTaskLine");
                taskLine.Value = carColumn;
                var taskLayer = props.Find(t => t.EquipmentTypeTemplateCode == "wcsTaskLayer");
                taskLayer.Value = carLayer;
                var taskHeader = props.Find(t => t.EquipmentTypeTemplateCode == "wcsTaskHeaderId");
                taskHeader.Value = taskHeaderId;
                var taskCar = props.Find(t => t.EquipmentTypeTemplateCode == "wcsTaskCarId");
                taskCar.Value = taskCarId;
                var switchEnable = props.Find(t => t.EquipmentTypeTemplateCode == "wcsSwitch");
                switchEnable.Value = "1";
                propsToWriter.AddRange(new List<EquipmentProp>() { action, taskRow, taskLine, taskLayer, taskHeader, taskCar, switchEnable });
                //return S7Helper.PlcSplitWrite(plc, propsToWriter, 20);
                return plc.Writes(propsToWriter);
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error(ex.Message);
            }
        }

    }
}
