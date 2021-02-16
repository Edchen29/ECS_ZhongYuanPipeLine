using HHECS.Model.BllModel;
using HHECS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.PLCHelper.Interfaces
{
    /// <summary>
    /// PLC顶层接口
    /// </summary>
    public interface IPLC
    {
        /// <summary>
        /// 连接，通常指定PLC的初始化请在连接中进行
        /// </summary>
        /// <returns></returns>
        BllResult Connect();

        /// <summary>
        /// 连接，连接指定IP的PLC
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        BllResult Connect(string ip);

        /// <summary>
        /// 断开,通常在断开之前请先停止逻辑处理，延迟后再调用
        /// </summary>
        /// <returns></returns>
        BllResult DisConnect();

        /// <summary>
        /// 断开，断开指定IP的PLC连接
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        BllResult DisConnect(string ip);

        /// <summary>
        /// 获取连接状态
        /// hack:对于使用OPC实现，此链接状态指示为client与OPC服务器的连接状态；所以使用OPC的情况下需要额外检测读写是否正确。
        /// </summary>
        /// <returns></returns>
        BllResult GetConnectStatus();

        /// <summary>
        /// 获取指定IPPlc的连接状态
        /// hack:对于使用OPC实现，此链接状态指示为client与OPC服务器的连接状态；所以使用OPC的情况下需要额外检测读写是否正确。
        /// </summary>
        /// <returns></returns>
        BllResult GetConnectStatus(string plcIp);

        /// <summary>
        /// 读取地址
        /// </summary>
        /// <param name="equipmentProps"></param>
        /// <returns></returns>
        BllResult Reads(List<EquipmentProp> equipmentProps);

        /// <summary>
        /// 写入地址
        /// </summary>
        /// <param name="equipmentProps"></param>
        /// <returns></returns>
        BllResult Writes(List<EquipmentProp> equipmentProps);

        /// <summary>
        /// 读取单个地址
        /// </summary>
        /// <param name="equipmentProp"></param>
        /// <returns></returns>
        BllResult Read(EquipmentProp equipmentProp);

        /// <summary>
        /// 写入单个地址
        /// </summary>
        /// <param name="equipmentProp"></param>
        /// <returns></returns>
        BllResult Write(EquipmentProp equipmentProp);

    }
}
