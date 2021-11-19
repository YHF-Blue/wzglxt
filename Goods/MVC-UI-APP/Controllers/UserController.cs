using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_UI_APP.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            //拿到token
            ViewBag.token = HttpContext.Session.GetString("token");
            return View();
        }


       
    }
}
