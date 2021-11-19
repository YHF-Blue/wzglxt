using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UserDTO
{
   public  class UserOutput
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNum { get; set; }
        public string Email { get; set; }
        public string DepartmentId { get; set; }
        public byte Sex { get; set; }

        //public string PassWord { get; set; }
        public DateTime AddTime { get; set; }
        public byte DelFlag { get; set; }

        //public DateTime? DelTime { get; set; }


        //连表，前端需要展示的数据
        public string DepartmentName { get; set; }

        public string RoleName { get; set; }

        //图片
        public string Url { get; set; }
    }
}
