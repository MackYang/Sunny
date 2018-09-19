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
            string jsonStr = null;
            if (obj != null)
            {
                try
                {
                    jsonStr = JsonConvert.SerializeObject(obj);
                }
                catch (Exception ex)
                {
                    new Exception("将类型" + obj.ToString() + "的对象转换成Json字符串时发生异常:" + ex);
                }
            }

            return jsonStr;
        }

        /// <summary>
        /// 将json字符串转换成指定类型的对象
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <param name="jsonString">json字符串</param>
        /// <returns>指定类型的对象</returns>
        static public T FromJsonString<T>(string jsonString)
        {
            T t = default(T);
            if (!string.IsNullOrWhiteSpace(jsonString))
            {
                try
                {

                    t = JsonConvert.DeserializeObject<T>(jsonString);
                }
                catch (Exception ex)
                {
                    new Exception("将Json字符串  " + jsonString + " 转换成 " + typeof(T) + " 类型的对象时发生异常: " + ex);
                }
            }
            return t;
        }

        /// <summary>
        /// 将json字符串转换成dynamic类型的对象
        /// </summary>
        /// <param name="jsonString">json字符串</param>
        /// <returns>动态解析类型的对象</returns>
        static public dynamic FromJsonString(string jsonString)
        {
            if (!string.IsNullOrWhiteSpace(jsonString))
            {
                try
                {
                    return JsonConvert.DeserializeObject(jsonString);
                }
                catch (Exception ex)
                {
                    new Exception("将Json字符串  " + jsonString + " 转换成动态类型的对象时发生异常: " + ex);
                }
            }
            return null;
        }
    }
}
