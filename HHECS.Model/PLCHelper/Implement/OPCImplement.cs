using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.PLCHelper.Interfaces;
using HHECS.Model.PLCHelper.PLCComponent;
using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.PLCHelper.Implement
{
    /// <summary>
    /// 封装OPC
    /// 使用此类，请先行传递OPCIP并赋值完整Equipments，然后使用连接函数
    /// </summary>
    public class OPCImplement : IPLC
    {
        public string OPCIP { get; set; }
        public OPCServer s7 = new OPCServer();
        public OPCGroup s7Group;

        /// <summary>
        /// 需要在连接前事先完整设置
        /// </summary>
        public List<Equipment> Equipments { get; set; }

        public OPCImplement(string IP)
        {
            OPCIP = IP;
        }

        /// <summary>
        /// 创建组
        /// </summary>
        public bool CreateGroup(string name)
        {
            try
            {
                s7Group = s7.OPCGroups.Add(name);
                SetGroupProperty();
            }
            catch (Exception)
            {
                //MessageBox.Show("创建组出现错误：" + err.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置组属性
        /// </summary>
        private void SetGroupProperty()
        {
            s7.OPCGroups.DefaultGroupIsActive = true;
            s7.OPCGroups.DefaultGroupDeadband = 0;
            s7Group.UpdateRate = 100;// 刷新率 ms  ，原为250，更改为100
            s7Group.IsActive = true;  //设置该组为激活状态
            s7Group.IsSubscribed = false;//设置该组数据为后台刷新
        }

        public BllResult Connect()
        {
            try
            {
                s7.Connect("OPC.SimaticNET", OPCIP);
                if (s7.ServerState == (int)OPCServerState.OPCRunning)
                {
                    //注册地址
                    Equipments.SelectMany(t => t.EquipmentProps).ToList().ForEach(t =>
                    {
                        t.Equipment = Equipments.FirstOrDefault(i => i.Id == t.EquipmentId);
                        //组合地址
                        if (!t.Address.StartsWith("S7"))
                        {
                            t.Address = $"S7:[{t.Equipment.ConnectName}]{t.Address}";
                        }
                    });
                    CreateGroup("group1");
                    AddOPCItems(Equipments.SelectMany(t => t.EquipmentProps).ToList());
                    return BllResultFactory.Sucess();
                }
                else
                {
                    return BllResultFactory.Error();
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error(ex.Message);
            }
        }

        /// <summary>
        /// 添加地址
        /// </summary>
        /// <param name="props"></param>
        public void AddOPCItems(List<EquipmentProp> props)
        {
            string[] ids = new string[props.Count() + 1];
            int[] clients = new int[props.Count() + 1];
            ids[0] = "";
            clients[0] = 0;
            for (int i = 1; i < ids.Length; i++)
            {
                ids[i] = props[i - 1].Address;
                clients[i] = i;
            }
            Array clientsIds = (Array)clients;
            Array itemsIds = (Array)ids;
            Array serverHandls;
            Array errors;
            s7Group.OPCItems.AddItems(props.Count(), ref itemsIds, ref clientsIds, out serverHandls, out errors);
            for (int i = 0; i < props.Count(); i++)
            {
                props[i].ServerHandle = Convert.ToInt32(serverHandls.GetValue(i + 1));
            }

        }

        public BllResult DisConnect()
        {
            try
            {
                s7.Disconnect();
                if (s7.ServerState == (int)OPCServerState.OPCDisconnected)
                {
                    return BllResultFactory.Sucess();
                }
                else
                {
                    return BllResultFactory.Error();
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error(ex.Message);
            }
        }

        /// <summary>
        /// 连接，指定IP,使用OPC则指定对OPC服务的连接
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public BllResult Connect(string ip)
        {
            return Connect();
        }

        /// <summary>
        /// 断开连接，指定IP，使用OPC则指定对OPC服务的断开连接
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public BllResult DisConnect(string ip)
        {
            return DisConnect();
        }

        public BllResult GetConnectStatus()
        {
            if (s7.ServerState == (int)OPCServerState.OPCRunning)
            {
                return BllResultFactory.Sucess();
            }
            else
            {
                return BllResultFactory.Error();
            }
        }

        public BllResult GetConnectStatus(string plcIp)
        {
            if (s7.ServerState == (int)OPCServerState.OPCRunning)
            {
                return BllResultFactory.Sucess();
            }
            else
            {
                return BllResultFactory.Error();
            }
        }

        public BllResult Read(EquipmentProp equipmentProp)
        {
            return Reads(new List<EquipmentProp>() { equipmentProp });
        }

        public BllResult Reads(List<EquipmentProp> equipmentProps)
        {
            if (equipmentProps == null || equipmentProps.Count == 0)
            {
                return BllResultFactory.Error(null, "无地址传入");
            }
            if (this == null || this.GetConnectStatus().Success == false)
            {
                return BllResultFactory.Error(null, "地址读取失败,请检查通讯连接");
            }

            int[] handles = equipmentProps.Select(t => t.ServerHandle).ToArray();
            return this.ReadData(handles, equipmentProps);
        }

        /// <summary>
        /// 读取数据,成功返回值，失败返回null
        /// 按照 OPC 规范，数组始终以索引 1 开始。
        /// </summary>
        public BllResult ReadData(int[] handle, List<EquipmentProp> equipmentProps)
        {
            try
            {
                int count = handle.Length;
                int[] temp = new int[count + 1];
                temp[0] = 0;
                for (int i = 1; i < temp.Length; i++)
                {
                    temp[i] = handle[i - 1];
                }
                Array serverHandles = (Array)temp;
                Array values;
                Array Errors;
                object Qualities;
                object TimeStamps;
                //OPCAutomation.OPCDataSource.OPCCache;
                s7Group.SyncRead(1, count, ref serverHandles, out values, out Errors, out Qualities, out TimeStamps);
                var a = (Array)Qualities;
                for (int i = 1; i < serverHandles.Length; i++)
                {
                    var prop = equipmentProps.Find(t => t.ServerHandle == (int)serverHandles.GetValue(i));
                    if ((Int16)a.GetValue(i) == 0)
                    {
                        //读取失败，获取默认值
                        prop.Value = GetDefaultValue(prop);

                    }
                    else
                    {
                        //读取成功
                        if (!Enum.TryParse(prop.EquipmentTypeTemplate.DataType, out PLCDataType dataType))
                        {
                            return BllResultFactory.Error($"未识别的数据类型：{prop.EquipmentTypeTemplate.DataType}");
                        }
                        else
                        {
                            var result = TransforAddressDataToWCSData(dataType, values.GetValue(i));
                            if (!result.Success)
                            {
                                return BllResultFactory.Error(null, $"读取PLC地址时：属性{prop.EquipmentTypeTemplate}；错误消息：{result.Msg}");
                            }
                            prop.Value = result.Data;
                        }
                    }
                }
                return BllResultFactory.Sucess();
            }
            catch (Exception err)
            {
                return BllResultFactory.Error(err.Message);
            }
        }

        public BllResult Write(EquipmentProp equipmentProp)
        {
            return Writes(new List<EquipmentProp>() { equipmentProp });
        }

        public BllResult Writes(List<EquipmentProp> equipmentProps)
        {
            if (equipmentProps == null || equipmentProps.Count == 0)
            {
                return BllResultFactory.Error(null, "无地址传入");
            }
            if (this == null || GetConnectStatus().Success == false)
            {
                return BllResultFactory.Error(null, "地址读取失败,请检查通讯连接");
            }
            var serverHandel = equipmentProps.Select(t => t.ServerHandle).ToArray();
            object[] values = new object[serverHandel.Count<int>()];
            for (int i = 0; i < equipmentProps.Count; i++)
            {
                if (!Enum.TryParse(equipmentProps[i].EquipmentTypeTemplate.DataType, out PLCDataType dataType))
                {
                    return BllResultFactory.Error($"未识别的数据类型：{equipmentProps[i].EquipmentTypeTemplate.DataType}");
                }
                else
                {
                    var result = TansforWCSDataToAddressData(dataType, equipmentProps[i].Value);
                    if (!result.Success)
                    {
                        return BllResultFactory.Error(null, "转换WCS数据到PLC类型错误,值：" + equipmentProps[i].Value + " 类型:" + equipmentProps[i].EquipmentTypeTemplate.DataType);
                    }
                    values[i] = result.Data;
                }

            }
            if (this.WriteData(serverHandel, values))
            {
                return BllResultFactory.Sucess(null, "成功");
            }
            else
            {
                return BllResultFactory.Error(null, "写入失败！");
            }
        }

        /// <summary>
        /// 获取默认值，注意，当获取失败时会返回error
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        private string GetDefaultValue(EquipmentProp prop)
        {
            string str = "";
            if (!Enum.TryParse(prop.EquipmentTypeTemplate.DataType, out PLCDataType dataType))
            {
                return "Error";
            }
            switch (dataType)
            {
                case PLCDataType.BYTE:
                case PLCDataType.DWORD:
                case PLCDataType.WORD:
                case PLCDataType.INT:
                case PLCDataType.DINT:
                    str = "0"; break;
                case PLCDataType.BOOL:
                    str = "False"; break;
                case PLCDataType.CHAR:
                default:
                    str = "";
                    break;
            }
            return str;
        }

        /// <summary>
        /// 写入数据,handle,索引值数组,value对应值数组
        /// 按照 OPC 规范，数组始终以索引 1 开始。
        /// </summary>
        public bool WriteData(int[] handle, object[] value)
        {
            try
            {
                int[] temp = new int[handle.Length + 1];
                temp[0] = 0;
                object[] temp1 = new object[handle.Length + 1];
                temp1[0] = "";
                for (int i = 1; i < temp.Length; i++)
                {
                    temp[i] = handle[i - 1];
                    temp1[i] = value[i - 1];
                }
                Array serverHandles = (Array)temp;
                Array values = (Array)temp1;
                Array Errors;
                //OPCAutomation.OPCDataSource.OPCCache;
                s7Group.SyncWrite(handle.Length, ref serverHandles, ref values, out Errors);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 数据类型转换WCS-->PLC
        /// </summary>
        /// <param name="type"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        private BllResult TansforWCSDataToAddressData(PLCDataType type, string data)
        {
            try
            {
                object obj = null;
                switch (type)
                {
                    case PLCDataType.BYTE:
                        obj = Convert.ToInt32(data); break;
                    case PLCDataType.BOOL:
                        obj = Convert.ToBoolean(data); break;
                    case PLCDataType.DWORD:
                        obj = Convert.ToUInt32(data); break;
                    case PLCDataType.WORD:
                        obj = Convert.ToUInt16(data); break;
                    case PLCDataType.INT:
                        obj = Convert.ToInt16(data); break;
                    case PLCDataType.DINT:
                        obj = Convert.ToInt32(data); break;
                    case PLCDataType.CHAR:
                        obj = ConverHelper.StringToASCII(data); break;
                    default:
                        obj = data;
                        break;
                }
                return BllResultFactory.Sucess(obj, "成功");
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error(null, "WCS到PLC数据转换出现异常,值：" + data + " 目标类型:" + type + " 异常：" + ex.ToString());
            }
        }

        /// <summary>
        /// 数据类型转换PLC-->WCS
        /// </summary>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private BllResult<String> TransforAddressDataToWCSData(PLCDataType type, object data)
        {
            string str;
            try
            {
                switch (type)
                {
                    case PLCDataType.BYTE:
                        int i = Convert.ToInt32(data);
                        str = i.ToString();
                        break;
                    case PLCDataType.BOOL:
                    case PLCDataType.DWORD:
                    case PLCDataType.WORD:
                    case PLCDataType.INT:
                    case PLCDataType.DINT:
                        str = data.ToString(); break;
                    case PLCDataType.CHAR:
                        str = ConverHelper.ASCIIToString((short[])data).Trim().Replace("$03", "").Replace("\u0003", "").Replace("\0", ""); break;
                    default:
                        str = data.ToString();
                        break;
                }

                return BllResultFactory<string>.Sucess(str, "成功");
            }
            catch (Exception ex)
            {
                return BllResultFactory<string>.Error(null, "PLC到WCS数据转换出现异常,值：" + data + " 目标类型:" + type + " 异常：" + ex.ToString());
            }
        }
    }
}
