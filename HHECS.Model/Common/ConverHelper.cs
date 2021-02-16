using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Common
{
    public static class ConverHelper
    {
        #region 字符串和Byte之间的转化
        /// <summary>
        /// 数字和字节之间互转
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int IntToBitConverter(int num)
        {
            int temp = 0;
            byte[] bytes = BitConverter.GetBytes(num);//将int32转换为字节数组
            temp = BitConverter.ToInt32(bytes, 0);//将字节数组内容再转成int32类型
            return temp;
        }

        /// <summary>
        /// 将字符串转为16进制字符，允许中文
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string StringToHexString(string s, Encoding encode, string spanString)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
            string result = string.Empty;
            for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符
            {
                result += Convert.ToString(b[i], 16) + spanString;
            }
            return result;
        }
        /// <summary>
        /// 将16进制字符串转为字符串
        /// </summary>
        /// <param name="hs"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string HexStringToString(string hs, Encoding encode)
        {
            string strTemp = "";
            byte[] b = new byte[hs.Length / 2];
            for (int i = 0; i < hs.Length / 2; i++)
            {
                strTemp = hs.Substring(i * 2, 2);
                b[i] = Convert.ToByte(strTemp, 16);
            }
            //按照指定编码将字节数组变为字符串
            return encode.GetString(b);
        }
        /// <summary>
        /// byte[]转为16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }
        /// <summary>
        /// 将16进制的字符串转为byte[]
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] StrToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public static string ASCIIToString(short[] ints)
        {
            String str = "";
            foreach (var item in ints)
            {
                str += Chr(item);
            }
            return str;
        }

        public static string Chr(int asciiCode)
        {
            if (asciiCode >= 0 && asciiCode <= 255)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                byte[] byteArray = new byte[] { (byte)asciiCode };
                string strCharacter = asciiEncoding.GetString(byteArray);
                return (strCharacter);
            }
            else
            {
                throw new Exception("ASCII Code is not valid.");
            }
        }

        public static byte[] StringToASCII(string str)
        {
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            return asciiEncoding.GetBytes(str.PadRight(20, ' '));
        }

        public static short Asc(string character)
        {
            if (character.Length == 1)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                short intAsciiCode = (short)asciiEncoding.GetBytes(character)[0];
                return (intAsciiCode);
            }
            else
            {
                throw new Exception("Character is not valid.");
            }

        }


        #endregion


        #region 字节位操作


        //index从0开始  
        //获取取第index位  
        public static int GetBit(byte b, int index) { return ((b & (1 << index)) > 0) ? 1 : 0; }
        //将第index位设为1  
        public static byte SetBit(byte b, int index) { return (byte)(b | (1 << index)); }
        //将第index位设为0   
        public static byte ClearBit(byte b, int index) { return (byte)(b & (byte.MaxValue - (1 << index))); }
        //将第index位取反   
        public static byte ReverseBit(byte b, int index) { return (byte)(b ^ (byte)(1 << index)); }

        #endregion
    }
}
