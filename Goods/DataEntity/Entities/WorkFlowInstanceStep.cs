using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Entities
{
    public partial class WorkFlowInstanceStep
    {
        public int Id { get; set; }
        public string InstanceId { get; set; }
        public string ReviewerId { get; set; }
        public string ReviewReason { get; set; }
        public byte ReviewStatus { get; set; }
        public DateTime? ReviewTime { get; set; }
        public string NextReviewerId { get; set; }
    }
}
