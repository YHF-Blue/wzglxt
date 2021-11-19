using DataEntity.Entities;
using IService;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class RoleService : IRoleService
    {
        #region 构造函数依赖注入，创建对象的事情，由IOC容器来做

        private readonly DbContext _dbContext;
        public RoleService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion
        /// <summary>
        /// 获取用户角色ID，来判断是否拥有权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserRoles(string userId)
        {

            //ADO.NET 和EF Core等orm框架的对比：优势和劣势是什么---1.EF Core会翻译成sql语句，性能稍微比ADO.NET低一点  2..EF Core翻译成的sql语句不太可控，有时候不一定是我们想要的效果，所以需要监听。

            //怎么监听：sql server管理工具内置的监听工具。

            //连表获取未被禁用的角色信息--内连接
            //var linqlist = (from a in _dbContext.Set<RUserInfoRoleInfo>().Where(u => u.UserId == userId)
            //           join b in _dbContext.Set<RoleInfo>() on a.RoleId equals b.RoleId
            //           select new //匿名实体类，临时用来装你需要的连表数据
            //           {
            //               UserId = a.UserId,
            //               RoleId = a.RoleId,
            //               DelFlag = b.DelFlag,
            //           }).Where(u=>u.DelFlag==0).ToList();

            //左连接
            var linqlist = (from a in _dbContext.Set<RUserInfoRoleInfo>().Where(u => u.UserId == userId)
                            join b in _dbContext.Set<RoleInfo>() on a.RoleId equals b.RoleId into join_a
                            from c in join_a.DefaultIfEmpty()
                            select new //匿名实体类，临时用来装你需要的连表数据
                            {
                                UserId = a.UserId,
                                RoleId = a.RoleId,
                                DelFlag = c.DelFlag,
                            }).Where(u => u.DelFlag == 0).ToList();

            //字符串拼接，用逗号隔开
            string str = "";
            foreach (var item in linqlist)
            {
                str += item.RoleId + ",";
            }
            //把最后一个逗号去掉
            str = str.Substring(0, str.Length - 1);

            return str;

        }
        /// <summary>
        /// 分页查询角色所有数据
        /// </summary>
        /// <returns></returns>
        public List<RoleInfo> GetRolesList(int page, int limit, out int totalCount, string selectInfo)
        {
            IQueryable<RoleInfo> RoleInfos = null;
            if (selectInfo==null)
            {
                RoleInfos = _dbContext.Set<RoleInfo>().Skip((page - 1) * limit).Take(limit);
                totalCount = _dbContext.Set<RoleInfo>().Count();
                return _dbContext.Set<RoleInfo>().ToList();
            }
            else
            {
                RoleInfos = _dbContext.Set<RoleInfo>().Where(r => r.RoleName.Contains(selectInfo)).Skip((page - 1) * limit).Take(limit);
                totalCount = _dbContext.Set<RoleInfo>().Where(r => r.RoleName.Contains(selectInfo)).Count();
                return _dbContext.Set<RoleInfo>().Where(r => r.RoleName.Contains(selectInfo)).ToList();
            }
            

            // return  _dbContext.Set<RoleInfo>().ToList();    
        }

        /// <summary>
        //分配角色展示未被禁用的
        /// </summary>
        /// <returns></returns>
        public List<RoleInfo> GetRoles()
        {
            return _dbContext.Set<RoleInfo>().Where(r=>r.DelFlag==0).ToList();
        }


        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <returns></returns>
        public bool Add(RoleInfo inputEntity)
        {
            RoleInfo insert = new RoleInfo
            {
                RoleId=inputEntity.RoleId,
                RoleName=inputEntity.RoleName,
                Description=inputEntity.Description,
                DelFlag=0,
                AddTime=DateTime.Now,
                DelTime = null
            };
            _dbContext.Set<RoleInfo>().Add(insert);
            return _dbContext.SaveChanges() > 0;
        }


        /// <summary>
        /// 角色禁用或开启
        /// </summary>
        /// <param name="id"></param>
        /// <param name="delFlag"></param>
        /// <returns></returns>
        public bool ForbidUser(int id, string delFlag)
        {
            //【1】要禁用/开启谁？？？---根据id去查
            RoleInfo roleInfo = _dbContext.Set<RoleInfo>().Find(id);
            if (roleInfo !=null)
            {
                if (delFlag=="true")
                {
                    roleInfo.DelFlag = 0;
                }
                else
                {
                    roleInfo.DelFlag = 1;
                }
                //【3】打上修改标记，操作数据库
                _dbContext.Update(roleInfo);
                return _dbContext.SaveChanges() > 0;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <returns></returns>
        public bool Update(RoleInfo inputEntity)
        {
            RoleInfo roleInfo = _dbContext.Set<RoleInfo>().Where(r => r.RoleId == inputEntity.RoleId).FirstOrDefault();
            if (roleInfo!=null )
            {
                roleInfo.RoleId = inputEntity.RoleId;
                roleInfo.RoleName = inputEntity.RoleName;
                roleInfo.Description = inputEntity.Description;
            }
            _dbContext.Set<RoleInfo>().Update(roleInfo);
            return _dbContext.SaveChanges() > 0;
        }


        /// <summary>
        /// 判断角色是否存在，存在返回true
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CheckRepeat(string value)
        {
            if (_dbContext.Set<RoleInfo>().Where(r => r.RoleId == value.Trim()).Count() > 0)
            {
                return true; //查看用户是否已存在，如果存在返回true
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 给用户分配角色
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool AssignRoles(string UserId,string [] roleId)
        {

            //查询用户拥有的角色
            var Role = _dbContext.Set<RUserInfoRoleInfo>().Where(e => e.UserId == UserId).ToList();
            //把有的角色删除
            if (Role!=null)
            {
                foreach (var item in Role)
                {
                    _dbContext.Set<RUserInfoRoleInfo>().Remove(item);
                }
                _dbContext.SaveChanges();
            }
            //给用户添加角色

            //记录成功次数
            int conok = 0;
            foreach (var ritem in roleId)
            {
                RUserInfoRoleInfo rUserInfoRoleInfo = new RUserInfoRoleInfo
                {
                    UserId = UserId,
                    RoleId = ritem
                };
                _dbContext.Set<RUserInfoRoleInfo>().Add(rUserInfoRoleInfo);
                conok += _dbContext.SaveChanges();
            }
            //分配次数等于传来角色数组长度
            return conok == roleId.Length ? true : false;
        }


        /// <summary>
        /// 根据ID获取角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleInfo GetById(int id)
        {
            RoleInfo roleInfo = _dbContext.Set<RoleInfo>().Find(id);
            return new RoleInfo
            {
                Id = roleInfo.Id,
                RoleId = roleInfo.RoleId,
                RoleName = roleInfo.RoleName,
                Description = roleInfo.Description
            };
        }
    }
}
