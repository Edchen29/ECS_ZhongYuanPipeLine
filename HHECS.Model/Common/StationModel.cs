using HHECS.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Common
{
    /// <summary>
    /// 站台模型类
    /// </summary>
    public class StationModel : INotifyPropertyChanged
    {
        public string ManualSign { get; set; }
        public string ProductId { get; set; }

        public string RequestGoodsCount { get; set; }
        public string Request_Load { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public string RequestMessage { get; set; }
        public string RequestLoadStatus { get; set; }
        public string RequestNumber { get; set; }
        public string RequestBarcode { get; set; }
        public string RequestWeight { get; set; }
        public string RequestLength { get; set; }
        public string RequestWidth { get; set; }
        public string RequestHeight { get; set; }
        public string RequestRetCode { get; set; }
        public string ArriveMessage { get; set; }
        public string ArriveResult { get; set; }
        public string ArriveRealAddress { get; set; }
        public string ArriveAllcationAddress { get; set; }
        public string ArriveBarcode { get; set; }
        public string WCSReplyMessage { get; set; }
        public string WCSReplyLoadStatus { get; set; }
        public string WCSReplyNumber { get; set; }
        public string WCSReplyBarcode { get; set; }
        public string WCSReplyWeight { get; set; }
        public string WCSReplyLength { get; set; }
        public string WCSReplyWidth { get; set; }
        public string WCSReplyHeight { get; set; }
        public string WCSReplyAddress { get; set; }
        public string WCSACKMessage { get; set; }
        public string WCSACKLoadStatus { get; set; }
        public string WCSACKNumber { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetProp(List<EquipmentProp> props)
        {
            if (props == null)
            {
                return;
            }
            foreach (var item in this.GetType().GetProperties())
            {
                var temp = props.FirstOrDefault(t => t.EquipmentTypeTemplateCode == item.Name);
                if (temp != null)
                {
                    item.SetValue(this, temp.Value);
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(item.Name));
                    }
                }
                //if (item.Name == "ManualSign")
                //{
                //    var Type = props.FirstOrDefault(t => t.EquipmentTypeTemplateCode == "ManualSign");
                //    if (Type == null)
                //    {
                //        item.SetValue(this, "未获取");
                //    }
                //    else
                //    {
                //        if (Type.Value == 1.ToString())
                //        {
                //            item.SetValue(this, "手动确认");
                //        }
                //        else
                //        {
                //            item.SetValue(this, "自动");
                //        }
                //    }
                //    if (this.PropertyChanged != null)
                //    {
                //        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(item.Name));
                //    }
                //}

            }
        }
    }
}
