using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums.Car
{
    /// <summary>
    /// 穿梭车的任务类型 
    /// </summary>
    public enum CarActionType
    {
        /// <summary>
        /// 初始
        /// </summary>
        [Description("初始")]
        Init = 0,
        /// <summary>
        /// 取货
        /// </summary>
       [Description("取货")]
        TakeGoods = 1,
        /// <summary>
        /// 放货
        /// </summary>
        [Description("放货")]
        PutGoods = 2,
        /// <summary>
        /// 上母车
        /// </summary>
        [Description("上母车")]
        GetOn = 3,
        /// <summary>
        /// 下母车
        /// </summary>
        [Description("下母车")]
        GetOff = 4,
        /// <summary>
        /// 倒料
        /// </summary>
        [Description("倒料")]
        ToMaterial = 5,
        /// <summary>
        /// 上料 
        /// </summary>
        [Description("上料")]
        LoadMaterial = 6,
        /// <summary>
        /// 回端头
        /// </summary>
        [Description("回端头")]
        GetWaitingPoint = 7,
        /// <summary>
        /// 出库查看  
        /// </summary>
        [Description("出库查看")]
        OutBoundCheck = 8,
        /// <summary>
        /// 上空盘
        /// </summary>
        [Description("上空盘")]
        LoadEmpty = 9,
        /// <summary>
        /// 重置行列层
        /// </summary>
        [Description("重置行列层")]
        ResetLocation = 10,
        /// <summary>
        /// 重置位置
        /// </summary>
        [Description("重置位置")]
        ResetPosition = 11
    }

   /// <summary>
   /// 小车的状态
   /// </summary>
    public enum CarStatus
    {
        /// <summary>
        ///待机中
        /// </summary>
        [Description("待机中")]
        idle = 0,
        /// <summary>
        /// 执行中
        /// </summary>
        [Description("执行中")]
        Executing = 1,
        /// <summary>
        /// 完成任务
        /// </summary>
        [Description("完成任务")]
        Finish = 2,
    }

    /// <summary>
    /// 小车就绪状态
    /// </summary>
    public enum CarReadyStatus
    {
        /// <summary>
        /// 初始化
        /// </summary>
        Init = 0,
        /// <summary>
        /// 未准备就绪
        /// </summary>
        Wait = 1,
        /// <summary>
        /// 准备就绪
        /// </summary>
        Ready = 2
    }

    /// <summary>
    /// 小车控制模式
    /// </summary>
    public enum CarControlMode
    {
        关闭 = 0,
        自动 = 1,
        手动行走 = 2,
        手动升降 = 3
    }

    /// <summary>
    /// 穿梭车的任务状态
    /// </summary>
    public enum TaskCarStatus
    {
        /// <summary>
        /// 初始化
        /// </summary>
        Init = 0,
        /// <summary>
        /// 执行中
        /// </summary>
        Executing = 10,
        /// <summary>
        /// 执行完成
        /// </summary>
        Executed = 20,
        /// <summary>
        /// 等待过账
        /// </summary>
        Waiting = 25,
        /// <summary>
        /// 过账
        /// </summary>
        Account = 30
    }

    /// <summary>
    /// 小车工作状态
    /// </summary>
    public enum CarTaskState
    {
        /// <summary>
        /// 初始化
        /// </summary>
        Init = 0,
        /// <summary>
        /// 初始
        /// </summary>
        Start = 10,
        /// <summary>
        /// 执行中
        /// </summary>
        Executing = 20,
        /// <summary>
        /// 完成
        /// </summary>
        Finish = 30
    }

    /// <summary>
    /// 穿梭板上是否有货
    /// </summary>
    public enum HasPallet
    {
        /// <summary>
        /// 初始化
        /// </summary>
        初始化 = 0,
        /// <summary>
        /// 无货
        /// </summary>
        无货 = 1,
        /// <summary>
        /// 有货
        /// </summary>
        有货 = 2,
        
    }

    /// <summary>
    /// 小车到达
    /// </summary>
    public enum ArriveMessage
    {
        /// <summary>
        /// 初始化
        /// </summary>
        默认 = 0,
        /// <summary>
        /// 到达
        /// </summary>
        到达 = 2,
        /// <summary>
        /// 回复到达
        /// </summary>
        回复到达=8,

    }

    /// <summary>
    /// 充电状态
    /// </summary>
    public enum ChargeState
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        Unknown,
        /// <summary>
        /// 充电
        /// </summary>
        [Description("充电")]
        Charging,
        /// <summary>
        /// 未充电
        /// </summary>
        [Description("未充电")]
        Uncharge,
        /// <summary>
        /// 充电错误
        /// </summary>
        [Description("充电错误")]
        Error
    }

    /// <summary>
    /// 电池状态
    /// </summary>
    public enum BatteryState
    {
        /// <summary>
        /// 初始化
        /// </summary>
        Init = 0,
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 馈电
        /// </summary>
        Low = 2,
    }

    /// <summary>
    /// 设备故障
    /// </summary>
    public enum CarError
    {
        /// <summary>
        /// 无故障
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 有故障
        /// </summary>
        Error = 1
    }  

    /// <summary>
    /// 小车位置
    /// </summary>
    public enum CarPosition
    {
        初始化 = 0,
        在巷道中 = 1,
        在母车上 = 2,
        在1充电桩 = 3,
        在2充电桩 = 4,
        等待接空盘 = 5,
        装料点 = 6,
        左端头 = 7,
        右端头 = 8,
    }


}
