using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.UserDTO
{
   public  class UserInput
    {
        public string UserId { get; set; }


        [Required]//说明这个字段必须不能为空
        [StringLength(5)]//说明这个字段长度不能大于5
        public string UserName { get; set; }
        public string PhoneNum { get; set; }
        public string Email { get; set; }
        public string DepartmentId { get; set; }
        public byte Sex { get; set; }
    }
}
