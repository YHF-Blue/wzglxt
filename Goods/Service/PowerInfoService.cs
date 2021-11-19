using DataEntity.Entities;
using IService;
using Microsoft.EntityFrameworkCore;
using Model.PowerInfoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class PowerInfoService : IPowerInfoService
    {

        #region 构造函数依赖注入，创建对象的事情，由IOC容器来做

        private readonly DbContext _dbContext;
        public PowerInfoService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion


        public List<PowerInfoOutput> GetPowerInfo()
        {
            List<PowerInfo> powerInfos = _dbContext.Set<PowerInfo>().ToList();

            List<PowerInfoOutput> powerInfo = new List<PowerInfoOutput>();

            //ParssTree()
            return null;


        }


        public void ParssTree(List<PowerInfo> powers, List<PowerInfoOutput> powerInfoOutputs,int ? id)
        {
            List<PowerInfo> powerInfos = powers.Where(u => u.Id == 0).ToList();

            foreach (PowerInfo item in powerInfos)
            {
                PowerInfoOutput output = new PowerInfoOutput();
                output.Id = item.Id;
                output.Title = item.Name;
                output.Href = item.MenuIconUrl;
                powerInfoOutputs.Add(output);



                ParssTree(powers, output.Children,output.Id);
            }

           

        }
    }
}
