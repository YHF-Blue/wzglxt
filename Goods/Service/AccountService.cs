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
    
    public class AccountService : IAccountService
    {
        #region 依赖注入

        private readonly DbContext _dbContext;
        public AccountService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="UserID">用户的账号</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public UserOutput Login(string UserID, string pwd)
        {
            string PwdMd5 = MD5HelperOne.GetMD5(pwd);
            UserInfo userInfo=_dbContext.Set<UserInfo>().Where(u => u.UserId == UserID && u.PassWord == PwdMd5).FirstOrDefault();
            if (userInfo!=null)
            {
                return new UserOutput {
                    Id=userInfo.Id,
                    UserId = userInfo.UserId,
                    UserName = userInfo.UserName,
                };

            }
            return null;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="NewPassWord"></param>
        /// <returns></returns>
        public bool UpdatePwd(string UserID,string PassWord, string NewPassWord )
        {
            UserInfo userInfo = _dbContext.Set<UserInfo>().Where(u => u.UserId == UserID).FirstOrDefault();
            if (userInfo.PassWord==MD5HelperOne.GetMD5(PassWord))
            {
               
                userInfo.PassWord =MD5HelperOne.GetMD5( NewPassWord);
                _dbContext.Set<UserInfo>().Update(userInfo);
                return _dbContext.SaveChanges() > 0; 
            }
            return false;
            /*string PwdMd5 = MD5HelperOne.GetMD5(NewPassWord);

            UserInfo userInfo = _dbContext.Set<UserInfo>().Where(u => u.UserId == UserID).FirstOrDefault();
            userInfo.PassWord = PwdMd5;
            _dbContext.Set<UserInfo>().Update(userInfo);
            return _dbContext.SaveChanges() > 0;*/

        }
    }
}
