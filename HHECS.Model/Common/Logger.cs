using HHECS.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Common
{
    public class Logger
    {
        #region 事件

        public static event Delegates.LogWriteEventHandle LogWrite;
        //记录日志时间和文本
        public static List<Generics<DateTime, string>> contentList = new List<Generics<DateTime, string>>();

        public static void Log(string content, LogLevel logLevel, Exception exception = null)
        {
            //如果这条记录5秒之内插入过，就不再插入
            if (contentList.Exists(t => DateTime.Now.Subtract(t.x).TotalSeconds < 9 && t.y == content))
            {
                return;
            }
            if (contentList.Count > 50)
            {
                contentList.RemoveAt(0);
            }
            contentList.Add(new Generics<DateTime, string>(DateTime.Now, content));
            LogWrite?.Invoke(null, LogEventArgs.GetLogEventArgs(content, logLevel, exception));
        }

        public static void Log(LogTitle logTitle, string content, LogLevel logLevel, Exception exception = null)
        {
            //如果这条记录5秒之内插入过，就不再插入
            if (contentList.Exists(t => DateTime.Now.Subtract(t.x).TotalSeconds < 9 && t.y == content))
            {
                return;
            }
            if (contentList.Count > 50)
            {
                contentList.RemoveAt(0);
            }
            contentList.Add(new Generics<DateTime, string>(DateTime.Now, content));
            LogWrite?.Invoke(null, LogEventArgs.GetLogEventArgs(logTitle, content, logLevel, exception));
        }
        #endregion
    }
}
