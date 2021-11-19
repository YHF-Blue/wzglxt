﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Entities
{
    public partial class PowerInfo
    {
        public int Id { get; set; }
        public string PowerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Sort { get; set; }
        public string ActionUrl { get; set; }
        public string ParentId { get; set; }
        public string MenuIconUrl { get; set; }
        public string HttpMethod { get; set; }
    }
}
