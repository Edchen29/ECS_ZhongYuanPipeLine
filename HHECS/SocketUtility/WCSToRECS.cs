using HHECS.Bll;
using HHECS.EquipmentExcute.Truss;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.SRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace HHECS.SocketUtility
{
    class WCSToRECS
    {
        public void WCSToRECSWA()
        {
            DateTime time = DateTime.Now;
            if (time.Minute == 9 || time.Hour == 17)
            {
                BllResult dataResult = null;
                var alarmResult = AppSession.Dal.GetCommonModelByCondition<WareHouseAlarm>("where instruct = 'WA' and sendFlag = 0");
                if (alarmResult.Success && alarmResult.Data.Count > 0)
                {
                    foreach (var item in alarmResult.Data)
                    {
                        dataResult = ConversionDataForAlarm(item.Pn, item.Instruct, item.EquipmentCode, item.EquipmentError, item.EquipmentFailureTime, item.EquipmentEndFailureTime);
                        if (dataResult.Success)
                        {
                            //回传IP
                            //var SocketClientIP = AppSession.BllService.GetConfig(SysConst.SocketClientIP.ToString());
                            SocketClient socketClient = new SocketClient("172.16.29.42",8500);
                            socketClient.connect();
                            if (socketClient.connected())
                            {
                                string result = socketClient.send(dataResult.Msg);
                                if (result == "OK")
                                {
                                    Logger.Log($"Socket数据回传成功", LogLevel.Success);
                                    string sql = $"update wcswarehousealarm set sendflag = 1 where id={item.Id}";
                                    AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
                                }
                                socketClient.close();
                            }
                        }
                    }
                }
            }

        }

        public void WCSToRECSWS(List<Equipment> equipments)
        {
            var srms = equipments.FindAll(t => t.EquipmentTypeId == 1);
            if (srms.Count > 0)
            {
                foreach (var srm in srms)
                {
                    var WsResult = AppSession.Dal.GetCommonModelByConditionWithZero<WareHouseAlarm>($"where instruct = 'WS' and equipmentCode = '{srm.DestinationArea}' and  DateDiff(minute,created,getdate())<1 ");
                    if (!WsResult.Success)
                    {
                        return;
                    }
                    if (WsResult.Data.Count == 0)
                    {
                        var dataWS = SendStatusForWS(srm);
                        if (dataWS.Success)
                        {
                            //var SocketClientIP = AppSession.BllService.GetConfig(SysConst.SocketClientIP.ToString());
                            IPHostEntry iPHostEntry = Dns.GetHostEntry("www.huahenglogistics.com.cn");
                            IPAddress iPAddress = iPHostEntry.AddressList[0];
                            SocketClient socketClient = new SocketClient(iPAddress.ToString(), 8010);
                            // SocketClient socketClient = new SocketClient("192.168.1.195", 8010);                           
                            
                            socketClient.connect();
                            if (socketClient.connected())
                            {
                                string result1 = socketClient.send(dataWS.Msg);
                                if (result1 == "OK")
                                {
                                    Logger.Log($"Socket数据回传成功", LogLevel.Success);
                                }
                                socketClient.close();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 状态指令
        /// </summary>
        /// <param name="stocker"></param>
        /// <returns></returns>
        private BllResult SendStatusForWS(Equipment srm)
        {

            string project = "2020040901";
            string instruct = "WS";
            string code = srm.DestinationArea;
            //string code = "SRM001";
            var statusSRM = FilterForData(srm);
            if (statusSRM.Success)
            {
                var statusResult = AppSession.Dal.GetCommonModelByCondition<WareHouseAlarm>($"where instruct = 'WS' and  equipmentCode ='{code}' ORDER BY id DESC ");
                if (statusResult.Success && statusResult.Data.Count > 0)
                {
                    var status = statusResult.Data[0];
                    if (statusSRM.Msg != status.EquipmentStatus)
                    {
                        WareHouseAlarm wareHouseAlarm = new WareHouseAlarm();
                        wareHouseAlarm.Pn = project;
                        wareHouseAlarm.Instruct = "WS";
                        wareHouseAlarm.EquipmentStatus = statusSRM.Msg;
                        wareHouseAlarm.EquipmentCode = code;
                        wareHouseAlarm.Created = DateTime.Now;
                        wareHouseAlarm.EquipmentFailureTime = DateTime.Now;
                        wareHouseAlarm.EquipmentEndFailureTime = DateTime.Now;
                        wareHouseAlarm.CreatedBy = "WCS";
                        var res = AppSession.Dal.InsertCommonModel<WareHouseAlarm>(wareHouseAlarm);
                        if (!res.Success)
                        {
                            string s = res.Msg;
                        }
                        var dataResult = ConversionDataForStatus(project, instruct, code, statusSRM.Msg, DateTime.Now, DateTime.Now);
                        return BllResultFactory.Sucess(null, dataResult.Msg);
                    }
                    else
                    {
                        return BllResultFactory.Error(null, "失败");
                    }                    
                }
                else if(!statusResult.Success)
                {
                    WareHouseAlarm wareHouseAlarm = new WareHouseAlarm();
                    wareHouseAlarm.Pn = project;
                    wareHouseAlarm.Instruct = "WS";
                    wareHouseAlarm.EquipmentStatus = statusSRM.Msg;
                    wareHouseAlarm.EquipmentCode = code;
                    wareHouseAlarm.Created = DateTime.Now;
                    wareHouseAlarm.EquipmentFailureTime = DateTime.Now;
                    wareHouseAlarm.EquipmentEndFailureTime = DateTime.Now;
                    wareHouseAlarm.CreatedBy = "WCS";
                    var res = AppSession.Dal.InsertCommonModel<WareHouseAlarm>(wareHouseAlarm);
                    if (!res.Success)
                    {
                        string s = res.Msg;
                    }
                    var dataResult = ConversionDataForStatus(project, instruct, code, statusSRM.Msg, DateTime.Now, DateTime.Now);
                    return BllResultFactory.Sucess(null, dataResult.Msg);
                }
                else
                {
                    return BllResultFactory.Error(null, "失败");
                }
            }
            else
            {
                return BllResultFactory.Error(null, "失败");
            }
        }
        /// <summary>
        /// 堆垛机状态
        /// </summary>
        /// <param name="stocker"></param>
        /// <returns></returns>
        private BllResult FilterForData(Equipment srm)
        {
            string status = String.Empty;
            //判断未开机
            var result = HHECS.Model.Common.CommonForPing.PingTest(srm.IP);
            if (!result)
            {
                status = "00";
                return BllResultFactory.Sucess(null, status);
            }
            else
            {
                //判断堆垛机 联机 有故障。
                if (srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.OperationModel.ToString()).Value == SRMOperationModel.联机.GetIndexString() &&
                srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.TotalError.ToString()).Value == "True")
                {
                    status = "10";
                    return BllResultFactory.Sucess(null, status);
                }
                //判断堆垛机 联机，无故障
                else if (srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.OperationModel.ToString()).Value == SRMOperationModel.联机.GetIndexString() &&
                srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.TotalError.ToString()).Value == "False")
                {
                    var Fork1TaskExcuteStatus = srm.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1TaskExcuteStatus.ToString());
                    if (Fork1TaskExcuteStatus != null)
                    {
                        SRMTaskExcuteStatus temp = (SRMTaskExcuteStatus)(Convert.ToInt32(Fork1TaskExcuteStatus.Value));
                        switch (temp)
                        {
                            case SRMTaskExcuteStatus.待机:
                                status = "01";
                                break;
                            case SRMTaskExcuteStatus.任务执行中:
                            case SRMTaskExcuteStatus.任务完成:
                                status = "03";
                                break;
                            case SRMTaskExcuteStatus.任务中断_出错:
                            case SRMTaskExcuteStatus.下发任务错误:
                                status = "10";
                                break;
                        }
                        return BllResultFactory.Sucess(null, status);
                    }
                }
                else if (srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.OperationModel.ToString()).Value != SRMOperationModel.联机.GetIndexString())
                {
                    status = "04";
                    return BllResultFactory.Sucess(null, status);
                }
                status = "00";
                return BllResultFactory.Sucess(null, status);
            }
        }
        /// <summary>
        /// 转换状态数据To RECS
        /// </summary>
        /// <param name="project">项目</param>
        /// <param name="instruct">指令</param>
        /// <param name="code">机器编码</param>
        /// <param name="status">状态</param>
        /// <param name="startTime">发生时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public BllResult ConversionDataForStatus(string project, string instruct, string code, string status, DateTime startTime, DateTime endTime)
        {
            string startResult = startTime.ToString("yyyyMMddHHmmss");
            string endResult = endTime.ToString("yyyyMMddHHmmss");
            char stx = (char)0x02;
            char placeholder = ' ';
            char etx = (char)0x03;
            string data = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", stx, placeholder, project, instruct, code, status, startResult, endResult, etx);
            return BllResultFactory.Sucess(null, data);
        }
        /// <summary>
        /// 转换报警数据To RECS
        /// </summary>
        /// <returns></returns>
        public BllResult ConversionDataForAlarm(string project, string instruct, string code, string content, DateTime startTime, DateTime endTime)
        {
            string startResult = startTime.ToString("yyyyMMddHHmmss");
            string endResult = endTime.ToString("yyyyMMddHHmmss");
            //byte[] textbuf = Encoding.Default.GetBytes(content);
            //string textAscii = string.Empty;//用来存储转换过后的ASCII码
            //for (int i = 0; i < textbuf.Length; i++)
            //{
            //    textAscii += textbuf[i].ToString("X");
            //}
            char stx = (char)0x02;
            char placeholder = ' ';
            char etx = (char)0x03;
            string data = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", stx, placeholder, project, instruct, code, startResult, endResult, content, etx);
            return BllResultFactory.Sucess(null, data);
        }
    }
}
