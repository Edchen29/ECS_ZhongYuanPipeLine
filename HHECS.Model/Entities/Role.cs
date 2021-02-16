using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    [Table("wcsrole")]
    public class Role : BaseModel
    {
        private string roleName;

        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; HandlerPropertyChanged("RoleName"); }
        }

        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; HandlerPropertyChanged("Remark"); }
        }
        public bool Disable { get; set; }
    }
}
