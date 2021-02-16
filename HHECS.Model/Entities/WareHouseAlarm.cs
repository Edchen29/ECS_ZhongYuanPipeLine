using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{

    [Table("wcswarehousealarm")]
    public class WareHouseAlarm : BaseModel
    {

        private string pn;
        private string instruct;
        private string equipmentCode;
       // private string warehouseCode;
        private string equipmentError;
        private string equipmentStatus;
        private int flag;
        private int sendFlag;
        private DateTime equipmentFailureTime;
        private DateTime equipmentEndFailureTime;



        public string Pn { get => pn; set => pn = value; }
        public string Instruct { get => instruct; set => instruct = value; }
        public string EquipmentCode { get => equipmentCode; set => equipmentCode = value; }
     //   public string WarehouseCode { get => warehouseCode; set => warehouseCode = value; }
        public string EquipmentError { get => equipmentError; set => equipmentError = value; }
        public string EquipmentStatus { get => equipmentStatus; set => equipmentStatus = value; }
        public int Flag { get => flag; set => flag = value; }
        public int SendFlag { get => sendFlag; set => sendFlag = value; }
        public DateTime EquipmentFailureTime { get => equipmentFailureTime; set => equipmentFailureTime = value; }
        public DateTime EquipmentEndFailureTime { get => equipmentEndFailureTime; set => equipmentEndFailureTime = value; }

    }
}
