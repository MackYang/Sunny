using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sunny.Common.Helper
{
    public class SerializeHelper
    {
        /// <summary>
        /// 将对象序列化为Base64字符串
        /// </summary>
        /// <param name="obj">需要序列化的对象</param>
        /// <returns>Base64字符串</returns>
        public static string SerializeObject(object obj)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                byte[] buffer = new byte[ms.Length];
                ms.Read(buffer, 0, buffer.Length);
                ms.Flush();
                ms.Close();
                return Convert.ToBase64String(buffer);
            }
            catch (Exception ex)
            {
                throw new Exception("将对象序列化成Base64字符串时发生异常:" + ex);
            }

        }

        /// <summary>
        /// 将Base64字符串反序列化为指定类型的对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="base64Str">Base64字符串</param>
        /// <param name="throwException">出现异常时是否抛出</param>
        /// <returns>T类型的对象</returns>
        public static T Desrialize<T>(string base64Str)
        {

            try
            {
                T obj = default(T);
                IFormatter formatter = new BinaryFormatter();
                byte[] buffer = Convert.FromBase64String(base64Str);
                MemoryStream memoryStream = new MemoryStream(buffer);
                obj = (T)formatter.Deserialize(memoryStream);
                memoryStream.Flush();
                memoryStream.Close();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception("将Base64字符串反序列化为对象时出现异常:" + ex);
            }


        }
    }
}
