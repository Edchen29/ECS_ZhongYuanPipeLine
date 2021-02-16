using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.PipeLine;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Threading.Tasks;

namespace HHECS.EquipmentExcute.Marking
{
    /// <summary>
    /// 激光打标机
    /// </summary>
    public class MarkingNormalExcute : MarkingExcute
    {
        /// <summary>
        /// 激光打标机处理逻辑
        /// </summary>
        /// <param name="cars"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult Excute(Equipment marking, IPLC plc)
        {
            return BllResultFactory.Sucess();
        }

        private static bool IsOpen = false;

        /// <summary>
        /// 时钟同步锁
        /// </summary>
        private static object locker = new object();

        /// <summary>
        /// 把条码发送给打标机
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public static BllResult PrintBarcode(string barcode, string serialNumber)
        {
            #region 旧代码
            //开启监听
            //if (IsOpen == false)
            //{
            //    SendDataToMarking.Start();
            //    IsOpen = true;
            //}

            ////发送tx7指令开始打标
            //SendDataToMarking.SendData(barcode);
            //Action action = SendDataToMarking.Print;
            //var printResult = action.BeginInvoke(null, null);
            //printResult.AsyncWaitHandle.WaitOne();//阻塞当前线程直到收到信号量

            //Action action1 = SendDataToMarking.DeleMark;
            //var deleMark = action1.BeginInvoke(null, null);
            //deleMark.AsyncWaitHandle.WaitOne();//阻塞当前线程直到收到信号量

            ////打印完成修改套料状态
            //ChageStatus(barcode);

            //return BllResultFactory.Sucess();
            #endregion
            var task = Task.Run(() =>
             {
                 lock (locker)
                 {
                     try
                     {
                         SendDataToMarking.SendData(barcode);
                         SendDataToMarking.Print();
                         SendDataToMarking.DeleMark();
                         ChageStatus(serialNumber);
                     }
                     catch (Exception ex)
                     {
                         //如果打标失败，就把套料的状态改为 打标之前的状态
                         var sql = $"update cutplan set status = {CutPlanStatus.发送定长.GetIndexInt()},updateBy='{App.User.UserCode}',updateTime='{DateTime.Now}' where id = {serialNumber}";
                         AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
                         Logger.Log($"打标程序处理出现异常，已经回滚套料工单状态为[{CutPlanStatus.发送定长}]，异常提示：{ex.Message}", LogLevel.Exception, ex);
                     }
                 }
             });
            task.Start();
            return BllResultFactory.Sucess();
        }

        /// <summary>
        /// 把套料状态改为 已打标
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public static BllResult<int> ChageStatus(string serialNumber)
        {
            var sql = $"update cutplan set status = {CutPlanStatus.打标完成.GetIndexInt()},updateBy='{App.User.UserCode}',updateTime='{DateTime.Now}' where id = {serialNumber}";
            var updateResult = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
            if (updateResult.Success)
            {
                Logger.Log($"打标机打码成功，修改套料工单[{serialNumber}]的状态为[{CutPlanStatus.打标完成}]！", LogLevel.Success);
            }
            else
            {
                Logger.Log($"打标机打码成功，修改套料工单[{serialNumber}]的状态为[{CutPlanStatus.打标完成}]失败！原因：{updateResult.Msg}", LogLevel.Error);
            }
            return updateResult;
        }
    }
}
