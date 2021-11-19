using System;
using System.Collections.Generic;
using System.Text;

namespace Model.WorkFlowDTO
{
   public  class WorkFlowInstanceOutput
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public string UserId { get; set; }
        public byte Status { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
        public string NextReviewer { get; set; }
        public DateTime AddTime { get; set; }
        public double OutNum { get; set; }
        public string OutGoodsId { get; set; }

        public string InstanceId { get; set; }
        /// <summary>
        /// 发起人名字
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 当前审核人名字
        /// </summary>
        public string ReviewerName { get; set; }

        /// <summary>
        /// 工作流模板名称
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// 物品名称
        /// </summary>
        public string OutGoodsName { get; set; }
    }
}
