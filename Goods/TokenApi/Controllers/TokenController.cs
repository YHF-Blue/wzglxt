using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.UserDTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenApi.Utility;

namespace TokenApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        #region 构造函数注入

        private readonly IJWTService _iJWTService;

        public TokenController(IJWTService service)
        {
            _iJWTService = service;

        }
        #endregion


        /// <summary>
        /// 登录并发放jwt-token
        /// </summary>
        [HttpGet]
        public string GetToken(string userId, string pwd)
        {
            ///这里肯定是需要去连接数据库做数据校验

            if (userId == "u1003" && pwd == "123")
            {
                //本应去数据库查
                UserOutput userOutput = new UserOutput
                {
                    UserId = userId,
                    UserName = "小黑",
                    RoleName = "R1001,R1002",
                    DepartmentId = "1",
                };
                string token = _iJWTService.GetToken(userOutput);
                return JsonConvert.SerializeObject(new
                {
                    result = true,
                    token
                });
            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    result = false,
                    token = ""
                });
            }
        }
    }
}
