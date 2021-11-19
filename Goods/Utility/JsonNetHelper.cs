using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
   public  class JsonNetHelper
    {
        /// <summary>
        /// 格式化日期+序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializetoJson(object obj)
        {
            //IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            //timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd HH':'mm':'ss";//格式化时间，默认是ISO8601格式


            var setting = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),//启用小驼峰命名
                DateFormatString = "yyyy-MM-dd HH:mm:ss"//格式化日期
            };


            return JsonConvert.SerializeObject(obj, Formatting.Indented, setting);//真正调用json.net框架，实现序列化

        }
    }
}
