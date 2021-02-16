using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    [Table("wcsinterfacelog")]
    public class InterfaceLog : BaseModel
    {
        private string interfaceName;

        public string InterfaceName
        {
            get { return interfaceName; }
            set { interfaceName = value;  HandlerPropertyChanged("InterfaceName"); }
        }

        private string request;

        public string Request
        {
            get { return request; }
            set { request = value;  HandlerPropertyChanged("Request:"); }
        }

        private string response;

        public string Response
        {
            get { return response; }
            set { response = value; HandlerPropertyChanged("Response"); }
        }

        private string flag;

        public string Flag
        {
            get { return flag; }
            set { flag = value; HandlerPropertyChanged("Flag"); }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; HandlerPropertyChanged("Content"); }
        }

        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value;  HandlerPropertyChanged("Remark"); }
        }



    }
}
