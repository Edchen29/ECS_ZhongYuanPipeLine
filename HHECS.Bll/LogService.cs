using HHECS.Model.BllModel;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HHECS.Bll
{
    /// <summary>
    /// 日志相关，取代LogExecute
    /// </summary>
    public class LogService : BaseService
    {
        /// <summary>
        /// 采取单例模式
        /// </summary>
        private static LogService instance;

        /// <summary>
        /// 设置日志保存路径
        /// </summary>
        private string logPath;

        /// <summary>
        /// 日志队列
        /// </summary>
        private static Dictionary<String, Queue<String>> dic = new Dictionary<String, Queue<String>>();

        /// <summary>
        ///  单例锁
        /// </summary>
        private static readonly object instanceLock = new object();

        private LogService()    
        {
        }

        private LogService(string path)
        {
            if (instance == null)
            {
                logPath = path;
                Execute();
            }
        }

        public static LogService getInstance(string path)
        {
            if (instance == null)
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new LogService(path);
                    }
                }
            }
            return instance;
        }

        ///// <summary>
        ///// 信息跟踪  Info  记录形式为： 时间+信息
        ///// </summary>
        ///// <param name="Message"></param>
        ///// <param name="IsSucc"></param>
        //public void WriteInfoLog(string Message, bool IsSucc)
        //{
        //    string s = IsSucc ? "成功" : "失败";
        //    WriteInfoLog(Message + ",操作结果[" + s + "]");
        //}


        /// <summary>
        /// 数据库操作异常信息跟踪 DBExecute 
        /// </summary>
        /// <param name="ex"></param>
        public void WriteDBExceptionLog(Exception ex, LogTitle logTitle = LogTitle.设备调度日志)
        {
            WriteExceptionLog("DBExecute", ex, logTitle);
        }

        /// <summary>
        /// 自定义文件名称的数据操作异常信息跟踪 title
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="ex"></param>
        public void WriteExceptionLog(string tile, Exception ex, LogTitle logTitle = LogTitle.设备调度日志)
        {
            try
            {
                if (ex != null && ex.Message != "ExceptionTag")
                {
                    StringBuilder sb = new StringBuilder();
                    string NowDateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
                    sb.AppendLine(string.Format("****************************{0},Exception[{1}]****************************", NowDateTime, tile));
                    sb.AppendLine(ex.ToString());
                    sb.AppendLine(ex.Message.ToString());
                    sb.AppendLine(ex.TargetSite.ToString());
                    sb.AppendLine(ex.StackTrace.ToString());
                    addLog("Exception", sb.ToString());
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Info 日志记录函数
        /// </summary>
        /// <param name="Message"></param>
        public void WriteInfoLog(string Message, LogTitle logTitle = LogTitle.设备调度日志)
        {
            if (String.IsNullOrEmpty(Message))
            {
                addLog("Info", "");
            }
            else
            {
                addLog("Info", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "  " + Message);
            }
        }


        /// <summary>
        /// 自定义日志文件名称的日志记录函数 title开头
        /// </summary>
        /// <param name="title"></param>
        /// <param name="Message"></param>
        public void WriteLog(string title, string Message, LogTitle logTitle = LogTitle.设备调度日志)
        {
            if (String.IsNullOrEmpty(Message))
            {
                addLog(title, "");
            }
            else
            {
                addLog(title, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "  " + Message);
            }
        }

        ///// <summary>
        ///// 自定义日志文件名称的日志记录函数 title开头
        ///// </summary>
        ///// <param name="title"></param>
        ///// <param name="Message"></param>
        //public void WriteLog(LogTitle logTitle, string title, string Message)
        //{
        //    if (String.IsNullOrEmpty(Message))
        //    {
        //        addLog(title, "");
        //    }
        //    else
        //    {
        //        addLog(title, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "  " + Message);
        //    }
        //}


        ///// <summary>
        ///// 重要数据记录日志函数 Data
        ///// </summary>
        ///// <param name="Message"></param>
        //public void WriteLineDataLog(string Message)
        //{
        //    if (String.IsNullOrEmpty(Message))
        //    {
        //        addLog("Data", "");
        //    }
        //    else
        //    {
        //        addLog("Data", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "  " + Message);
        //    }
        //}

        /// <summary>
        /// 添加日志到队列
        /// </summary>
        /// <param name="title"></param>
        /// <param name="Message"></param>
        private static void addLog(string title, string Message)
        {
            if (dic.ContainsKey(title) == false)
            {
                Queue<String> queue = new Queue<String>();
                dic.Add(title, queue);
            }
            dic[title].Enqueue(Message);
        }

        /// <summary>
        /// 每隔5秒把队列写入到文本
        /// </summary>
        private void Execute()
        {
            System.Threading.ThreadPool.QueueUserWorkItem((s) =>
            {
                while (true)
                {
                    try
                    {
                        foreach (var item in dic)
                        {
                            if (item.Value.Count > 0)
                            {
                                StringBuilder sb = new StringBuilder();
                                while (item.Value.Count > 0)
                                {
                                    sb.AppendLine(item.Value.Dequeue());
                                }
                                if (!Directory.Exists(logPath))
                                {
                                    Directory.CreateDirectory(logPath);
                                }
                                string fileFullName = System.IO.Path.Combine(logPath, string.Format("{0}{1}.log", item.Key, DateTime.Now.ToString("yyyyMMdd")));
                                if (!System.IO.File.Exists(fileFullName))
                                {
                                    FileStream stream = System.IO.File.Create(fileFullName);
                                    stream.Close();
                                }
                                using (StreamWriter writer = System.IO.File.AppendText(fileFullName))
                                {
                                    writer.WriteLine(sb.ToString());
                                    writer.Close();
                                }
                            }
                        }
                        System.Threading.Thread.Sleep(5000);
                    }
                    catch (Exception ex) { }
                }
            });
            AutoDeleteOldLog();
        }

        /// <summary>
        /// 清除超过30天的日志
        /// </summary>
        private void AutoDeleteOldLog()
        {
            try
            {
                if (Directory.Exists(logPath))
                {
                    DirectoryInfo dirinfo = new DirectoryInfo(logPath);
                    IEnumerable<FileInfo> list = dirinfo.GetFiles("*.log").Where(s => s.CreationTime < DateTime.Now.AddDays(-30));
                    foreach (FileInfo item in list)
                    {
                        item.Delete();
                    }
                }
            }
            catch (Exception msg)
            {

            }
        }

        /// <summary>
        /// 记录普通日志到数据库
        /// </summary>
        /// <param name="title"></param>
        /// <param name="log"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public BllResult<int?> LogContent(LogTitle logTitle , string log,string userCode ,LogLevel flag)
        {
            ContentLog contentLog = new ContentLog();
            contentLog.Title = logTitle.ToString();
            contentLog.Content = log;
            contentLog.Flag = flag.ToString();
            contentLog.Created = DateTime.Now;
            contentLog.CreatedBy = userCode.ToString();
            return AppSession.Dal.InsertCommonModel<ContentLog>(contentLog);
        }

        /// <summary>
        /// 记录接口日志到数据库
        /// </summary>
        /// <param name="interfaceName"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public BllResult<int?> LogInterface(string interfaceName, string request, string response, LogLevel flag, string content, string remark)
        {
            InterfaceLog interfaceLog = new InterfaceLog();
            interfaceLog.InterfaceName = interfaceName;
            interfaceLog.Request = request;
            interfaceLog.Response = response;
            interfaceLog.Flag = flag.ToString();
            interfaceLog.Content = content;
            interfaceLog.Remark = remark;
            interfaceLog.CreatedBy = Accounts.WCSInterface.ToString();
            interfaceLog.Created = DateTime.Now;
            return AppSession.Dal.InsertCommonModel<InterfaceLog>(interfaceLog);
        }
    }
}
