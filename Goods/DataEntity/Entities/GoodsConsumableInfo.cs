using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Entities
{
    public partial class GoodsConsumableInfo
    {
        public int Id { get; set; }
        public string GoodsId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Specification { get; set; }
        public double Num { get; set; }
        public string Unit { get; set; }
        public decimal? Money { get; set; }
        public double? WarningNum { get; set; }
        public byte DelFlag { get; set; }
        public DateTime AddTime { get; set; }
    }
}
