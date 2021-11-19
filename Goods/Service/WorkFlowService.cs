using DataEntity.Entities;
using IService;
using Microsoft.EntityFrameworkCore;
using Model.Emun;
using Model.EquipmentDTO;
using Model.WorkFlowDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
  public  class WorkFlowService: IWorkFlowService
    {
        #region 依赖注入

        private DbContext _dbContext;
        public WorkFlowService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        /// <summary>
        /// 根据工作流实例发起人Id（申请人） 分页获取工作流实例
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="userId"></param>
        /// <param name="isConsumable"></param>
        /// <returns></returns>
        public List<WorkFlowInstanceOutput> LoadPageEntitiesByUserId(int pageIndex, int pageSize, out int totalCount, string userId, bool isConsumable)
        {
            if (isConsumable)
            {
                //耗材

                totalCount = _dbContext.Set<WorkFlowInstance>().Where(u => u.UserId == userId && u.ModelId == (int)MyEnum.WorkFlowModelEnum.Consumable).Count();
                IQueryable<WorkFlowInstance> iquery = _dbContext.Set<WorkFlowInstance>().Where(u => u.UserId == userId && u.ModelId == 2).OrderByDescending(u => u.AddTime).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                var linq = from a in iquery
                           join b in _dbContext.Set<UserInfo>() on a.UserId equals b.UserId into join_a
                           from c in join_a.DefaultIfEmpty()
                           join d in _dbContext.Set<UserInfo>() on a.NextReviewer equals d.UserId into join_b
                           from e in join_b.DefaultIfEmpty()
                           join f in _dbContext.Set<WorkFlowModel>() on a.ModelId equals f.Id into join_c
                           from g in join_c.DefaultIfEmpty()
                           join h in _dbContext.Set<GoodsConsumableInfo>() on a.OutGoodsId equals h.GoodsId into join_d
                           from i in join_d.DefaultIfEmpty()
                           select new WorkFlowInstanceOutput
                           {
                               Id = a.Id,
                               AddTime = a.AddTime,
                               Description = a.Description,
                               ModelId = a.ModelId,
                               ModelName = g.Title,
                               InstanceId = a.InstanceId,

                               UserId = a.UserId,
                               UserName = c.UserName,
                               NextReviewer = a.NextReviewer,
                               ReviewerName = e.UserName,
                               OutGoodsName = i.Name,
                               OutNum = a.OutNum,
                               Reason = a.Reason,
                               Status = a.Status,
                           };
                return linq.ToList();
            }
            else
            {
                //非耗材
                //计算总条数
                totalCount = _dbContext.Set<WorkFlowInstance>().Where(u => u.UserId == userId && u.ModelId == (int)MyEnum.WorkFlowModelEnum.Equipment).Count();
                //分页，并排序，
                IQueryable<WorkFlowInstance> iquery = _dbContext.Set<WorkFlowInstance>().Where(u => u.UserId == userId && u.ModelId == 3).OrderByDescending(u => u.AddTime).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                //连表
                var linq = from a in iquery
                           join b in _dbContext.Set<UserInfo>() on a.UserId equals b.UserId into join_a
                           from c in join_a.DefaultIfEmpty()
                           join d in _dbContext.Set<UserInfo>() on a.NextReviewer equals d.UserId into join_b
                           from e in join_b.DefaultIfEmpty()
                           join f in _dbContext.Set<WorkFlowModel>() on a.ModelId equals f.Id into join_c
                           from g in join_c.DefaultIfEmpty()
                           join h in _dbContext.Set<GoodsEquipmentInfo>() on a.OutGoodsId equals h.GoodsId into join_d
                           from i in join_d.DefaultIfEmpty()
                           select new WorkFlowInstanceOutput
                           {
                               Id = a.Id,
                               AddTime = a.AddTime,
                               Description = a.Description,
                               ModelId = a.ModelId,
                               ModelName = g.Title,
                               InstanceId = a.InstanceId,

                               UserId = a.UserId,
                               UserName = c.UserName,//注意--
                               NextReviewer = a.NextReviewer,
                               ReviewerName = e.UserName,//注意--
                               OutGoodsName = i.Name,
                               OutNum = a.OutNum,
                               Reason = a.Reason,
                               Status = a.Status,
                           };
                return linq.ToList();
            }
        }


        /// <summary>
        /// 获取实例步骤表
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public List<WorkFlowInstanceStepOutput> LoadStepEntities(string instanceId)
        {
            IQueryable<WorkFlowInstanceStep> iquery = _dbContext.Set<WorkFlowInstanceStep>().Where(w => w.InstanceId == instanceId);
            var linq = from a in iquery
                       join b in _dbContext.Set<UserInfo>() on a.ReviewerId equals b.UserId into join_a
                       from c in join_a.DefaultIfEmpty()
                       join d in _dbContext.Set<UserInfo>() on a.NextReviewerId equals d.UserId into join_b
                       from e in join_b.DefaultIfEmpty()
                       select new WorkFlowInstanceStepOutput
                       {
                           Id = a.Id,
                           InstanceId = a.InstanceId,
                           ReviewerId = a.ReviewerId,
                           ReviewReason = a.ReviewReason,
                           ReviewStatus = a.ReviewStatus,
                           ReviewTime = a.ReviewTime,
                           NextReviewerId = a.NextReviewerId,
                           ReviewerName = c.UserName,
                           NextReviewerName = e.UserName,
                       };
            return linq.ToList();
        }


        #region 两个可以合并
        /// <summary>
        /// 撤销非耗材申请
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool RevokeEquipment(int Id)
        {
            //先查，再查
            WorkFlowInstance entity = _dbContext.Set<WorkFlowInstance>().Find(Id);
            entity.Status = (int)MyEnum.EquipmentStatus.IsRevoke;
            _dbContext.Update(entity);
            return _dbContext.SaveChanges() > 0;
        }


        /// <summary>
        /// 撤销耗材申请
        /// </summary> 
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool RevokeConsumable(int Id)
        {
            //先查，再查
            WorkFlowInstance entity = _dbContext.Set<WorkFlowInstance>().Find(Id);
            entity.Status = (int)MyEnum.ConsumableStatus.IsRevoke;
            _dbContext.Update(entity);
            return _dbContext.SaveChanges() > 0;
        }
        #endregion



        /// <summary>
        /// 非耗材借出申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool ApplyEquipment(WorkFlowInstanceInput input)
        {
            //【1】添加一条流程到流程实例表中
            WorkFlowInstance workFlowInstance = new WorkFlowInstance
            {

                Description = input.Description,
                OutGoodsId = input.OutGoodsId,
                OutNum = input.OutNum,
                Reason = input.Reason,
                UserId = input.UserId,//申请人

                AddTime = DateTime.Now,
                Status = (int)MyEnum.EquipmentStatus.InReview, //设置状态为审核中(使用枚举)
                ModelId = (int)MyEnum.WorkFlowModelEnum.Equipment,
                InstanceId = input.UserId + DateTime.Now.ToString("yyyyMMddHHmmss"),//“订单号”，申请单号-->必须保证唯一,而且希望能够有所意义--本项目不是高并发（同一时间内有很多人同时请求你的服务器）项目==>如果是高并发项目--使用：雪花算法
                                                                                    //连表获取
                NextReviewer = (from a in _dbContext.Set<UserInfo>().Where(u => u.UserId == input.UserId)
                                join b in _dbContext.Set<DepartmentInfo>() on a.DepartmentId equals b.DepartmentId into join_a
                                from c in join_a.DefaultIfEmpty()                 
                                select new
                                {
                                    LeaderId = c.LeaderId,
                                }).FirstOrDefault().LeaderId,
            };
            //如果是部门领导借，就让部门领导的上级审核，最高级领导借就自己审核
            if (input.UserId== workFlowInstance.NextReviewer)
            {
                workFlowInstance.NextReviewer = (from a in _dbContext.Set<DepartmentInfo>().Where(u => u.LeaderId == workFlowInstance.UserId)
                                                 join b in _dbContext.Set<DepartmentInfo>() on a.ParentId equals b.DepartmentId into join_c
                                                 from c in join_c.DefaultIfEmpty()
                                                 select new
                                                 {
                                                     leaderId = c.LeaderId,

                                                 }).FirstOrDefault().leaderId;
            }
            _dbContext.Add(workFlowInstance);//打上添加标记
            //_dbContext.SaveChanges();//提交并操作到数据库



            //【2】添加步骤到实例步骤表中-->有多少个主管签核，就应该添加多少个步骤，并维护好每个步骤的“父子关系”
            WorkFlowInstanceStep workFlowInstanceStep = new WorkFlowInstanceStep
            {
                InstanceId = workFlowInstance.InstanceId,//设置InstanceId，必须与流程实例的InstanceId相同
                ReviewStatus = (int)MyEnum.ReviewStatus.UnReview,
                ReviewerId = workFlowInstance.NextReviewer,//获取第一个审核人
                NextReviewerId = "无",//设置下一个审核人，如果是最高审核人，设置为无

            };
            _dbContext.Add(workFlowInstanceStep);//打上添加标记
            return _dbContext.SaveChanges() > 0;//提交并操作到数据库------EF Core内置事务功能：只要你是在同一个SaveChanges()提交的，都默认是一个事务



        }
        /// <summary>
        /// 根据登陆人UserID分页获取需要其他审核的工作流实例
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="nextReviewerId"></param>
        /// <param name="isConsumable"></param>
        /// <returns></returns>
        public List<WorkFlowInstanceOutput> LoadPageEntitiesByReviewer(int pageIndex, int pageSize, out int totalCount, string nextReviewerId, bool isConsumable)
        {

            if (isConsumable)//说明是耗材模板
            {
                totalCount = _dbContext.Set<WorkFlowInstance>().Where(u => u.NextReviewer == nextReviewerId && u.Status == (int)MyEnum.ConsumableStatus.InReview && u.ModelId == (int)MyEnum.WorkFlowModelEnum.Consumable).Count();
                //注意：必须是审核中的状态的才行
                IQueryable<WorkFlowInstance> iquery = _dbContext.Set<WorkFlowInstance>().Where(u => u.NextReviewer == nextReviewerId && u.Status == (int)MyEnum.ConsumableStatus.InReview && u.ModelId == (int)MyEnum.WorkFlowModelEnum.Consumable).OrderByDescending(u => u.AddTime).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                var linq = from a in iquery
                           join b in _dbContext.Set<UserInfo>() on a.UserId equals b.UserId into join_a
                           from c in join_a.DefaultIfEmpty()
                           join d in _dbContext.Set<UserInfo>() on a.NextReviewer equals d.UserId into join_b
                           from e in join_b.DefaultIfEmpty()
                           join f in _dbContext.Set<WorkFlowModel>() on a.ModelId equals f.Id into join_c
                           from g in join_c.DefaultIfEmpty()
                           join h in _dbContext.Set<GoodsConsumableInfo>() on a.OutGoodsId equals h.GoodsId into join_d
                           from i in join_d.DefaultIfEmpty()
                           select new WorkFlowInstanceOutput
                           {
                               Id = a.Id,
                               AddTime = a.AddTime,
                               Description = a.Description,
                               ModelId = a.ModelId,
                               ModelName = g.Title,
                               InstanceId = a.InstanceId,

                               UserId = a.UserId,
                               UserName = c.UserName,
                               NextReviewer = a.NextReviewer,
                               ReviewerName = e.UserName,
                               OutGoodsName = i.Name,
                               OutNum = a.OutNum,
                               Reason = a.Reason,
                               Status = a.Status,
                           };
                return linq.ToList();
            }
            else
            {
                totalCount = _dbContext.Set<WorkFlowInstance>().Where(u => u.NextReviewer == nextReviewerId && u.Status == (int)MyEnum.EquipmentStatus.InReview && u.ModelId == (int)MyEnum.WorkFlowModelEnum.Equipment).Count();
                //注意：必须是审核中的状态的才行
                IQueryable<WorkFlowInstance> iquery = _dbContext.Set<WorkFlowInstance>().Where(u => u.NextReviewer == nextReviewerId && u.Status == (int)MyEnum.EquipmentStatus.InReview && u.ModelId == (int)MyEnum.WorkFlowModelEnum.Equipment).OrderByDescending(u => u.AddTime).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                var linq = from a in iquery
                           join b in _dbContext.Set<UserInfo>() on a.UserId equals b.UserId into join_a
                           from c in join_a.DefaultIfEmpty()
                           join d in _dbContext.Set<UserInfo>() on a.NextReviewer equals d.UserId into join_b
                           from e in join_b.DefaultIfEmpty()
                           join f in _dbContext.Set<WorkFlowModel>() on a.ModelId equals f.Id into join_c
                           from g in join_c.DefaultIfEmpty()
                           join h in _dbContext.Set<GoodsEquipmentInfo>() on a.OutGoodsId equals h.GoodsId into join_d
                           from i in join_d.DefaultIfEmpty()
                           select new WorkFlowInstanceOutput
                           {
                               Id = a.Id,
                               AddTime = a.AddTime,
                               Description = a.Description,
                               ModelId = a.ModelId,
                               ModelName = g.Title,
                               InstanceId = a.InstanceId,

                               UserId = a.UserId,
                               UserName = c.UserName,
                               NextReviewer = a.NextReviewer,
                               ReviewerName = e.UserName,
                               OutGoodsName = i.Name,
                               OutNum = a.OutNum,
                               Reason = a.Reason,
                               Status = a.Status,
                           };
                return linq.ToList();
            }


        }


        /// <summary>
        /// 需要出库的工作流实例
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="selectInfo"></param>
        /// <param name="isConsumable">true为耗材，false为非耗材</param>
        /// <returns></returns>
        public List<WorkFlowInstanceOutput> LoadPageEntitiesByKeeper(int pageIndex, int pageSize, out int totalCount, string selectInfo, bool isConsumable)
        {
            if (isConsumable)
            {
                IQueryable<WorkFlowInstance> iquery;
                if (string .IsNullOrEmpty(selectInfo))
                {
                    totalCount = _dbContext.Set<WorkFlowInstance>().Where(u => u.Status == (int)MyEnum.ConsumableStatus.IsPass && u.ModelId == (int)MyEnum.WorkFlowModelEnum.Consumable).Count();
                    iquery = _dbContext.Set<WorkFlowInstance>().Where(u => u.Status == (int)MyEnum.ConsumableStatus.IsPass && u.ModelId == (int)MyEnum.WorkFlowModelEnum.Consumable).OrderBy(u => u.AddTime).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                }
                else
                {
                    totalCount=_dbContext.Set<WorkFlowInstance>().Where(u=>u.Status == (int)MyEnum.ConsumableStatus.IsPass && u.UserId == selectInfo && u.ModelId==(int)MyEnum.WorkFlowModelEnum.Consumable).Count();
                    iquery = _dbContext.Set<WorkFlowInstance>().Where(u => u.Status == (int)MyEnum.ConsumableStatus.IsPass && u.UserId == selectInfo && u.ModelId == (int)MyEnum.WorkFlowModelEnum.Consumable).OrderBy(u => u.AddTime).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                }

                var linq=from a in iquery
                         join b in _dbContext.Set<UserInfo>() on a.UserId equals b.UserId into join_a
                         from c in join_a.DefaultIfEmpty()
                         join d in _dbContext.Set<UserInfo>() on a.NextReviewer equals d.UserId into join_b
                         from e in join_b.DefaultIfEmpty()
                         join f in _dbContext.Set<WorkFlowModel>() on a.ModelId equals f.Id into join_c
                         from g in join_c.DefaultIfEmpty()
                         join h in _dbContext.Set<GoodsConsumableInfo>() on a.OutGoodsId equals h.GoodsId into join_d
                         from i in join_d.DefaultIfEmpty()
                         select new WorkFlowInstanceOutput
                         {
                             Id = a.Id,
                             AddTime = a.AddTime,
                             Description = a.Description,
                             ModelId = a.ModelId,
                             ModelName = g.Title,
                             InstanceId = a.InstanceId,

                             UserId = a.UserId,
                             UserName = c.UserName,
                             NextReviewer = a.NextReviewer,
                             ReviewerName = e.UserName,
                             OutGoodsName = i.Name,
                             OutNum = a.OutNum,
                             Reason = a.Reason,
                             Status = a.Status,
                         };
                return linq.ToList();
            }
            else
            {
                IQueryable<WorkFlowInstance> iquery;
                if (string.IsNullOrEmpty(selectInfo))
                {
                    totalCount = _dbContext.Set<WorkFlowInstance>().Where(u => u.Status == (int)MyEnum.EquipmentStatus.IsPass && u.ModelId == (int)MyEnum.WorkFlowModelEnum.Equipment).Count();
                    iquery = _dbContext.Set<WorkFlowInstance>().Where(u => u.Status == (int)MyEnum.EquipmentStatus.IsPass && u.ModelId == (int)MyEnum.WorkFlowModelEnum.Equipment).OrderBy(u => u.AddTime).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                }
                else
                {
                    totalCount = _dbContext.Set<WorkFlowInstance>().Where(u => u.Status == (int)MyEnum.EquipmentStatus.IsPass && u.UserId == selectInfo && u.ModelId == (int)MyEnum.WorkFlowModelEnum.Equipment).Count();
                    iquery = _dbContext.Set<WorkFlowInstance>().Where(u => u.Status == (int)MyEnum.EquipmentStatus.IsPass && u.UserId == selectInfo && u.ModelId == (int)MyEnum.WorkFlowModelEnum.Equipment).OrderBy(u => u.AddTime).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                }
                var linq = from a in iquery
                           join b in _dbContext.Set<UserInfo>() on a.UserId equals b.UserId into join_a
                           from c in join_a.DefaultIfEmpty()
                           join d in _dbContext.Set<UserInfo>() on a.NextReviewer equals d.UserId into join_b
                           from e in join_b.DefaultIfEmpty()
                           join f in _dbContext.Set<WorkFlowModel>() on a.ModelId equals f.Id into join_c
                           from g in join_c.DefaultIfEmpty()
                           join h in _dbContext.Set<GoodsEquipmentInfo>() on a.OutGoodsId equals h.GoodsId into join_d
                           from i in join_d.DefaultIfEmpty()
                           select new WorkFlowInstanceOutput
                           {
                               Id = a.Id,
                               AddTime = a.AddTime,
                               Description = a.Description,
                               ModelId = a.ModelId,
                               ModelName = g.Title,
                               InstanceId = a.InstanceId,

                               UserId = a.UserId,
                               UserName = c.UserName,
                               NextReviewer = a.NextReviewer,
                               ReviewerName = e.UserName,
                               OutGoodsName = i.Name,
                               OutNum = a.OutNum,
                               Reason = a.Reason,
                               Status = a.Status,
                           };
                return linq.ToList();
            }
        }


        /// <summary>
        /// 获取实例实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public WorkFlowInstanceOutput LoadInstanceEntitiy(int Id)
        {
            var entity = _dbContext.Set<WorkFlowInstance>().Find(Id);
            return new WorkFlowInstanceOutput
            {
                Id=entity.Id,
                AddTime=entity.AddTime,
                Description=entity.Description,
                ModelId=entity.ModelId,
                InstanceId=entity.InstanceId,
                OutNum=entity.OutNum,
                Reason=entity.Reason,
                Status=entity.Status,
                OutGoodsId=entity.OutGoodsId,
            };
        }

        /// <summary>
        /// 非耗材借出审核
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="reviewerId"></param>
        /// <returns></returns>
        public bool ReviewEquipment(string instanceId, string reviewerId, bool isPass, string reason)
        {
            //【1】获取对应的实例表和登陆者对应实例的对应步骤
            WorkFlowInstance instanceEntity = _dbContext.Set<WorkFlowInstance>().Where(u => u.InstanceId == instanceId).FirstOrDefault();
            WorkFlowInstanceStep stepEntity = _dbContext.Set<WorkFlowInstanceStep>().Where(u => u.InstanceId == instanceId && u.ReviewerId == reviewerId).FirstOrDefault();

            //【2】给步骤表添加审核时间和审核理由
            stepEntity.ReviewTime = DateTime.Now;
            stepEntity.ReviewReason = reason;

            if (isPass)
            {

                //说明主管审核通过

                stepEntity.ReviewStatus = (int)MyEnum.ReviewStatus.IsPass;//修改步骤表的状态为已通过

                if (stepEntity.NextReviewerId == "无")
                {
                    //说明是最后一个审核人--
                    instanceEntity.Status = (int)MyEnum.EquipmentStatus.IsPass;//修改实例表的状态为已通过
                }
                else
                {
                    //如果不是最后一个审核人？应该把下一个审核人的userid放到实例表的NextReviewer字段里，这样就能把签核流程传递下去了
                    //耗材部分，，就要补全这里的业务逻辑
                }

            }
            else
            {
                //说明主管驳回申请
                stepEntity.ReviewStatus = (int)MyEnum.ReviewStatus.IsRefused;//修改步骤表的状态为已驳回
                instanceEntity.Status = (int)MyEnum.EquipmentStatus.IsRefused;//修改实例表的状态为已驳回
            }
            //【3】打上标记，内置事务提交
            _dbContext.Update(stepEntity);
            _dbContext.Update(instanceEntity);
            return _dbContext.SaveChanges() > 0;
        }


        /// <summary>
        /// 耗材借出审核
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="reviewerId"></param>
        /// <returns></returns>
        /// 

        
        public bool ReviewConsumable(string instanceId, string reviewerId, bool isPass, string reason)
        {
            DateTime dateTime = DateTime.Now;
            //【1】获取对应的实例表和登陆者对应实例的对应步骤
            WorkFlowInstance instanceEntity = _dbContext.Set<WorkFlowInstance>().Where(u => u.InstanceId == instanceId).FirstOrDefault();
            WorkFlowInstanceStep stepEntity = _dbContext.Set<WorkFlowInstanceStep>().Where(u => u.InstanceId == instanceId && u.ReviewerId == reviewerId).FirstOrDefault();

            //【2】给步骤表添加审核时间和审核理由
            stepEntity.ReviewTime = (DateTime)dateTime;
            stepEntity.ReviewReason = reason;//理由



            if (isPass)
            {

                //获取最后审核人
                var last = (from a in _dbContext.Set<DepartmentInfo>().Where(u => u.LeaderId == reviewerId)
                            join b in _dbContext.Set<DepartmentInfo>() on a.ParentId equals b.DepartmentId into join_c
                            from c in join_c.DefaultIfEmpty()
                            select new
                            {
                                last = c.LeaderId
                            }).FirstOrDefault().last;
                if (reviewerId == last)
                {
                    stepEntity.ReviewStatus = (int)MyEnum.ReviewStatus.IsPass;

                    instanceEntity.Status = (int)MyEnum.ConsumableStatus.IsPass;
                }
                else
                {
                    //把WorkFlow_Instance的审核人更改为最后一个审核人
                    //instanceEntity.NextReviewer = last;

                    var StepNum = _dbContext.Set<WorkFlowInstanceStep>().Where(u => u.InstanceId == instanceId).Count();
                    if (StepNum==2)
                    {
                        instanceEntity.NextReviewer = last;
                    }
                    else if(StepNum==1)
                    {
                        instanceEntity.Status = (int)MyEnum.ConsumableStatus.IsPass;
                    }

                    

                    stepEntity.ReviewStatus = (int)MyEnum.ReviewStatus.IsPass;


                }
            }
            else
            {
                //说明主管驳回申请
                stepEntity.ReviewStatus = (int)MyEnum.ReviewStatus.IsRefused;//修改步骤表的状态为已驳回

                instanceEntity.Status = (int)MyEnum.ConsumableStatus.IsRefused;//修改实例表的状态为已驳回
            }


            _dbContext.Update(stepEntity);
            _dbContext.Update(instanceEntity);
            return _dbContext.SaveChanges() > 0;
        }
       



        /// <summary>
        /// 非耗材批量出库
        /// </summary>
        /// <param name="arrInstance"></param>
        /// <param name="reviewerId">仓库管理员编号</param>
        /// <returns></returns>
        public bool OutputEquipment(string arrInstance, string reviewerId)
        {
            //【1】分割字符串，获取所有实例编号 
            string[] arr = arrInstance.Split(',');
            //【2】循环，并修改实例表和添加实例步骤表记录,同时减掉库存  注意： arr.Length - 1：因为前端没有解决最后一个逗号的问题,那我干脆不循环到它就行了
            for (int i = 0; i < arr.Length - 1; i++)
            {
                //先查
                WorkFlowInstance instanceEntity = _dbContext.Set<WorkFlowInstance>().Where(u => u.InstanceId == arr[i]).FirstOrDefault();
                //修改状态为已借出
                instanceEntity.Status = (int)MyEnum.EquipmentStatus.IsLent;
                instanceEntity.NextReviewer = reviewerId;//由你爱，可改可不改，
                //减掉库存-思考：需要做库存验证吗？--严谨上来说，为了代码的健壮性--但是，实际场景，如果库存不够，难道仓库管理员不知道吗？
                GoodsEquipmentInfo equipmentEntity = _dbContext.Set<GoodsEquipmentInfo>().Where(u => u.GoodsId == instanceEntity.OutGoodsId).FirstOrDefault();
                equipmentEntity.Num -= instanceEntity.OutNum;
                //添加一个步骤，只是为了展示进度而已
                WorkFlowInstanceStep stepEntity = new WorkFlowInstanceStep
                {
                    Id = 0,
                    InstanceId = arr[i],//根据循环的实例编号，添加一条步骤（只是为了展示借出时间和借出人而已）
                    NextReviewerId = "无",//无所谓
                    ReviewStatus = (int)MyEnum.ReviewStatus.IsPass,//无所谓
                    ReviewTime = DateTime.Now,
                    ReviewerId = reviewerId,//借出人
                    ReviewReason = "已借出",//关键--用于展示进度的判断依据
                };

                _dbContext.Set<WorkFlowInstance>().Update(instanceEntity);
                _dbContext.Set<GoodsEquipmentInfo>().Update(equipmentEntity);
                _dbContext.Set<WorkFlowInstanceStep>().Add(stepEntity);

            }

            //【3】内置事务，提交操作
            return _dbContext.SaveChanges() > 0;

        }



        /// <summary>
        /// 耗材批量出库
        /// </summary>
        /// <param name="arrInstance"></param>
        /// <param name="reviewerId">仓库管理员编号</param>
        /// <returns></returns>
        public bool OutputConsumable(string arrInstance, string reviewerId)
        {
            //【1】分割字符串，获取所有实例编号 
            string[] arr = arrInstance.Split(',');
            //【2】循环，并修改实例表和添加实例步骤表记录,同时减掉库存  注意： arr.Length - 1：因为前端没有解决最后一个逗号的问题,那我干脆不循环到它就行了
            for (int i = 0; i < arr.Length - 1; i++)
            {
                //先查
                WorkFlowInstance instanceEntity = _dbContext.Set<WorkFlowInstance>().Where(u => u.InstanceId == arr[i]).FirstOrDefault();
                //修改状态为已领取
                instanceEntity.Status = (int)MyEnum.ConsumableStatus.IsOutput;
                instanceEntity.NextReviewer = reviewerId;//由你爱，可改可不改，
                //减掉库存-思考：需要做库存验证吗？--严谨上来说，为了代码的健壮性--但是，实际场景，如果库存不够，难道仓库管理员不知道吗？
                GoodsConsumableInfo consumableEntity=_dbContext.Set<GoodsConsumableInfo>().Where(u=>u.GoodsId==instanceEntity.OutGoodsId).FirstOrDefault();
                consumableEntity.Num -= instanceEntity.OutNum;
               
                //添加一个步骤，只是为了展示进度而已
                WorkFlowInstanceStep stepEntity = new WorkFlowInstanceStep
                {
                    Id = 0,
                    InstanceId = arr[i],//根据循环的实例编号，添加一条步骤（只是为了展示领取时间和领取人而已）
                    NextReviewerId = "无",//无所谓
                    ReviewStatus = (int)MyEnum.ReviewStatus.IsPass,//无所谓
                    ReviewTime = DateTime.Now,
                    ReviewerId = reviewerId,//借出人
                    ReviewReason = "已领取",//关键--用于展示进度的判断依据
                };

                _dbContext.Set<WorkFlowInstance>().Update(instanceEntity);
                _dbContext.Set<GoodsConsumableInfo>().Update(consumableEntity);
                _dbContext.Set<WorkFlowInstanceStep>().Add(stepEntity);

            }

            //【3】内置事务，提交操作
            return _dbContext.SaveChanges() > 0;

        }





        /// <summary>
        /// 非耗材批量归还
        /// </summary>
        /// <param name="arrInstance"></param>
        /// <param name="reviewerId"></param>
        /// <returns></returns>
        public bool ReturnEquipment(string arrInstance, string reviewerId)
        {
            //【1】分割字符串，获取所有实例编号 
            string[] arr = arrInstance.Split(',');
           //【2】循环，并修改实例表和添加实例步骤表记录,同时减掉库存 注意： arr.Length - 1：因为前端没有解决最后一个逗号的问题,那我干脆不循环到它就行了
            for (int i = 0; i < arr.Length - 1; i++)
            {
                //先查
                WorkFlowInstance instanceEntity = _dbContext.Set<WorkFlowInstance>().Where(u => u.InstanceId == arr[i]).FirstOrDefault();
                //修改状态为已归还
                instanceEntity.Status = (int)MyEnum.EquipmentStatus.IsReturn;
                instanceEntity.NextReviewer = reviewerId;//由你爱，可改可不改，
                //减掉库存-思考：需要做库存验证吗？--严谨上来说，为了代码的健壮性--但是，实际场景，如果库存不够，难道仓库管理员不知道吗？
                GoodsEquipmentInfo equipmentEntity = _dbContext.Set<GoodsEquipmentInfo>().Where(u => u.GoodsId == instanceEntity.OutGoodsId).FirstOrDefault();
                equipmentEntity.Num += instanceEntity.OutNum;
                //添加一个步骤，只是为了展示进度而已
                WorkFlowInstanceStep stepEntity = new WorkFlowInstanceStep
                {
                    Id = 0,
                    InstanceId = arr[i],//根据循环的实例编号，添加一条步骤（只是为了展示借出时间和借出人而已）
                    NextReviewerId = "无",//无所谓
                    ReviewStatus = (int)MyEnum.ReviewStatus.IsPass,//无所谓
                    ReviewTime = DateTime.Now,
                    ReviewerId = reviewerId,//借出人
                    ReviewReason = "已归还",//关键--用于展示进度的判断依据
                };

                _dbContext.Set<WorkFlowInstance>().Update(instanceEntity);
                _dbContext.Set<GoodsEquipmentInfo>().Update(equipmentEntity);
                _dbContext.Set<WorkFlowInstanceStep>().Add(stepEntity);

            }

            //【3】内置事务，提交操作
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 耗材借出申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool ApplyConsumable(WorkFlowInstanceInput input)
        {
            //【1】添加一条流程到流程实例表中
            WorkFlowInstance workFlowInstance = new WorkFlowInstance
            {
                Description = input.Description,
                OutGoodsId = input.OutGoodsId,
                OutNum = input.OutNum,
                Reason = input.Reason,
                UserId = input.UserId,//申请人

                AddTime = DateTime.Now,
                Status = (int)MyEnum.ConsumableStatus.InReview,//设置状态为审核中
                ModelId = (int)MyEnum.WorkFlowModelEnum.Consumable,
                InstanceId = input.UserId + DateTime.Now.ToString("yyyyMMddHHmmss"),//订单号，有意义


                NextReviewer = (from a in _dbContext.Set<UserInfo>().Where(u => u.UserId == input.UserId)
                                join b in _dbContext.Set<DepartmentInfo>() on a.DepartmentId equals b.DepartmentId into join_a
                                from c in join_a.DefaultIfEmpty()
                                select new
                                {
                                    LeaderId = c.LeaderId,
                                }).FirstOrDefault().LeaderId,

            };

            _dbContext.Add(workFlowInstance);

            //查最后审核人
            var Final = (from a in _dbContext.Set<DepartmentInfo>().Where(u => u.LeaderId == workFlowInstance.NextReviewer)
                                 join b in _dbContext.Set<DepartmentInfo>() on a.ParentId equals b.DepartmentId into join_a
                                 from c in join_a.DefaultIfEmpty()
                                 select new
                                 {
                                     LeaderId = c.LeaderId,
                                 }).FirstOrDefault().LeaderId;

            if (workFlowInstance.NextReviewer== Final)
            {
                WorkFlowInstanceStep workFlowInstanceStep = new WorkFlowInstanceStep
                {
                    InstanceId = workFlowInstance.InstanceId,//设置InstanceId，必须与流程实例的InstanceId相同
                    ReviewStatus = (int)MyEnum.ReviewStatus.UnReview,
                    ReviewerId = workFlowInstance.NextReviewer,//获取第一个审核人
                    NextReviewerId = "无",//设置下一个审核人，如果是最高审核人，设置为无

                };
                _dbContext.Add(workFlowInstanceStep);//打上添加标记
            }
            else
            {
                if (workFlowInstance.OutNum>10)
                {
                    WorkFlowInstanceStep workFlowInstanceStep = new WorkFlowInstanceStep
                    {
                        InstanceId = workFlowInstance.InstanceId,//设置InstanceId，必须与流程实例的InstanceId相同
                        ReviewStatus = (int)MyEnum.ReviewStatus.UnReview,
                        ReviewerId = workFlowInstance.NextReviewer,//获取第一个审核人
                        NextReviewerId = Final,//设置下一个审核人，如果是最高审核人，设置为无

                    };
                    _dbContext.Add(workFlowInstanceStep);//打上添加标记
                }
               


                WorkFlowInstanceStep workFlowInstanceStep2 = new WorkFlowInstanceStep
                {
                    InstanceId = workFlowInstance.InstanceId,//设置InstanceId，必须与流程实例的InstanceId相同
                    ReviewStatus = (int)MyEnum.ReviewStatus.UnReview,
                    ReviewerId = workFlowInstance.OutNum>10 ?  Final:workFlowInstance.NextReviewer,//获取第一个审核人
                    NextReviewerId = "无",//设置下一个审核人，如果是最高审核人，设置为无

                };
                _dbContext.Add(workFlowInstanceStep2);//打上添加标记
            }
            return _dbContext.SaveChanges() > 0;
        }


        /// <summary>
        /// 非耗材待归还记录获取 分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <param name="selectInfo"></param>
        /// <returns></returns>
        public List<WorkFlowInstanceOutput> GetEquipmentIsLent(int page, int limit, out int totalCount, string selectInfo)
        {
            IQueryable<WorkFlowInstance> iquery;
            if (string.IsNullOrEmpty(selectInfo))
            {
                totalCount = _dbContext.Set<WorkFlowInstance>().Where(u => u.Status == (int)MyEnum.EquipmentStatus.IsLent && u.ModelId == (int)MyEnum.WorkFlowModelEnum.Equipment).Count();
                iquery = _dbContext.Set<WorkFlowInstance>().Where(u => u.Status == (int)MyEnum.EquipmentStatus.IsLent && u.ModelId == (int)MyEnum.WorkFlowModelEnum.Equipment).OrderBy(u => u.AddTime).Skip((page - 1) * limit).Take(limit);
            }
            else
            {
                totalCount = _dbContext.Set<WorkFlowInstance>().Where(u => u.Status == (int)MyEnum.EquipmentStatus.IsLent && u.UserId == selectInfo && u.ModelId == (int)MyEnum.WorkFlowModelEnum.Equipment).Count();
                iquery = _dbContext.Set<WorkFlowInstance>().Where(u => u.Status == (int)MyEnum.EquipmentStatus.IsLent && u.UserId == selectInfo && u.ModelId == (int)MyEnum.WorkFlowModelEnum.Equipment).OrderBy(u => u.AddTime).Skip((page - 1) * limit).Take(limit);
            }
            var linq = from a in iquery
                       join b in _dbContext.Set<UserInfo>() on a.UserId equals b.UserId into join_a
                       from c in join_a.DefaultIfEmpty()
                       join d in _dbContext.Set<UserInfo>() on a.NextReviewer equals d.UserId into join_b
                       from e in join_b.DefaultIfEmpty()
                       join f in _dbContext.Set<WorkFlowModel>() on a.ModelId equals f.Id into join_c
                       from g in join_c.DefaultIfEmpty()
                       join h in _dbContext.Set<GoodsEquipmentInfo>() on a.OutGoodsId equals h.GoodsId into join_d
                       from i in join_d.DefaultIfEmpty()
                       select new WorkFlowInstanceOutput
                       {
                           Id = a.Id,
                           AddTime = a.AddTime,
                           Description = a.Description,
                           ModelId = a.ModelId,
                           ModelName = g.Title,
                           InstanceId = a.InstanceId,

                           UserId = a.UserId,
                           UserName = c.UserName,
                           NextReviewer = a.NextReviewer,
                           ReviewerName = e.UserName,
                           OutGoodsName = i.Name,
                           OutNum = a.OutNum,
                           Reason = a.Reason,
                           Status = a.Status,
                       };
            return linq.ToList();
        }

       
    }
}
