using CaptchaGen.NetCore;
using DataEntity.Entities;
using Goods.MVC_UI.Models;
using IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.UserDTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Goods.MVC_UI.Controllers
{
    

    public class LoginController : Controller
    {

        #region 依赖注入

        private IAccountService _iaccountService;
        private IRoleService _iroleService;
        private ILogger<LoginController> _logger;
        public LoginController(IAccountService accountService, ILogger<LoginController> logger, IRoleService roleService)
        {
            _iaccountService = accountService;
            _iroleService = roleService;
            _logger = logger;
        }
        #endregion

        #region 页面
        /// <summary>
        /// 登陆视图
        /// </summary>
        /// <returns></returns>
        /// 
        //[HttpGet]//必须是get方式请求--展示登录页面
        public IActionResult LoginView()
        {
            HttpContext.Session.Clear();
            //清除全部Session
            return View();
        }

        /// <summary>
        /// 非法访问，无权限视图提示
        /// </summary>
        /// <returns></returns>
        public ActionResult NoPower()
        {
            return View();
        }

        /// <summary>
        /// 修改密码页面
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdadePwd()
        {
            ViewBag.UserId = HttpContext.Session.GetString("UserId");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }


        #endregion



        /// <summary>
        /// 登陆请求
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string UserID,string PassWord,string SecurityCode)
        {
            //验证模型是否正确
            if (ModelState.IsValid)
            {
                //string a = (string)TempData["VerifyCode"];
                //如果效验通过，才开始做下面的事
                if (SecurityCode == (string)TempData["VerifyCode"])
                {
                    UserOutput userOutPut = _iaccountService.Login(UserID, PassWord);
                    //UserInfo user = _iaccountService.Login(model.UserId, model.PassWord);
                    if (userOutPut != null)
                    {
                        //把用户信息存到session中，给后续的页面/方法使用、、、、、、、
                        HttpContext.Session.SetString("UserId", userOutPut.UserId);
                        HttpContext.Session.SetString("UserName", userOutPut.UserName);
                        HttpContext.Session.SetString("Id", userOutPut.Id.ToString());
                        //return RedirectToAction("Index", "Home");//跳转到Home控制器下的Index
                       // HttpContext.Session.Clear();
                       //清除全部Session

                       //为了做基于角色的权限控制，需要先拿到用户所拥有的所有的RoleId集合（或者字符串组合）
                       var roleId = _iroleService.GetUserRoles(UserID);
                        HttpContext.Session.SetString("RoleId", roleId);

                        //登录成功，跳转到主页
                        return RedirectToAction("Index", "Home");
                       // return Content("0");
                        //登陆成功，跳转
                    }
                    else
                    {
                        return Content("2");
                        //密码错误
                    }
                }
                else
                {
                    return Content("1");
                    //验证码错误
                }
            }
            else
            {
                return Content("3");
                //三个输入框有没填写完
            }
            

        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public string  UPwd(string UserID,string PassWord, string NewPassWord)
        {
            if (_iaccountService.UpdatePwd(UserID, PassWord, NewPassWord))
            {
                return "ok";
            }
            return "no";
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult SecurityCode()
        {
            //生成验证码值
            string Security = new SecurityHelper().CreateVerifyCode(4);
            TempData["VerifyCode"] = Security;
            MemoryStream ms = ImageFactory.BuildImage(Security, 60, 130, 40, 2);//40是大小
            return File(ms, "image/jpeg");
        }
    }
}
