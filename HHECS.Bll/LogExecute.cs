using HHECS.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Bll
{
    /// <summary>
    /// 日志帮助组件
    /// </summary>
    public class LogExecute
    {
        private static Dictionary<String, Queue<String>> dic = new Dictionary<String, Queue<String>>();
        
        static readonly string LogPath = "E:\\Log\\WcsLog";

        public const string ExceptionTag = "ExceptionTag";

        public static void WriteInfoLog(string Message, bool IsSucc)
        {
            string s = IsSucc ? "成功" : "失败";
            WriteInfoLog(Message + ",操作结果[" + s + "]");
        }

        public static void WriteDBExceptionLog(Exception ex)
        {
            WriteExceptionLog("DBExecute", ex);
        }

        public static void WriteExceptionLog(string tile, Exception ex)
        {
            try
            {
                if (ex != null && ex.Message != ExceptionTag)
                {
                    StringBuilder sb = new StringBuilder();
                    string NowDateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
                    sb.AppendLine(string.Format("****************************{0},Exception[{1}]****************************", NowDateTime, tile));
                    sb.AppendLine(ex.ToString());
                    addLog("Exception", sb.ToString());
                }
            }
            catch
            {
            }
        }

        public static void WriteInfoLog(string Message)
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

        public static void WriteLineDataLog(string Message)
        {
            if (String.IsNullOrEmpty(Message))
            {
                addLog("Data", "");
            }
            else
            {
                addLog("Data", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "  " + Message);
            }
        }

        public static void WriteLog(string title, string Message)
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

        private static void addLog(string title, string Message)
        {
            if (dic.ContainsKey(title) == false)
            {
                Queue<String> queue = new Queue<String>();
                dic.Add(title, queue);
            }
            dic[title].Enqueue(Message);
        }


        static LogExecute()
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
                                if (!Directory.Exists(LogPath))
                                {
                                    Directory.CreateDirectory(LogPath);
                                }
                                string fileFullName = System.IO.Path.Combine(LogPath, string.Format("{0}{1}.log", item.Key, DateTime.Now.ToString("yyyyMMdd")));
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

        private static void AutoDeleteOldLog()
        {
            try
            {
                if (Directory.Exists(LogPath))
                {
                    DirectoryInfo dirinfo = new DirectoryInfo(LogPath);
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

        //static void WriteLogExecute(string FileName, string Message)
        //{
        //    //如果日志文件目录不存在,则创建
        //    if (!Directory.Exists(LogPath))
        //    {
        //        Directory.CreateDirectory(LogPath);
        //    }
        //    string filename = LogPath + "\\" + FileName + "_" + System.DateTime.Now.ToString("yyyyMMdd") + ".txt";

        //    try
        //    {
        //        FileStream fs = new FileStream(filename, FileMode.Append);
        //        StreamWriter strwriter = new StreamWriter(fs);
        //        try
        //        {
        //            strwriter.WriteLine(Message);
        //            strwriter.Flush();
        //        }
        //        catch
        //        {
        //        }
        //        finally
        //        {
        //            strwriter.Close();
        //            strwriter = null;
        //            fs.Close();
        //            fs = null;
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}
    }
}
