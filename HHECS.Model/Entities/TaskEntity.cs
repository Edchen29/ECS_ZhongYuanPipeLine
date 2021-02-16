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
    [Table("wcstask")]
    public class TaskEntity : BaseModel
    {

        private int preTaskId;

        /// <summary>
        /// 上游的前置任务号在wcs中的内部任务号
        /// </summary>
        public int PreTaskId
        {
            get { return preTaskId; }
            set { preTaskId = value; }
        }

        private string remoteTaskNo;

        /// <summary>
        /// 上游任务号
        /// </summary>
        public string RemoteTaskNo
        {
            get { return remoteTaskNo; }
            set { remoteTaskNo = value; HandlerPropertyChanged("RemoteTaskNo"); }
        }

        /// <summary>
        /// 上游的前置任务号
        /// </summary>
        public string PreRemoteTaskNo { get; set; }

        private int priority;

        /// <summary>
        /// 优先级，约定越大优先级越高
        /// </summary>
        public int Priority
        {
            get { return priority; }
            set { priority = value; HandlerPropertyChanged("Priority"); }
        }

        private int taskType;

        /// <summary>
        /// 任务类型
        /// </summary>
        public int TaskType
        {
            get { return taskType; }
            set { taskType = value; HandlerPropertyChanged("TaskType"); }
        }

        private int taskStatus;

        /// <summary>
        /// 状态
        /// </summary>
        public int TaskStatus
        {
            get { return taskStatus; }
            set { taskStatus = value; HandlerPropertyChanged("TaskStatus"); }
        }


        private string containerCode;

        /// <summary>
        /// 容器编码
        /// </summary>
        public string ContainerCode
        {
            get { return containerCode; }
            set { containerCode = value; HandlerPropertyChanged("ContainerCode"); }
        }

        private string fromLocationCode;

        /// <summary>
        /// 从库位
        /// </summary>
        public string FromLocationCode
        {
            get { return fromLocationCode; }
            set { fromLocationCode = value; HandlerPropertyChanged("FromLocationCode"); }
        }

        private string toLocationCode;

        /// <summary>
        /// 去向库位
        /// </summary>
        public string ToLocationCode
        {
            get { return toLocationCode; }
            set { toLocationCode = value; HandlerPropertyChanged("ToLocationCode"); }
        }

        /// <summary>
        /// 入口port，值为入口的设备code，应用的场景为立库外接输送线，需到达指定入口入库
        /// </summary>
        public string FromPort { get; set; }

        private string toPort;

        /// <summary>
        /// 出库口、拣选口编码，实际制定最终达到的口
        /// </summary>
        public string ToPort
        {
            get { return toPort; }
            set { toPort = value; HandlerPropertyChanged("ToPort"); }
        }


        /// <summary>
        /// 用于实时记录位置的变量
        /// </summary>
        private string gateway;

        /// <summary>
        /// 用于实时记录位置的变量，设备的code
        /// </summary>
        public string Gateway
        {
            get { return gateway; }
            set { gateway = value; }
        }

        ///// <summary>
        ///// 记录任务对应的巷道
        ///// </summary>
        //private int roadway;

        ///// <summary>
        ///// 任务对应的巷道
        ///// </summary>
        //public int Roadway
        //{
        //    get { return roadway; }
        //    set { roadway = value; }
        //}

        public string DestinationArea { get; set; }

        private int stage;

        /// <summary>
        /// 阶段标记，现为出和入
        /// </summary>
        public int Stage
        {
            get { return stage; }
            set { stage = value; HandlerPropertyChanged("Stage"); }
        }

        private int isEmptyOut;

        /// <summary>
        /// 是否空出（默认false)
        /// </summary>
        public int IsEmptyOut
        {
            get { return isEmptyOut; }
            set { isEmptyOut = value; HandlerPropertyChanged("IsEmptyOut"); }
        }

        private int isDoubleIn;

        /// <summary>
        /// 是否重入
        /// </summary>
        public int IsDoubleIn
        {
            get { return isDoubleIn; }
            set { isDoubleIn = value; HandlerPropertyChanged("IsDoubleIn"); }
        }

        private int isForkError;

        /// <summary>
        /// 是否取货错误
        /// </summary>
        public int IsForkError
        {
            get { return isForkError; }
            set { isForkError = value; }
        }

        private string doubleInLocationCode;

        /// <summary>
        /// 发生重入时的库位，默认为空
        /// </summary>
        public string DoubleInLocationCode
        {
            get { return doubleInLocationCode; }
            set { doubleInLocationCode = value; HandlerPropertyChanged("DoubleInLocationCode"); }
        }

        private int sendAgain;

        /// <summary>
        /// 任务重发标志
        /// </summary>
        public int SendAgain
        {
            get { return sendAgain; }
            set { sendAgain = value; HandlerPropertyChanged("SendAgain"); }
        }

        /// <summary>
        /// 当前任务是否已反馈
        /// 请在任务创建时就标定是否需要反馈
        /// </summary>
        public int CommitFlag { get; set; }

        private string platform;

        /// <summary>
        /// 平台
        /// </summary>
        public string Platform
        {
            get { return platform; }
            set { platform = value; }
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
            set { deleted = value; HandlerPropertyChanged("Deleted"); }
        }

        private string reqLength;

        public string ReqLength
        {
            get { return reqLength ?? "0"; }
            set { reqLength = value; }
        }

        private string reqWeight;

        public string ReqWeight
        {
            get { return reqWeight ?? "0"; }
            set { reqWeight = value; }
        }

        private string reqHeight;

        public string ReqHeight
        {
            get { return reqHeight ?? "0"; }
            set { reqHeight = value; }
        }

        private string reqWidth;

        public string ReqWidth
        {
            get { return reqWidth ?? "0"; }
            set { reqWidth = value; }
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
        public Equipment ToPortEquipment { get; set; }
        public Equipment FromPortEquipment { get; set; }
        public Equipment ArrivaEquipment { get; set; }

        public int assignCarNo { get; set; }

        #endregion
    }
}
