using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
   public  class SecurityHelper
    {
        /// <summary>
        /// 生成验证码字符串
        /// </summary>
        /// <param name="len">验证码长度</param>
        /// <returns></returns>
        public string CreateVerifyCode(int len)
        {
            char[] data = { 'a','b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k',  'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',  '2', '3', '4', '5', '6', '7', '8', '9' };
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < len; i++)
            {
                int index = random.Next(data.Length);
                char ch = data[index];
                sb.Append(ch);
            }
            return sb.ToString();
        }
    }
}

