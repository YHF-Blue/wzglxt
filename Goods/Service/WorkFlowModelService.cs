using DataEntity.Entities;
using IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class WorkFlowModelService : IWorkFlowModelService
    {

        #region 依赖注入

        private readonly DbContext _dbContext;
        public WorkFlowModelService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion


        ///// <summary>
        ///// 获取类别信息
        ///// </summary>
        ///// <returns></returns>
        //public List<WorkFlowModel> GetWorkFlowModel()
        //{
        //    return (from a in _dbContext.Set<WorkFlowModel>()
        //            select new WorkFlowModel
        //            {
        //                Id=a.Id,
        //                Title=a.Title,

        //            }).ToList();
            
        //}
    }
}
