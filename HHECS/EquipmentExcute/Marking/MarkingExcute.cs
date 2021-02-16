using HHECS.Bll;
using HHECS.EquipmentExcute.Marking.MarkingEnums;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Task;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.EquipmentExcute.Marking
{
    /// <summary>
    /// 打标机处理类
    /// </summary>
    public abstract class MarkingExcute
    {
        /// <summary>
        /// 对应的设备类型
        /// </summary>
        public EquipmentType EquipmentType { get; set; }

        /// <summary>
        /// 打标机处理逻辑
        /// </summary>
        /// <param name="cars"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public virtual BllResult Excute(Equipment marking, IPLC plc)
        {
            return BllResultFactory.Sucess();
        }
    }
}
