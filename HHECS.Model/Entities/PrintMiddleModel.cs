using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    /// <summary>
    /// 打标机
    /// </summary>
    [Table("print_Middle")]
    public class PrintMiddleModel : SysEntity
    {
        /// <summary>
        /// 关联step_trace主表ID
        /// </summary>
        [Column("step_traceId")]
        public string Step_traceId { get; set; }

        /// <summary>
        /// CMD 
        /// 长度：2 格式：01-发送   10-打印  
        /// </summary>
        [Column("cmd")]
        public string Cmd { get; set; }

        /// <summary>
        /// code 
        /// 长度：4 可变	格式：0001 -1号设备  
        /// 员工应该根据这个，选择哪个人工补焊工位的数据
        /// </summary>
        [Column("code")]
        public string Code { get; set; }

        /// <summary>
        ///长度： 4 可变     格式：0011 -11号工人
        /// </summary>
        [Column("user")]
        public string User { get; set; }

        
        /// <summary>
        /// 长度：20 可变	格式：可变的需要添加分隔符; ASCII 3B
        /// 条码，也就是打标机需要的数据
        /// </summary>
        [Column("barcode")]
        public string Barcode { get; set; }

        /// <summary>
        ///长度： 2 固定
        /// </summary>
        [Column("count")]
        public string Count { get; set; }

        /// <summary>
        ///长度： 4 固定	格式：CMD+CODE+BARCODE+COUNT+5SPLIT的字节数和。
        /// </summary>
        [Column("length")]
        public string Length { get; set; }

        /// <summary>
        /// 0默认未发送，1已发送，2发送失败，3需要重新发送
        /// </summary>
        [Column("commintFlag")]
        public int CommintFlag { get; set; }

        /// <summary>
        /// 中控系统维护过的数据，需要更新打印CommintFlag
        /// </summary>
        [Column("editBarcode")]
        public string EditBarcode { get; set; }

        /// <summary>
        /// 打印回复，
        /// 处理成功与失败与否
        /// 或者接收数据成与否
        /// </summary>
        [Column("printReply")]
        public string PrintReply { get; set; }

        /// <summary>
        /// 数据发送到那台设备或ip记录
        /// </summary>
        [Column("sendDevice")]
        public string SendDevice { get; set; }
    }
}
