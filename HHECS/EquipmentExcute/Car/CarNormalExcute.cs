using HHECS.Bll;
using HHECS.EquipmentExcute.Car.CarEnums;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Car;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HHECS.EquipmentExcute
{
    /// <summary>
    /// AGV实现
    /// </summary>
    public class CarNormalExcute : CarExcute
    {

        //所有的小车
        List<Equipment> allCars = null;

        public override BllResult Excute(List<Equipment> equipments, List<Equipment> allEquipments, IPLC plc)
        {
            try
            {                
                allCars = equipments;                
                //accountByWMS();
                // step1: 获取一个任务状态为拣选台回库的 没有进行穿梭车任务解析的
                foreach (var car in allCars)
                {
                    ExcuteTDR(car, plc);                    
                }
                return BllResultFactory.Sucess();
            }
            catch (Exception ex)
            {
                Logger.Log($"小车处理过程中出现异常：{ex.Message}", LogLevel.Exception);
                return BllResultFactory.Error("");
            }
        }

        /// <summary>
        /// 执行小车任务
        /// </summary>
        /// <param name="car"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public BllResult ExcuteTDR(Equipment car, IPLC plc)
        {
            //子任务号
            int taskCarId = int.Parse(car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "TaskCarId").Value);
            //主任务号
            int taskheaderId = int.Parse(car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "TaskHeaderID").Value);
            //小车任务类型
            string actionType = car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "actionType").Value;
            //小车行
            string row = car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "row").Value;
            ////小车位置
            //var position = car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "position").Value;
            //小车状态
            string carStatus = car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "carStatus").Value;
            //小车错误状态
            string carError = car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "carError").Value;
            //小车编码
            int carNo = int.Parse(car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "carNo").Value);
            //小车到达
            string arriveMessage = car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "arriveMessage").Value;
            //输出穿梭板上是否有货
            string hasPallet = car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "hasPallet").Value;
            //AGV控制模式
            string controlMode = car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "controlMode").Value;

            if (controlMode != CarControlMode.自动.GetIndexString() || carError != CarError.Normal.GetIndexString())
            {
                return BllResultFactory.Error();
            }

            var TaskCarResult = AppSession.Dal.GetCommonModelByCondition<CarTask>($"where status < {TaskCarStatus.Executed.GetIndexInt()} and carNo={carNo}");
            if (TaskCarResult.Success)
            {
                List<CarTask> tasks = TaskCarResult.Data.OrderBy(t => t.Id).ToList();

                // 如果小车反馈完成，就修改任务状态为“完成”，然后回复小车确认完成
                if (carStatus == CarStatus.Finish.GetIndexString())
                {
                    CarTask carTask = tasks.FirstOrDefault(t => t.Id == taskCarId);
                    if (carTask != null)
                    {
                        //保存旧数据
                        var carTaskClone = (CarTask)carTask.DeepClone();
                        //修改状态和完成时间
                        carTask.EndTime = DateTime.Now;
                        carTask.Status = TaskCarStatus.Waiting.GetIndexInt();
                        if (AppSession.Dal.UpdateCommonModel<CarTask>(carTask).Success)
                        {
                            //确定过账任务完成
                            var sendResult = sendConfirmTaskFinish(car, plc);
                            if (sendResult.Success)
                            {
                                Logger.Log($"小车[car{carNo}]完成[{taskCarId}]号任务，ECS给PLC确认完成信号成功！", LogLevel.Warning);
                            }
                            else
                            {
                                AppSession.Dal.UpdateCommonModel<CarTask>(carTaskClone);
                                Logger.Log($"小车[car{carNo}]完成[{taskCarId}]号任务，但是ECS给PLC确认完成信号失败，原因：{sendResult.Msg}！", LogLevel.Warning);
                            }
                        }
                        return BllResultFactory.Sucess();
                    }
                    else
                    {
                        Logger.Log($"小车[car{carNo}]任务已经完成ID为[{taskCarId}]的任务，但是该任务不存在！", LogLevel.Warning);
                        return BllResultFactory.Error();
                    }
                }

                // 如果小车是执行中，就清除小车开启信号
                if (carStatus == CarStatus.Executing.GetIndexString())
                {
                    CarTask carTask = tasks.FirstOrDefault(t => t.Id == taskCarId);
                    if (carTask != null)
                    {
                        if (carTask.Status == TaskCarStatus.Init.GetIndexInt())
                        {
                            var sendResult = sendWcsSwitch(car, plc, "0");
                            if (sendResult.Success)
                            {
                                // 更新小车任务状态  
                                carTask.Status = TaskCarStatus.Executing.GetIndexInt();
                                carTask.StartTime = DateTime.Now;
                                AppSession.Dal.UpdateCommonModel<CarTask>(carTask);
                            }
                        }
                    }
                    ////小车到达一个地方后，如果需要ECS确认后才能走，就发出到达信号，然后等待ECS回复后，清除到达信号，最后ECS也清除回复信号
                    //var arrive = car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == CarProps.arriveMessage.GetIndexString());
                    //var replyArrive = car.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == CarProps.wcsArriveMessage.GetIndexString());

                    //if (arrive?.Value == ArriveMessage.到达.GetIndexString() && replyArrive?.Value == ArriveMessage.默认.GetIndexString())
                    //{
                    //    //回复到达
                    //    ExcuteArrive(car, ArriveMessage.回复到达, plc);
                    //}
                    //if (arrive?.Value == ArriveMessage.默认.GetIndexString() && replyArrive?.Value == ArriveMessage.回复到达.GetIndexString())
                    //{
                    //    ExcuteArrive(car, ArriveMessage.默认, plc);
                    //}
                    return BllResultFactory.Sucess();
                }

                //如果小车是准备就绪，并且状态是WCS模式就可以运行
                if (carStatus == CarStatus.idle.GetIndexString())
                {
                    CarTask carTask = tasks.FirstOrDefault();
                    // 如果有初始化的任务，并且任务id不是刚完成的任务id
                    if (carTask != null && carTask.Status == TaskCarStatus.Init.GetIndexInt())
                    {
                        var result = SendTaskToCar(car, carTask, plc);
                        if (result.Success)
                        {
                            Logger.Log($"给小车[{car.Code}]下发ID为[{carTask.Id}]的[{((CarTaskType)carTask.Type).ToDescriptionString()}]任务成功！", LogLevel.Info);
                            return BllResultFactory.Sucess();
                        }
                        else
                        {
                            Logger.Log($"给小车[{car.Code}]下发ID为[{carTask.Id}]的[{((CarTaskType)carTask.Type).ToDescriptionString()}]任务失败！原因：{result.Msg}", LogLevel.Error);
                            return BllResultFactory.Error();
                        }
                    }
                }
            }     
            return BllResultFactory.Sucess();
        }

        /// <summary>
        /// 处理到达信号
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="car"></param>
        /// <param name="v"></param>
        private BllResult ExcuteArrive( Equipment car, ArriveMessage arriveMessage, IPLC plc)
        {
            var operate = arriveMessage == ArriveMessage.回复到达 ? "写入" : "清除";
            var replyArrive = car .EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == ArriveMessage.回复到达.GetIndexString());
            replyArrive.Value = arriveMessage.ToString();
            BllResult plcResult = plc.Write(replyArrive);
            if (plcResult.Success)
            {
                Logger.Log($"{operate}设备[{car.Name}] ECS确认到达完成 信号成功", LogLevel.Success);
            }
            else
            {
                Logger.Log($"{operate}设备[{car.Name}] ECS确认到达完成 信号失败，写入PLC失败：原因：{plcResult.Msg}", LogLevel.Error);
            }
            return plcResult;
        }








        /// <summary>
        /// WMS 过账
        /// </summary>
        public BllResult accountByWMS()
        {
            //try
            //{
            //    var taskCarResult = AppSession.Dal.GetCommonModelByCondition<TaskCar>($" where status={TaskCarStatus.Waiting.GetIndexInt()}");
            //    if (taskCarResult.Success)
            //    {
            //        foreach (TaskCar task in taskCarResult.Data)
            //        {
            //            // 0 = 初始 1 = 取货 2 = 放货 3 = 上母车 4 = 下母车 5 = 倒料 6 = 上料 7 = 充电  8 = 出库查看
            //            if (task.type == 2 || task.type == 4 || task.type == 5)
            //            {
            //                var taskResult1 = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($" where id = {task.taskId}");
            //                if (taskResult1.Success)
            //                {
            //                    TaskEntity entity = taskResult1.Data.FirstOrDefault();
            //                    if (entity.Type == TaskType.穿梭车移位.GetIndexInt())
            //                    {
            //                        if (entity.FirstStatus < TaskEntityStatus.响应堆垛机库内放货任务完成.GetIndexInt())
            //                        {
            //                            return BllResultFactory.Error($"主任务还没更新到[响应堆垛机库内放货任务完成]需要等待，任务号:{task.taskId}");
            //                        }
            //                        entity.FirstStatus = TaskEntityStatus.任务完成.GetIndexInt();
            //                        entity.LastStatus = TaskEntityStatus.任务完成.GetIndexInt();
            //                        entity.EndTime = DateTime.Now;
            //                        entity.LastUpdated = DateTime.Now;
            //                        var tmp = AppSession.Dal.UpdateCommonModel<TaskEntity>(entity);
            //                        if (tmp.Success == false)
            //                        {
            //                            Logger.Log($"更新小车对应的主失败，任务号:{task.taskId}", LogLevel.Error);
            //                            return BllResultFactory.Error();
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if (task.type == 2 || task.type == 5)
            //                        {
            //                            if (entity.FirstStatus < TaskEntityStatus.响应堆垛机库内放货任务完成.GetIndexInt())
            //                            {
            //                                return BllResultFactory.Error($"主任务还没更新到[响应堆垛机库内放货任务完成]需要等待，任务号:{task.taskId}");
            //                            }
            //                            //TODO: 修改任务-----完成任务
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    Logger.Log($"小车{task.carNo}过账失败，任务号:{task.taskId}的主任务找不到！", LogLevel.Error);
            //                    return BllResultFactory.Error();
            //                }
            //            }
            //            task.status = TaskCarStatus.Account.GetIndexInt();
            //            task.endTime = DateTime.Now;
            //            task.Updated = DateTime.Now;
            //            if (AppSession.Dal.UpdateCommonModel<TaskCar>(task).Success)
            //            {
            //                Logger.Log($"小车任务过账成功 任务号:{task.taskId}", LogLevel.Success);
            //            }
            //            else
            //            {
            //                Logger.Log($"小车任务[{task.taskId}]过账失败, 更新小车任务状态失败", LogLevel.Error);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return BllResultFactory.Error("小车过账异常：" + ex.Message);
            //}
            return BllResultFactory.Sucess();
        }


    }
}
