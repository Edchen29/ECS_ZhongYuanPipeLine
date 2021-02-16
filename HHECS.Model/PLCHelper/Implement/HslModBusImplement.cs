using HHECS.Model.BllModel;
using HHECS.Model.Entities;
using HHECS.Model.PLCHelper.PLCComponent.HslComponent;
using HslCommunication;
using HslCommunication.ModBus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HHECS.Model.PLCHelper.Implement
{
    public class HslModBusImplement
    {

        #region 属性

        private List<ModbusTcpNet> modbusTcpNets = new List<ModbusTcpNet>();

        /// <summary>
        /// 缓存已被解析的地址
        /// </summary>
        private List<HslModbusDataEntity> _hslModbusDataEntities { get; set; } = new List<HslModbusDataEntity>();

        #endregion


        /// <summary>
        /// 初始化连接client，传递Modbus构建类型list
        /// </summary>
        /// <param name="siemensPLCBuildModels"></param>
        public HslModBusImplement(List<ModbusBuildModel> modbusBuildModels)
        {
            if (modbusBuildModels == null || modbusBuildModels.Count == 0)
            {
                throw new ArgumentNullException("请传递参数");
            }
            try
            {
                foreach (var item in modbusBuildModels)
                {
                    var modbusTcpNet = new ModbusTcpNet(item.IP, item.Port, item.Station);
                    modbusTcpNet.AddressStartWithZero = false;
                    modbusTcpNets.Add(modbusTcpNet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 对内部关联的所有Modbus进行连接,存在连接失败，则连接失败
        /// </summary>
        /// <returns></returns>
        public BllResult Connect()
        {
            foreach (var item in modbusTcpNets)
            {
                var result = item.ConnectServer();
                if (!result.IsSuccess)
                {
                    //连接失败，则失败
                    return BllResultFactory.Error($"连接Modbus：{item.IpAddress}失败：{result.Message}");
                }
            }
            return BllResultFactory.Sucess("连接成功");
        }

        /// <summary>
        /// 连接指定IP的Modbus
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public BllResult Connect(string ip)
        {
            var client = modbusTcpNets.Find(t => t.IpAddress == ip);
            if (client == null)
            {
                return BllResultFactory.Error($"未配置IP为{ip}的Modbus");
            }
            var result = client.ConnectServer();
            if (result.IsSuccess)
            {
                return BllResultFactory.Sucess($"连接Modbus：{ip}成功");
            }
            else
            {
                return BllResultFactory.Error($"连接Modbus：{ip}失败：{result.Message}");
            }
        }

        /// <summary>
        /// 断开所有Modbus
        /// </summary>
        /// <returns></returns>
        public BllResult DisConnect()
        {
            foreach (var item in modbusTcpNets)
            {
                //基本不需要查看关闭成功与否
                item.ConnectClose();
            }
            return BllResultFactory.Sucess("关闭成功");
        }

        /// <summary>
        /// 断开指定IP的Modbus
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public BllResult DisConnect(string ip)
        {
            var client = modbusTcpNets.Find(t => t.IpAddress == ip);
            if (client == null)
            {
                return BllResultFactory.Error($"未配置IP为{ip}的Modbus");
            }
            client.ConnectClose();
            return BllResultFactory.Sucess($"关闭Modbus:{ip}成功");
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
        /// <param name="ModbusIp"></param>
        /// <returns></returns>
        public BllResult GetConnectStatus(string ModbusIp)
        {
            if (HHECS.Model.Common.CommonForPing.PingTest(ModbusIp))
            {
                return BllResultFactory.Sucess();
            }
            else
            {
                return BllResultFactory.Error("Modbus地址Ping不通");
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
                    //此IP对应的Modbus
                    var client = modbusTcpNets.Find(t => t.IpAddress == ip);

                    //此IP下的属性
                    var props = equipmentProps.Where(t => t.Equipment.IP == ip).ToList();

                    //缓存中已经存在的
                    List<HslModbusDataEntity> list = _hslModbusDataEntities.Where(t => props.Exists(a => a.Id == t.AddressId)).OrderBy(t => t.Address).ToList();

                    string address = null;
                    foreach (var item in list)
                    {
                        if (item.Address == address)
                        {
                            continue;
                        }
                        address = item.Address;

                        var result = client.Read(address, item.Length);
                        if (!result.IsSuccess)
                        {
                            return BllResultFactory.Error($"读取错误，IP:{ip}，参考信息：{result.Message}");
                        }
                        item.Buffer = result.Content;
                        var transResult = ModbusHelper.TransferBufferToString(item);
                        if (!transResult.Success)
                        {
                            return BllResultFactory.Error($"读取时，IP:{ip},地址{address}，转换数据失败:{transResult.Msg}");
                        }
                        if (item.DataType == PLCComponent.ModbusDataType.Bool)
                        {
                            //如果是Bool类型，需要先换为二进制，然后读取每一位判断是0，还是1
                            var returnValue = Convert.ToString(short.Parse(transResult.Data), 2);
                            foreach (var temp in list.Where(t => t.Address == address).OrderBy(t => t.BitOffset))
                            {
                                var prop = props.Find(t => t.Id == temp.AddressId);
                                prop.Value = returnValue[temp.BitOffset] == '1' ? true.ToString() : false.ToString();
                            }
                        }
                        else
                        {
                            var prop = props.Find(t => t.Id == item.AddressId);
                            prop.Value = transResult.Data;
                        }                        
                    }
                    return BllResultFactory.Sucess("读取成功");
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
                //此IP对应的Modbus
                var client = modbusTcpNets.Find(t => t.IpAddress == ip);

                //此IP下的属性
                var props = equipmentProps.Where(t => t.Equipment.IP == ip).ToList();

                List<HslModbusDataEntity> list = new List<HslModbusDataEntity>();
                //缓存中已经存在的
                list.AddRange(_hslModbusDataEntities.Where(t => props.Exists(a => a.Id == t.AddressId)).OrderBy(t => t.Address).ToList());

                string address = null;
                foreach (var item in list)
                {
                    //多个boole会共用第一个地址，一个地址只写一次
                    if (item.Address == address)
                    {
                        continue;
                    }
                    address = item.Address;
                    string propValue = null;
                    if (item.DataType == PLCComponent.ModbusDataType.Bool)
                    {
                        //在属性中找出所有的bool，然后合并成short再写入寄存器
                        char[] tampValue = new char [16] {'0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0'};
                        foreach (var temp in list.Where(t => t.Address == item.Address).OrderBy(t => t.BitOffset))
                        {
                            tampValue[temp.BitOffset] = props.Find(t => t.Id == temp.AddressId).Value == "True" ? '1' : '0';
                        }
                        propValue = Convert.ToInt16(new string(tampValue), 2).ToString();
                    }
                    else
                    {
                        var prop = props.Find(t => t.Id == item.AddressId);
                        propValue = prop.Value;
                    }
                    //转化位byte数组
                    var transResult = ModbusHelper.TransferStringToBuffer(item.DataType, propValue);
                    if (!transResult.Success)
                    {
                        return BllResultFactory.Error($"转换到Modbus数据类型失败，IP:{ip},地址：{item.Address}:{transResult.Msg}");
                    }
                    OperateResult result =  client.Write(item.Address, transResult.Data);
                    if (!result.IsSuccess)
                    {
                        return BllResultFactory.Error($"写入ip:{ip},地址:{item.Address}，数据：{propValue}失败：{result.Message}");
                    }
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
            //是否存在没有对应的Modbus
            var temp = ips.Where(t => !modbusTcpNets.Exists(a => a.IpAddress == t)).ToArray();
            if (temp.Length > 0)
            {
                return BllResultFactory.Error<List<string>>($"存在IP为{string.Join(",", temp)}没有对应的Modbus实例，请检查Modbus的IP配置");
            }

            //缓存中未存在的属性
            var tempProps = equipmentProps.Where(t => !_hslModbusDataEntities.Exists(a => a.AddressId == t.Id)).ToList();
            //解析属性并加入缓存
            foreach (var item in tempProps)
            {
                var result = ModbusHelper.ParseAddress(item);
                if (!result.Success)
                {
                    return BllResultFactory.Error<List<string>>($"解析地址错误：{result.Msg}");
                }
                _hslModbusDataEntities.Add(result.Data);
            }
            return BllResultFactory.Sucess(ips);
        }
    }
}
