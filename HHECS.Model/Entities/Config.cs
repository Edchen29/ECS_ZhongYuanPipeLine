using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    [Table("wcsconfig")]
    public class Config:BaseModel
    {
        private string code;

        public string Code
        {
            get { return code; }
            set { code = value;  HandlerPropertyChanged("Code"); }
        }

        private string warehouseCode;

        public string WarehouseCode
        {
            get { return warehouseCode; }
            set { warehouseCode = value; HandlerPropertyChanged("WarehouseCode"); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; HandlerPropertyChanged("Name"); }
        }

        private string value;

        public string Value
        {
            get { return value; }
            set { this.value = value; HandlerPropertyChanged("Value"); }
        }

        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; HandlerPropertyChanged("Remark"); }
        }



    }
}
