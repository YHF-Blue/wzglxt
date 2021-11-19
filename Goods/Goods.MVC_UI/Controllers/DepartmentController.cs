using DataEntity.Entities;
using IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goods.MVC_UI.Controllers
{
    public class DepartmentController : Controller
    {
        #region 依赖注入
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        #endregion
        public IActionResult DepartmentIndex()
        {
            return View();
        }
        /// <summary>
        /// 添加部门  
        /// </summary>
        /// <returns></returns>
        public IActionResult AddIndex()
        {
           //获取主管
            ViewBag.Director = _departmentService.GetDepartmentDirector();
            //获取父级部门
            ViewBag.Boos = _departmentService.GetBossDepartment();
            return View();
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateIndex(int id)
        {
            //获取主管
            ViewBag.Director = _departmentService.GetDepartmentDirector();
            //获取父级部门
           ViewBag.Boos = _departmentService.GetBossDepartment();

            //根据ID查询
            ViewBag.department = _departmentService.GetDepartmentById(id);
            return View();
        }



        /// <summary>
        /// 分页查询部门信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="selectInfo"></param>
        /// <returns></returns>
        public IActionResult GetDepartments(int page, int limit, string selectInfo)
        {
            int totalCount = 0;
            var list = _departmentService.GeDepartmentListPages(page, limit, out totalCount, selectInfo);

            return new JsonResult(new
            {
                code = 0,
                msg = "",
                count = totalCount,
                data = list //数据
            });
        }


        /// <summary>
        /// 验证部门是否存在
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string ChenkRepeat(string value)
        {
            if (_departmentService.CheckRepeat(value))
            {
                return "no";//用户已存在
            }
            return "ok";
        }



        public string Add(DepartmentInfo inputEntity)
        {
            //必须对前端传过来的东西进行验证
            if (!ModelState.IsValid)
            {
                //说明验证不通过
                return "no";
            }
            if (_departmentService.Add(inputEntity))
            {
                return "ok";
            }
            return "no";
        }

        /// <summary>
        /// 部门修改
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <returns></returns>
        public string Update(DepartmentInfo inputEntity)
        {
            //必须对前端传过来的东西进行验证
            if (!ModelState.IsValid)
            {
                //说明验证不通过
                return "no";
            }
            if (_departmentService.Update(inputEntity))
            {
                return "ok";
            }
            return "no";
        }

    }
}
