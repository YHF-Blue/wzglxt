using DataEntity.Entities;
using IService;
using Microsoft.EntityFrameworkCore;
using Model.EquipmentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class Goods_EquipmentService : IGoods_EquipmentService
    {
        #region 依赖注入
        private DbContext _dbContext;
        public Goods_EquipmentService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        /// <summary>
        /// 分页获取非耗材信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="selectInfo"></param>
        /// <returns></returns>
        public List<EquipmentOutput> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string selectInfo)
        {
            IQueryable<GoodsEquipmentInfo> iquery;
            if (string.IsNullOrEmpty(selectInfo))
            {
                //没有点击搜索
                totalCount = _dbContext.Set<GoodsEquipmentInfo>().Count();
                iquery = _dbContext.Set<GoodsEquipmentInfo>().OrderBy(u => u.GoodsId).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                //点击了搜索
                totalCount = _dbContext.Set<GoodsEquipmentInfo>().Where(u => u.Name.Contains(selectInfo)).Count();
                iquery = _dbContext.Set<GoodsEquipmentInfo>().Where(u => u.Name.Contains(selectInfo)).OrderBy(u => u.GoodsId).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            return (from a in iquery
                    join b in _dbContext.Set<GoodsCategory>() on a.CategoryId equals b.Id into join_a
                    from c in join_a.DefaultIfEmpty()
                    select new EquipmentOutput
                    {
                        AddTime = a.AddTime,
                        CategoryId = a.CategoryId,
                        CategoryName = c.CategoryName,
                        DelFlag = a.DelFlag,
                        GoodsId = a.GoodsId,
                        Id = a.Id,
                        Money = a.Money,
                        Name = a.Name,
                        Num = a.Num,
                        Specification = a.Specification,
                        Unit = a.Unit,
                        Description = a.Description,
                    }).ToList();
        }


        /// <summary>
        /// 检查库存
        /// </summary>
        /// <param name="consumablesID"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool CheckStore(string goodsId, double num)
        {
            bool isOk = false;
            var entity = _dbContext.Set<GoodsEquipmentInfo>().Where(u => u.GoodsId == goodsId).FirstOrDefault();
            if (entity != null)
            {
                if (entity.Num >= num)
                {
                    //如果库存大于要借的数量，返回true
                    isOk = true;
                }
            }
            return isOk;
        }
        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <returns></returns>
        public List<EquipmentOutput> LoadEntities(int id)
        {
            return (from a in _dbContext.Set<GoodsEquipmentInfo>().Where(u => u.Id == id)
                    join b in _dbContext.Set<GoodsCategory>() on a.CategoryId equals b.Id into join_a
                    from c in join_a.DefaultIfEmpty()
                    select new EquipmentOutput
                    {
                        AddTime = a.AddTime,
                        CategoryId = a.CategoryId,
                        CategoryName = c.CategoryName,
                        DelFlag = a.DelFlag,
                        GoodsId = a.GoodsId,
                        Id = a.Id,
                        Money = a.Money,
                        Name = a.Name,
                        Num = a.Num,
                        Specification = a.Specification,
                        Unit = a.Unit,
                        Description = a.Description,

                    }).ToList();
        }
        /// <summary>
        /// 添加非耗材（单条）
        /// </summary>
        /// <param name="equipmentOutput"></param>
        /// <returns></returns>
        public bool Add(EquipmentOutput inser)
        {
            GoodsEquipmentInfo goodsEquipmentInfo = new GoodsEquipmentInfo
            {
                GoodsId = inser.GoodsId,
                CategoryId = inser.CategoryId,
                Name = inser.Name,
                Unit = inser.Unit,
                Num = inser.Num,
                Money = inser.Money,
                DelFlag = 0,
                AddTime = DateTime.Now,
            };
            _dbContext.Set<GoodsEquipmentInfo>().Add(goodsEquipmentInfo);
            return _dbContext.SaveChanges() > 0;
           
        }


        /// <summary>
        /// 非耗材Excel导入功能
        /// </summary>
        /// <returns></returns>
        public int ExcelUpload(List<string> ExcelContent, string AddUserId)
        {
            try
            {
                int row = 0;//每次从第几行开始读取
                int time = ExcelContent.Count / (ExcelContent.Count / 8);//提取一条完整的数据的循环的次数
                for (int p = 0; p < ExcelContent.Count / 8; p++)
                {
                    GoodsEquipmentInfo goods = new GoodsEquipmentInfo();
                   // GoodsConsumableInfo goods = new GoodsConsumableInfo();
                    //开始循环把数据赋予goods
                    for (int i = row; i < time; i++)
                    {
                        string[] arrRole = ExcelContent[i].Split("&&");
                        switch (arrRole[0])
                        {
                            case "编号（必填）":
                                if (arrRole[1] != "无")
                                {
                                    goods.GoodsId = arrRole[1];
                                }
                                break;
                            case "非耗材类别（1是办公用品，2是电子设备，3是生活用品）":
                                if (arrRole[1] != "无")
                                {
                                    goods.CategoryId = Convert.ToInt32(arrRole[1]);
                                }
                                break;
                            case "名称（必填）":
                                if (arrRole[1] != "无")
                                {
                                    goods.Name = arrRole[1];
                                }
                                break;
                            case "型号（非必填）":
                                if (arrRole[1] != "无")
                                {
                                    goods.Specification = arrRole[1];
                                }
                                break;
                            case "数量（必填）":
                                if (arrRole[1] != "无")
                                {
                                    goods.Num = Convert.ToDouble(arrRole[1]);
                                }
                                break;
                            case "单位（必填）":
                                if (arrRole[1] != "无")
                                {
                                    goods.Unit = arrRole[1];
                                }
                                break;
                            case "价格（必填）":
                                if (arrRole[1] != "无")
                                {
                                    goods.Money = Convert.ToDecimal(arrRole[1]);
                                }
                                break;
                            //case "警告库存（必填）":
                            //    if (arrRole[1] != "无")
                            //    {
                            //        goods.WarningNum = Convert.ToDouble(arrRole[1]);
                            //    }
                            //    break;
                        }
                    }
                    goods.DelFlag = 0;
                    goods.AddTime = DateTime.Now;
                    //判断是否恶意填写数据
                    if (!string.IsNullOrEmpty(goods.GoodsId) && goods.CategoryId != 0 && !string.IsNullOrEmpty(goods.Name) && goods.Num != 0 && !string.IsNullOrEmpty(goods.Unit)  && goods.Money > 0)
                    {
                        GoodsEquipmentInfo info = _dbContext.Set<GoodsEquipmentInfo>().Where(u => u.GoodsId == goods.GoodsId).FirstOrDefault();
                        GoodsEquipmentInput input = _dbContext.Set<GoodsEquipmentInput>().Where(u => u.GoodsId == goods.GoodsId).FirstOrDefault();
                        //GoodsConsumableInfo info = _dbContext.Set<GoodsConsumableInfo>().Where(u => u.GoodsId == goods.GoodsId).FirstOrDefault();
                       // GoodsConsumableInput input = new GoodsConsumableInput();
                        //判断数据库中是否存在该物品
                        if (info == null)
                        {
                            _dbContext.Set<GoodsEquipmentInfo>().Add(goods);
                            //向记录表中添加一条数据
                            input = new GoodsEquipmentInput
                            {
                                GoodsId = goods.GoodsId,
                                Num = goods.Num,
                                AddTime = goods.AddTime,
                                AddUserId = AddUserId
                            };
                            _dbContext.Set<GoodsEquipmentInput>().Add(input);
                        }
                        else
                        {
                            info.Name = goods.Name;
                            if (!string.IsNullOrEmpty(goods.Specification))
                            {
                                info.Specification = goods.Specification;
                            }
                            info.Num = info.Num + goods.Num;
                            info.Unit = goods.Unit;
                            info.Money = goods.Money;
                            //info.WarningNum = goods.WarningNum;
                            _dbContext.Set<GoodsEquipmentInfo>().Update(info);
                            //向记录表中添加一条数据
                            input = new GoodsEquipmentInput
                            {
                                GoodsId = info.GoodsId,
                                Num = goods.Num,
                                AddTime = goods.AddTime,
                                AddUserId = AddUserId
                            };
                            _dbContext.Set<GoodsEquipmentInput>().Add(input);
                        }
                        //更新读取行数
                        row = time;
                        time = time + 8;
                    }
                    else
                    {
                        return 2;//specification文件不规范
                    }
                }
                if (_dbContext.SaveChanges() > 0)
                {
                    return 0;//ok成功
                }
                else
                {
                    return 1;//no失败
                }
            }
            catch
            {

                return 2;//specification文件不规范
            }
        }


        /// <summary>
        /// 通过ID获取需要修改的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EquipmentOutput GetEquipmentById(int id)
        {
            return (from a in _dbContext.Set<GoodsEquipmentInfo>().Where(u => u.Id == id).OrderBy(u => u.GoodsId)
                    join b in _dbContext.Set<GoodsCategory>() on a.CategoryId equals b.Id into join_a
                    from c in join_a.DefaultIfEmpty()
                    select new EquipmentOutput
                    {
                        Id=a.Id,
                        GoodsId=a.GoodsId,
                        Name=a.Name,
                        Unit=a.Unit,
                        CategoryName=c.CategoryName,
                        Money=a.Money,
                        Specification=a.Specification,
                        AddTime=a.AddTime,
                        CategoryId=a.CategoryId,
                        Num=a.Num,
                        Description=a.Description,
                        DelFlag=a.DelFlag,
                    }).ToList()[0];
            
        }



        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="equipmentOutput"></param>
        /// <returns></returns>
        public bool Update(EquipmentOutput equipmentOutput)
        {
            GoodsEquipmentInfo info = _dbContext.Set<GoodsEquipmentInfo>().Where(u => u.GoodsId == equipmentOutput.GoodsId).FirstOrDefault();
            if (info != null)
            {
                info.Name = equipmentOutput.Name;
                info.Unit = equipmentOutput.Unit;
                info.Num = equipmentOutput.Num;
                info.CategoryId = equipmentOutput.CategoryId;
                info.Money = equipmentOutput.Money;
                info.Specification = equipmentOutput.Specification;
            }
            _dbContext.Set<GoodsEquipmentInfo>().Update(info);
            return _dbContext.SaveChanges() > 0;


        }
    }
}
