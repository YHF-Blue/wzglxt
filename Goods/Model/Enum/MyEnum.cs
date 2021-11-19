using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Emun
{
   public class MyEnum
    {

        /// <summary>
        /// 耗材领取审核总状态--对应表WorkFlow_Instance
        /// </summary>
        public enum ConsumableStatus
        {
            InReview,//审核中         
            IsPass,//审核已通过
            IsRevoke,//已撤销（流程结束）
            IsRefused,//已拒绝（驳回，流程结束）
            IsOutput,//已领取（流程结束）        
        }

        /// <summary>
        /// 非耗材借还总状态--对应表WorkFlow_Instance
        /// </summary>
        public enum EquipmentStatus
        {
            InReview,//借出审核中     
            IsPass,//审已核通过
            IsRevoke,//已撤销（流程结束）
            IsRefused,//已拒绝（驳回，流程结束）
            IsLent,//已借出
            IsReturn,//已归还（流程结束）
        }


        public enum WorkFlowModelEnum
        {
            Consumable = 2,//耗材   
            Equipment = 3,//非耗材

        }



        /// <summary>
        /// 主管签核状态--对应表WorkFlow_InstanceStep
        /// </summary>
        public enum ReviewStatus
        {
            UnReview,//未审核     
            IsPass,//审已核通过
            IsRefused,//已拒绝（驳回，流程结束）
        }
    }
}
