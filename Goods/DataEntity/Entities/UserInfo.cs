using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Entities
{
    public partial class UserInfo
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNum { get; set; }
        public string Email { get; set; }
        public string DepartmentId { get; set; }
        public byte Sex { get; set; }
        public string PassWord { get; set; }
        public DateTime AddTime { get; set; }
        public byte DelFlag { get; set; }
        public DateTime? DelTime { get; set; }


        //图片路径
        public string Url { get; set; }
    }
}
