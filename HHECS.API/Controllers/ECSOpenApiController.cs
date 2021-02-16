using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HHECS.API.Models;
using HHECS.Model;
using HHECS.Model.BllModel;

namespace HHECS.API.Controllers
{
    /// <summary>
    /// 用于对外提供接口
    /// </summary>
    public class ECSOpenApiController : ApiController
    {

        [HttpGet]
        public BllResult<List<string>> GetAllUser(string userCode = "")
        {
            if (string.IsNullOrEmpty(userCode))
            {
                return BllResultFactory<List<string>>.Sucess(new List<string>() { "admin", "user1", "user2" });
            }
            else
            {
                return BllResultFactory<List<string>>.Sucess(new List<string>() { userCode });
            }
        }

        [HttpPost]
        public BllResult Login(LoginModel model)
        {
            if (model.UserCode == "admin" && model.Password == "123")
            {
                return BllResultFactory.Sucess();
            }
            else
            {
                return BllResultFactory.Error("用户名或密码错误");
            }
        }

    }
}
