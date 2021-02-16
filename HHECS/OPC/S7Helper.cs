//using HHECS.Bll;
//using HHECS.Model;
//using HHECS.Model.BllModel;
//using HHECS.Model.Entities;
//using S7.Net;
//using S7.Net.Types;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HHECS.OPC
//{
//    public class S7Helper
//    {
//        /// <summary>
//        /// 将DataItem中的value强制转换后赋值给value，用于读
//        /// </summary>
//        /// <param name="plc"></param>
//        /// <param name="props"></param>
//        /// <returns></returns>
//        public static BllResult ConvertPLCToNet(List<EquipmentProp> props)
//        {
//            try
//            {
//                foreach (var item in props)
//                {
//                    switch (item.EquipmentTypeTemplate.DataType)
//                    {
//                        case "DINT":
//                            item.Value = ((uint)item.DataItem.Value).ConvertToInt().ToString();
//                            break;
//                        case "INT":
//                            item.Value = ((ushort)item.DataItem.Value).ConvertToShort().ToString();
//                            break;
//                        case "BYTE":
//                        case "DWORD":
//                        case "BOOL":
//                        case "WORD": item.Value = item.DataItem.Value.ToString(); break;
//                        case "CHAR": item.Value = item.DataItem.Value.ToString().Trim().Replace("$03","").Replace("\u0003", "").Replace("\0", ""); break;
//                        default:
//                            item.Value = item.DataItem.Value.ToString();
//                            break;
//                    }
//                }
//                return BllResultFactory.Sucess("成功");
//            }
//            catch (Exception ex)
//            {
//                return BllResultFactory.Error($"数据类型转换异常:{ex.Message}");
//            }
//        }

//        /// <summary>
//        /// 将属性value中的值强制转换赋值给dataitem，用于向PLC写入
//        /// </summary>
//        /// <param name="plc"></param>
//        /// <param name="props"></param>
//        /// <param name="templates"></param>
//        /// <returns></returns>
//        public static BllResult ConvertNetToPLC(List<EquipmentProp> props)
//        {
//            try
//            {
//                foreach (var item in props)
//                {
//                    switch (item.EquipmentTypeTemplate.DataType)
//                    {
//                        case "INT":
//                            item.DataItem.Value = Convert.ToInt16(item.Value);
//                            break;
//                        case "DINT":
//                            item.DataItem.Value = Convert.ToInt32(item.Value);
//                            break;
//                        case "WORD":
//                            item.DataItem.Value = Convert.ToUInt16(item.Value);
//                            break;
//                        case "DWORD":
//                            item.DataItem.Value = Convert.ToUInt32(item.Value);
//                            break;
//                        case "CHAR":
//                            item.DataItem.Value = item.Value.ToString();
//                            break;
//                        case "BOOL":
//                            item.DataItem.Value = Convert.ToBoolean(item.Value);
//                            break;
//                        default:
//                            item.DataItem.Value = item.Value.ToString();
//                            break;
//                    }
//                }
//                return BllResultFactory.Sucess("成功");
//            }
//            catch (Exception ex)
//            {
//                return BllResultFactory.Error($"数据类型转换异常:{ex.Message}");
//            }
//        }

//        /// <summary>
//        /// 从PLC读取值，读取的值赋值给value属性
//        /// </summary>
//        /// <param name="plc"></param>
//        /// <param name="props"></param>
//        /// <param name="splitCount"></param>
//        /// <param name="templates"></param>
//        /// <returns></returns>
//        public static BllResult PlcSplitRead(Plc plc, List<EquipmentProp> props, int splitCount)
//        {
//            string tamp = "";
//            if (props.Count == 0)
//            {
//                return BllResultFactory.Error("待读集合无数据");
//            }
//            try
//            {
//                var tempList = props.Select(t => t.DataItem).ToList();
//                int preIndex = 0;
//                int currentIndex = splitCount;
//                plc.Open();
//                while (preIndex < tempList.Count)
//                {
//                    List<DataItem> temp1 = null;
//                    if (preIndex < tempList.Count)
//                    {
//                        temp1 = tempList.Skip(preIndex).Take(currentIndex).ToList();
//                        preIndex += currentIndex;
//                    }
//                    else
//                    {
//                        temp1 = tempList.Skip(preIndex).ToList();
//                    }
//                    //tamp = props[preIndex].EquipmentTypePropTemplateCode;
//                    plc.ReadMultipleVars(temp1);
//                }
//                //赋值属性
//                var result = ConvertPLCToNet(props);
//                if (result.Success)
//                {
//                    return BllResultFactory.Sucess("去读成功.");
//                }
//                else
//                {
//                    return BllResultFactory.Error($"读取失败：{result.Msg}");
//                }
//            }
//            catch (Exception ex)
//            {
//                return BllResultFactory.Error($"读取PLC值出错：{ex.Message}");
//            }

