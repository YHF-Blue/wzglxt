using DataEntity.Entities;
using IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class Goods_CategoryService : IGoods_CategoryService
    {
        #region 依赖注入

        private readonly DbContext _dbContext;
        public Goods_CategoryService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion


        /// <summary>
        /// 获取类别信息
        /// </summary>
        /// <returns></returns>
        public List<GoodsCategory> GetGoodsCategory()
        {
            return (from a in _dbContext.Set<GoodsCategory>()
                    select new GoodsCategory{
                        Id=a.Id,
                        CategoryName=a.CategoryName,
                        Description=a.Description
                    }).ToList();
           
        }
    }
}
