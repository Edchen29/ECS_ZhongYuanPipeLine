using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.PLCHelper.PLCComponent.HslComponent
{
    /// <summary>
    /// 适配HSL的Modbus通信数据实体
    /// </summary>
    public class HslModbusDataEntity
    {
        /// <summary>
        /// 被解析的Modbus地址ID
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// 对应HSL的寄存器地址格式
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public ModbusDataType DataType { get; set; }

        /// <summary>
        /// 位偏移，相对于读取bit而言，取值为0到7，其他类型为0
        /// </summary>
        public int BitOffset { get; set; } = 0;

        /// <summary>
        /// 定义为此数据类型的个数，在现行项目下，一般为1，char一般为20；
        /// </summary>
        public int DataAmount { get; set; } = 1;

        /// <summary>
        /// 缓存buffer
        /// </summary>
        public byte[] Buffer { get; set; }

        //获取数据的字节长度
        public ushort ByteSize
        {
            get
            {
                return (ushort)(ModbusHelper.GetByteSize(DataType) * DataAmount);
            }
        }

        //获取数据的字节长度
        public ushort Length
        {
            get
            {
                if (ByteSize % 2 == 0)
                {
                    return (ushort)(ByteSize / 2);
                }
                else
                {
                    return (ushort)((ByteSize / 2) + 1);
                }
            }
        }


        ////下个地址，Modbus是一个数据至少放一个寄存器，如果放不下就放多个寄存器
        //public int NextAddress
        //{
        //    get
        //    {
        //        if (ByteSize % 2 == 0)
        //        {
        //            return Address + (ByteSize / 2);
        //        }
        //        else
        //        {
        //            return Address + (ByteSize / 2) + 1;
        //        }                
        //    }
        //}


    }
}
