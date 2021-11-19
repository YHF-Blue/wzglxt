using System;
using System.Collections.Generic;
using System.Text;

namespace Model.WorkFlowDTO
{
   public  class WorkFlowInstanceInput
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
        //验证--自己做
        public double OutNum { get; set; }
        public string OutGoodsId { get; set; }
    }
}
