using System;
using System.Collections.Generic;
using System.Text;

namespace  System
{
   public static class DateTimeExtend
    {
        /// <summary>
        /// 返回yyyy-MM-dd HH:mm:ss 格式的时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToNormalString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
