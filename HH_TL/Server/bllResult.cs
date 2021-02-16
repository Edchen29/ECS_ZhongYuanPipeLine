using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH_TL.Server
{
    public static class BllResultFactory
    {
        public static BllResult Sucess(Object data, String msg)
        {
            return new BllResult(true, msg, data);
        }

        public static BllResult Sucess(Object data)
        {
            return new BllResult(true, "", data);
        }

        public static BllResult Sucess(String msg)
        {
            return new BllResult(true, msg, null);
        }

        public static BllResult Sucess()
        {
            return new BllResult(true, "", null);
        }

        public static BllResult Error(object data, String msg)
        {
            return new BllResult(false, msg, data);
        }

        public static BllResult Error(object data)
        {
            return new BllResult(false, "", data);
        }

        public static BllResult Error(String msg)
        {
            return new BllResult(false, msg, null);
        }

        public static BllResult Error()
        {
            return new BllResult(false, "", null);
        }

        public static BllResult Create(BllResultCode code, Object data, string msg)
        {
            return new BllResult(code, msg, data);
        }

        #region 泛型
        public static BllResult<T> Create<T>(BllResultCode code, T data, string msg)
        {
            return new BllResult<T>(code, msg, data);
        }
        public static BllResult<T> Sucess<T>(T data, String msg)
        {
            return new BllResult<T>(true, msg, data);
        }

        public static BllResult<T> Sucess<T>(T data)
        {
            return new BllResult<T>(true, "", data);
        }

        public static BllResult<T> Sucess<T>(String msg)
        {
            return new BllResult<T>(true, msg, default(T));
        }

        public static BllResult<T> Sucess<T>()
        {
            return new BllResult<T>(true, "", default(T));
        }

        public static BllResult<T> Error<T>(T data, String msg)
        {
            return new BllResult<T>(false, msg, data);
        }

        public static BllResult<T> Error<T>(T data)
        {
            return new BllResult<T>(false, "", data);
        }

        public static BllResult<T> Error<T>(String msg)
        {
            return new BllResult<T>(false, msg, default(T));
        }

        public static BllResult<T> Error<T>()
        {
            return new BllResult<T>(false, "", default(T));
        }
        #endregion
    }

    public class BllResultFactory<T>
    {
        #region 泛型
        public static BllResult Create(BllResultCode code, Object data, string msg)
        {
            return new BllResult(code, msg, data);
        }

        public static BllResult<T> Sucess(T data, String msg)
        {
            return new BllResult<T>(true, msg, data);
        }

        public static BllResult<T> Sucess(T data)
        {
            return new BllResult<T>(true, "", data);
        }

        public static BllResult<T> Sucess(String msg)
        {
            return new BllResult<T>(true, msg, default(T));
        }

        public static BllResult<T> Sucess()
        {
            return new BllResult<T>(true, "", default(T));
        }

        public static BllResult<T> Error(T data, String msg)
        {
            return new BllResult<T>(false, msg, data);
        }

        public static BllResult<T> Error(T data)
        {
            return new BllResult<T>(false, "", data);
        }

        public static BllResult<T> Error(String msg)
        {
            return new BllResult<T>(false, msg, default(T));
        }

        public static BllResult<T> Error()
        {
            return new BllResult<T>(false, "", default(T));
        }
        #endregion
    }

    public class BllResult<T>
    {
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

        private bool success;

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

        private bool success;

        public bool Success
        {
            get { return Code == BllResultCode.OK ? true : false; }
            set { if (value) { Code = BllResultCode.OK; } else { Code = BllResultCode.Fail; } }
        }

        public string Msg { get; set; }
        public object Data { get; set; }

        public BllResultCode Code { get; set; }

    }

    public enum BllResultCode
    {
        OK,
        Fail,
        Exception
    }
}
