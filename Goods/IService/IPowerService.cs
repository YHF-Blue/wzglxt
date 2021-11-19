using Model.PowerDTO;
using Model.PowerInfoDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IService
{
  public   interface IPowerService
    {
        /// <summary>
        /// 根据用户的角色字符串获取去重之后的目录
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        List<PowerOutput> GetMenu(string roleId);


        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        PowerInfoOutput GetPowerTreesList(string roleId);


        /// <summary>
        /// 分配权限方法
        /// </summary>
        /// <param name="permissions"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        bool SetPower(string permissions, string roleId);
    }
}
