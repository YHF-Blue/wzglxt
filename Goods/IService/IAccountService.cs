using DataEntity.Entities;
using Model.UserDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IService
{
   public  interface IAccountService
    {
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="UserID">用户账号</param>
        /// <param name="pwd">用户密码</param>
        /// <returns></returns>
        UserOutput Login(string UserID, string pwd);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="NewPassWord"></param>
        /// <returns></returns>
        bool UpdatePwd(string UserID, string PassWord, string NewPassWord);
    }
}
