using IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.EquipmentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Goods.MVC_UI.Controllers
{
    public class Goods_EquipmentController : Controller
    {
        #region 依赖注入
        private readonly IGoods_EquipmentService  _goods_EquipmentService;
        private IWorkFlowService _workFlowService;
        private IGoods_CategoryService  _goods_CategoryService;
      public Goods_EquipmentController(IGoods_EquipmentService  goods_EquipmentService,IWorkFlowService workFlowService, IGoods_CategoryService goods_CategoryService)
        {
            _goods_EquipmentService = goods_EquipmentService;
            _workFlowService = workFlowService;
            _goods_CategoryService = goods_CategoryService;
        }
        #endregion

        /// <summary>
        /// 非耗材领取
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 提交申请 （申请领取）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult ApplyingIndex(int id)
        {
            //获取非耗材信息
            EquipmentOutput outputEntity = _goods_EquipmentService.LoadEntities(id).FirstOrDefault();

            ViewBag.equipment = outputEntity;
            return View();
        }
        /// <summary>
        /// 非耗材审核页面
        /// </summary>  
        /// <returns></returns>
        public IActionResult ReviewIndex()
        {
            return View();
        }

        /// <summary>
        /// 非耗材入库
        /// </summary>
        /// <returns></returns>
        public IActionResult InputIndex()
        {
            return View();
        }

        /// <summary>
        /// 非耗材出库页面
        /// </summary>   
        /// <returns></returns>
        public IActionResult OutputIndex()
        {
            return View();

        }

        /// <summary>
        /// 非耗材归还
        /// </summary>
        /// <returns></returns>
        public IActionResult ReturnIndex()
        {
            return View();

        }


        /// <summary>
        /// 非耗材表格导入
        /// </summary>
        /// <returns></returns>
        public IActionResult ExcelIndex()
        {
            return View();

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult UpdateIndex(int id)
        {
            ViewBag.Category = _goods_CategoryService.GetGoodsCategory();

            ViewBag.Equipment = _goods_EquipmentService.GetEquipmentById(id);


            return View();
        }



        /// <summary>
        /// 非耗材单条入库
        /// </summary>
        /// <returns></returns>
        public IActionResult AddIndex()
        {
            //获取类别
            //var a = _WorkFlowModel.GetWorkFlowModel();
            var a = _goods_CategoryService.GetGoodsCategory();

            ViewBag.GetCategory = a;
            return View();
        }

        /// <summary>
        /// 非耗材审核提交页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult ReviewingIndex(int id)
        {
            //获取需要审核的实例信息
            ViewBag.Instance = _workFlowService.LoadInstanceEntitiy(id);
            return View();
        }

        /// <summary>
        /// 获取非耗材信息  分页
        /// </summary>
        /// <returns></returns>
        public IActionResult GetJsonList(int page, int limit, string selectInfo)
        {

            int totalCount = 0;

            List<EquipmentOutput> list = _goods_EquipmentService.LoadPageEntities(page, limit, out totalCount, selectInfo);

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
        /// <param name="consumablesID"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public string CheckStore(string equipmentsId, double num)
        {
            if (_goods_EquipmentService.CheckStore(equipmentsId, num))
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
        /// <param name="EquipmentOutput"></param>
        /// <returns></returns>
        public string Update(EquipmentOutput equipmentOutput)
        {
            if (!ModelState.IsValid)
            {
                //说明验证不通过
                 return "no";
            }
            if (_goods_EquipmentService.Update(equipmentOutput))
            {
                return "ok";
            }
            else
            {
                return "no";
            }

        }


        /// <summary>
        /// 非耗材添加（单条）
        /// </summary>
        /// <param name="equipmentOutput"></param>
        /// <returns></returns>
        public string Add(EquipmentOutput equipmentOutput)
        {
            if (_goods_EquipmentService.Add(equipmentOutput))
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
            int results = _goods_EquipmentService.ExcelUpload(ExcelContent, HttpContext.Session.GetString("UserId"));
            return results;
        }

    }
}
