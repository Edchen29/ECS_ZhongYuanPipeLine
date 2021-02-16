using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    /// <summary>
    /// 记录AGV请求数据
    /// </summary>
    [Table("wcsagvmutual")]
    public class AGVRecordRequest
    {
        [Key]
        public int? Id { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string Order_id { get; set; }

        /// <summary>
        /// agv编号
        /// </summary>
        public int Agv_id { get; set; }

        /// <summary>
        /// 库位编号
        /// </summary>
        public string Loc_id { get; set; }

        /// <summary>
        /// 托盘编号
        /// </summary>
        public string Pallet_no { get; set; }

        /// <summary>
        /// Agv上料:1请求上料 2请求上料辊筒转动 3上料完成
        ///Agv下料 :4请求下料 5请求下料辊筒转动 6下料完成
        ///todo:用枚举封装
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 预留参数1
        /// </summary>
        public string Custom_param1 { get; set; }

        /// <summary>
        /// 预留参数2
        /// </summary>
        public string Custom_param2 { get; set; }

        /// <summary>
        /// 记录成功与失败地标志
        /// </summary>
        public int WCSFlag { get; set; }

        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
    }
}
