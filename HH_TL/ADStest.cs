using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace HH_TL
{
    class ADStest
    {

    }

	public BllResult AdsInit()
	{
		try
		{
			//初始化变量，此例中【SeverData.S_Ex_CData.TraData】为plc提供的变量地址，类型为结构体。读取写入类型为其他时同样处理。
			readStructIndex = ShuttlesADSClient.CreateVariableHandle("SeverData.S_Ex_CData.TraData");
			writeStructIndex = ShuttlesADSClient.CreateVariableHandle("SeverData.S_Ex_CData.RecData");
			return BllResult.Sucess();
		}
		catch (Exception ex)
		{
			string Emessage = string.Format("Ads Add Struct Error\n{0}", ex.Message);
			return BllResult.Error("AdsInit:" + Emessage);
		}
	}
	public BllResult ReadAds()
	{
		try
		{
			//ReadStruct 为结构体，通过初始化时得到的 readStructIndex索引，进行读取
			var value = (ReadStruct)ShuttlesADSClient.ReadAny(readStructIndex, typeof(ReadStruct));
			Array.Copy(value.bytesVal, readBytes, readBytes.Length);
			//4字节
			NetIp = readBytes[31] + "." + readBytes[32] + "." + readBytes[33] + "." + readBytes[34];
			//NetIp = Encoding.ASCII.GetString(readBytes, 31, 4).Replace("\0", "");
			//2字节
			NetPort = BitConverter.ToUInt16(readBytes, 35);
			return BllResult.Sucess();
		}
		catch (Exception ex)
		{
			return BllResult.Error("ReadAds:" + ex.Message);
		}
	}


	public BllResult WriteAds(byte[] senddata)
	{
		try
		{

			WriteStruct complexStruct = new WriteStruct();
			complexStruct.bytesVal = new byte[100];
			Array.Copy(senddata, complexStruct.bytesVal, complexStruct.bytesVal.Length);
			//结构体写入时，同样通过writeStructIndex索引，直接写入结构体。
			ShuttlesADSClient.WriteAny(writeStructIndex, complexStruct);

			return BllResult.Sucess();
		}
		catch (Exception ex)
		{
			return BllResult.Error("WriteAds:" + ex.Message);
		}
	}

	public class ReadStruct
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
		public byte[] bytesVal;
	}
}
