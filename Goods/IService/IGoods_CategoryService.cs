using DataEntity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IService
{
   public  interface IGoods_CategoryService
    {

        /// <summary>
        /// 获取类别信息
        /// </summary>
        /// <returns></returns>
        List<GoodsCategory> GetGoodsCategory();
    }
}
