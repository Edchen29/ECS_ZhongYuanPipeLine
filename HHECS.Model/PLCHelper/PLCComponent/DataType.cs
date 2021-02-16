using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.PLCHelper.PLCComponent
{
    /// <summary>
    /// 通用的数据类型
    /// 目前使用S7数据类型，后期添加三菱等其他厂商PLC再行扩展
    /// </summary>
    public enum PLCDataType
    {
        BYTE,
        BOOL,
        DWORD,
        WORD,
        INT,
        DINT,
        CHAR //指单个字符或多个字符组成的字符串
    }

    /// <summary>
    /// Modbus的所有数据类型
    /// </summary>
    public enum ModbusDataType
    {        
        Bool,
        Short,
        UShort,
        Int,
        UInt,
        Long,
        ULong,
        Float,
        Double,
        String,
    }

}
