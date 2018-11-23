using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Caching.Distributed
{
    static public class IDistributedCacheExtend
    {
        static public int DefaultSlidingExpiration { get; set; } = 600;

        static Random random = new Random();

        /// <summary>
        /// 将对象转成json字符串后存到缓存中
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="options">过期选项,如果不指定,则在配置的默认时间后滑动过期</param>
        public static void Set(this IDistributedCache cache, string key, object value, DistributedCacheEntryOptions options = null)
        {
            if (options == null)
            {
                options = new DistributedCacheEntryOptions();
            }

            options.SlidingExpiration = TimeSpan.FromSeconds(DefaultSlidingExpiration + random.Next(10));

            cache.SetString(key, JsonConvert.SerializeObject(value), options);
        }

        /// <summary>
        /// 将对象转成json字符串后存到缓存中
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="options">过期选项,如果不指定,则在配置的默认时间后滑动过期</param>
        public static async Task SetAsync(this IDistributedCache cache, string key, object value, DistributedCacheEntryOptions options = null)
        {
            if (options == null)
            {
                options = new DistributedCacheEntryOptions();
            }

            options.SlidingExpiration = TimeSpan.FromSeconds(DefaultSlidingExpiration + random.Next(10));

            var jsonStr = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(value));

            await cache.SetStringAsync(key, jsonStr, options);
        }

        /// <summary>
        /// 从缓存中获取json字符串并转成指定类型对象返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue">如果没有取到返回的默认值</param>
        /// <returns></returns>
        public static T Get<T>(this IDistributedCache cache, string key,T defaultValue=default)
        {
            var jsonStr = cache.GetString(key);
            return string.IsNullOrWhiteSpace(jsonStr) ? defaultValue : JsonConvert.DeserializeObject<T>(jsonStr);
        }

        /// <summary>
        /// 从缓存中获取json字符串并转成指定类型对象返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue">如果没有取到返回的默认值</param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(this IDistributedCache cache, string key, T defaultValue = default)
        {
            var jsonStr =await cache.GetStringAsync(key);
            
            if (string.IsNullOrWhiteSpace(jsonStr))
            {
                return defaultValue;
            }

            return  await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(jsonStr));
        }

        /// <summary>
        /// 指定的key是否存在
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<bool> ExistsAsync(this IDistributedCache cache, string key)
        {
            var bytes = await cache.GetAsync(key);

            return bytes != null && bytes.Length > 0;
        }

    }

}
