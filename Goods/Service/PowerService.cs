using DataEntity.Entities;
using IService;
using Microsoft.EntityFrameworkCore;
using Model.PowerDTO;
using Model.PowerInfoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
   public  class PowerService: IPowerService
    {
        #region 构造函数依赖注入

        private readonly DbContext _dbContext;
        public PowerService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        /// <summary>
        /// roleId:R1001,R1002
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<PowerOutput> GetMenu(string roleId)
        {
            //【1】根据roleId获取所有目录信息
            var listMenu = (from a in _dbContext.Set<RRoleInfoPowerInfo>().Where(u => roleId.Contains(u.RoleId))
                            join b in _dbContext.Set<PowerInfo>() on a.PowerId equals b.PowerId into join_a
                            from c in join_a.DefaultIfEmpty()
                            select c
                        ).Where(u => u.Description.Contains("目录")).ToList();

            //【2】去重--或者用groupby
            //第一步：创建一个新的list集合
            List<PowerInfo> list = new List<PowerInfo>();
            //第二步：遍历查询到的数据
            foreach (var item in listMenu)
            {
                //如果list没有数据，直接添加
                if (list.Count == 0)
                {
                    list.Add(item);
                }
                int flag = 0;//标记
                //第三步：遍历新的list集合
                foreach (var item1 in list)
                {
                    //第四步：如果新的list集合里，已经存在的数据，把flag改成1
                    if (item1.PowerId == item.PowerId)
                    {
                        flag = 1;
                    }
                }
                if (flag == 0)//只有当flag等于0 的时候，才会把数据添加到新的list集合中
                {
                    list.Add(item);
                }
            }
            //【4】转换
            List<PowerOutput> outlist = (from a in list
                                         select new PowerOutput
                                         {
                                             Id = a.Id,
                                             ActionUrl = a.ActionUrl,
                                             Description = a.Description,
                                             HttpMethod = a.HttpMethod,
                                             MenuIconUrl = a.MenuIconUrl,
                                             Name = a.Name,
                                             ParentId = a.ParentId,
                                             PowerId = a.PowerId,
                                             Sort = a.Sort,
                                         }).ToList();
            return outlist;


        }

        public PowerInfoOutput GetPowerTreesList(string roleId)
        {
            List<PowerInfo> infos = _dbContext.Set<PowerInfo>().ToList();
            //List<PowerInfo> infos2 = db.Set<PowerInfo>().Where(u=>u.ParentId=="0").ToList();
            List<RRoleInfoPowerInfo> rRoles = _dbContext.Set<RRoleInfoPowerInfo>().Where(u => u.RoleId == roleId).ToList();
            List<PowerInfoOutput> result = new List<PowerInfoOutput>();

            foreach (var item in infos)
            {
                if (item.ParentId == "0")
                {
                    PowerInfoOutput power = new PowerInfoOutput
                    {
                        Id = Convert.ToInt32(item.PowerId),
                        Title = item.Name,
                        //FieId = item.PowerId,
                        Href = item.ActionUrl,
                        Spread = true,
                        Checked = false,
                        Disabled = false,
                        Children = Powers(item.PowerId, infos, rRoles),
                    };

                    result.Add(power);
                }
            }


            PowerInfoOutput power1 = new PowerInfoOutput
            {
                Id = 0,
                Title = "权限目录",
                Spread = false,
                Checked = false,
                Disabled = false,
                Children = result,
            };
            return power1;
        }

        public List<PowerInfoOutput> Powers(string PowerId, List<PowerInfo> infos, List<RRoleInfoPowerInfo> rRoles)
        {
            List<PowerInfoOutput> result = new List<PowerInfoOutput>();
            foreach (var item in infos)
            {
                if (item.ParentId == PowerId)
                {
                    PowerInfoOutput power = new PowerInfoOutput
                    {
                        Id = Convert.ToInt32(item.PowerId),
                        Title = item.Name,
                        //FieId = item.PowerId,
                        Href = item.ActionUrl,
                        Spread = false,
                        Checked = false,
                        Disabled = false,
                        Children = Powers(item.PowerId, infos, rRoles),

                    };

                    foreach (var item2 in rRoles)
                    {
                        //var a = Convert.ToInt32(item2.PowerId);
                        if (item.PowerId == item2.PowerId && power.Children.Count() == 0)
                        {
                            power.Checked = true;
                        }
                    }

                    result.Add(power);
                }
            }

            return result;
        }

        public bool SetPower(string permissions, string roleId)
        {
            var query = _dbContext.Set<RRoleInfoPowerInfo>().Where(u => u.RoleId == roleId).ToList();

            if (query.Count() > 0)
            {
                foreach (var item in query)
                {
                    _dbContext.Remove(item);
                }
            }

            string[] arr = permissions.Split('$');

            for (int i = 0; i < arr.Length; i++)
            {
                RRoleInfoPowerInfo info = new RRoleInfoPowerInfo
                {
                    RoleId = roleId,
                    PowerId = arr[i],
                };
                _dbContext.Add(info);
            }


            return _dbContext.SaveChanges() > 0;
        }
    }
}
