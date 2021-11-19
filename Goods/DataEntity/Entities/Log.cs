using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Entities
{
    public partial class Log
    {
        public int Id { get; set; }
        public string Decription { get; set; }
        public DateTime? AddTime { get; set; }
    }
}