//        }

//        /// <summary>
//        /// 写入PLC值，待写的值为value属性
//        /// </summary>
//        /// <param name="plc"></param>
//        /// <param name="props"></param>
//        /// <param name="splitCount"></param>
//        /// <param name="templates"></param>
//        /// <returns></returns>
//        public static BllResult PlcSplitWrite(Plc plc, List<EquipmentProp> props, int splitCount)
//        {
//            if (props.Count == 0)
//            {
//                return BllResultFactory.Error("待写集合无数据");
//            }
//            if (splitCount > 20)
//            {
//                return BllResultFactory.Error("分批读写个数不能超过20");
//            }
//            //先转换
//            var result = ConvertNetToPLC(props);
//            if (result.Success)
//            {
//                try
//                {
//                    var tempList = props.Select(t => t.DataItem).ToList();
//                    int preIndex = 0;
//                    int currentIndex = splitCount;
//                    plc.Open();
//                    while (preIndex < tempList.Count)
//                    {
//                        List<DataItem> temp1 = null;
//                        if (preIndex < tempList.Count)
//                        {
//                            temp1 = tempList.Skip(preIndex).Take(currentIndex).ToList();
//                            preIndex += currentIndex;
//                        }
//                        else
//                        {
//                            temp1 = tempList.Skip(preIndex).ToList();
//                        }
//                        plc.Write(temp1.ToArray());
//                    }
//                    return BllResultFactory.Sucess("写入成功");
//                }
//                catch (Exception ex)
//                {
//                    return BllResultFactory.Error($"写入出现异常：{ex.Message}");
//                }
//            }
//            else
//            {
//                return BllResultFactory.Error($"写入失败，数据转换异常：{result.Msg}");
//            }

//        }

//        /// <summary>
//        /// 创建dataitem，这里解决了原本库中bit位bug，支持char，标准格式DB1000.DBC20.30，表示读取DB1000，起始地址20,以char的形式读取，读取30位;
//        /// char读写时，超出部分会被截断
//        /// </summary>
//        /// <param name="address"></param>
//        /// <returns></returns>
//        public static DataItem FromAddress(string address)
//        {
//            Parse(address, out var dataType, out var dbNumber, out var varType, out var startByte,
//                out var bitNumber, out var count);
//            return new DataItem
//            {
//                DataType = dataType,
//                DB = dbNumber,
//                VarType = varType,
//                StartByteAdr = startByte,
//                BitAdr = (byte)(bitNumber == -1 ? 0 : bitNumber),
//                Count = count
//            };
//        }

//        public static void Parse(string input, out DataType dataType, out int dbNumber, out VarType varType, out int address, out int bitNumber, out int count)
//        {
//            count = 1;
//            bitNumber = -1;
//            dbNumber = 0;

//            switch (input.Substring(0, 2))
//            {
//                case "DB":
//                    string[] strings = input.Split(new char[] { '.' });
//                    if (strings.Length < 2)
//                        throw new BllException("To few periods for DB address");

//                    dataType = DataType.DataBlock;
//                    dbNumber = int.Parse(strings[0].Substring(2));
//                    address = int.Parse(strings[1].Substring(3));

