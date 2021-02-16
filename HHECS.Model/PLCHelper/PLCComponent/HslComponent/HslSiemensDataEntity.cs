using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.PLCHelper.PLCComponent.HslComponent
{
    /// <summary>
    /// 适配HSL西门子通信的数据实体
    /// </summary>
    public class HslSiemensDataEntity
    {
        /// <summary>
        /// 被解析的OPC地址ID
        /// </summary>
        public int OPCAddressId { get; set; }

        private string address;

        /// <summary>
        /// 对应HSL的数据地址格式
        /// 定位到字节偏移量："M100",  "I100",  "Q100",  "DB20.100"
        /// </summary>
        public string Address
        {
            get
            {
                switch (Area)
                {
                    case SiemensArea.I:

                    case SiemensArea.Q:

                    case SiemensArea.M:

                    case SiemensArea.D:

                    case SiemensArea.T:

                    case SiemensArea.C:

                    case SiemensArea.V:
                        return Area.ToString() + DataOffset;
                    case SiemensArea.DB:
                        return Area.ToString() + BlockIndex + "." + DataOffset;
                    default:
                        return null;
                }
            }
        }

        private string addressX;

        /// <summary>
        /// 对应HSL的数据地址格式
        /// 定位到位："M100.6",  "I100.7",  "Q100.0",  "DB20.100.0"
        /// </summary>
        public string AddressX
        {
            get
            {
                switch (Area)
                {
                    case SiemensArea.I:

                    case SiemensArea.Q:

                    case SiemensArea.M:

                    case SiemensArea.D:

                    case SiemensArea.T:

                    case SiemensArea.C:

                    case SiemensArea.V:
                        if (DataType == PLCDataType.BOOL)
                        {
                            return Area.ToString() + DataOffset + "." + BitOffset;
                        }
                        else
                        {
                            return Area.ToString() + DataOffset;
                        }
                    case SiemensArea.DB:
                        if (DataType == PLCDataType.BOOL)
                        {
                            return Area.ToString() + BlockIndex + "." + DataOffset + "." + BitOffset;
                        }
                        else
                        {
                            return Area.ToString() + BlockIndex + "." + DataOffset;
                        }
                    default:
                        return null;
                }
            }
        }



        /// <summary>
        /// DB,M,I...
        /// </summary>
        public SiemensArea Area { get; set; }


        /// <summary>
        /// 数据类型，word,int...
        /// </summary>
        public PLCDataType DataType { get; set; }

        /// <summary>
        /// 块索引
        /// 比如DB100，这个值为100；M区，这个值为0
        /// </summary>
        public int BlockIndex { get; set; }

        /// <summary>
        /// 起始量
        /// 比如DB100X20.3，则此值为20；
        /// MW100，此值为100；
        /// </summary>
        public int DataOffset { get; set; }

        /// <summary>
        /// 位偏移，相对于读取bit而言，取值为0到7，其他类型为0
        /// </summary>
        public int BitOffset { get; set; }

        /// <summary>
        /// 定义为此数据类型的个数，在现行项目下，一般为1，char一般为20；
        /// </summary>
        public int DataAmount { get; set; } = 1;

        /// <summary>
        /// 缓存buffer
        /// </summary>
        public byte[] Buffer { get; set; }

        private ushort byteSize;

        public ushort ByteSize
        {
            get
            {
                return (ushort)(SiemensHelper.GetByteSize(DataType) * DataAmount);
            }
        }


    }
}
