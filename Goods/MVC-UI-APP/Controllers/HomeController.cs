using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_UI_APP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult HomeIndex(string token)
        {
            //把token存起来，存到session中
            HttpContext.Session.SetString("token", token);
            return View();
        }
    }
}
