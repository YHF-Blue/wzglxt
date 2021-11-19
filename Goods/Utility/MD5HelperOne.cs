using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DataEntity
{
   public static class MD5HelperOne
    {
        public static string GetMD5(string pwd)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.Unicode.GetBytes(pwd);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte25tring = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte25tring += targetData[i].ToString("x2");
            }
            return byte25tring;
        }
    }
}
