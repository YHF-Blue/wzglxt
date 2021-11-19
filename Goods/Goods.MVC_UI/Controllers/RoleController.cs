using DataEntity.Entities;
using IService;
using Microsoft.AspNetCore.Mvc;
using Model.PowerInfoDTO;
using Model.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goods.MVC_UI.Controllers
{
    public class RoleController : Controller
    {
        #region 依赖注入
        private readonly IRoleService _roleService;
        private IPowerService _powerService;
       public RoleController(IRoleService roleService, IPowerService powerService)
        {
            _roleService = roleService;
            _powerService = powerService;
        }
        #endregion


        #region 页面
        public IActionResult RoleIndex()
        {
            return View();
        }

        public IActionResult RoleAdd()
        {
            return View();
        }

        /// <summary>
        /// 分配权限
        /// </summary>
        /// <returns></returns>
        public IActionResult PrivilegesIndex(string RoleId)
        {
            ViewBag.RoleId = RoleId;
            return View();
        }

        public IActionResult UpdateIndex(int id)
        {
            ViewBag.RoleList = _roleService.GetById(id);
            return View();
        }

        /// <summary>
        /// 配置角色信息   
        /// </summary>
        /// <returns></returns>
        public IActionResult ConfigIndex(string UserId)
        {
            ViewBag.UserId = UserId;
            return View();
        }
        #endregion



        /// <summary>
        /// 分页查角色信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="selectInfo"></param>
        /// <returns></returns>
        public IActionResult GetRoleList(int page, int limit, string selectInfo)
        {

            //配置数据条数默认值为0
            int totalCount = 0;

            //分页查询
            var list = _roleService.GetRolesList(page, limit, out totalCount, selectInfo);

            return new JsonResult(new
            {
                code = 0,
                msg = "",
                count = totalCount,
                data = list //数据
            });
            //return null;
        }
        /// <summary>
        /// 用于分配角色，展示未被禁用的
        /// </summary>
        /// <returns></returns>
        public IActionResult GetRoles()
        {

            //配置数据条数默认值为0
            int totalCount = 0;

            //分页查询
            var list = _roleService.GetRoles();

            return new JsonResult(new
            {
                code = 0,
                msg = "",
                count = totalCount,
                data = list //数据
            });
            //return null;
        }

        /// <summary>
        /// 禁用或开启用户
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="delFlag"></param>
        /// <returns></returns>
        public string ForbidRole(int id, string delFlag)
        {

            /*if (HttpContext.Session.GetString("Id") == id.ToString())
            {
                //登录人，不能禁用自己
                return "noyou";
            }*/
            if (_roleService.ForbidUser(id, delFlag))
            {
                return "ok";
            }
            return "no";
        }

        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        public string  Update(RoleInfo roleInfo)
        {
            if (!ModelState.IsValid)
            {
                //说明验证不通过
                return "no";
            }

            if (_roleService.Update(roleInfo))
            {
                return "ok";
            }
            return "no";
        }


        /// <summary>
        /// 验证角色是否存在
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string ChenkRepeat(string value)
        {
            if (_roleService.CheckRepeat(value))
            {
                return "no";//用户已存在
            }
            return "ok";
        }

        /// <summary>
        /// 角色信息添加
        /// </summary>
        /// <returns></returns>
        public string Add(RoleInfo roleInfo)
        {
            //必须对前端传过来的东西进行验证
            if (!ModelState.IsValid)
            {
                //说明验证不通过
                return "no";
            }
            if (_roleService.Add(roleInfo))
            {
                return "ok";
            }
            return "no";
        }
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <returns></returns>
        public string Config(string UserId,string[] roleId)
        {
            //ViewBag.UserId=_roleService.
            if (_roleService.AssignRoles(UserId, roleId))
            {
                return "ok";
            }
            else
            {
                return "no";
            }
        }


        /// <summary>
        /// 获取权限方法
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IActionResult GetPower(string roleId)
        {
            //string roleId = "R1002";
            PowerInfoOutput powers = _powerService.GetPowerTreesList(roleId);

            return Json(new { data = powers });
            /*var a = JsonNetHelper.SerializetoJson(powers);*/

            //return "[" + a + "]";
        }

        /// <summary>
        /// 分配权限方法
        /// </summary>
        /// <param name="permissions"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public string SetPower(string permissions, string roleId)
        {
            if (_powerService.SetPower(permissions, roleId))
            {
                return "ok";
            }
            return "on";
        }
    }
}
