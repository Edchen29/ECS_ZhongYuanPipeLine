using HHECS.Bll;
using HHECS.Model;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.OPC
{
    public class OPCHelp
    {
        string PCIP;
        public OPCServer s7 = new OPCServer();
        public OPCGroup s7Group;

        /// <summary>
        /// PC站IP地址
        /// </summary>
        /// <param name="OPCIP"></param>
        public OPCHelp(string OPCIP)
        {
            PCIP = OPCIP;
        }

        /// <summary>
        /// 打开OPC连接
        /// </summary>
        /// <returns></returns>
        public bool OpenConn()
        {
            try
            {
                s7.Connect("OPC.SimaticNET", PCIP);
                if (s7.ServerState == (int)OPCServerState.OPCRunning)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <returns></returns>
        public bool CloseConn()
        {
            try
            {
                s7.Disconnect();
                if (s7.ServerState == (int)OPCServerState.OPCDisconnected)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取OPCserver当前状态，TRUE：正在运行，false：连接关闭
        /// </summary>
        /// <returns></returns>
        public bool GetConnStatus()
        {
            return s7.ServerState == (int)OPCServerState.OPCRunning ? true : false;
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
            s7.OPCGroups.DefaultGroupIsActive = Convert.ToBoolean(true);
            s7.OPCGroups.DefaultGroupDeadband = Convert.ToInt32(0);
            s7Group.UpdateRate = Convert.ToInt32(250);// 刷新率 ms  
            s7Group.IsActive = Convert.ToBoolean(true);  //设置该组为激活状态
            s7Group.IsSubscribed = Convert.ToBoolean(false);//设置该组数据为后台刷新
        }

        /// <summary>
        /// 添加项目地址
        /// </summary>
        public int AddAddr(string Addr, int ClientHandle)
        {
            int returnvalue = 0;
            if (!string.IsNullOrEmpty(Addr))
            {
                try
                {
                    OPCItem tempItem = s7Group.OPCItems.AddItem(Addr, ClientHandle);

                    //ServerHandle,这是一个重要的东西，个人理解为组中的项的索引，读取程序根据这个索引找到db块并进行读写操作。
                    //程序中应对ServerHandle与实际的变量地址已经这个变量地址所代表的意义进行一个映射。
                    returnvalue = tempItem.ServerHandle;
                }
                catch (Exception err)
                {

                }
            }
            return returnvalue;
        }

        /// <summary>
        /// 读取数据,成功返回值，失败返回null
        /// 按照 OPC 规范，数组始终以索引 1 开始。
        /// </summary>
        public Array ReadData(int[] handle)
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
                return values;
            }
            catch (Exception err)
            {
                return null;
            }
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


        #region 数据互转

        /// <summary>
        /// 数据类型转换WCS-->PLC
        /// </summary>
        /// <param name="type"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        private BllResult TansforWCSDataToAddressData(string type, string data)
        {
            try
            {
                object obj = null;
                switch (type)
                {
                    case "BYTE": obj = Convert.ToUInt16(data); break;
                    case "DWORD": obj = Convert.ToUInt32(data); break;
                    case "BOOL": obj = Convert.ToBoolean(data); break;
                    case "WORD": obj = Convert.ToUInt16(data); break;
                    case "INT": obj = Convert.ToInt16(data); break;
                    case "DINT": obj = Convert.ToInt32(data); break;
                    case "CHAR": obj = ConverHelper.StringToASCII(data); break;
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
        private BllResult<String> TransforAddressDataToWCSData(string type, object data)
        {
            string str;
            try
            {
                switch (type)
                {
                    case "BYTE":
                    case "DWORD":
                    case "INT":
                    case "DINT":
                    case "BOOL":
                    case "WORD": str = data.ToString(); break;
                    case "CHAR": str = ConverHelper.ASCIIToString((short[])data).Trim().Replace("$03", "").Replace("\u0003", "").Replace("\0", ""); break;
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

        #endregion

        /// <summary>
        /// 添加地址
        /// </summary>
        /// <param name="props"></param>
        public void AddOPCItems(List<EquipmentProp> props)
        {
            for (int i = 0; i < props.Count; i++)
            {
                var temp = props[i];
                temp.ServerHandle = this.AddAddr(temp.Address, i);
            }
        }

        /// <summary>
        /// 读取特定地址
        /// </summary>
        /// <param name="deviceAddressEntities"></param>
        public BllResult ReadAddress(List<EquipmentProp> props)
        {
            if (this == null || this.GetConnStatus() == false || props == null || props.Count == 0)
            {
                //AddLogToUI("地址读取失败,请检查通讯连接", 2);
                return BllResultFactory.Error(null, "地址读取失败,请检查通讯连接");
            }

            int[] handles = props.Select(t => t.ServerHandle).ToArray();
            var array = this.ReadData(handles);
            for (int i = 0; i < props.Count; i++)
            {
                var result = TransforAddressDataToWCSData(props[i].EquipmentTypeTemplate.DataType, array.GetValue(i + 1));
                if (!result.Success)
                {
                    return BllResultFactory.Error(null, "转换PLC数据到WCS类型错误,值：" + array.GetValue(i + 1) + " 类型:" + props[i].EquipmentTypeTemplate.DataType);
                }
                props[i].Value = result.Data;
            }
            return BllResultFactory.Sucess(null, "成功");
        }

        /// <summary>
        /// 写入PLC数据
        /// </summary>
        /// <param name="deviceAddressEntities"></param>
        /// <returns></returns>
        public BllResult WriteAddress(List<EquipmentProp> props)
        {
            if (this == null || this.GetConnStatus() == false || props == null || props.Count == 0)
            {
                LogExecute.WriteLog("写入PLC数据时，地址读取失败,请检查通讯连接", "Error");
                return BllResultFactory.Error(null, "地址读取失败,请检查通讯连接");
            }
            var serverHandel = props.Select(t => t.ServerHandle).ToArray();
            object[] values = new object[serverHandel.Count<int>()];
            for (int i = 0; i < props.Count; i++)
            {
                var result = TansforWCSDataToAddressData(props[i].EquipmentTypeTemplate.DataType, props[i].Value);
                if (!result.Success)
                {
                    LogExecute.WriteLog("写入PLC数据时，转换WCS数据到PLC类型错误,值：" + props[i].Value + " 类型:" + props[i].EquipmentTypeTemplate.DataType, "Error");
                    return BllResultFactory.Error(null, "转换WCS数据到PLC类型错误,值：" + props[i].Value + " 类型:" + props[i].EquipmentTypeTemplate.DataType);
                }
                values[i] = result.Data;
            }
            if (this.WriteData(serverHandel, values))
            {
                props.ForEach(t => {
                    if (t.EquipmentTypeTemplateCode != "WCSHeartBeat" && t.EquipmentTypeTemplateCode != "SwitchStatus")
                    {
                        LogExecute.WriteInfoLog("向PLC中写入模版：" + t.EquipmentTypeTemplateCode + "，地址：" + t.Address + "，值：" + t.Value);
                    }
                });
                if (props.Exists(t => t.EquipmentTypeTemplateCode != "WCSHeartBeat" && t.EquipmentTypeTemplateCode != "SwitchStatus"))
                {
                    LogExecute.WriteInfoLog("");
                }
                return BllResultFactory.Sucess(null, "成功");
            }
            else
            {
                return BllResultFactory.Error(null, "写入失败！");
            }

        }
    }
}
