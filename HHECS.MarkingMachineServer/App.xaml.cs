using HHECS.Bll;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Runtime.Remoting;
using System.Threading;

namespace HHECS.MarkingMachineServer
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        #region 属性

        //public static User User { get; set; }

        //public static string WarehouseCode { get; set; } = ConfigurationManager.AppSettings["WarehouseCode"];

        //public static string LogPath { get; set; }



        //public static String PrinterName = ConfigurationManager.AppSettings["PrinterName"];
        //public static String Report = ConfigurationManager.AppSettings["PrinterReport"];

        //public static Dict Url { get; set; } = AppSession.BllService.GetDictWithDetails(SysConst.RemoteUrls.ToString()).Success ? AppSession.BllService.GetDictWithDetails(SysConst.RemoteUrls.ToString()).Data : null;

        //public static Dict PortName { get; set; }= AppSession.BllService.GetDictWithDetails(SysConst.Port.ToString()).Success ? AppSession.BllService.GetDictWithDetails("Port").Data : null;

        /// <summary>
        /// 时钟间隔
        /// </summary>
        //public static double Interval { get; internal set; } = Convert.ToDouble(ConfigurationManager.AppSettings["Interval"]);

        //public static HttpClient Client = new HttpClient(new HttpClientHandler()
        //{
        //    UseCookies = true
        //})
        //{
        //    BaseAddress = new Uri(ConfigurationManager.AppSettings["ServerUrl"])
        //};

        #endregion

        //private void Application_Startup(object sender, StartupEventArgs e)
        //{
        //    //UI线程未捕获异常处理事件（UI主线程）
        //    this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        //    //非UI线程未捕获异常处理事件(例如自己创建的一个子线程)
        //    AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        //    //Task线程内未捕获异常处理事件
        //    TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

        //    //赋值warehouseCode 
        //    //AppSession.WarehouseCode = App.WarehouseCode;
        //}

        ////UI线程未捕获异常处理事件（UI主线程）
        //private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        //{
        //    Exception ex = e.Exception;
        //    string msg = String.Format("{0}\n\n{1}", ex.Message, ex.StackTrace);//异常信息 和 调用堆栈信息
        //    MessageBox.Show(msg, "UI线程异常");
        //    Logger.Log($"UI线程异常{ex.ToString()}", LogLevel.Exception);
        //    e.Handled = true;//表示异常已处理，可以继续运行
        //}

        ////非UI线程未捕获异常处理事件(例如自己创建的一个子线程)
        ////如果UI线程异常DispatcherUnhandledException未注册，则如果发生了UI线程未处理异常也会触发此异常事件
        ////此机制的异常捕获后应用程序会直接终止。没有像DispatcherUnhandledException事件中的Handler=true的处理方式，可以通过比如Dispatcher.Invoke将子线程异常丢在UI主线程异常处理机制中处理
        //private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        //{
        //    if (e.ExceptionObject is Exception ex)
        //    {
        //        string msg = String.Format("发生异常，程序即将停止\n\n{0}\n\n{1}", ex.Message, ex.StackTrace);//异常信息 和 调用堆栈信息
        //        Logger.Log($"非UI线程异常：{ex.ToString()}", LogLevel.Exception);
        //        MessageBox.Show(msg, "非UI线程异常");
        //    }
        //}

        ////Task线程内未捕获异常处理事件
        //private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        //{
        //    Exception ex = e.Exception;
        //    string msg = String.Format("{0}\n\n{1}", ex.Message, ex.StackTrace);
        //    Logger.Log($"非UI线程异常：{ex.ToString()}", LogLevel.Exception);
        //    //MessageBox.Show(msg, "Task异常");
        //}

        ////异常处理 封装
        //private void OnExceptionHandler(Exception ex)
        //{
        //    if (ex != null)
        //    {
        //        string errorMsg = "";
        //        if (ex.InnerException != null)
        //        {
        //            errorMsg += String.Format("【InnerException】{0}\n{1}\n", ex.InnerException.Message, ex.InnerException.StackTrace);
        //        }
        //        errorMsg += String.Format("{0}\n{1}", ex.Message, ex.StackTrace);

        //        AppSession.LogService.WriteExceptionLog("", ex);
        //    }
        //}

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    bool createNew;
        //    Mutex mutex = new Mutex(true, "WCS", out createNew);
        //    if (createNew)
        //        base.OnStartup(e);
        //    else
        //    {
        //        MessageBox.Show("程序已经启动了");
        //        Application.Current.Shutdown();
        //    }
        //}
    }
}
