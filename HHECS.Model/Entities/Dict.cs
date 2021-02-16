using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    [Table("wcsdict")]
    public class Dict : BaseModel
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

        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; HandlerPropertyChanged("Remark"); }
        }

        //逻辑外键，对应明细
        private List<DictDetail> dictDetails;

        public List<DictDetail> DictDetails
        {
            get { if (dictDetails == null) { dictDetails = new List<DictDetail>(); } return dictDetails; }
            set { dictDetails = value; }
        }

    }
}
