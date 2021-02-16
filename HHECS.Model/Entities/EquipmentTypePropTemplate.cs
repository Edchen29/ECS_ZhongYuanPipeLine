using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    [Table("wcsequipmenttypeproptemplate")]
    public class EquipmentTypePropTemplate : BaseModel
    {
        private string code;

        public string Code
        {
            get { return code; }
            set { code = value; HandlerPropertyChanged("Code"); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; HandlerPropertyChanged("Name"); }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; HandlerPropertyChanged("Description"); }
        }

        private string propType;

        public string PropType
        {
            get { return propType; }
            set { propType = value; HandlerPropertyChanged("PropType"); }
        }


        private string dataType;

        public string DataType
        {
            get { return dataType; }
            set { dataType = value; HandlerPropertyChanged("DataType"); }
        }

        private string offset;
        /// <summary>
        /// 偏移量
        /// </summary>
        public string Offset
        {
            get { return offset; }
            set { offset = value; HandlerPropertyChanged("Offset"); }
        }

        private int readLength;
        /// <summary>
        /// 读取长度
        /// </summary>
        public int ReadLength
        {
            get { return readLength; }
            set { readLength = value; HandlerPropertyChanged("ReadLength"); }
        }

        private bool isMonitor;
        private string _monitorCompareValue;
        private string _monitorNormal;
        private string _monitorFailure;
        private string _address;
        private int _equipmentTypeId;

        public bool IsMonitor
        {
            get { return isMonitor; }
            set { isMonitor = value; HandlerPropertyChanged("IsMonitor"); }
        }

        public string MonitorCompareValue { get => _monitorCompareValue; set { _monitorCompareValue = value; HandlerPropertyChanged("MonitorCompareValue"); } }
        public string MonitorNormal { get => _monitorNormal; set { _monitorNormal = value; HandlerPropertyChanged("MonitorNormal"); } }
        public string MonitorFailure { get => _monitorFailure; set { _monitorFailure = value; HandlerPropertyChanged("MonitorFailure"); } }
        public string Address { get => _address;set { _address = value;HandlerPropertyChanged("Address"); } }
        //逻辑外键
        public int EquipmentTypeId
        {
            get => _equipmentTypeId;
            set
            {
                _equipmentTypeId = value;
                HandlerPropertyChanged("EquipmentTypeId");
            }
        }
        public EquipmentType EquipmentType { get; set; }

        private string headCode;
        /// <summary>
        /// 额外记录所属设备类型编码
        /// </summary>
        [Editable(false)]
        public string HeadCode
        {
            get { return headCode; }
            set { headCode = value; HandlerPropertyChanged("HeadCode"); }
        }

        private string headName;
        /// <summary>
        /// 额外记录所属设备类型名称
        /// </summary>
        [Editable(false)]
        public string HeadName
        {
            get { return headName; }
            set { headName = value; HandlerPropertyChanged("HeadName"); }
        }
    }
}
