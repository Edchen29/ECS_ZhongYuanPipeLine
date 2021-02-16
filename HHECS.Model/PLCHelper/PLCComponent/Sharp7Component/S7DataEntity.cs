using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.PLCHelper.PLCComponent.Sharp7Component
{
    /// <summary>
    /// 构造Snap7 dataitem类
    /// </summary>
    public class S7DataEntity
    {
        /// <summary>
        /// DB,M,I...
        /// </summary>
        public int Area { get; set; }

        /// <summary>
        /// 数据类型，word,int...
        /// </summary>
        public int DataType { get; set; }

        /// <summary>
        /// DB块或其他块的索引值
        /// </summary>
        public int DataNumber { get; set; }


        /// <summary>
        /// 起始量
        /// </summary>
        public int DataOffset { get; set; }

        /// <summary>
        /// DB偏移量(注意，这里读取的这个数据类型的数量而不是size)
        /// </summary>
        public int DataAmount { get; set; }

        /// <summary>
        /// 缓存buffer
        /// </summary>
        public byte[] Buffer { get; set; }
    }
}
