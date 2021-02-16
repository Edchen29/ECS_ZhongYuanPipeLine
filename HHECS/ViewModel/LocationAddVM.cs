using HHECS.Model.BllModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.ViewModel
{
    public class LocationAddVM
    {
        public string Prefix { get; set; }

        public int TotalRows { get; set; }

        public int TotalColumns { get; set; }

        public int TotalLayers { get; set; }

        public int RowIndex1 { get; set; }

        public int RowIndex2 { get; set; }

        /// <summary>
        /// 堆垛机标记
        /// </summary>
        public string SRMCode { get; set; }

        /// <summary>
        /// 库位的类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 连接符
        /// </summary>
        public string Connector { get; set; }

        public string DestinationArea { get; set; }

        public string WarehouseCode { get; set; }


        public BllResult Validate()
        {
            if (string.IsNullOrWhiteSpace(SRMCode) || TotalRows == 0 || TotalColumns == 0 || TotalLayers == 0 || RowIndex1 == 0 || RowIndex2 == 0 || string.IsNullOrWhiteSpace(Type) || string.IsNullOrWhiteSpace(WarehouseCode))
            {
                return BllResultFactory.Error("请将属性录入完整");
            }
            else
            {
                return BllResultFactory.Sucess();
            }
        }

    }
}
