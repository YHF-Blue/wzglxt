using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goods.MVC_UI.Filters
{

    /// <summary>
    /// 用于配置角色权限的属性--，
    /// 情况1：如果为null，就说明，只需要做登录验证-->比如系统主页
    /// 情况2：如果配置了值：“R1001”说明，只需要用户拥有R1001的角色都能验证通过-->"R1001,R1002"是可以验证通过的
    /// 情况3：如果配置了值：“R1001,R1003”说明，只需要用户拥有R1001和R1003中的其中一个角色都能验证通过--->"R1003,R1002"是可以验证通过的
    /// 
    /// 情况4：如果配置了值：“R1001,R1003?”说明，只需要用户同时拥有R1001和R1003中的才能验证通过(自己拓展)-->"R1003,R1002"是不能验证通过的
    /// </summary>
    public class MyRolePowerAttribute:ActionFilterAttribute
    {


        #region 构造函数注入
        private readonly ILogger<MyRolePowerAttribute> _logger;
        public string ConfigRoleId { get; set; }//属性--程序员配置属性--用于配置该方法能让谁进来

        public MyRolePowerAttribute(ILogger<MyRolePowerAttribute> logger, string configRoleId)
        {
            ConfigRoleId = configRoleId;
            _logger = logger;
        }
        #endregion
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            //session验证，因为只有登陆了，才能获取到RoleId字符串，才能做角色验证
            if (context.HttpContext.Session.GetString("UserId") != null)
            {

                //【1】获取用户拥有的角色
                string roleId = context.HttpContext.Session.GetString("RoleId");

                //【2】判断用户拥有的角色是不是和配置好的ConfigRoleId匹配上，如果匹配上，说明拥有权限-->?先分割，再匹配
                if (ConfigRoleId != null)
                {
                    #region 情况4
                    //情况4  判断数组值一样
                    /*string[] Role = ConfigRoleId.Split(',');//分割配置的

                    string[] role = roleId.Split(',');//分割拥有权限的
                    if (Enumerable.SequenceEqual(Role, role))//如果两个数组一样
                    {
                        return;//说明匹配符上了，验证通过；
                    }*/
                    #endregion

                    string[] arrRole = ConfigRoleId.Split(',');//根据 ， 进行分割
                    foreach (var item in arrRole)
                    {
                        if (roleId.Contains(item))
                        {
                            return;//说明匹配符上了，验证通过；
                        }
                    }
                    //跳转到错误页--同时记录日志（自己写  XXX试图非法访问 xxx方法，ip地址为XXX，时间为XXXX）
                    _logger.LogError($"用户ID：{context.HttpContext.Session.GetString("UserId")}用户名：{context.HttpContext.Session.GetString("UserName")}试图非法访问控制器：{context.HttpContext.Request.RouteValues["controller"]}，时间为{DateTime.Now.ToString("yyyy-MM-dd:HH-mm-ss")}");

                    context.Result = new RedirectResult("/Login/NoPower");//跳转到没有权限页面
                }

            }
            else
            {
                //返回登录页
                context.Result = new RedirectResult("/Login/LoginView");
            }
        }
    }
}
