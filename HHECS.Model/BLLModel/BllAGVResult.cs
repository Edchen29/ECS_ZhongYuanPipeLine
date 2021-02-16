using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.BllModel
{
    public class Request_Result
    {
        public Request_Result() { }
        public BllAGVResult request_result = new BllAGVResult();
    }


    public class BllAGVResult
    {

        public BllAGVResult()
        {
        }
        public BllAGVResult(int return_Code, string return_Desc)
        {
            return_code = return_code;
            return_desc = return_desc;
        }

        public int return_code { get; set; }

        public string return_desc { get; set; }
    }
}

