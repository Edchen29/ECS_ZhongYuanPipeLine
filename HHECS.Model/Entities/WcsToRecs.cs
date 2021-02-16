using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    [Table("wcswarehousealarm")]
    public class WcsToRecs
    {
        private int id;
        //private string warehouseCode;
        private string pn;
        private string instruct;
        private string equipmentCode;
        private string equipmentStatus;
        private string equipmentError;        
        private DateTime? equipmentFailureTime;
        private DateTime? equipmentEndFailureTime;
        private int flag;
        private DateTime? created;
        private string createdBy;

        public int Id { get => id; set => id = value; }
        //public string WarehouseCode { get => warehouseCode; set => warehouseCode = value; }
        public string Pn { get => pn; set => pn = value; }
        public string Instruct { get => instruct; set => instruct = value; }
        public string EquipmentCode { get => equipmentCode; set => equipmentCode = value; }
        public string EquipmentStatus { get => equipmentStatus; set => equipmentStatus = value; }
        public string EquipmentError { get => equipmentError; set => equipmentError = value; }
        public DateTime? EquipmentFailureTime { get => equipmentFailureTime; set => equipmentFailureTime = value; }
        public DateTime? EquipmentEndFailureTime { get => equipmentEndFailureTime; set => equipmentEndFailureTime = value; }
        public int Flag { get => flag; set => flag = value; }
        public DateTime? Created { get => created; set => created = value; }
        public string CreatedBy { get => createdBy; set => createdBy = value; }
    }
}
