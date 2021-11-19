using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Entities
{
    public partial class WorkFlowInstance
    {
        public int Id { get; set; }
        public string InstanceId { get; set; }
        public int ModelId { get; set; }
        public string UserId { get; set; }
        public byte Status { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
        public string NextReviewer { get; set; }
        public DateTime AddTime { get; set; }
        public double OutNum { get; set; }
        public string OutGoodsId { get; set; }
    }
}
