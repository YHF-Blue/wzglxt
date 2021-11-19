using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[Action]")] //配置路由规则，这里先不使用Restful，我们使用和MVC默认一样的路由规则；这样，够用了
    /* [Route("api/[controller]")]*/
    [ApiController]//跟model绑定相关
    public class UserController : ControllerBase
    {
        [HttpPost]
        public string Delete(string UserID)
        {
            return "加油 加油!123" + UserID;
        }

        [HttpGet]
        public string AA()
        {
            return "ok";
        }

        [Authorize]//使用token授权
        [HttpGet]
        
        public string GetUser(string UserID)
        {
            //获取token中的Claims值--
            var roleId = HttpContext.AuthenticateAsync().Result.Principal.Claims.FirstOrDefault(a => a.Type.Equals("RoleId"))?.Value;
            return "加油 加油!123"+ roleId;
        }
    }
}
