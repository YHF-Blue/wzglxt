using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Entities
{
    public partial class GoodsConsumableInput
    {
        public int Id { get; set; }
        public string GoodsId { get; set; }
        public double Num { get; set; }
        public DateTime AddTime { get; set; }
        public string AddUserId { get; set; }
    }
}
