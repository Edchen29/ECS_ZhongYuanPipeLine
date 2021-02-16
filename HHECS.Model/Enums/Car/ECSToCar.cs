using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums.Car
{
    public  enum ECSToCar
    {
        /// <summary>
        /// 输入任务号
        /// </summary>
        wcsTaskHeaderId ,
        /// <summary>
        /// 输入子任务号
        /// </summary>
        wcsTaskCarId,
        /// <summary>
        /// 输入作业类型
        /// </summary>
        wcsActionType,
        /// <summary>
        /// 输入取货排
        /// </summary>
        wcsStartRow,
        /// <summary>
        /// 输入放货排
        /// </summary>
        wcsDestinationRow,
        /// <summary>
        /// 输入开启信号
        /// </summary>
        wcsSwitch,
        /// <summary>
        /// 输入删除任务指令
        /// </summary>
        wcsDeleteCommand,
        /// <summary>
        /// 输入任务过账确认
        /// </summary>
        wcsConfirmTaskFinish,
        /// <summary>
        /// 输入确认小车到达信号
        /// </summary>
        wcsArriveMessage,
        /// <summary>
        /// 控制模式
        /// </summary>
        wcsControlMode,
        /// <summary>
        /// 输入复位信号
        /// </summary>
        wcsResetCommand,


    }

}
