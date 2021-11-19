using Model.UserDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IService
{
   public  interface IUserService
    {
        /// <summary>
        /// 通过Id获取用户信息
        /// </summary>
        /// <returns></returns>
        public UserOutput GeUserById(int id);


        /// <summary>
        /// 分页获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public List<UserOutput> GeUserListPages(int page, int limit, out int totalCount, string selectInfo);


        /// <summary>
        /// 禁用或开启用户
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="delFlag"></param>
        /// <returns></returns>
        public bool ForbidUser(int id, string delFlag);


        /// <summary>
        /// 判断用户是否存在，存在返回true
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool CheckRepeat(string value);

        /// <summary>
        /// 添加管理员信息
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <returns></returns>
        bool Add(UserInput inputEntity);



        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <returns></returns>
        bool Update(UserInput inputEntity);
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ResetPwd(int[] id);

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Ur"></param>
        /// <returns></returns>
        bool Upload(string UserId, string Ur);
    }
}
