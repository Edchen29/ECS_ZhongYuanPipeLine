using HHECS.Model.BllModel;
using HHECS.Model.Entities;
using HHECS.Model.PLCHelper.Interfaces;
using HHECS.Model.PLCHelper.PLCComponent;
using HHECS.Model.PLCHelper.PLCComponent.Sharp7Component;
using Sharp7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HHECS.Model.PLCHelper.Implement
{
    /// <summary>
    /// 封装高性能Snap7，提供多PLC支持，提供线程安全，无需安装多余组件
    /// 使用此类，请先行构造S7PLCHelpers
    /// </summary>
    public class S7Implement : IPLC
    {

        /// <summary>
        /// 连接配置
        /// </summary>
        public List<S7PLCHelper> S7PLCHelpers { get; set; } = new List<S7PLCHelper>();

        private List<LockInt> _lockIds = new List<LockInt>();

        private Dictionary<string, AutoResetEvent> _lockEvent = new Dictionary<string, AutoResetEvent>();

        /// <summary>
        /// 获取锁
        /// </summary>
        private void GetLock(string key)
        {
            if (Interlocked.Increment(ref _lockIds.First(t => t.Key == key).I) == 1)
            {
                //如果是第一次进入
                return;
            }
            else
            {
                _lockEvent.First(t => t.Key == key).Value.WaitOne();
            }
        }

        /// <summary>
        /// 重置锁
        /// </summary>
        private void ResetLock(string key)
        {
            _lockEvent.First(t => t.Key == key).Value.Set();
        }

        /// <summary>
        /// 连接方法，使用此方法之前请先构造S7PLCHelpers
        /// </summary>
        /// <returns></returns>
        public BllResult Connect()
        {
            if (S7PLCHelpers.Count() == 0)
            {
                return BllResultFactory.Error($"未配置PLC参数");
            }
            int i = 0;
            foreach (var item in S7PLCHelpers)
            {
                S7Client s7Client = new S7Client();
                switch (Enum.Parse(typeof(PLCType), item.PLCType))
                {
                    case PLCType.S7_300:
                        i = s7Client.ConnectTo(item.PLCIP, 0, 1);
                        break;
                    case PLCType.S7_400:
                        i = s7Client.ConnectTo(item.PLCIP, item.Rack, item.Slot);
                        break;
                    case PLCType.S7_1200:
                        i = s7Client.ConnectTo(item.PLCIP, 0, 1);
                        break;
                    case PLCType.S7_1500:
                        i = s7Client.ConnectTo(item.PLCIP, 0, 1);
                        break;
                    default:
                        return BllResultFactory.Error($"无法识别{item.PLCType}");
                }
                if (i != 0)
                {
                    return BllResultFactory.Error(s7Client.ErrorText(i));
                }
                if (!_lockIds.Exists(t => t.Key == item.PLCIP))
                {
                    _lockIds.Add(new LockInt()
                    {
                        Key = item.PLCIP
                    });
                }
                if (!_lockEvent.TryGetValue(item.PLCIP, out AutoResetEvent e))
                {
                    _lockEvent.Add(item.PLCIP, new AutoResetEvent(false));
                }
                item.S7Client = s7Client;
            }
            return BllResultFactory.Sucess();
        }

        /// <summary>
        /// 断开连接，安全做法是先停止控制循环，然后调用
        /// </summary>
        /// <returns></returns>
        public BllResult DisConnect()
        {
            if (S7PLCHelpers.Count() == 0)
            {
                return BllResultFactory.Error($"未配置PLC参数");
            }
            foreach (var item in S7PLCHelpers)
            {
                item.S7Client?.Disconnect();
            }
            //重置所有锁
            _lockIds.ForEach(t => t.I = 0);
            return BllResultFactory.Sucess();
        }

        public BllResult Connect(string ip)
        {
            if (S7PLCHelpers.Count() == 0)
            {
                return BllResultFactory.Error($"未配置PLC参数");
            }
            var item = S7PLCHelpers.Find(t => t.PLCIP == ip);
            if (item == null)
            {
                return BllResultFactory.Error($"未找到IP为{ip}的PLC");
            }
            int i = 0;
            S7Client s7Client = new S7Client();
            switch (Enum.Parse(typeof(PLCType), item.PLCType))
            {
                case PLCType.S7_300:
                    i = s7Client.ConnectTo(item.PLCIP, 0, 1);
                    break;
                case PLCType.S7_400:
                    i = s7Client.ConnectTo(item.PLCIP, item.Rack, item.Slot);
                    break;
                case PLCType.S7_1200:
                    i = s7Client.ConnectTo(item.PLCIP, 0, 1);
                    break;
                case PLCType.S7_1500:
                    i = s7Client.ConnectTo(item.PLCIP, 0, 1);
                    break;
                default:
                    return BllResultFactory.Error($"无法识别{item.PLCType}");
            }
            if (i != 0)
            {
                return BllResultFactory.Error(s7Client.ErrorText(i));
            }
            if (!_lockIds.Exists(t => t.Key == item.PLCIP))
            {
                _lockIds.Add(new LockInt()
                {
                    Key = item.PLCIP
                });
            }
            if (!_lockEvent.TryGetValue(item.PLCIP, out AutoResetEvent e))
            {
                _lockEvent.Add(item.PLCIP, new AutoResetEvent(false));
            }
            item.S7Client = s7Client;
            return BllResultFactory.Sucess();
        }

        public BllResult DisConnect(string ip)
        {
            if (S7PLCHelpers.Count() == 0)
            {
                return BllResultFactory.Error($"未配置PLC参数");
            }
            var item = S7PLCHelpers.Find(t => t.PLCIP == ip);
            if (item == null)
            {
                return BllResultFactory.Error($"未找到IP为{ip}的PLC");
            }
            item.S7Client?.Disconnect();
            //解锁
            var temp = _lockIds.Find(t => t.Key == item.PLCIP);
            if (temp != null)
            {
                temp.I = 0;
            }
            return BllResultFactory.Sucess();
        }

        public BllResult GetConnectStatus()
        {
            if (S7PLCHelpers.Count() == 0)
            {
                return BllResultFactory.Error($"未配置PLC参数");
            }
            foreach (var item in S7PLCHelpers)
            {
                if (item.S7Client?.Connected != true)
                {
                    return BllResultFactory.Error($"存在IP为{item.PLCIP}的PLC未连接");
                }
            }
            return BllResultFactory.Sucess();
        }

        public BllResult GetConnectStatus(string plcIp)
        {
            var temp = S7PLCHelpers.FirstOrDefault(t => t.PLCIP == plcIp);
            if (temp == null)
            {
                return BllResultFactory.Error($"未找到指定IP{plcIp}的PLC");
            }
            else
            {
                if (temp.S7Client?.Connected == true)
                {
                    return BllResultFactory.Sucess();
                }
                else
                {
                    return BllResultFactory.Error($"IP为{plcIp}的PLC未连接");
                }
            }
        }

        public BllResult Read(EquipmentProp equipmentProp)
        {

            //确定属性中对应的PLC是否存在连接
            return Reads(new List<EquipmentProp>() { equipmentProp });

        }

        public BllResult Reads(List<EquipmentProp> equipmentProps)
        {
            if (equipmentProps == null || equipmentProps.Count() == 0)
            {
                return BllResultFactory.Error($"未传递属性");
            }
            //选取设备类
            var ips = equipmentProps.Select(t => t.Equipment).Select(t => t.IP).Distinct().ToList();
            //设备类是否对应PLC配置IP
            foreach (var item in ips)
            {
                var temp = S7PLCHelpers.FirstOrDefault(t => t.PLCIP == item);
                //确定属性中对应的PLC是否存在连接
                if (temp.S7Client == null || !temp.S7Client.Connected)
                {
                    return BllResultFactory.Error($"存在IP为{item}的PLC未连接");
                }
            }

            try
            {
                foreach (var item in ips)
                {
                    //缓存
                    List<byte[]> list = new List<byte[]>();
                    var props = equipmentProps.Where(t => t.Equipment.IP == item).ToList();
                    var client = S7PLCHelpers.FirstOrDefault(t => t.PLCIP == item).S7Client;
                    S7MultiVar s7MultiVar = new S7MultiVar(client);
                    int count = 10;
                    int i = 0;
                    while (true)
                    {
                        var tempProps = props.Skip(i).Take(count);
                        i += count;
                        if (tempProps.Count() == 0)
                        {
                            break;
                        }
                        //添加dataitem
                        foreach (var prop in tempProps)
                        {
                            var result = S7PLCHelper.AddressAnalyze(prop.Address);
                            if (!result.Success)
                            {
                                return BllResultFactory.Error($"地址解析错误，属性明细Id：{prop.Id}，详情：{result.Msg}");
                            }
                            var tran = result.Data;
                            var temp = new byte[256];
                            if (s7MultiVar.Add(tran.Area, tran.DataType, tran.DataNumber, tran.DataOffset, tran.DataAmount, ref temp) == false)
                            {
                                return BllResultFactory.Error("添加地址失败");
                            }
                            list.Add(temp);
                        }
                        GetLock(item);
                        int flag = s7MultiVar.Read();
                        ResetLock(item);
                        if (flag != 0)
                        {
                            return BllResultFactory.Error($"读取错误：{client.ErrorText(flag)}");
                        }
                        var tempp = s7MultiVar.Results.ToList().Where(t => t != 0).ToList();
                        if (tempp.Count > 0)
                        {
                            return BllResultFactory.Error($"存在读取错误:{client.ErrorText(tempp[0])}");
                        }
                        s7MultiVar.Clear();
                    };

                    //转换读取后的值
                    Console.WriteLine(DateTime.Now);
                    for (int g = 0; g < props.Count; g++)
                    {
                        if (!Enum.TryParse<PLCDataType>(props[g].EquipmentTypeTemplate.DataType, out PLCDataType dataType))
                        {
                            return BllResultFactory.Error($"未识别的数据类型{props[g].EquipmentTypeTemplate.DataType}");
                        }
                        var result = S7PLCHelper.TransferBufferToString(dataType, list[g]);
                        if (result.Success)
                        {
                            props[g].Value = result.Data;
                            Console.WriteLine(props[g].EquipmentTypeTemplateCode + "：" + props[g].Value);
                        }
                        else
                        {
                            return BllResultFactory.Error($"读取数据出错：{result.Msg}");
                        }
                    }
                    s7MultiVar.Clear();
                    Console.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error($"读取出现异常：" + ex.ToString());
            }

            return BllResultFactory.Sucess();
        }

        public BllResult Write(EquipmentProp equipmentProp)
        {
            return Writes(new List<EquipmentProp>() { equipmentProp });
        }

        public BllResult Writes(List<EquipmentProp> equipmentProps)
        {
            if (equipmentProps == null || equipmentProps.Count() == 0)
            {
                return BllResultFactory.Error($"未传递属性");
            }
            var ips = equipmentProps.Select(t => t.Equipment).Select(t => t.IP).Distinct().ToList();
            foreach (var item in ips)
            {
                var temp = S7PLCHelpers.FirstOrDefault(t => t.PLCIP == item);
                //确定属性中对应的PLC是否存在连接
                if (!temp.S7Client.Connected)
                {
                    return BllResultFactory.Error($"存在IP为{item}的PLC未连接");
                }
            }

            try
            {
                foreach (var item in ips)
                {
                    var props = equipmentProps.Where(t => t.Equipment.IP == item).ToList();
                    var client = S7PLCHelpers.FirstOrDefault(t => t.PLCIP == item).S7Client;
                    S7MultiVar s7MultiVar = new S7MultiVar(client);
                    int count = 20;
                    int i = 0;
                    while (true)
                    {
                        var tempProps = props.Skip(i).Take(count);
                        i += count;
                        if (tempProps.Count() == 0)
                        {
                            break;
                        }
                        //添加dataitem
                        foreach (var prop in tempProps)
                        {
                            var result = S7PLCHelper.AddressAnalyze(prop.Address);
                            if (!result.Success)
                            {
                                return BllResultFactory.Error($"地址解析错误，属性明细Id：{prop.Id}，详情：{result.Msg}");
                            }
                            var tran = result.Data;
                            if (!Enum.TryParse<PLCDataType>(prop.EquipmentTypeTemplate.DataType, out PLCDataType dataType))
                            {
                                return BllResultFactory.Error($"未识别的数据类型{prop.EquipmentTypeTemplate.DataType}");
                            }
                            var temp = S7PLCHelper.TransferStringToBuffer(dataType, prop.Value.PadRight(20, ' '));
                            if (!temp.Success)
                            {
                                return BllResultFactory.Error($"转换数据出错：{temp.Msg}");
                            }
                            var tempBytes = temp.Data;
                            if (s7MultiVar.Add(tran.Area, tran.DataType, tran.DataNumber, tran.DataOffset, tran.DataAmount, ref tempBytes) == false)
                            {
                                return BllResultFactory.Error("添加地址失败");
                            }
                        }
                        GetLock(item);
                        int flag = s7MultiVar.Write();
                        ResetLock(item);
                        if (flag != 0)
                        {
                            return BllResultFactory.Error($"写入错误：{client.ErrorText(flag)}");
                        }
                        var tempp = s7MultiVar.Results.ToList().Where(t => t != 0).ToList();
                        if (tempp.Count > 0)
                        {
                            return BllResultFactory.Error($"存在写入错误:{client.ErrorText(tempp[0])}");
                        }
                        s7MultiVar.Clear();
                    };
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error($"写入出现异常：{ex.ToString() }");
            }

            return BllResultFactory.Sucess();
        }


    }
}
