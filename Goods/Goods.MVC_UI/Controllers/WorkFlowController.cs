using IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.WorkFlowDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goods.MVC_UI.Controllers
{
    public class WorkFlowController : Controller
    {
        #region  构造函数注入
        private readonly IWorkFlowService _workFlowService;

        public WorkFlowController(IWorkFlowService workFlowService)
        {
            _workFlowService = workFlowService;
        }
        #endregion
        //public IActionResult Index()
        //{
        //    return View();
        //}


        /// <summary>
        /// 查看实例步骤--页面
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public IActionResult WorkFlowStepIndex(string instanceId)
        {
            //查询对应工作流实例的步骤
            ViewBag.list = _workFlowService.LoadStepEntities(instanceId);
            return View();
        }




        /// <summary>
        /// 获取登陆者的非耗材申请
        /// </summary>
        /// <returns></returns>
        public IActionResult GetEquipmentApplyByUser(int page, int limit)
        {
            string userId = HttpContext.Session.GetString("UserId");

            int totalCount = 0;

            List<WorkFlowInstanceOutput> list = _workFlowService.LoadPageEntitiesByUserId(page, limit, out totalCount, userId, false);

            return new JsonResult(new
            {
                code = 0,
                msg = "",
                count = totalCount,
                data = list
            });
        }

        /// <summary>
        /// 获取非耗材需要审核的工作流实例
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public IActionResult GetEquipmentApplyByReviewer(int page, int limit)
        {
            //获取登陆者id
            string userId = HttpContext.Session.GetString("UserId");
            int totalCount = 0;
            //获取需要登陆者审核的数据
            List<WorkFlowInstanceOutput> list = _workFlowService.LoadPageEntitiesByReviewer(page, limit, out totalCount, userId, false);
            return new JsonResult(new
            {

                code = 0,
                msg = "",
                count = totalCount,//一共有多少条数据，用于前端动态生成页码。
                data = list,

            });
        }



        /// <summary>
        /// 获取耗材需要审核的工作流实例
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public IActionResult GetConsumableApplyByReviewer(int page, int limit)
        {
            //获取登陆者id
            string userId = HttpContext.Session.GetString("UserId");
            int totalCount = 0;
            //获取需要登陆者审核的数据
            List<WorkFlowInstanceOutput> list = _workFlowService.LoadPageEntitiesByReviewer(page, limit, out totalCount, userId, true);
            return new JsonResult(new
            {

                code = 0,
                msg = "",
                count = totalCount,//一共有多少条数据，用于前端动态生成页码。
                data = list,

            });

        }
        /// <summary>
        /// 获取登陆者的耗材申请
        /// </summary>   
        /// <returns></returns>
        public IActionResult GetConsumableApplyByUser(int page, int limit)
        {
            string userId = HttpContext.Session.GetString("UserId");

            int totalCount = 0;

            List<WorkFlowInstanceOutput> list = _workFlowService.LoadPageEntitiesByUserId(page, limit, out totalCount, userId, true);

            return new JsonResult(new
            {
                code = 0,
                msg = "",
                count = totalCount,
                data = list
            });
        }


        /// <summary>
        /// 非耗材待归还记录获取
        /// </summary>
        /// <returns></returns>
        public IActionResult GetEquipmentIsLent(int page, int limit, string selectInfo)
        {
            //配置数据条数默认值为0
            int totalCount = 0;

            var list = _workFlowService.GetEquipmentIsLent(page, limit, out totalCount, selectInfo);
            return new JsonResult(new
            {
                code = 0,
                msg = "",
                count = totalCount,
                data = list
            });
           
        }
        #region 可以合并在一起
        /// <summary>
        /// 撤销非耗材领取申请
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RevokeEquipment(int id)
        {
            if (_workFlowService.RevokeEquipment(id))
            {
                return "ok";
            }
            else
            {
                return "no";
            }
        }


        /// <summary>
        /// 撤销耗材领取申请
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RevokeConsumable(int id)
        {
            if (_workFlowService.RevokeConsumable(id))
            {
                return "ok";
            }
            else
            {
                return "no";
            }
        }
        #endregion


        /// <summary>
        /// 提交非耗材领取申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string ApplyEquipment(WorkFlowInstanceInput input)
        {
            //获取登陆者的userid，存到input中
            input.UserId = HttpContext.Session.GetString("UserId");
            //
            if (_workFlowService.ApplyEquipment(input))
            {
                return "ok";
            }
            else
            {
                return "no";
            }
        }


        /// <summary>
        /// 主管审核非耗材领取
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="isPass"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public string ReviewEquipment(string instanceId, bool isPass, string reason)
        {
            string reviwerId = HttpContext.Session.GetString("UserId");

            if (_workFlowService.ReviewEquipment(instanceId, reviwerId, isPass, reason))
            {
                return "ok";
            }
            else
            {
                return "no";
            }
        }


        /// <summary>
        /// 主管审核耗材领取
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="isPass"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public string ReviewConsumable(string instanceId, bool isPass, string reason)
        {
            string reviwerId = HttpContext.Session.GetString("UserId");

            if (_workFlowService.ReviewConsumable(instanceId, reviwerId, isPass, reason))
            {
                return "ok";
            }
            else
            {
                return "no";
            }
        }


        /// <summary>
        /// 提交耗材领取申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string ApplyConsumable(WorkFlowInstanceInput input)
        {
            //获取登陆者的userid，存到input中
            input.UserId = HttpContext.Session.GetString("UserId");
            if (_workFlowService.ApplyConsumable(input))
            {
                return "ok";
            }
            else
            {
                return "no";
            }
        }



         /// <summary>
        /// 获取需要出库的非耗材的工作流实例
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="selectInfo"></param>
        /// <returns></returns>
        public IActionResult GetEquipmentOutput(int page, int limit, string selectInfo)
        {
            int totalCount = 0;
            List<WorkFlowInstanceOutput> list = _workFlowService.LoadPageEntitiesByKeeper(page, limit, out totalCount, selectInfo, false);

            return new JsonResult(new
            {

                code = 0,
                msg = "",
                count = totalCount,
                data = list,
            });

        }




        /// <summary>
        /// 获取需要出库的耗材的工作流实例
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="selectInfo"></param>
        /// <returns></returns>
        public IActionResult GetConsumableOutput(int page, int limit, string selectInfo)
        {
            int totalCount = 0;
            List<WorkFlowInstanceOutput> list = _workFlowService.LoadPageEntitiesByKeeper(page, limit, out totalCount, selectInfo, true);

            return new JsonResult(new
            {

                code = 0,
                msg = "",
                count = totalCount,
                data = list,
            });

        }


        /// <summary>
        /// 非耗材批量出库
        /// </summary>
        /// <param name="arrStr"></param>
        /// <returns></returns>
        public string OutputEquipment(string arrStr)
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (_workFlowService.OutputEquipment(arrStr, userId))
            {
                return "ok";
            }
            else
            {
                return "no";
            }
        }



        /// <summary>
        /// 耗材批量出库
        /// </summary>
        /// <param name="arrStr"></param>
        /// <returns></returns>
        public string OutputConsumable(string arrStr)
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (_workFlowService.OutputConsumable(arrStr, userId))
            {
                return "ok";
            }
            else
            {
                return "no";
            }
        }


        /// <summary>
        /// 非耗材批量归还
        /// </summary>
        /// <param name="arrStr"></param>
        /// <returns></returns>
        public string ReturnEquipment(string arrStr)
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (_workFlowService.ReturnEquipment(arrStr, userId))
            {
                return "ok";
            }
            else
            {
                return "no";
            }
        }
    }
}
