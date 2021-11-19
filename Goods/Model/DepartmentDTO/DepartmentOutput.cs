using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DepartmentDTO
{
   public  class DepartmentOutput
    {
        public int Id { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string LeaderId { get; set; }
        public string ParentId { get; set; }
        public DateTime? AddTime { get; set; }

        /// <summary>
        /// 主管ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 主管名
        /// </summary>
        public string UserName { get; set; }

        public string LeaderNane { get; set; }

        public string ParentName { get; set; }
    }
}
