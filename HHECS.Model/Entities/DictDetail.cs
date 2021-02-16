using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    [Table("wcsdictdetail")]
    public class DictDetail:BaseModel
    {
        private int headId;

        public int HeadId
        {
            get { return headId; }
            set { headId = value; HandlerPropertyChanged("HeadId"); }
        }

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

        private string value;

        public string Value
        {
            get { return value; }
            set { this.value = value; HandlerPropertyChanged("Value"); }
        }

        private int sort;

        public int Sort
        {
            get { return sort; }
            set { sort = value; HandlerPropertyChanged("Sort"); }
        }


        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; HandlerPropertyChanged("Remark"); }
        }

        private string headCode;
        /// <summary>
        /// 额外记录字典编码
        /// </summary>
        [Editable(false)]
        public string HeadCode
        {
            get { return headCode; }
            set { headCode = value; HandlerPropertyChanged("HeadCode"); }
        }

        private string headName;
        /// <summary>
        /// 额外记录字典名称
        /// </summary>
        [Editable(false)]
        public string HeadName
        {
            get { return headName; }
            set { headName = value; HandlerPropertyChanged("HeadName"); }
        }



    }
}
