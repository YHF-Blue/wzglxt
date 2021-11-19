using IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.ConsumableDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Goods.MVC_UI.Controllers
{
    public class Goods_ConsumableController : Controller
    {
        #region 依赖注入
        private readonly IGoods_ConsumableService _goods_ConsumableService;
        private IWorkFlowService _workFlowService;
        public IGoods_CategoryService _goods_CategoryService;
        public Goods_ConsumableController(IGoods_ConsumableService goods_ConsumableService, IWorkFlowService workFlowService, IGoods_CategoryService goods_CategoryService)
        {
            _goods_ConsumableService = goods_ConsumableService;
            _workFlowService = workFlowService;
            _goods_CategoryService = goods_CategoryService;
        }
        #endregion

        #region 页面
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 提交申请 （申请领取）耗材
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public IActionResult ApplyingIndex(int id)
        {
            //获取耗材信息 
            ConsumableOutput outputEntity = _goods_ConsumableService.LoadEntities(id).FirstOrDefault();

            ViewBag.consumable = outputEntity;
            return View();
        }
        /// <summary>
        /// 耗材审核页面
        /// </summary>
        /// <returns></returns>
        public IActionResult ReviewIndex()
        {
            return View();
        }


        /// <summary>
        /// 耗材入库
        /// </summary>
        /// <returns></returns>
        public IActionResult InputIndex()
        {
            return View();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateIndex(int id)
        {
            ViewBag.Category= _goods_CategoryService.GetGoodsCategory();

            //var a = _goods_ConsumableService.GetConsumableById(id);
            ViewBag.ConsumableList= _goods_ConsumableService.GetConsumableById(id);


            return View();
        }


        /// <summary>
        /// 耗材表格导入
        /// </summary>
        /// <returns></returns>
        public IActionResult ExcelIndex()
        {
            return View();

        }



        /// <summary>
        /// 耗材出库页面
        /// </summary>   
        /// <returns></returns>
        public IActionResult OutputIndex()
        {
            return View();

        }


        /// <summary>
        /// 耗材审核提交页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult ReviewingIndex(int id)
        {
            //获取需要审核的实例信息
            ViewBag.Instance = _workFlowService.LoadInstanceEntitiy(id);
            return View();
        }
        #endregion



        /// <summary>
        /// 获取耗材信息
        /// </summary>
        /// <returns></returns>
        public IActionResult GetJsonList(int page, int limit, string selectInfo)
        {

            int totalCount = 0;

            List<ConsumableOutput> list = _goods_ConsumableService.LoadPageEntities(page, limit, out totalCount, selectInfo);

            return new JsonResult(new
            {
                code = 0,
                msg = "",
                count = totalCount,
                data = list
            });
        }

        /// <summary>
        /// 检查库存是否够
        /// </summary>
        /// <param name="ConsumableId"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public string  CheckStore(string ConsumableId,double num)
        {
            if (_goods_ConsumableService.CheckStore(ConsumableId,num))
            {
                return "ok";
            }
            else
            {
                return "no";
            }
        }



        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="consumableOutput"></param>
        /// <returns></returns>
        public string Update(ConsumableOutput consumableOutput)
        {
            if (!ModelState.IsValid)
            {
                //说明验证不通过
                return "no";
            }
            if (_goods_ConsumableService.Update(consumableOutput))
            {
                return "ok";
            }
            else
            {
                return "no";
            }
           
        }



        /// <summary>
        /// 表格导入
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public int ExcelUpload(IFormFile file)
        {
            List<string> ExcelContent = NpoiHelper.InputExcel(file);
            int results = _goods_ConsumableService.ExcelUpload(ExcelContent, HttpContext.Session.GetString("UserId"));
            return results;
        }
    }
}
