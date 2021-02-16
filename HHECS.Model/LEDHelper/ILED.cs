using HHECS.Model.BllModel;
using HHECS.Model.LEDHelper.LEDComponent.DefaultLEDComponent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.LEDHelper
{
    /// <summary>
    /// LED接口
    /// </summary>
    public interface ILED
    {
        /// <summary>
        /// 参数T作为一个配置项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        /// <param name="t"></param>
        void Push<T>(string key, string msg, T t);

        void Push(string key, string msg);

        void BeginSendInfo();

        
    }
}
