using DataEntity.Entities;
using Model.EquipmentDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IService
{
   public  interface IGoods_EquipmentService
    {
        /// <summary>
        /// 分页获取信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="selectInfo"></param>
        /// <returns></returns>
        List<EquipmentOutput> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string selectInfo);


        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <returns></returns>
        List<EquipmentOutput> LoadEntities(int id);


        /// <summary>
        /// 检查库存
        /// </summary>
        /// <param name="consumablesID"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        bool CheckStore(string goodsId, double num);

        /// <summary>
        /// 添加非耗材（单条）
        /// </summary>
        /// <param name="equipmentOutput"></param>
        /// <returns></returns>
        bool Add(EquipmentOutput equipmentOutput);

        /// <summary>
        /// 表格导入
        /// </summary>
        /// <param name="ExcelContent"></param>
        /// <param name="AddUserId"></param>
        /// <returns></returns>
        int ExcelUpload(List<string> ExcelContent, string AddUserId);

        /// <summary>
        /// 通过ID获取需要修改的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        EquipmentOutput GetEquipmentById(int id);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="equipmentOutput"></param>
        /// <returns></returns>
        bool Update(EquipmentOutput equipmentOutput);
    }
}
