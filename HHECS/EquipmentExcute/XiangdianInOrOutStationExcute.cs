using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HHECS.Bll;
using HHECS.Model;
using HHECS.OPC;
using S7.Net;

namespace HHECS.Common
{
    /// <summary>
    /// todo:湘电可由开关控制的出或入站台
    /// </summary>
    public class XiangdianInOrOutStationExcute : IStationExcute
    {
        public EquipmentType EquipmentType { get; set; }

        /// <summary>
        /// 暂时只考虑只入的情况
        /// </summary>
        /// <param name="stations"></param>
        /// <param name="plcs"></param>
        /// <returns></returns>
        public BllResult Excute(List<Equipment> stations, List<Plc> plcs)
        {
            foreach (var station in stations)
            {
                InnerExcute(station, plcs.Find(t => t.IP == station.IP));
            }
            return BllResultFactory.Sucess();
        }

        private void InnerExcute(Equipment station, Plc plc)
        {
            #region 只入情况

            //判断请求
            //PLC有请求，但WCS没有，则WCS还没有响应
            if (station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "RequestMessage").Value == "1" && station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "WCSReplyMessage").Value == "0")
            {
                //
                var barcode = station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "RequestBarcode").Value;
                if (string.IsNullOrWhiteSpace(barcode))
                {
                    Logger.Log($"站台{station.Name}有地址请求但是没有条码信息", LogLevel.Error);
                    return;
                }
                else
                {
                    //获取任务
                    var taskResult = AppSession.Bll.GetCommonModelByCondition<TaskEntity>($"where containerCode = '{barcode}' and lastStatus < {TaskEntityStatus.任务完成.GetIndexInt()}");
                    if (taskResult.Success)
                    {
                        if (taskResult.Data.Count > 1)
                        {
                            Logger.Log($"站台{station.Name}根据条码{barcode}查找到了多条任务，请检查数据", LogLevel.Error);
                            return;
                        }
                        else
                        {
                            var task = taskResult.Data[0];
                            int tempStatus = task.FirstStatus; //记录原始状态以便回滚
                            //更新任务状态到60
                            task.FirstStatus = TaskEntityStatus.拣选台回库.GetIndexInt();
                            task.LastStatus = TaskEntityStatus.拣选台回库.GetIndexInt();
                            var temp = AppSession.Bll.UpdateCommonModel<TaskEntity>(task);
                            if (temp.Success)
                            {
                                //回复地址请求,这里的去向地址就是其自身
                                temp = SendAddressReply(station, plc,"6", "0", station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "RequestNumber").Value, barcode, station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "RequestWeight").Value, station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "RequestLength").Value, station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "RequestWidth").Value, station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "RequestHeight").Value, station.SelfAddress, "");
                                if (temp.Success)
                                {
                                    Logger.Log($"响应站台{station.Name}地址请求成功，条码{barcode},任务{task.Id}", LogLevel.Info);
                                    return;
                                }
                                else
                                {
                                    Logger.Log($"响应站台{station.Name}地址请求失败，条码{barcode},任务{task.Id}，回滚任务状态", LogLevel.Error);
                                    task.FirstStatus = tempStatus;
                                    task.LastStatus = tempStatus;
                                    AppSession.Bll.UpdateCommonModel<TaskEntity>(task);
                                    return;
                                }
                            }
                            else
                            {
                                Logger.Log($"站台{station.Name}，响应请求时更新任务{task.Id}状态失败。", LogLevel.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        Logger.Log($"站台{station.Name}根据条码{barcode}没有查询到任务。", LogLevel.Error);
                        return;
                    }
                }
            }

            //PLC没有，WCS有，说明PLC已经清除而WCS没有清除
            if (station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "RequestMessage").Value == "0" && station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "WCSReplyMessage").Value == "6")
            {
                var result = SendAddressReply(station, plc, "0","0", "0", "0", "0", "0", "0", "0", "0", "0");
                if (result.Success)
                {
                    Logger.Log($"站台{station.Name}响应地址请求完成后，清除WCS地址区成功", LogLevel.Info);
                    return;
                }
                else
                {
                    Logger.Log($"站台{station.Name}响应地址请求完成后，清除WCS地址区失败", LogLevel.Error);
                    return;
                }
            }

            //判断位置到达
            //PLC有位置到达，而WCSACK没有回复，则WCS还没有响应
            if (station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "ArriveMessage").Value == "2" && station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "WCSACKMessage").Value == "0")
            {
                var isSuccess = station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "ArriveResult").Value;
                if (isSuccess == "1")
                {
                    //获取条码
                    var barcode = station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "ArriveBarcode").Value;
                    if (string.IsNullOrWhiteSpace(barcode))
                    {
                        Logger.Log($"站台{station.Name}有位置到达但是没有条码信息", LogLevel.Error);
                        return;
                    }
                    else
                    {
                        var taskResult = AppSession.Bll.GetCommonModelByCondition<TaskEntity>($"where containerCode = '{barcode}' and lastStatus < {TaskEntityStatus.任务完成.GetIndexInt()}");
                        if (taskResult.Success)
                        {
                            if (taskResult.Data.Count > 1)
                            {
                                Logger.Log($"站台{station.Name}根据条码{barcode}查找到了多条任务，请检查数据", LogLevel.Error);
                                return;
                            }
                            else
                            {
                                var task = taskResult.Data[0];
                                int tempStatus = task.FirstStatus; //记录原始状态以便回滚
                                task.FirstStatus = TaskEntityStatus.响应接入口位置到达.GetIndexInt();
                                task.LastStatus = TaskEntityStatus.响应接入口位置到达.GetIndexInt();
                                var temp = AppSession.Bll.UpdateCommonModel<TaskEntity>(task);
                                if (temp.Success)
                                {
                                    //回复位置到达
                                    temp = SendAck(station, plc, "8","1", station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "ArriveRealAddress").Value, "0");
                                    if (temp.Success)
                                    {
                                        Logger.Log($"响应站台{station.Name}位置到达成功，条码{barcode},任务{task.Id}", LogLevel.Info);
                                        return;
                                    }
                                    else
                                    {
                                        Logger.Log($"响应站台{station.Name}位置到达失败，条码{barcode},任务{task.Id}，回滚任务状态", LogLevel.Error);
                                        task.FirstStatus = tempStatus;
                                        task.LastStatus = tempStatus;
                                        AppSession.Bll.UpdateCommonModel<TaskEntity>(task);
                                        return;
                                    }
                                }
                                else
                                {
                                    Logger.Log($"响应站台{station.Name}位置到达请求失败，条码{barcode},任务{task.Id}，回滚任务状态", LogLevel.Error);
                                    task.FirstStatus = tempStatus;
                                    task.LastStatus = tempStatus;
                                    AppSession.Bll.UpdateCommonModel<TaskEntity>(task);
                                    return;
                                }
                            }

                        }
                        else
                        {
                            Logger.Log($"站台{station.Name}根据条码{barcode}没有查询到任务。", LogLevel.Error);
                            return;
                        }
                    }
                }
                else
                {
                    Logger.Log($"站台{station.Name}位置到达失败", LogLevel.Error);
                    return;
                }

            }

            //PLC没位置到达，而WCSACK有回复，则PLC已经响应但还没有清除
            if (station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "ArriveMessage").Value == "0" && station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "WCSACKMessage").Value == "8" && station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "WCSACKLoadStatus").Value == "1")
            {
                var result = SendAck(station, plc, "0", "0", "0", "0");
                if (result.Success)
                {
                    Logger.Log($"站台{station.Name}响应位置到达完成后，清除WCS地址区成功", LogLevel.Info);
                    return;
                }
                else
                {
                    Logger.Log($"站台{station.Name}响应位置到达完成后，清除WCS地址区失败", LogLevel.Error);
                    return;
                }
            }


            #endregion



        }

        private BllResult SendAddressReply(Equipment station, Plc plc, string message,string loadStatus, string number, string barcode, string weight, string lenght, string width, string height, string address, string backup)
        {
            var prop1 = station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "WCSReplyMessage");
            prop1.Value = message;
            var prop2 = station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "WCSReplyLoadStatus");
            prop2.Value = loadStatus;
            var prop3 = station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "WCSReplyNumber");
            prop3.Value = number;
            var prop4 = station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "WCSReplyBarcode");
            prop4.Value = barcode;
            var prop5 = station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "WCSReplyWeight");
            prop5.Value = weight;
            var prop6 = station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "WCSReplyLength");
            prop6.Value = lenght;
            var prop7 = station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "WCSReplyWidth");
            prop7.Value = width;
            var prop8 = station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "WCSReplyHeight");
            prop8.Value = height;
            var prop9 = station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "WCSReplyAddress");
            prop9.Value = address;
            var prop10 = station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "WCSReplyBackUp");
            prop10.Value = backup;
            List<EquipmentProp> props = new List<EquipmentProp> { prop2, prop3, prop4, prop5, prop6, prop7, prop8, prop9, prop10, prop1 };
            return S7Helper.PlcSplitWrite(plc, props, 20);
        }

        private BllResult SendAck(Equipment station,Plc plc,string message,string loadStatus,string number,string backup)
        {
            var prop1 = station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "WCSACKMessage");
            prop1.Value = message;
            var prop2 = station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "WCSACKLoadStatus");
            prop2.Value = loadStatus;
            var prop3 = station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "WCSACKNumber");
            prop3.Value = number;
            var prop4 = station.EquipmentProps.Find(t => t.EquipmentTypePropTemplateCode == "WCSACKBackup");
            prop4.Value = backup;
            List<EquipmentProp> props = new List<EquipmentProp>() { prop2,prop3,prop4,prop1};
            return S7Helper.PlcSplitWrite(plc, props, 20);
        }
    }
}