//                    string dbType = strings[1].Substring(0, 3);
//                    switch (dbType)
//                    {
//                        case "DBB":
//                            varType = VarType.Byte;
//                            return;
//                        case "DBW":
//                            varType = VarType.Word;
//                            return;
//                        case "DBD":
//                            varType = VarType.DWord;
//                            return;
//                        case "DBX":
//                            bitNumber = int.Parse(strings[2]);
//                            if (bitNumber > 7)
//                                throw new BllException("Bit can only be 0-7");
//                            varType = VarType.Bit;
//                            return;
//                        case "DBC":
//                            varType = VarType.String;
//                            count = int.Parse(strings[2]);
//                            return;
//                        default:
//                            throw new BllException();
//                    }
//                case "EB":
//                    // Input byte
//                    dataType = DataType.Input;
//                    dbNumber = 0;
//                    address = int.Parse(input.Substring(2));
//                    varType = VarType.Byte;
//                    return;
//                case "EW":
//                    // Input word
//                    dataType = DataType.Input;
//                    dbNumber = 0;
//                    address = int.Parse(input.Substring(2));
//                    varType = VarType.Word;
//                    return;
//                case "ED":
//                    // Input double-word
//                    dataType = DataType.Input;
//                    dbNumber = 0;
//                    address = int.Parse(input.Substring(2));
//                    varType = VarType.DWord;
//                    return;
//                case "AB":
//                    // Output byte
//                    dataType = DataType.Output;
//                    dbNumber = 0;
//                    address = int.Parse(input.Substring(2));
//                    varType = VarType.Byte;
//                    return;
//                case "AW":
//                    // Output word
//                    dataType = DataType.Output;
//                    dbNumber = 0;
//                    address = int.Parse(input.Substring(2));
//                    varType = VarType.Word;
//                    return;
//                case "AD":
//                    // Output double-word
//                    dataType = DataType.Output;
//                    dbNumber = 0;
//                    address = int.Parse(input.Substring(2));
//                    varType = VarType.DWord;
//                    return;
//                case "MB":
//                    // Memory byte
//                    dataType = DataType.Memory;
//                    dbNumber = 0;
//                    address = int.Parse(input.Substring(2));
//                    varType = VarType.Byte;
//                    return;
//                case "MW":
//                    // Memory word
//                    dataType = DataType.Memory;
//                    dbNumber = 0;
//                    address = int.Parse(input.Substring(2));
//                    varType = VarType.Word;
//                    return;
//                case "MD":
//                    // Memory double-word
//                    dataType = DataType.Memory;
//                    dbNumber = 0;
//                    address = int.Parse(input.Substring(2));
//                    varType = VarType.DWord;
//                    return;
//                default:
//                    switch (input.Substring(0, 1))
//                    {
//                        case "E":
//                        case "I":
//                            // Input
//                            dataType = DataType.Input;
//                            varType = VarType.Bit;
//                            break;
//                        case "A":
//                        case "O":
//                            // Output
//                            dataType = DataType.Output;
//                            varType = VarType.Bit;
//                            break;
//                        case "M":
//                            // Memory
//                            dataType = DataType.Memory;
//                            varType = VarType.Byte;
//                            break;
//                        case "T":
//                            // Timer
//                            dataType = DataType.Timer;
//                            dbNumber = 0;
//                            address = int.Parse(input.Substring(1));
//                            varType = VarType.Timer;
//                            return;
//                        case "Z":
//                        case "C":
//                            // Counter
//                            dataType = DataType.Timer;
//                            dbNumber = 0;
//                            address = int.Parse(input.Substring(1));
//                            varType = VarType.Counter;
//                            return;
//                        default:
//                            throw new BllException(string.Format("{0} is not a valid address", input.Substring(0, 1)));
//                    }

//                    string txt2 = input.Substring(1);
//                    if (txt2.IndexOf(".") == -1)
//                        throw new BllException("To few periods for DB address");

//                    address = int.Parse(txt2.Substring(0, txt2.IndexOf(".")));
//                    bitNumber = int.Parse(txt2.Substring(txt2.IndexOf(".") + 1));
//                    if (bitNumber > 7)
//                        throw new BllException("Bit can only be 0-7");
//                    return;
//            }
//        }

//    }
//}
