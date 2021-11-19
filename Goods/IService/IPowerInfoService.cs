using Model.PowerInfoDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IService
{
   public  interface IPowerInfoService
    {
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        List<PowerInfoOutput> GetPowerInfo();
    }
}
