using Model.ConsumableDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IService
{
  public   interface IGoods_ConsumableService
    {
        /// <summary>
        /// 分页获取耗材信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="selectInfo"></param>
        /// <returns></returns>
        List<ConsumableOutput> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string selectInfo);



        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <returns></returns>
        List<ConsumableOutput> LoadEntities(int id);


        /// <summary>
        /// 检查库存
        /// </summary>
        /// <param name="consumablesID"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        bool CheckStore(string goodsId, double num);


        /// <summary>
        /// 导入表格
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
        ConsumableOutput GetConsumableById(int id);


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="consumableOutput"></param>
        /// <returns></returns>
        bool Update(ConsumableOutput consumableOutput);
    }
}
