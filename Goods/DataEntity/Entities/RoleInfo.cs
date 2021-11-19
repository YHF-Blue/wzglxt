using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Entities
{
    public partial class RoleInfo
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public DateTime AddTime { get; set; }
        public byte DelFlag { get; set; }
        public DateTime? DelTime { get; set; }
    }
}
