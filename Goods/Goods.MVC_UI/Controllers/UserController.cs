using Goods.MVC_UI.Filters;
using IService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.UserDTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Goods.MVC_UI.Controllers
{
    [TypeFilter(typeof(MyRolePowerAttribute), Arguments = new string[] { "R1001,R1002,R1003,R1004" })]//验证只有拥有R1001权限才能进入这个控制器
    public class UserController : Controller
    {

        #region 依赖注入
        private readonly IUserService _userService;
        private readonly IDepartmentService _departmentService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public UserController(IUserService userService, IDepartmentService departmentService, IRoleService roleService, IWebHostEnvironment webHostEnvironment)
        {
            _hostingEnvironment = webHostEnvironment;
            _userService = userService;
            _departmentService = departmentService;
        }
        #endregion



        #region 页面
        /// <summary>
        /// 用户信息的主页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 添加用户信息页面
        /// </summary>
        /// <returns></returns>
        public IActionResult AddIndex()
        {
            //获取部门信息-->用于前端页面初始化

            ViewBag.departmentList = _departmentService.GetDepartments();

            return View();
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult UpdateIndex(int id)
        {
            //获取部门信息-->用于前端页面初始化
            ViewBag.departmentList = _departmentService.GetDepartments();
            //获取需要修改的值
            ViewBag.UserList = _userService.GeUserById(id);
            return View();
           
        }

        /// <summary>
        /// 登陆用户的基本信息
        /// </summary>
        /// <returns></returns>
        public IActionResult InformationView(int id)
        {
            string Id = HttpContext.Session.GetString("Id");
            id =Convert.ToInt32(Id);
            ////获取部门信息-->用于前端页面初始化
            ViewBag.departmentList = _departmentService.GetDepartments();
            ////获取需要修改的值
            ViewBag.UserList = _userService.GeUserById(id);
            return View();
        }


        #endregion


        #region 非页面
        /// <summary>
        /// 分页获取用户信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="selectInfo"></param>
        /// <returns></returns>
        public string  GetUser(int page, int limit, string selectInfo)
        {

            //配置数据条数默认值为0
            int totalCount = 0;

            //分页查询
            var list = _userService.GeUserListPages(page, limit, out totalCount, selectInfo);

           /* return new JsonResult(new
            {
                code = 0,
                msg = "",
                count = totalCount,
                data = list
            });
*/
            //去数据库查


            return JsonNetHelper.SerializetoJson(new
            {
                code = 0,
                msg = "",
                count = totalCount,//一共有多少条数据，用于前端动态生成页码。
                data = list,

            });

        }

        /// <summary>
        /// 禁用或开启用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="delFlag"></param>
        /// <returns></returns>
        public string ForbidUser(int id, string delFlag)
        {
            if (HttpContext.Session.GetString("Id") == id.ToString())
            {
                //登录人，不能禁用自己
                return "noyou";
            }
            if (_userService.ForbidUser(id, delFlag))
            {
                return "ok";
            }
            return "no";
        }

        /// <summary>
        /// 验证用户是否存在
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string ChenkRepeat(string value)
        {
            if (_userService.CheckRepeat(value))
            {
                return "no";//用户已存在
            }
            return "ok";
        }


        /// <summary>
        /// 用户信息添加
        /// </summary>
        /// <returns></returns>
        public string Add(UserInput userInput)
        {
            //必须对前端传过来的东西进行验证
            if (!ModelState.IsValid)
            {
                //说明验证不通过
                return "no";
            }
            if (_userService.Add(userInput))
            {
                return "ok";
            }
            return "no";
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>

        public string Update(UserInput userInput)
        {

            if (!ModelState.IsValid)
            {
                //说明验证不通过
                return "no";
            }

            if (_userService.Update(userInput))
            {
                return "ok";
            }
            return "no";

        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        public string ResetPwd(int[] id)
        {
            if (_userService.ResetPwd(id))
            {
                return "ok";
            }
            else
            {
                return "no";
            }
        }




        //图片上传
        public async Task<ActionResult> PictureUpload()
        {
            try
            {
                IFormFileCollection files = Request.Form.Files;

                var file = files[0];
                //获取文件名后缀
                string extName = Path.GetExtension(file.FileName).ToLower();
                //获取保存目录的物理路径
                if (System.IO.Directory.Exists(_hostingEnvironment.WebRootPath + "/upload/") == false)//如果不存在就创建images文件夹
                {
                    System.IO.Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "/upload/");
                }
                string path = _hostingEnvironment.WebRootPath + "/upload/"; //path为某个文件夹的绝对路径，不要直接保存到数据库
                                                                            //    string path = "F:\\TgeoSmart\\Image\\";
                                                                            //生成新文件的名称，guid保证某一时刻内图片名唯一（文件不会被覆盖）
                string fileNewName = Guid.NewGuid().ToString();
                string ImageUrl = path + fileNewName + extName;
                //SaveAs将文件保存到指定文件夹中
                using (var stream = new FileStream(ImageUrl, FileMode.Create))
                {

                    await file.CopyToAsync(stream);
                }

                //此路径为相对路径，只有把相对路径保存到数据库中图片才能正确显示（不加~为相对路径）
                string url = "\\upload\\" + fileNewName + extName;

                HttpContext.Session.SetString("Ur", url);
                var UserId = HttpContext.Session.GetString("UserId");
                var Ur = HttpContext.Session.GetString("Ur");
                _userService.Upload(UserId, Ur);


                return Json(new
                {
                    Result = true,
                    Data = url
                });
            }
            catch (Exception exception)
            {
                return Json(new
                {
                    Result = false,
                    exception.Message
                });
            }
        }

        #endregion



    }
}
