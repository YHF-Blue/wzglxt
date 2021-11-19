using System;
using System.Collections.Generic;
using System.Text;

namespace Model.WorkFlowDTO
{
   public  class WorkFlowInstanceStepOutput
    {

        public int Id { get; set; }
        public string InstanceId { get; set; }
        public string ReviewerId { get; set; }
        public string ReviewReason { get; set; }
        public byte ReviewStatus { get; set; }
        public DateTime? ReviewTime { get; set; }
        public string NextReviewerId { get; set; }

        /// <summary>
        /// 审核人名字
        /// </summary>
        public string ReviewerName { get; set; }

        /// <summary>
        /// 下一个审核人名字
        /// </summary>
        public string NextReviewerName { get; set; }
    }
}
