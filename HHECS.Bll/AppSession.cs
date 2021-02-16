using HHECS.DAL;
using HHECS.Model.Entities;
using HHECS.Model.LEDHelper;
using System.Configuration;

namespace HHECS.Bll
{
    public static class AppSession
    {
        static AppSession()
        {
            //默认是支持sqlserver，这里更改为mysql
            Dapper.SimpleCRUD.SetDialect(Dapper.SimpleCRUD.Dialect.SQLServer);
        }

        public static string ConnectionString { get; set; } = ConfigurationManager.AppSettings["ConnectionStr"];

        /// <summary>
        /// 这个字段不由配置读取
        /// </summary>
        public static string WarehouseCode { get; set; }

        public static string LogPath { get; set; } = string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["LogPath"]) ? System.IO.Directory.GetCurrentDirectory() + "//LOG" : ConfigurationManager.AppSettings["LogPath"];

        public static DALBase Dal { get; set; } = new DapperDAL(ConnectionString);

        public static ILED LEDExcute { get; set; }

        public static BllService BllService { get; set; } = new BllService();

        public static CommonService CommonService { get; set; } = new CommonService();

        public static ContainerService ContainerService { get; set; } = new ContainerService();

        public static JobService JobService { get; set; } = new JobService();

        public static LocationService LocationService { get; set; } = new LocationService();

        public static TaskService TaskService { get; set; } = new TaskService();
        public static StepTraceService StepTraceService { get; set; } = new StepTraceService();

        public static WMSService WMSService { get; set; } = new WMSService();

        public static ExcuteService ExcuteService { get; set; } = new ExcuteService();

        public static LogService LogService { get; set; } = LogService.getInstance(LogPath);

        //public static MaterialService MaterialService { get; set; } = new MaterialService();

        public static PrinterService PrinterService { get; set; } = new PrinterService();

        public static StationService StationService { get; set; } = new StationService();
        public static User User { get; set; } = new User();
    }
}
