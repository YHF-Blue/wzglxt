using DotNet_Goods1MVC.Filters;
using Goods.MVC_UI.Filters;
using Goods.MVC_UI.Models;
using IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Goods.MVC_UI.Controllers
{

    public class HomeController : Controller
    {

        #region 依赖注入
        private readonly ILogger<HomeController> _logger;
        private readonly IPowerService _powerService;
        private readonly IUserService _userService;
       public HomeController(ILogger<HomeController> logger, IRoleService roleService, IPowerService powerService, IUserService userService)
        {
            _logger = logger;
            _powerService = powerService;
            _userService = userService;
        }
        #endregion

        [TypeFilter(typeof(MyRolePowerAttribute), Arguments = new string[] { "R1001,R1002,R1003,R1004" })]//验证只有拥有R1001权限才能进入这个控制器
        public IActionResult Index()
        {
            ViewBag.Name = HttpContext.Session.GetString("UserName");

            //获取当前用户拥有所有的角色对应的所有的目录信息（而且是去重之后的目录）
            ViewBag.Menu = _powerService.GetMenu(HttpContext.Session.GetString("RoleId"));
            int id = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            ViewBag.list = _userService.GeUserById(id);

            return View();
        }
        /// <summary>
        /// 启动默认首页
        /// </summary>
        /// <returns></returns>
        public IActionResult HomeIndex()
        {
            return View();
        }
       
    }
}
