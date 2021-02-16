using HHECS.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Common
{
    public class LogEventArgs : EventArgs
    {
        public LogTitle LogTitle { get; set; }
        public string Content { get; set; }
        public LogLevel LogLevel { get; set; }
        public Exception Exception { get; set; }
        public LogEventArgs() : base()
        {

        }
        public static LogEventArgs GetLogEventArgs(string content, LogLevel logLevel, Exception exception = null)
        {
            return new LogEventArgs()
            {
                LogTitle = LogTitle.设备调度日志,
                Content = content,
                LogLevel = logLevel,
                Exception = exception
            };
        }

        public static LogEventArgs GetLogEventArgs(LogTitle logTitle, string content, LogLevel logLevel, Exception exception = null)
        {
            return new LogEventArgs()
            {
                LogTitle = logTitle,
                Content = content,
                LogLevel = logLevel,
                Exception = exception
            };
        }
    }
}
