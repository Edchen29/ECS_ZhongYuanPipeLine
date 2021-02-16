using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums
{
    public enum EquipmentPropType
    {
        /// <summary>
        /// 循环时读PLC
        /// </summary>
        PLC_Read,
        /// <summary>
        /// 处理类读PLC
        /// </summary>
        PLC_DelayRead,
        /// <summary>
        /// 不主动读PLC
        /// </summary>
        PLC_NotRead,
        /// <summary>
        /// 后台线程中读取PLC
        /// </summary>
        PLC_BackgroundRead,
        /// <summary>
        /// 开机就读取Modbus
        /// </summary>
        Modbus_Read,
        /// <summary>
        /// 处理类中读取Modbus
        /// </summary>
        Modbus_DelayRead,
        /// <summary>
        /// 不读取Modbus
        /// </summary>
        Modbus_NotRead,
        /// <summary>
        /// 后台线程中读取Modbus
        /// </summary>
        Modbus_BackgroundRead,
    }
}
