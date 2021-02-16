using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HslCommunication.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.PLCHelper.PLCComponent.HslComponent
{
    /// <summary>
    /// Modbus帮助类
    /// </summary>
    public class ModbusHelper
    {
        /// <summary>
        /// 转换类
        /// </summary>
        private static ReverseBytesTransform reverseBytesTransform = new ReverseBytesTransform();

        public static BllResult<HslModbusDataEntity> ParseAddress(EquipmentProp prop)
        {
            HslModbusDataEntity entity = new HslModbusDataEntity();
            entity.AddressId = prop.Id.Value;
            try
            {
                if (prop.Address.Contains(','))     // 寄存器地址 + . + 字符长度，这种是字符串跨越多个寄存器
                {
                    var temp = prop.Address.Split(',');
                    entity.Address = temp[0];
                    entity.DataAmount = Convert.ToInt32(temp[1]);
                }
                else if (prop.Address.Contains('.'))    // 寄存器地址 + . + bit的偏移量，这种是一个寄存器放多个bit值，
                {
                    var temp = prop.Address.Split('.');
                    entity.Address = temp[0];
                    entity.BitOffset = Convert.ToInt32(temp[1]);
                    entity.DataAmount = 1;
                }
                else    // 普通的地址
                {
                    entity.Address = prop.Address;
                    entity.DataAmount = 1;
                }
                entity.DataType = (ModbusDataType)Enum.Parse(typeof(ModbusDataType), prop.EquipmentTypeTemplate.DataType);
                return BllResultFactory<HslModbusDataEntity>.Sucess(entity);
            }
            catch (Exception ex)
            {
                return BllResultFactory<HslModbusDataEntity>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 根据数据类型获取其对应的字节长度
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ushort GetByteSize(ModbusDataType type)
        {
            switch (type)
            {
                case ModbusDataType.Bool:  //一个寄存器是16位，可以包含16个Bool量，所以Bool类型占用2个字节
                    return 2;
                case ModbusDataType.Short:
                    return 2;
                case ModbusDataType.UShort:
                    return 2;
                case ModbusDataType.Int:
                    return 4;
                case ModbusDataType.UInt:
                    return 4;
                case ModbusDataType.Long:
                    return 8;
                case ModbusDataType.ULong:
                    return 8;
                case ModbusDataType.Float:
                    return 4;
                case ModbusDataType.Double:
                    return 8;
                case ModbusDataType.String:
                    return 1;
                default:
                    return 2;
            }
        }


        /// <summary>
        /// 数据转换，byte[]到string
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static BllResult<string> TransferBufferToString(HslModbusDataEntity data, int index = 0) 
        {
            try
            {
                switch (data.DataType)
                {
                    //对于bool类型，先转化为Bool，然后转二进制，再根据对应的位来判断是0还是1
                    case ModbusDataType.Bool:                        
                    case ModbusDataType.Short:
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransInt16(data.Buffer, index).ToString(), null);
                    case ModbusDataType.UShort:
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransUInt16(data.Buffer, index).ToString(), null);
                    case ModbusDataType.Int:
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransInt32(data.Buffer, index).ToString(), null);
                    case ModbusDataType.UInt:
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransUInt32(data.Buffer, index).ToString(), null);
                    case ModbusDataType.Long:
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransInt64(data.Buffer, index).ToString(), null);
                    case ModbusDataType.ULong:
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransUInt64(data.Buffer, index).ToString(), null);
                    case ModbusDataType.Float:
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransSingle(data.Buffer, index).ToString(), null);
                    case ModbusDataType.Double:
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransDouble(data.Buffer, index).ToString(), null);
                    case ModbusDataType.String:
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransString(data.Buffer, index, data.Buffer.Length, Encoding.ASCII), null);
                    default:
                        return BllResultFactory<string>.Error("未识别");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory<string>.Error($"转换出错：{ex.Message}");
            }
        }

        /// <summary>
        /// 数据转换，string到byte[]
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static BllResult<byte[]> TransferStringToBuffer(ModbusDataType dataType, string value)
        {
            try
            {
                switch (dataType)
                {
                    case ModbusDataType.Bool:
                        return BllResultFactory<byte[]>.Sucess(reverseBytesTransform.TransByte(Convert.ToBoolean(value)));
                    case ModbusDataType.Short:
                        return BllResultFactory<byte[]>.Sucess(reverseBytesTransform.TransByte(new Int16[] { Convert.ToInt16(value) }));
                    case ModbusDataType.UShort:
                        return BllResultFactory<byte[]>.Sucess(reverseBytesTransform.TransByte(new UInt16[] { Convert.ToUInt16(value) }));
                    case ModbusDataType.Int:
                        return BllResultFactory<byte[]>.Sucess(reverseBytesTransform.TransByte(new Int32[] { Convert.ToInt32(value) }));
                    case ModbusDataType.UInt:
                        return BllResultFactory<byte[]>.Sucess(reverseBytesTransform.TransByte(new UInt32[] { Convert.ToUInt32(value) }));
                    case ModbusDataType.Long:
                        return BllResultFactory<byte[]>.Sucess(reverseBytesTransform.TransByte(new Int64[] { Convert.ToInt64(value) }));
                    case ModbusDataType.ULong:
                        return BllResultFactory<byte[]>.Sucess(reverseBytesTransform.TransByte(new UInt64[] { Convert.ToUInt64(value) }));
                    case ModbusDataType.Float:
                        return BllResultFactory<byte[]>.Sucess(reverseBytesTransform.TransByte(new Single[] { Convert.ToSingle(value) }));
                    case ModbusDataType.Double:
                        return BllResultFactory<byte[]>.Sucess(reverseBytesTransform.TransByte(new Double[] { Convert.ToDouble(value) }));
                    case ModbusDataType.String:
                        return BllResultFactory<byte[]>.Sucess(reverseBytesTransform.TransByte(value.PadRight(20,' '), Encoding.ASCII));
                    default:
                        return BllResultFactory<byte[]>.Error("未识别的数据类型");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory<byte[]>.Error("转换出现问题：" + ex.Message);
            }
        }


    }
}
