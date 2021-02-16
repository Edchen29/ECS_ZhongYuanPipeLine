using HHECS.Model.BllModel;
using HHECS.Model.Common.Transfer;
using Sharp7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.PLCHelper.PLCComponent.Sharp7Component
{
    /// <summary>
    /// 封装S7实现类，一个PLC对应一个
    /// </summary>
    public class S7PLCHelper
    {
        public string PLCIP { get; set; }

        public string PLCType { get; set; }

        public int Rack { get; set; }

        public int Slot { get; set; }

        public S7Client S7Client { get; set; }

        /// <summary>
        /// 转换类
        /// </summary>
        private static ReverseBytesTransform reverseBytesTransform = new ReverseBytesTransform();


        /// <summary>
        /// 解析地址
        /// 地址格式：DB100W10,DB120X20.1,DB300CHAR20,29
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static BllResult<S7DataEntity> AddressAnalyze(string address)
        {
            S7DataEntity entity = new S7DataEntity();
            try
            {
                if (address[0] == 'I')
                {
                    entity.Area = S7Consts.S7AreaPE;
                    return BllResultFactory<S7DataEntity>.Error("暂时不支持I区访问");
                }
                else if (address[0] == 'Q')
                {
                    entity.Area = S7Consts.S7AreaPA;
                    return BllResultFactory<S7DataEntity>.Error("暂时不支持Q区访问");
                }
                else if (address[0] == 'M')
                {
                    entity.Area = S7Consts.S7AreaMK;
                    entity.DataNumber = 0;
                    entity.DataAmount = 1;
                    string temp = address.Substring(1);
                    if (temp.StartsWith("DINT"))
                    {
                        temp = temp.Substring(4);
                        entity.DataOffset = Convert.ToInt32(temp);
                        entity.DataType = S7Consts.S7WLDInt;
                    }
                    else if (temp.StartsWith("INT"))
                    {
                        temp = temp.Substring(3);
                        entity.DataOffset = Convert.ToInt32(temp);
                        entity.DataType = S7Consts.S7WLInt;
                    }
                    else if (temp.StartsWith("D"))
                    {
                        temp = temp.Substring(1);
                        entity.DataOffset = Convert.ToInt32(temp);
                        entity.DataType = S7Consts.S7WLDWord;
                    }
                    else if (temp.StartsWith("W"))
                    {
                        temp = temp.Substring(1);
                        entity.DataOffset = Convert.ToInt32(temp);
                        entity.DataType = S7Consts.S7WLWord;
                    }
                    else if (temp.StartsWith("X"))
                    {
                        temp = temp.Substring(1);
                        var adss = temp.Split('.');
                        entity.DataOffset = Convert.ToInt32(adss[0]) * 8 + Convert.ToInt32(adss[1]);
                        entity.DataType = S7Consts.S7WLBit;
                    }
                    else if (temp.StartsWith("CHAR"))
                    {
                        temp = temp.Substring(4);
                        var adss = temp.Split(',');
                        entity.DataOffset = Convert.ToInt32(adss[0]);
                        entity.DataAmount = Convert.ToInt32(adss[1]);
                        entity.DataType = S7Consts.S7WLChar;
                    }
                    else
                    {
                        return BllResultFactory<S7DataEntity>.Error("解析错误，无效数据类型");
                    }
                    return BllResultFactory<S7DataEntity>.Sucess(entity);
                }
                else if (address[0] == 'D' || address.Substring(0, 2) == "DB")
                {
                    entity.Area = S7Consts.S7AreaDB;
                    if (address[1] == 'B')
                    {
                        string temp = address.Substring(2);
                        if (temp.Contains("W"))
                        {
                            var ads = temp.Split('W');
                            entity.DataNumber = Convert.ToInt32(ads[0]);
                            entity.DataOffset = Convert.ToInt32(ads[1]);
                            entity.DataAmount = 1;
                            entity.DataType = S7Consts.S7WLWord;
                        }
                        else if (temp.Contains("DINT"))
                        {
                            var ads = temp.Split(new char[] { 'D', 'I', 'N', 'T' }, StringSplitOptions.RemoveEmptyEntries);
                            entity.DataNumber = Convert.ToInt32(ads[0]);
                            entity.DataOffset = Convert.ToInt32(ads[1]);
                            entity.DataAmount = Convert.ToInt32(1);
                            entity.DataType = S7Consts.S7WLDInt;
                        }
                        else if (temp.Contains("INT"))
                        {
                            var ads = temp.Split(new char[] { 'I', 'N', 'T' }, StringSplitOptions.RemoveEmptyEntries);
                            entity.DataNumber = Convert.ToInt32(ads[0]);
                            entity.DataOffset = Convert.ToInt32(ads[1]);
                            entity.DataAmount = Convert.ToInt32(1);
                            entity.DataType = S7Consts.S7WLInt;
                        }
                        else if (temp.Contains("D"))
                        {
                            var ads = temp.Split('D');
                            entity.DataNumber = Convert.ToInt32(ads[0]);
                            entity.DataOffset = Convert.ToInt32(ads[1]);
                            entity.DataAmount = 1;
                            entity.DataType = S7Consts.S7WLDWord;

                        }
                        else if (temp.Contains("B"))
                        {
                            var ads = temp.Split('B');
                            entity.DataNumber = Convert.ToInt32(ads[0]);
                            entity.DataOffset = Convert.ToInt32(ads[1]);
                            entity.DataAmount = 1;
                            entity.DataType = S7Consts.S7WLByte;
                        }
                        else if (temp.Contains("X"))
                        {
                            var ads = temp.Split('X');
                            entity.DataNumber = Convert.ToInt32(ads[0]);
                            var adss = ads[1].Split('.');
                            entity.DataOffset = Convert.ToInt32(adss[0]) * 8 + Convert.ToInt32(adss[1]) + 1;
                            entity.DataAmount = 1;
                            entity.DataType = S7Consts.S7WLBit;
                        }
                        else if (temp.Contains("CHAR"))
                        {
                            var ads = temp.Split(new char[] { 'C', 'H', 'A', 'R' }, StringSplitOptions.RemoveEmptyEntries);
                            entity.DataNumber = Convert.ToInt32(ads[0]);
                            var adss = ads[1].Split(',');
                            entity.DataOffset = Convert.ToInt32(adss[0]);
                            entity.DataAmount = Convert.ToInt32(adss[1]);
                            entity.DataType = S7Consts.S7WLChar;
                        }
                        else
                        {
                            return BllResultFactory<S7DataEntity>.Error("解析错误，无效数据类型");
                        }

                    }
                    else
                    {
                        return BllResultFactory<S7DataEntity>.Error("暂时只支持DB块访问");
                    }
                    return BllResultFactory<S7DataEntity>.Sucess(entity);
                }
                else if (address[0] == 'T')
                {
                    entity.Area = S7Consts.S7AreaTM;
                    return BllResultFactory<S7DataEntity>.Error("暂时不支持T区访问");
                }
                else if (address[0] == 'C')
                {
                    entity.Area = S7Consts.S7AreaCT;
                    return BllResultFactory<S7DataEntity>.Error("暂时不支持C区访问");
                }
                else if (address[0] == 'V')
                {
                    entity.Area = S7Consts.S7AreaDB;
                    return BllResultFactory<S7DataEntity>.Error("暂时不支持V区访问");
                    //result.Content1 = 0x84;
                    //result.Content3 = 1;
                    //result.Content2 = CalculateAddressStarted(address.Substring(1));
                }
                else
                {
                    return BllResultFactory<S7DataEntity>.Error("未知的地址开头");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory<S7DataEntity>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 数据转换，byte[]到string
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static BllResult<string> TransferBufferToString(PLCDataType dataType, byte[] buffer)
        {
            try
            {
                switch (dataType)
                {
                    case PLCDataType.BYTE:
                        //字节转成int处理
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransInt32(buffer, 0).ToString(), null);
                    case PLCDataType.BOOL:
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransBool(buffer, 0).ToString(), null);
                    case PLCDataType.DWORD:
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransUInt32(buffer, 0).ToString(), null);
                    case PLCDataType.WORD:
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransUInt16(buffer, 0).ToString(), null);
                    case PLCDataType.INT:
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransInt16(buffer, 0).ToString(), null);
                    case PLCDataType.DINT:
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransInt32(buffer, 0).ToString(), null);
                    case PLCDataType.CHAR:
                        return BllResultFactory<string>.Sucess(reverseBytesTransform.TransString(buffer, 0, buffer.Length, Encoding.ASCII).Substring(0, 20).Replace("\0", "").Trim(), null);
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
                        return BllResultFactory<byte[]>.Sucess(reverseBytesTransform.TransByte(value, Encoding.ASCII));
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
