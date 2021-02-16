using HHECS.Model.Common;
using HHECS.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HHECS.Model.Entities;
using HHECS.Model.Enums.Task;

namespace HHECS.Model.Entities
{
    /// <summary>
    /// 任务实体类
    /// </summary>
    [Table("wcstask_deleted")]
    public class TaskEntityDeleted : BaseModel
    {

        private string remoteTaskNo;

        /// <summary>
        /// 上游任务号
        /// </summary>
        public string RemoteTaskNo
        {
            get { return remoteTaskNo; }
            set { remoteTaskNo = value; }
        }


        private int priority;

        /// <summary>
        /// 优先级，约定越大优先级越高
        /// </summary>
        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        private int taskType;

        /// <summary>
        /// 任务类型
        /// </summary>
        public int TaskType
        {
            get { return taskType; }
            set { taskType = value; }
        }

        private string arriveEquipmentCode;

        /// <summary>
        /// 当前到达的设备，即记录实时位置
        /// </summary>
        public string ArriveEquipmentCode
        {
            get { return arriveEquipmentCode; }
            set { arriveEquipmentCode = value; }
        }

        private string containerCode;

        /// <summary>
        /// 容器编码
        /// </summary>
        public string ContainerCode
        {
            get { return containerCode; }
            set { containerCode = value; }
        }

        private string port;

        /// <summary>
        /// 出库口、拣选口编码，实际制定最终达到的口
        /// </summary>
        public string Port
        {
            get { return port; }
            set { port = value; }
        }

        private string fromLocationCode;

        /// <summary>
        /// 从库位
        /// </summary>
        public string FromLocationCode
        {
            get { return fromLocationCode; }
            set { fromLocationCode = value; }
        }

        private string toLocationCode;

        /// <summary>
        /// 去向库位
        /// </summary>
        public string ToLocationCode
        {
            get { return toLocationCode; }
            set { toLocationCode = value; }
        }

        private int taskStatus;

        /// <summary>
        /// 状态
        /// </summary>
        public int TaskStatus
        {
            get { return taskStatus; }
            set { taskStatus = value; }
        }

        private int stage;

        public int Stage
        {
            get { return stage; }
            set { stage = value; }
        }

        private bool isEmptyOut;

        /// <summary>
        /// 是否空出（默认false)
        /// </summary>
        public bool IsEmptyOut
        {
            get { return isEmptyOut; }
            set { isEmptyOut = value; }
        }

        private bool isDoubleIn;

        /// <summary>
        /// 是否重入
        /// </summary>
        public bool IsDoubleIn
        {
            get { return isDoubleIn; }
            set { isDoubleIn = value; }
        }

        private string doubleInLocationCode;

        /// <summary>
        /// 发生重入时的库位，默认为空
        /// </summary>
        public string DoubleInLocationCode
        {
            get { return doubleInLocationCode; }
            set { doubleInLocationCode = value; }
        }

        private int sendAgain;

        /// <summary>
        /// 任务重发标志
        /// </summary>
        public int SendAgain
        {
            get { return sendAgain; }
            set { sendAgain = value; }
        }

        private string warehouseCode;

        /// <summary>
        /// 仓库编码
        /// </summary>
        public string WarehouseCode
        {
            get { return warehouseCode; }
            set { warehouseCode = value; HandlerPropertyChanged("WarehouseCode"); }
        }


        private bool deleted;

        /// <summary>
        /// 任务是否删除，默认false
        /// </summary>
        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }


        #region 自定义属性

        /// <summary>
        /// 首状态描述
        /// </summary>
        [Editable(false)]
        public string StatusDesc
        {
            get { return typeof(TaskEntityStatus).GetDescriptionString(taskStatus); }
        }

        /// <summary>
        /// 任务类型描述
        /// </summary>
        [Editable(false)]
        public string TaskTypeDesc
        {
            get { return typeof(TaskType).GetDescriptionString(taskType); }
        }

        public Location FromLocation { get; set; }
        public Location ToLocation { get; set; }
        public Equipment PortEquipment { get; set; }
        public Equipment ArrivaEquipment { get; set; }

        #endregion
    }
}
