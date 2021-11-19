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
    public class MyExceptionAttribute: ExceptionFilterAttribute
    {
        #region 构造函数注入
        private readonly ILogger<MyExceptionAttribute> _logger;

        public MyExceptionAttribute(ILogger<MyExceptionAttribute> logger)
        {

            this._logger = logger;
        }
        #endregion

        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            base.OnException(context);
            //【1】判断异常是否被处理过--如果被处理过，就不再做任何处理，如果未被处理，那么我们就处理它
            //【2】记录日志
            //【3】判断前端的请求，是请求一个页面，还是请求一串数据（ajax）--如果请求的是一个页面，直接返回错误页--如果是ajax请求，直接返回错误码
            //【4】把处理标记字段改为“已被处理”
            if (!context.ExceptionHandled)//context.ExceptionHandled如果为false，说明异常未被处理过
            {
                //记录日志信息
                this._logger.LogError($"{context.HttpContext.Request.RouteValues["controller"]} is Error:" + context.Exception.Message);

                //判断前端发送的请求到底是请求页面，还是ajax请求
                if (this.IsAjaxRequest(context.HttpContext.Request))
                {
                    //如果是ajax请求，就直接返回错误状态码和友好的错误提示给前端
                    context.Result = new JsonResult(new
                    {
                        Code = 500,
                        Result = false,
                        Msg = "服务器，报错了，请联系管理员"
                    });
                }
                else
                {
                    //如果不是ajax请求，直接返回一个错误页面
                    context.Result = new RedirectResult("/Account/Error");


                }
                //把异常处理标记设置为true，就说明异常已经被处理
                context.ExceptionHandled = true;
            }
        }

        private bool IsAjaxRequest(HttpRequest request)
        {
            string header = request.Headers["X-Requested-With"];
            return "XMLHttpRequest".Equals(header);
        }
    }
}
