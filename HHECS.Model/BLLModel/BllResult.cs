using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.BllModel
{
    public class BllResult<T>
    {
        public BllResult()
        {
        }
        public BllResult(bool success, string msg, T data)
        {
            Success = success;
            Msg = msg;
            Data = data;
        }

        public BllResult(BllResultCode code, string msg, T data)
        {
            Code = code;
            Msg = msg;
            Data = data;
        }
        public bool Success
        {
            get { return Code == BllResultCode.OK ? true : false; }
            set { if (value) { Code = BllResultCode.OK; } else { Code = BllResultCode.Fail; } }
        }

        public string Msg { get; set; }
        public T Data { get; set; }

        public BllResultCode Code { get; set; }

    }

    public class BllResult
    {
        public BllResult()
        {
        }
        public BllResult(bool success, string msg, object data)
        {
            Success = success;
            Msg = msg;
            Data = data;
        }

        public BllResult(BllResultCode code, string msg, object data)
        {
            Code = code;
            Msg = msg;
            Data = data;
        }


        public bool Success
        {
            get { return Code == BllResultCode.OK ? true : false; }
            set { if (value) { Code = BllResultCode.OK; } else { Code = BllResultCode.Fail; } }
        }

        public string Msg { get; set; }
        public object Data { get; set; }

        public BllResultCode Code { get; set; }

    }
}
