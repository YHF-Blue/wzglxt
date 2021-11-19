using DataEntity.Entities;
using IService;
using Microsoft.EntityFrameworkCore;
using Model.DepartmentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class DepartmentService : IDepartmentService
    {
        #region 依赖注入

        private readonly DbContext _dbContext;
        public DepartmentService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        /// <summary>
        /// 获取部门信息用于添加时的下拉框
        /// </summary>
        /// <returns></returns>
        public List<DepartmentOutput> GetDepartments()
        {
            return (from a in _dbContext.Set<DepartmentInfo>()
                    select new DepartmentOutput
                    {
                        DepartmentId = a.DepartmentId,
                        DepartmentName = a.DepartmentName,
                        AddTime = a.AddTime,
                        Id = a.Id,
                        LeaderId = a.LeaderId,
                        ParentId = a.ParentId,
                    }).ToList();
        }


        /// <summary>
        /// 分页获取所有部门信息
        /// </summary>
        /// <returns></returns>
        public List<DepartmentOutput> GeDepartmentListPages(int page, int limit, out int totalCount, string selectInfo)
        {
            IQueryable<DepartmentInfo> departmentInfo = null;
            if (selectInfo==null)
            {
                //说明没有点击搜索按钮
                departmentInfo=_dbContext.Set<DepartmentInfo>().Skip((page - 1) * limit).Take(limit);
                totalCount = _dbContext.Set<DepartmentInfo>().Count();
                //return _dbContext.Set<DepartmentInfo>().ToList();
            }
            else
            {
                departmentInfo = _dbContext.Set<DepartmentInfo>().Where(d => d.DepartmentName.Contains(selectInfo)).Skip((page - 1) * limit).Take(limit);
                totalCount = _dbContext.Set<DepartmentInfo>().Where(d => d.DepartmentName.Contains(selectInfo)).Count();
                //return _dbContext.Set<DepartmentInfo>().Where(u=>u.DepartmentName.Contains(selectInfo)).ToList();
            }

            List<DepartmentOutput> result = (from a in departmentInfo
                                             join b in _dbContext.Set<UserInfo>() on a.LeaderId equals b.UserId into join_a
                                             from c in join_a.DefaultIfEmpty()
                                             join d in _dbContext.Set<DepartmentInfo>() on a.ParentId equals d.DepartmentId into join_b
                                             from e in join_b.DefaultIfEmpty()
                                             select new DepartmentOutput
                                             {
                                                 Id=a.Id,
                                                 DepartmentId=a.DepartmentId,
                                                 DepartmentName=a.DepartmentName,
                                                 LeaderId=a.LeaderId,
                                                 ParentId=a.ParentId,
                                                 AddTime=a.AddTime,

                                                 LeaderNane=c.UserName,
                                                 ParentName=e.DepartmentName
                                             }).ToList();
            return result;
            
        }


        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <returns></returns>
        public bool Update(DepartmentInfo inputEntity)
        {
            //先查再改
            DepartmentInfo info = _dbContext.Set<DepartmentInfo>().Where(u => u.DepartmentId == inputEntity.DepartmentId).FirstOrDefault();
            if (info!=null)
            {
                info.DepartmentName = inputEntity.DepartmentName;
                info.LeaderId = inputEntity.LeaderId;
                info.ParentId = inputEntity.ParentId;
            }
            _dbContext.Set<DepartmentInfo>().Update(info);
            return _dbContext.SaveChanges() > 0;
        }


        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <returns></returns>
        public bool Add(DepartmentInfo inputEntity)
        {
            DepartmentInfo info = new DepartmentInfo
            {
                AddTime = DateTime.Now,//默认当前时间
                DepartmentId = inputEntity.DepartmentId,
                DepartmentName = inputEntity.DepartmentName,
                LeaderId = inputEntity.LeaderId,
                ParentId = inputEntity.ParentId,
            };
            //打上添加的标记
            _dbContext.Set<DepartmentInfo>().Add(info);
            //添加
            return _dbContext.SaveChanges() > 0;
        }


        /// <summary>
        /// 判断部门是否存在，存在返回true
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CheckRepeat(string value)
        {
            if (_dbContext.Set<DepartmentInfo>().Where(u=>u.DepartmentId==value.Trim()).Count()>0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }


        
        /// <summary>
        /// 获取部门主管
        /// </summary>
        /// <returns></returns>
        public List<DepartmentOutput> GetDepartmentDirector()
        {
            return (from a in _dbContext.Set<UserInfo>()
                    join b in _dbContext.Set<DepartmentInfo>() on a.UserId equals b.LeaderId into join_b
                    from c in join_b.DefaultIfEmpty()
                    select new DepartmentOutput
                    {
                        LeaderId = a.UserId,
                        UserName = a.UserName,
                    }).ToList();
          
        }


        /// <summary>
        /// 获取父级部门
        /// </summary>
        /// <returns></returns>
        public List<DepartmentOutput> GetBossDepartment()
        {
            return (from a in _dbContext.Set<DepartmentInfo>()
                    select new DepartmentOutput
                    {
                        ParentId= a.DepartmentId,
                        DepartmentName=a.DepartmentName
                    }).ToList();
           // throw new NotImplementedException();
        }


        /// <summary>
        /// 根据ID获取部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DepartmentOutput GetDepartmentById(int id)
        {
            DepartmentInfo info = _dbContext.Set<DepartmentInfo>().Find(id);
            return new DepartmentOutput
            {
                Id=info.Id,
                DepartmentId=info.DepartmentId,
                DepartmentName=info.DepartmentName,
                LeaderId=info.LeaderId,
                ParentId=info.ParentId
              
            };
        }
    }
}
