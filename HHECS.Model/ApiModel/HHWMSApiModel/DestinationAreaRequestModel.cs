using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.ApiModel.HHWMSApiModel
{
    /// <summary>
    /// 目标区域获取
    /// </summary>
    public class DestinationAreaRequestModel
    {
        private string taskNo;
        private string height;
        private string weight;
        private string length;
        private string width;

        public string Height { get => height; set => height = value; }
        public string Weight { get => weight; set => weight = value; }
        public string Length { get => length; set => length = value; }
        public string Width { get => width; set => width = value; }
        public string TaskNo { get => taskNo; set => taskNo = value; }
    }
}
