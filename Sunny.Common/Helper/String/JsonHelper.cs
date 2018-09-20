using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace Sunny.Common.Helper.String
{
    public class JsonHelper
    {
        /// <summary>
        /// 将对象转换成json字符串
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>json字符串</returns>
        static public string ToJsonString(object obj)
        {

            return JsonConvert.SerializeObject(obj);

        }

        /// <summary>
        /// 将json字符串转换成指定类型的对象
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <param name="jsonString">json字符串</param>
        /// <returns>指定类型的对象</returns>
        static public T FromJsonString<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// 将json字符串转换成dynamic类型的对象
        /// </summary>
        /// <param name="jsonString">json字符串</param>
        /// <returns>动态解析类型的对象</returns>
        static public dynamic FromJsonString(string jsonString)
        {
            return JsonConvert.DeserializeObject(jsonString);
        }
    }
}
