using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet_Goods1MVC.Filters
{
    /// <summary>
    ///如何使用过滤器
    ///1.编写一个自定义类，继承行为结果过滤器
    ///2.根据你的需求（目前我们需要的是行为前过滤器），重写对应的过滤器，并在过滤器种写你自己的业务逻辑
    ///3.使用过滤器（3种使用方式  --1.方法特性 2.控制器特性  3.startup文件全局注册）
    /// </summary>
    public class MySessionAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            //if (context.ActionDescriptor.lsDefined(typeof(AllowAnonymousAttribute),true))
            //{

            //}

            //写业务逻辑-session判断--防“外敌”--遗留问题：“内鬼”如何防止 这个时候就要用roleid来判断了--基于角色的权限控制
            if (context.HttpContext.Session.GetString("UserId") != null)
            {
                //不做任何处理，代码就会进入到action中--->就认为验证通过-->
            }
            else
            {
                //跳转登录页
                context.Result = new RedirectResult("/Login/LoginView");
            }

        }
    }
}
