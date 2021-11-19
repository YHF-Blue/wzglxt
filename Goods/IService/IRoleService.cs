using DataEntity.Entities;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IService
{
   public interface IRoleService
    {
        /// <summary>
      /// 获取登录用户所拥有的未被禁用的角色 
      /// </summary>
      /// <returns></returns>
        string GetUserRoles(string userId);

        /// <summary>
        /// 分页获取角色所有数据
        /// </summary>
        /// <returns></returns>
        List<RoleInfo> GetRolesList(int page, int limit, out int totalCount, string selectInfo);
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        bool Add(RoleInfo roleInfo);

        /// <summary>
        /// 禁用或开启用户
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="delFlag"></param>
        /// <returns></returns>
        public bool ForbidUser(int id, string delFlag);


        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <returns></returns>
        bool Update(RoleInfo inputEntity);

        /// <summary>
        /// 判断用户是否存在，存在返回true
        /// </summary> Assign roles
        /// <param name="value"></param>
        /// <returns></returns>
        bool CheckRepeat(string value);
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool AssignRoles(string UserId, string [] roleId);

        /// <summary>
        /// 用于分配角色展示为被禁用的角色
        /// </summary>
        /// <returns></returns>
        List<RoleInfo> GetRoles();
        /// <summary>
        /// 根据ID获取角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RoleInfo GetById(int id);
    }
}
