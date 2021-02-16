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
    /// 西门子PLC帮助类
    /// </summary>
    public class SiemensHelper
    {
        /// <summary>
        /// 转换类
        /// </summary>
        private static ReverseBytesTransform reverseBytesTransform = new ReverseBytesTransform();

        public static BllResult<HslSiemensDataEntity> ParseAddress(EquipmentProp prop)
        {
            HslSiemensDataEntity entity = new HslSiemensDataEntity();
            entity.OPCAddressId = prop.Id.Value;
            string address = prop.Address;
            try
            {
                if (address[0] == 'I')
                {
                    entity.Area = SiemensArea.I;
                    return BllResultFactory<HslSiemensDataEntity>.Error("暂时不支持I区访问");
                }
                else if (address[0] == 'Q')
                {
                    entity.Area = SiemensArea.Q;
                    return BllResultFactory<HslSiemensDataEntity>.Error("暂时不支持Q区访问");
                }
                else if (address[0] == 'M')
                {
                    entity.Area = SiemensArea.M;
                    entity.BlockIndex = 0;
                    entity.DataAmount = 1;
                    string temp = address.Substring(1);
                    if (temp.StartsWith("DINT"))
                    {
                        temp = temp.Substring(4);
                        entity.DataOffset = Convert.ToInt32(temp);
                        entity.DataType = PLCDataType.DINT;
                    }
                    else if (temp.StartsWith("INT"))
                    {
                        temp = temp.Substring(3);
                        entity.DataOffset = Convert.ToInt32(temp);
                        entity.DataType = PLCDataType.INT;
                    }
                    else if (temp.StartsWith("D"))
                    {
                        temp = temp.Substring(1);
                        entity.DataOffset = Convert.ToInt32(temp);
                        entity.DataType = PLCDataType.DWORD;
                    }
                    else if (temp.StartsWith("W"))
                    {
                        temp = temp.Substring(1);
                        entity.DataOffset = Convert.ToInt32(temp);
                        entity.DataType = PLCDataType.WORD;
                    }
                    else if (temp.StartsWith("X"))
                    {
                        temp = temp.Substring(1);
                        var adss = temp.Split('.');
                        entity.DataOffset = Convert.ToInt32(adss[0]);
                        entity.BitOffset = Convert.ToInt32(adss[1]);
                        entity.DataType = PLCDataType.BOOL;
                    }
                    else if (temp.StartsWith("CHAR"))
                    {
                        temp = temp.Substring(4);
                        var adss = temp.Split(',');
                        entity.DataOffset = Convert.ToInt32(adss[0]);
                        entity.DataAmount = Convert.ToInt32(adss[1]);
                        entity.DataType = PLCDataType.CHAR;
                    }
                    else
                    {
                        return BllResultFactory<HslSiemensDataEntity>.Error("解析错误，无效数据类型");
                    }
                    return BllResultFactory<HslSiemensDataEntity>.Sucess(entity);
                }
                else if (address[0] == 'D' || address.Substring(0, 2) == "DB")
                {
                    if (address[1] == 'B')
                    {
                        entity.Area = SiemensArea.DB;
                        string temp = address.Substring(2);
                        if (temp.Contains("W"))
                        {
                            var ads = temp.Split('W');
                            entity.BlockIndex = Convert.ToInt32(ads[0]);
                            entity.DataOffset = Convert.ToInt32(ads[1]);
                            entity.DataAmount = 1;
                            entity.DataType = PLCDataType.WORD;
                        }
                        else if (temp.Contains("DINT"))
                        {
                            var ads = temp.Split(new char[] { 'D', 'I', 'N', 'T' }, StringSplitOptions.RemoveEmptyEntries);
                            entity.BlockIndex = Convert.ToInt32(ads[0]);
                            entity.DataOffset = Convert.ToInt32(ads[1]);
                            entity.DataAmount = 1;
                            entity.DataType = PLCDataType.DINT;
                        }
                        else if (temp.Contains("INT"))
                        {
                            var ads = temp.Split(new char[] { 'I', 'N', 'T' }, StringSplitOptions.RemoveEmptyEntries);
                            entity.BlockIndex = Convert.ToInt32(ads[0]);
                            entity.DataOffset = Convert.ToInt32(ads[1]);
                            entity.DataAmount = 1;
                            entity.DataType = PLCDataType.INT;
                        }
                        else if (temp.Contains("D"))
                        {
                            var ads = temp.Split('D');
                            entity.BlockIndex = Convert.ToInt32(ads[0]);
                            entity.DataOffset = Convert.ToInt32(ads[1]);
                            entity.DataAmount = 1;
                            entity.DataType = PLCDataType.DWORD;

                        }
                        else if (temp.Contains("B"))
                        {
                            var ads = temp.Split('B');
                            entity.BlockIndex = Convert.ToInt32(ads[0]);
                            entity.DataOffset = Convert.ToInt32(ads[1]);
                            entity.DataAmount = 1;
                            entity.DataType = PLCDataType.BYTE;
                        }
                        else if (temp.Contains("X"))
                        {
                            var ads = temp.Split('X');
                            entity.BlockIndex = Convert.ToInt32(ads[0]);
                            var adss = ads[1].Split('.');
                            entity.DataOffset = Convert.ToInt32(adss[0]);
                            entity.BitOffset = Convert.ToInt32(adss[1]);
                            entity.DataAmount = 1;
                            entity.DataType = PLCDataType.BOOL;
                        }
                        else if (temp.Contains("CHAR"))
                        {
                            var ads = temp.Split(new char[] { 'C', 'H', 'A', 'R' }, StringSplitOptions.RemoveEmptyEntries);
                            entity.BlockIndex = Convert.ToInt32(ads[0]);
                            var adss = ads[1].Split(',');
                            entity.DataOffset = Convert.ToInt32(adss[0]);
                            entity.DataAmount = Convert.ToInt32(adss[1]);
                            entity.DataType = PLCDataType.CHAR;
                        }
                        else
                        {
                            return BllResultFactory<HslSiemensDataEntity>.Error("解析错误，无效数据类型");
                        }

                    }
                    else
                    {
                        return BllResultFactory<HslSiemensDataEntity>.Error("暂时只支持DB块访问");
                    }
                    return BllResultFactory<HslSiemensDataEntity>.Sucess(entity);
                }
                else if (address[0] == 'T')
                {
                    entity.Area = SiemensArea.T;
                    return BllResultFactory<HslSiemensDataEntity>.Error("暂时不支持T区访问");
                }
                else if (address[0] == 'C')
                {
                    entity.Area = SiemensArea.C;
                    return BllResultFactory<HslSiemensDataEntity>.Error("暂时不支持C区访问");
                }
                else if (address[0] == 'V')
                {
                    entity.Area = SiemensArea.V;
                    return BllResultFactory<HslSiemensDataEntity>.Error("暂时不支持V区访问");
                    //result.Content1 = 0x84;
                    //result.Content3 = 1;
                    //result.Content2 = CalculateAddressStarted(address.Substring(1));
                }
                else
                {
                    return BllResultFactory<HslSiemensDataEntity>.Error("未知的地址开头");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory<HslSiemensDataEntity>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 根据数据类型获取其对应的字节长度，bool型为1个字节
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ushort GetByteSize(PLCDataType type)
        {
            switch (type)
            {
                case PLCDataType.BYTE:
                    return 1;
                case PLCDataType.BOOL:
                    return 1;
                case PLCDataType.DWORD:
                    return 4;
                case PLCDataType.WORD:
                    return 2;
                case PLCDataType.INT:
                    return 2;
                case PLCDataType.DINT:
                    return 4;
                case PLCDataType.CHAR:
                    return 1;
                default:
                    return 1;
            }
        }

        /// <summary>
        /// 数据转换，byte[]到string
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static BllResult<string> TransferBufferToString(HslSiemensDataEntity data)
        {
            try
            {
                switch (data.DataType)
                {
                    case PLCDataType.BYTE:
                        //字节转成int处理
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransByte(data.Buffer, 0).ToString(), null);
                    case PLCDataType.BOOL:
                        //对于bool类型，我们需要判断此字节中的指定位是否为0
                        return BllResultFactory<string>.Sucess(ConverHelper.GetBit(data.Buffer[0], data.BitOffset) == 1 ? true.ToString() : false.ToString(), null);
                    case PLCDataType.DWORD:
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransUInt32(data.Buffer, 0).ToString(), null);
                    case PLCDataType.WORD:
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransUInt16(data.Buffer, 0).ToString(), null);
                    case PLCDataType.INT:
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransInt16(data.Buffer, 0).ToString(), null);
                    case PLCDataType.DINT:
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransInt32(data.Buffer, 0).ToString(), null);
                    case PLCDataType.CHAR:
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransString(data.Buffer, 0, data.Buffer.Length, Encoding.ASCII).Replace("\u0003", "").Trim(), null);
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
        public static BllResult<byte[]> TransferStringToBuffer(PLCDataType dataType, string value)
        {
            try
            {
                switch (dataType)
                {
                    case PLCDataType.BYTE:
                        return BllResultFactory<byte[]>.Sucess(reverseBytesTransform.TransByte(new int[] { Convert.ToInt32(value) }));
                    case PLCDataType.BOOL:
                        return BllResultFactory<byte[]>.Sucess(reverseBytesTransform.TransByte(Convert.ToBoolean(value)));
                    case PLCDataType.DWORD:
                        return BllResultFactory<byte[]>.Sucess(reverseBytesTransform.TransByte(new UInt32[] { Convert.ToUInt32(value) }));
                    case PLCDataType.WORD:
                        return BllResultFactory<byte[]>.Sucess(reverseBytesTransform.TransByte(new UInt16[] { Convert.ToUInt16(value) }));
                    case PLCDataType.INT:
                        return BllResultFactory<byte[]>.Sucess(reverseBytesTransform.TransByte(new Int16[] { Convert.ToInt16(value) }));
                    case PLCDataType.DINT:
                        return BllResultFactory<byte[]>.Sucess(reverseBytesTransform.TransByte(new int[] { Convert.ToInt32(value) }));
                    case PLCDataType.CHAR:
                        return BllResultFactory<byte[]>.Sucess(reverseBytesTransform.TransByte(value.PadRight(20, ' '), Encoding.ASCII));
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
