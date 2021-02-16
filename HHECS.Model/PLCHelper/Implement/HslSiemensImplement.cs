using HHECS.Model.BllModel;
using HHECS.Model.Entities;
using HHECS.Model.PLCHelper.Interfaces;
using HHECS.Model.PLCHelper.PLCComponent.HslComponent;
using HslCommunication;
using HslCommunication.Profinet.Siemens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.PLCHelper.Implement
{
    /// <summary>
    /// 使用Hsl开源组件（7.0.1）实现的西门子访问类
    /// 提供对DB与M区读写的支持
    /// 默认string以20个为读写标准
    /// </summary>
    public class HslSiemensImplement : IPLC
    {
        #region 属性

        private List<SiemensS7Net> siemensTcpNets = new List<SiemensS7Net>();

        /// <summary>
        /// 读取限制，超过此值使用批量读取;
        /// </summary>
        private const int READLIMIT = 10;

        /// <summary>
        /// 缓存已被解析的地址
        /// </summary>
        private List<HslSiemensDataEntity> _hslSiemensDataEntities { get; set; } = new List<HslSiemensDataEntity>();

        #endregion


        /// <summary>
        /// 初始化连接client，传递PLC构建类型list
        /// </summary>
        /// <param name="siemensPLCS"></param>
        /// <exception cref="ArgumentNullException">未传递参数</exception>
        /// <exception cref="Exception">其他可能异常</exception>
        public HslSiemensImplement(List<SiemensPLCBuildModel> siemensPLCBuildModels)
        {
            if (siemensPLCBuildModels == null || siemensPLCBuildModels.Count == 0)
            {
                throw new ArgumentNullException("请传递参数");
            }
            try
            {
                foreach (var item in siemensPLCBuildModels)
                {
                    var siemensTcpNet = new SiemensS7Net(item.SiemensPLCS);
                    siemensTcpNet.IpAddress = item.IP;
                    siemensTcpNet.Port = item.Port;
                    siemensTcpNet.Rack = byte.Parse(item.Rack.ToString());
                    siemensTcpNet.Slot = byte.Parse(item.Slot.ToString());
                    //长链接模式；后面的每次请求都共享一个通道
                    //siemensTcpNet.ConnectServer();
                    siemensTcpNets.Add(siemensTcpNet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 对内部关联的所有PLC进行连接,存在连接失败，则连接失败
        /// </summary>
        /// <returns></returns>
        public BllResult Connect()
        {
            foreach (var item in siemensTcpNets)
            {
                var result = item.ConnectServer();
                if (!result.IsSuccess)
                {
                    //连接失败，则失败
                    return BllResultFactory.Error($"连接PLC：{item.IpAddress}失败：{result.Message}");
                }
            }
            return BllResultFactory.Sucess("连接成功");
        }

        /// <summary>
        /// 连接指定IP的PLC
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public BllResult Connect(string ip)
        {
            var client = siemensTcpNets.Find(t => t.IpAddress == ip);
            if (client == null)
            {
                return BllResultFactory.Error($"未配置IP为{ip}的PLC");
            }
            var result = client.ConnectServer();
            if (result.IsSuccess)
            {
                return BllResultFactory.Sucess($"连接PLC：{ip}成功");
            }
            else
            {
                return BllResultFactory.Error($"连接PLC：{ip}失败：{result.Message}");
            }
        }

        /// <summary>
        /// 断开所有PLC
        /// </summary>
        /// <returns></returns>
        public BllResult DisConnect()
        {
            foreach (var item in siemensTcpNets)
            {
                //基本不需要查看关闭成功与否
                item.ConnectClose();
            }
            return BllResultFactory.Sucess("关闭成功");
        }

        /// <summary>
        /// 断开指定IP的PLC
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public BllResult DisConnect(string ip)
        {
            var client = siemensTcpNets.Find(t => t.IpAddress == ip);
            if (client == null)
            {
                return BllResultFactory.Error($"未配置IP为{ip}的PLC");
            }
            client.ConnectClose();
            return BllResultFactory.Sucess($"关闭PLC:{ip}成功");
        }

        /// <summary>
        /// 获取连接状态
        /// 由于HSL不支持状态访问，所以这里直接返回成功，需要通过读写函数的返回值判断连接是否断开
        /// </summary>
        /// <returns></returns>
        public BllResult GetConnectStatus()
        {
            return BllResultFactory.Sucess();
        }

        /// <summary>
        /// 获取指定连接状态
        /// 由于HSL不支持状态访问，所以这里直接Ping
        /// </summary>
        /// <param name="plcIp"></param>
        /// <returns></returns>
        public BllResult GetConnectStatus(string plcIp)
        {
            if (HHECS.Model.Common.CommonForPing.PingTest(plcIp))
            {
                return BllResultFactory.Sucess();
            }
            else
            {
                return BllResultFactory.Error("PLC地址Ping不通");
            }
        }

        /// <summary>
        /// 读取单个属性
        /// </summary>
        /// <param name="equipmentProp"></param>
        /// <returns></returns>
        public BllResult Read(EquipmentProp equipmentProp)
        {
            return Reads(new List<EquipmentProp>() { equipmentProp });
        }

        /// <summary>
        /// 读取多个属性
        /// </summary>
        /// <param name="equipmentProps"></param>
        /// <returns></returns>
        public BllResult Reads(List<EquipmentProp> equipmentProps)
        {
            var valiResult = Validation(equipmentProps);
            if (!valiResult.Success)
            {
                return BllResultFactory.Error(valiResult.Msg);
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var ips = valiResult.Data;
            List<Task<BllResult>> tasks = new List<Task<BllResult>>();


            foreach (var ip in ips)
            {
                var task = Task.Run<BllResult>(() =>
                {
                    //此IP对应的PLC
                    var client = siemensTcpNets.Find(t => t.IpAddress == ip);

                    //此IP下的属性
                    var props = equipmentProps.Where(t => t.Equipment.IP == ip).ToList();

                    List<HslSiemensDataEntity> list = new List<HslSiemensDataEntity>();
                    //缓存中已经存在的
                    list.AddRange(_hslSiemensDataEntities.Where(t => props.Exists(a => a.Id == t.OPCAddressId)).ToList());
                    if (props.Count > READLIMIT)
                    {
                        //进行批量读取
                        //构造地址数组与对应字节长度数据
                        var adds = new List<string>();
                        var sizes = new List<ushort>();
                        foreach (var item in list)
                        {
                            adds.Add(item.Address);
                            sizes.Add(item.ByteSize);
                        }
                        var result = client.Read(adds.ToArray(), sizes.ToArray());
                        if (!result.IsSuccess)
                        {
                            return BllResultFactory.Error($"读取错误，IP:{ip}，参考信息：{result.Message}");
                        }
                        //获取读取后的数组结果
                        var bytes = result.Content.ToList();
                        foreach (var item in list)
                        {
                            //此数据的buffer
                            var buffer = bytes.Take(item.ByteSize);
                            if (buffer.Count() != (int)item.ByteSize)
                            {
                                //如果没有提取出等量的字节
                                return BllResultFactory.Error($"获取的字节序列个数与属性需求字节个数不匹配");
                            }
                            //剩余待解析
                            item.Buffer = buffer.ToArray();
                            var prop = props.Find(t => t.Id == item.OPCAddressId);
                            bytes = bytes.Skip(item.ByteSize).ToList();
                            var transResult = SiemensHelper.TransferBufferToString(item);
                            if (!transResult.Success)
                            {
                                return BllResultFactory.Error($"读取时，IP:{ip},地址{prop.Address}，转换数据失败:{transResult.Msg}");
                            }
                            prop.Value = transResult.Data;
                        }
                        return BllResultFactory.Sucess("读取成功");

                    }
                    else
                    {
                        //进行单个读取
                        foreach (var item in list)
                        {
                            var prop = props.Find(t => t.Id == item.OPCAddressId);
                            switch (item.DataType)
                            {
                                case PLCComponent.PLCDataType.BYTE:
                                    var result = client.ReadByte(item.Address);
                                    if (!result.IsSuccess)
                                    {
                                        return BllResultFactory.Error($"读取IP:{ip},地址{prop.Address}发生错误：{result.Message}");
                                    }
                                    prop.Value = ((int)result.Content).ToString();
                                    break;
                                case PLCComponent.PLCDataType.BOOL:
                                    var result2 = client.ReadBool(item.AddressX);
                                    if (!result2.IsSuccess)
                                    {
                                        return BllResultFactory.Error($"读取IP:{ip},地址{prop.Address}发生错误：{result2.Message}");
                                    }
                                    prop.Value = result2.Content.ToString();
                                    break;
                                case PLCComponent.PLCDataType.DWORD:
                                    var result3 = client.ReadUInt32(item.Address);
                                    if (!result3.IsSuccess)
                                    {
                                        return BllResultFactory.Error($"读取IP:{ip},地址{prop.Address}发生错误：{result3.Message}");
                                    }
                                    prop.Value = result3.Content.ToString();
                                    break;
                                case PLCComponent.PLCDataType.WORD:
                                    var result4 = client.ReadUInt16(item.Address);
                                    if (!result4.IsSuccess)
                                    {
                                        return BllResultFactory.Error($"读取IP:{ip},地址{prop.Address}发生错误：{result4.Message}");
                                    }
                                    prop.Value = result4.Content.ToString();
                                    break;
                                case PLCComponent.PLCDataType.INT:
                                    var result5 = client.ReadInt16(item.Address);
                                    if (!result5.IsSuccess)
                                    {
                                        return BllResultFactory.Error($"读取IP:{ip},地址{prop.Address}发生错误：{result5.Message}");
                                    }
                                    prop.Value = result5.Content.ToString();
                                    break;
                                case PLCComponent.PLCDataType.DINT:
                                    var result6 = client.ReadInt32(item.Address);
                                    if (!result6.IsSuccess)
                                    {
                                        return BllResultFactory.Error($"读取IP:{ip},地址{prop.Address}发生错误：{result6.Message}");
                                    }
                                    prop.Value = result6.Content.ToString();
                                    break;
                                case PLCComponent.PLCDataType.CHAR:
                                    var result7 = client.ReadString(item.Address, item.ByteSize);
                                    if (!result7.IsSuccess)
                                    {
                                        return BllResultFactory.Error($"读取IP:{ip},地址{prop.Address}发生错误：{result7.Message}");
                                    }
                                    prop.Value = result7.Content.ToString();
                                    break;
                                default:
                                    return BllResultFactory.Error($"未识别的数据类型:{item.DataType}");
                            }
                        }
                        return BllResultFactory.Sucess("读取成功");
                    }
                });
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());
            var errors = tasks.Where(t => t.Result.Success != true).ToList();
            if (errors.Count > 0)
            {
                return BllResultFactory.Error($"存在读取错误：{string.Join(";", errors.Select(t => t.Result.Msg).ToList())}");
            }
            stopwatch.Stop();
            long runtime = stopwatch.ElapsedMilliseconds;
            return BllResultFactory.Sucess("读取成功");
        }

        /// <summary>
        /// 写入单个属性
        /// </summary>
        /// <param name="equipmentProp"></param>
        /// <returns></returns>
        public BllResult Write(EquipmentProp equipmentProp)
        {
            return Writes(new List<EquipmentProp>() { equipmentProp });
        }

        /// <summary>
        /// 写入多个属性
        /// </summary>
        /// <param name="equipmentProps"></param>
        /// <returns></returns>
        public BllResult Writes(List<EquipmentProp> equipmentProps)
        {
            var valiResult = Validation(equipmentProps);
            if (!valiResult.Success)
            {
                return BllResultFactory.Error(valiResult.Msg);
            }
            var ips = valiResult.Data;
            foreach (var ip in ips)
            {
                //此IP对应的PLC
                var client = siemensTcpNets.Find(t => t.IpAddress == ip);

                //此IP下的属性
                var props = equipmentProps.Where(t => t.Equipment.IP == ip).ToList();

                List<HslSiemensDataEntity> list = new List<HslSiemensDataEntity>();

                //缓存中已经存在的，改为这样写是为了保持和equipmentProps一样的顺序
                foreach (var prop in props)
                {
                    var item = _hslSiemensDataEntities.FirstOrDefault(t => t.OPCAddressId == prop.Id);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }

                ////缓存中已经存在的
                //list.AddRange(_hslSiemensDataEntities.Where(t => props.Exists(a => a.Id == t.OPCAddressId)).ToList());

                //对于写入，由于地址可能不连续且写入地址不多，会导致覆盖问题，这里不使用批量写入
                foreach (var item in list)
                {
                    var prop = props.Find(t => t.Id == item.OPCAddressId);
                    var transResult = SiemensHelper.TransferStringToBuffer(item.DataType, prop.Value);
                    if (!transResult.Success)
                    {
                        return BllResultFactory.Error($"转换到PLC数据类型失败，IP:{ip},设备：[{prop.Equipment.Name}]，模板名称：[{prop.EquipmentTypeTemplate.Name}]，模板属性编码：[{prop.EquipmentTypeTemplate.Code}]，属性标识[{prop.Id}]，属性地址:[{prop.Address}]，数据：[{prop.Value}]:{transResult.Msg}");
                    }
                    var buffer = transResult.Data;
                    //如果不是bool，我们都是写字节，如果是bool，我们需要写位，这个调用具体方法实现
                    //浮点型暂不考虑
                    OperateResult result = null;
                    if (item.DataType != PLCComponent.PLCDataType.BOOL)
                    {
                        result = client.Write(item.Address, buffer);
                    }
                    else
                    {
                        result = client.Write(item.AddressX, bool.Parse(prop.Value));
                    }
                    if (result.IsSuccess)
                    {
                        if (prop.EquipmentTypeTemplateCode != "WCSHeartBeat" && prop.EquipmentTypeTemplateCode != "WCSWrite")
                        {
                            if (prop.Equipment != null && prop.EquipmentTypeTemplate != null)
                            {
                                Common.Logger.Log($"写入PLC成功，ip:[{ip}]，设备：[{prop.Equipment.Name}]，模板名称：[{prop.EquipmentTypeTemplate.Name}]，设备属性id：[{prop.Id}]，地址:[{prop.Address}]，数据：[{prop.Value}]", Enums.LogLevel.PLC);
                            }
                            else
                            {
                                Common.Logger.Log($"写入PLC成功，ip:[{ip}],地址:[{prop.Address}]，设备属性备注：[{prop.Remark}]，数据：[{prop.Value}]，但是找不到设备属性id[{prop.Id}]对应的设备和模板，需要核查配置！", Enums.LogLevel.PLC);
                            }
                        }
                    }
                    else
                    {
                        Common.Logger.Log($"写入PLC失败：ip:[{ip}],地址:[{prop.Address}]，设备属性备注：[{prop.Remark}]，数据：[{prop.Value}]失败，需要核查配置！", Enums.LogLevel.Error);
                        return BllResultFactory.Error($"写入PLC失败:[{ip}],地址:[{prop.Address}]，数据：[{prop.Value}]：{result.Message}");
                    }
                    //switch (item.DataType)
                    //{
                    //    case PLCComponent.PLCDataType.BYTE:
                    //        var result = client.Write(item.Address,buffer);
                    //        if (!result.IsSuccess)
                    //        {
                    //            return BllResultFactory.Error($"读取IP:{ip},地址{prop.Address}发生错误：{result.Message}");
                    //        }
                    //        prop.Value = ((int)result.Content).ToString();
                    //        break;
                    //    case PLCComponent.PLCDataType.BOOL:
                    //        var result2 = client.ReadBool(item.AddressX);
                    //        if (!result2.IsSuccess)
                    //        {
                    //            return BllResultFactory.Error($"读取IP:{ip},地址{prop.Address}发生错误：{result2.Message}");
                    //        }
                    //        prop.Value = result2.Content.ToString();
                    //        break;
                    //    case PLCComponent.PLCDataType.DWORD:
                    //        var result3 = client.ReadUInt32(item.Address);
                    //        if (!result3.IsSuccess)
                    //        {
                    //            return BllResultFactory.Error($"读取IP:{ip},地址{prop.Address}发生错误：{result3.Message}");
                    //        }
                    //        prop.Value = result3.Content.ToString();
                    //        break;
                    //    case PLCComponent.PLCDataType.WORD:
                    //        var result4 = client.ReadUInt16(item.Address);
                    //        if (!result4.IsSuccess)
                    //        {
                    //            return BllResultFactory.Error($"读取IP:{ip},地址{prop.Address}发生错误：{result4.Message}");
                    //        }
                    //        prop.Value = result4.Content.ToString();
                    //        break;
                    //    case PLCComponent.PLCDataType.INT:
                    //        var result5 = client.ReadInt16(item.Address);
                    //        if (!result5.IsSuccess)
                    //        {
                    //            return BllResultFactory.Error($"读取IP:{ip},地址{prop.Address}发生错误：{result5.Message}");
                    //        }
                    //        prop.Value = result5.Content.ToString();
                    //        break;
                    //    case PLCComponent.PLCDataType.DINT:
                    //        var result6 = client.ReadInt32(item.Address);
                    //        if (!result6.IsSuccess)
                    //        {
                    //            return BllResultFactory.Error($"读取IP:{ip},地址{prop.Address}发生错误：{result6.Message}");
                    //        }
                    //        prop.Value = result6.Content.ToString();
                    //        break;
                    //    case PLCComponent.PLCDataType.CHAR:
                    //        var result7 = client.ReadString(item.Address, item.ByteSize);
                    //        if (!result7.IsSuccess)
                    //        {
                    //            return BllResultFactory.Error($"读取IP:{ip},地址{prop.Address}发生错误：{result7.Message}");
                    //        }
                    //        prop.Value = result7.Content.ToString();
                    //        break;
                    //    default:
                    //        return BllResultFactory.Error($"未识别的数据类型:{item.DataType}");
                    //}

                }
            }
            return BllResultFactory.Sucess("写入成功");

        }

        /// <summary>
        /// 调用读写函数前的校验，此过程包含了解析地址并缓存
        /// </summary>
        /// <param name="equipmentProps"></param>
        /// <returns></returns>
        private BllResult<List<string>> Validation(List<EquipmentProp> equipmentProps)
        {
            if (equipmentProps == null || equipmentProps.Count() == 0)
            {
                return BllResultFactory.Error<List<string>>($"未传递属性");
            }
            //选取设备类
            var ips = equipmentProps.Select(t => t.Equipment).Select(t => t.IP).Distinct().ToList();
            //是否存在没有对应的PLC
            var temp = ips.Where(t => !siemensTcpNets.Exists(a => a.IpAddress == t)).ToArray();
            if (temp.Length > 0)
            {
                return BllResultFactory.Error<List<string>>($"存在IP为{string.Join(",", temp)}没有对应的PLC实例，请检查PLC的IP配置");
            }

            //缓存中未存在的属性
            var tempProps = equipmentProps.Where(t => !_hslSiemensDataEntities.Exists(a => a.OPCAddressId == t.Id)).ToList();
            //解析属性并加入缓存
            foreach (var item in tempProps)
            {
                var result = SiemensHelper.ParseAddress(item);
                if (!result.Success)
                {
                    return BllResultFactory.Error<List<string>>($"解析地址错误：{result.Msg}");
                }
                _hslSiemensDataEntities.Add(result.Data);
            }
            return BllResultFactory.Sucess(ips);
        }
    }
}
