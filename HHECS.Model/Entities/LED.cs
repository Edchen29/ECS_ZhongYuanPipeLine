using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    /// <summary>
    /// LED表
    /// </summary>
    [Table("wcsled")]
    public class LEDEntity 
    {
        public int? Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string IP { get; set; }

        public uint Port { get; set; }

        public int Timeout { get; set; }

        public string Remark { get; set; }

        public String WarehouseCode { get; set; }

        public string Createdby { get; set; }

        public DateTime Created { get; set; }

        public string Updatedby { get; set; }

        public DateTime Updated { get; set; }


    }
}
