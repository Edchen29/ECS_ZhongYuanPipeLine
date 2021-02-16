using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    [Table("task")]
    class WMSTask
    {
        [Key]
        public int Id { get; set; }

        public int MyProperty { get; set; }
    }
}
