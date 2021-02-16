using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.ApiModel.HHWMSApiModel
{
    public class LocationAssignReqModelInfo
    {
        private string taskNo;
        private string destinationArea;
        private string height;
        private string weight;
        private string length;
        private string width;

        public string Height { get => height; set => height = value; }
        public string Weight { get => weight; set => weight = value; }
        public string Length { get => length; set => length = value; }
        public string Width { get => width; set => width = value; }
        public string DestinationArea { get => destinationArea; set => destinationArea = value; }
        public string TaskNo { get => taskNo; set => taskNo = value; }

        public override string ToString()
        {
            return $" 任务：{taskNo},巷道:{destinationArea},长度:{length},宽度:{width},高度:{height},重量:{weight} ";
        }
    }
}
