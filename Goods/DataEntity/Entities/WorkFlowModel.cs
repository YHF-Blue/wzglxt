using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Entities
{
    public partial class WorkFlowModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime AddTime { get; set; }
        public byte DelFlag { get; set; }
        public string Description { get; set; }
    }
}
