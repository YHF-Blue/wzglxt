using DataEntity;
using DataEntity.Entities;
using IService;
using Microsoft.EntityFrameworkCore;
using Model.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class UserService : IUserService
    {
        #region 依赖注入
        private DbContext _dbContext;
        public UserService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion
        /// <summary>
        /// 添加管理员信息
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <returns></returns>
        public bool Add(UserInput inputEntity)
        {
            UserInfo entity = new UserInfo
            {
                //配置默认值
                DelFlag = 0,
                AddTime = DateTime.Now,
                PassWord = DataEntity.MD5HelperOne.GetMD5("123"),
                DelTime = null,

                //接收前端传过来的数据
                DepartmentId = inputEntity.DepartmentId,
                Email = inputEntity.Email,
                PhoneNum = inputEntity.PhoneNum,
                Sex = inputEntity.Sex,
                UserId = inputEntity.UserId,
                UserName = inputEntity.UserName,
            };
            //打上添加标记，并SaveChanges()
            _dbContext.Set<UserInfo>().Add(entity);
            return _dbContext.SaveChanges() > 0;
        }


        /// <summary>
        /// 判断用户是否存在，存在返回true
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CheckRepeat(string value)
        {
            if (_dbContext.Set<UserInfo>().Where(u => u.UserId == value.Trim()).Count() > 0)
            {
                return true; //查看用户是否已存在，如果存在返回true
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 禁用或开启用户
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="delFlag"></param>
        /// <returns></returns>
        public bool ForbidUser(int id, string delFlag)
        {

            //【1】要禁用/开启谁？？？---根据id去查
            UserInfo userInfo = _dbContext.Set<UserInfo>().Find(id);   //UserInfo userInfo = _dbContext.Set<UserInfo>().Where(u => u.Id == id).FirstOrDefault();

            if (userInfo != null)
            {
                //【2】到底是开启，还是禁用？？
                if (delFlag == "true")
                {
                    //说明要开启用户
                    userInfo.DelFlag = 0;
                }
                else
                {
                    userInfo.DelFlag = 1;
                }
                //【3】打上修改标记，操作数据库
                _dbContext.Update(userInfo);
                return _dbContext.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 通过Id获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserOutput GeUserById(int id)
        {
            UserInfo userInfo = _dbContext.Set<UserInfo>().Find(id);
            return new UserOutput
            {
                Id = userInfo.Id,
                DepartmentId = userInfo.DepartmentId,
                Email = userInfo.Email,
                PhoneNum = userInfo.PhoneNum,
                UserId = userInfo.UserId,
                UserName = userInfo.UserName,
                Sex = userInfo.Sex,
                Url=userInfo.Url,
            };
        }


        /// <summary>
        /// 分页获取所有用户信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <param name="selectInfo">模糊搜素</param>
        /// <returns></returns>
        public List<UserOutput> GeUserListPages(int page, int limit, out int totalCount, string selectInfo)
        {
            IQueryable<UserInfo> UserInfos = null;
            if (selectInfo==null)
            {
                //说明没有点击搜索按钮
                UserInfos = _dbContext.Set<UserInfo>().Skip((page - 1) * limit).Take(limit);
                totalCount = _dbContext.Set<UserInfo>().Count();
            }
            else
            {
                //点击了搜索按钮
                UserInfos = _dbContext.Set<UserInfo>().Where(u => u.UserName.Contains(selectInfo)).Skip((page - 1) * limit).Take(limit);
                totalCount = _dbContext.Set<UserInfo>().Where(u=>u.UserName.Contains(selectInfo)).Count();
            }
            List<UserOutput> oldList = (from a in UserInfos//把UserInfos 的数据重新命为a表
                                        join b in _dbContext.Set<DepartmentInfo>() on a.DepartmentId equals b.DepartmentId into join_a
                                        from c in join_a.DefaultIfEmpty()
                                        join d in _dbContext.Set<RUserInfoRoleInfo>() on a.UserId equals d.UserId into join_b
                                        from e in join_b.DefaultIfEmpty()
                                        join f in _dbContext.Set<RoleInfo>().Where(r => r.DelFlag == 0) on e.RoleId equals f.RoleId into join_c
                                        from g in join_c.DefaultIfEmpty()
                                        select new UserOutput //创建新的UserOutput，去接收所有查到的数据
                                        {
                                            Id = a.Id,
                                            UserId = a.UserId, //接收 a表的UserId列的数据
                                            UserName = a.UserName, //接收a表的UserName列的数据
                                            AddTime = a.AddTime,
                                            PhoneNum = a.PhoneNum,
                                            DelFlag = a.DelFlag,
                                            DepartmentName = c.DepartmentName,//接收连表后的DepartmentName数据
                                            Email = a.Email,
                                            Sex = a.Sex,
                                            RoleName = g.RoleName,
                                        }).ToList();


            #region 方案1，使用分组 group by
            //return (from a in oldList
            //        group a by new
            //        {
            //            //对oldlist集合的一下字段进行分组合并
            //            a.Id,
            //            a.UserId,
            //            a.UserName,
            //            a.Sex,
            //            a.DepartmentId,
            //            a.DepartmentName,
            //            a.Email,
            //            a.DelFlag,
            //            a.PhoneNum,
            //            a.AddTime,
            //        }).Select(u => new UserOutput
            //        {

            //            Id = u.Key.Id,
            //            UserId = u.Key.UserId,
            //            UserName = u.Key.UserName,
            //            Sex = u.Key.Sex,
            //            DepartmentId = u.Key.DepartmentId,
            //            DepartmentName = u.Key.DepartmentName,
            //            DelFlag = u.Key.DelFlag,
            //            Email = u.Key.Email,
            //            PhoneNum = u.Key.PhoneNum,
            //            AddTime = u.Key.AddTime,

            //            //进行拼接-->知识点小扩展  ，请问 String和string有啥区别-->本质上没区别：string在编译过程中会变成String
            //            RoleName = string.Join(",", u.Select(v => v.RoleName).ToArray())

            //        }).ToList();

            #endregion


            #region 方案2，自己写-->思路：去重，拼接-->就是创建一个新的list，遍历旧的list，如果新的list没有数据，就添加进去，如果新的list已经有数据了，直接字符串拼接
            List<UserOutput> newList = new List<UserOutput>();

            foreach (var olditem in oldList)
            {
                bool addFlag = false;//  这个字段（状态机字段，用来表示某种状态），用来判断新的list集合是否拥相同的UserId数据。如果有，就设置为true，

                if (newList.Count == 0)
                {
                    //如果新的list集合没有数据，直接添加
                    newList.Add(olditem);
                }
                else
                {
                    foreach (var newitem in newList)
                    {
                        if (olditem.UserId == newitem.UserId)
                        {
                            addFlag = true;//把addFlag设置为true

                            //说明新的list集合已经拥有相同的UserId的数据--这个时候，就在原有的RoleName字段上做字符串的拼接
                            newitem.RoleName += "," + olditem.RoleName;
                        }
                    }

                    if (!addFlag)
                    {
                        //如果状态机的状态仍然为false，就走这个代码--就达到我们的效果了
                        newList.Add(olditem);
                    }
                }
            }

            #endregion

            return newList;
        }


        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <returns></returns>
        public bool Update(UserInput inputEntity)
        {

            //先查，再改需要改的
            UserInfo entity = _dbContext.Set<UserInfo>().Where(u => u.UserId == inputEntity.UserId).FirstOrDefault();

            if (entity != null)
            {
                //只修改前端传过来的数据，其他的字段保持原样。
                entity.DepartmentId = inputEntity.DepartmentId;
                entity.PhoneNum = inputEntity.PhoneNum;
                entity.Sex = inputEntity.Sex;
                entity.UserName = inputEntity.UserName;
                entity.Email = inputEntity.Email;
            }
            //打上修改标记，并SaveChanges()
            _dbContext.Set<UserInfo>().Update(entity);
            return _dbContext.SaveChanges() > 0;
        }


        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool ResetPwd(int[] id)
        {
            foreach (var item in id)
            {
                UserInfo entity = _dbContext.Set<UserInfo>().Where(u => u.Id == item).FirstOrDefault();
                if (entity!=null)
                {
                    entity.Id = item;
                    entity.PassWord = MD5HelperOne.GetMD5("123");
                }
                _dbContext.Set<UserInfo>().Update(entity);
            }
            return _dbContext.SaveChanges() > 0;
            

        }


        //图片上传
        public bool Upload(string UserId, string Ur)
        {
            UserInfo entity = _dbContext.Set<UserInfo>().Where(u => u.UserId == UserId).FirstOrDefault();
            if (entity != null)
            {
                entity.Url = Ur;
            }
            //打上修改标记，并SaveChanges()
            _dbContext.Set<UserInfo>().Update(entity);
            return _dbContext.SaveChanges() > 0;
        }

    }
}
