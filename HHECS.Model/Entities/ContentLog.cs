using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    /// <summary>
    /// 通用日志
    /// </summary>
    [Table("wcscontentlog")]
    public class ContentLog : BaseModel
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; HandlerPropertyChanged("Title"); }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; HandlerPropertyChanged("Content"); }
        }

        private string flag;

        public string Flag
        {
            get { return flag; }
            set { flag = value; HandlerPropertyChanged("Flag"); }
        }



    }
}
