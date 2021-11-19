using Model.EquipmentDTO;
using Model.WorkFlowDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IService
{
   public  interface IWorkFlowService
    {
        /// <summary>
        /// 根据工作流实例发起人Id（申请人） 分页获取工作流实例
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="userId"></param>
        /// <param name="isConsumable">true为耗材，false为非耗材</param>
        /// <returns></returns>
        List<WorkFlowInstanceOutput> LoadPageEntitiesByUserId(int pageIndex, int pageSize, out int totalCount, string userId, bool isConsumable);



        /// <summary>
        /// 获取实例步骤表
        /// </summary>  
        /// <param name="instanceId"></param>
        /// <returns></returns>
        List<WorkFlowInstanceStepOutput> LoadStepEntities(string instanceId);

        /// <summary>
        /// 撤销非耗材申请
        /// </summary> 
        /// <param name="Id"></param>
        /// <returns></returns>
        bool RevokeEquipment(int Id);

        /// <summary>
        /// 撤销耗材申请
        /// </summary> 
        /// <param name="Id"></param>
        /// <returns></returns>
        bool RevokeConsumable(int Id);


        /// <summary>
        /// 非耗材借出申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool ApplyEquipment(WorkFlowInstanceInput input);

        /// <summary>
        /// 耗材借出申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool ApplyConsumable(WorkFlowInstanceInput input);

        /// <summary>
        /// 根据登陆人UserID分页获取需要其他审核的工作流实例
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="nextReviewerId"></param>
        /// <param name="isConsumable"></param>
        /// <returns></returns>
        List<WorkFlowInstanceOutput> LoadPageEntitiesByReviewer(int pageIndex, int pageSize, out int totalCount, string nextReviewerId, bool isConsumable);



        /// <summary>
        /// 需要出库的工作流实例
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="selectInfo"></param>
        /// <param name="isConsumable">true为耗材，false为非耗材</param>
        /// <returns></returns>
        List<WorkFlowInstanceOutput> LoadPageEntitiesByKeeper(int pageIndex, int pageSize, out int totalCount, string selectInfo, bool isConsumable);


        /// <summary>
        /// 获取实例实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        WorkFlowInstanceOutput LoadInstanceEntitiy(int Id);



        /// <summary>
        /// 非耗材借出审核
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="reviewerId"></param>
        /// <returns></returns>
        bool ReviewEquipment(string instanceId, string reviewerId, bool isPass, string reason);



        /// <summary>
        /// 耗材借出审核
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="reviewerId"></param>
        /// <returns></returns>
        bool ReviewConsumable(string instanceId, string reviewerId, bool isPass, string reason);


        /// <summary>
        /// 非耗材批量出库
        /// </summary>
        /// <param name="arrInstance"></param>
        /// <param name="reviewerId">仓库管理员编号</param>
        /// <returns></returns>
        bool OutputEquipment(string arrInstance, string reviewerId);



        /// <summary>
        /// 非耗材批量出库
        /// </summary>
        /// <param name="arrInstance"></param>
        /// <param name="reviewerId">仓库管理员编号</param>
        /// <returns></returns>
        bool OutputConsumable(string arrInstance, string reviewerId);


        /// <summary>
        /// 非耗材批量归还
        /// </summary>
        /// <param name="arrInstance"></param>
        /// <param name="reviewerId">仓库管理员编号</param>
        /// <returns></returns>
        bool ReturnEquipment(string arrInstance, string reviewerId);

        /// <summary>
        /// 非耗材待归还记录获取 分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <param name="selectInfo"></param>
        /// <returns></returns>
        List<WorkFlowInstanceOutput> GetEquipmentIsLent(int page, int limit, out int totalCount, string selectInfo);
       
    }

    
}
