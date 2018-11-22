using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Text;

namespace System
{
    public static class ClassTypeExtend
    {

        /// <summary>
        /// 使用另一个对象对源对象进行扩展，这类似于 jQuery 中的 extend 方法。
        /// </summary>
        /// <param name="source">源对象。</param>
        /// <param name="other">用于扩展的另一个对象。</param>
        /// <param name="isOverride">是否要覆盖源对象中同名属性的值,默认为否</param>
        /// <returns></returns>
        public static dynamic Extend<T>(this T source, object other, bool isOverride = false) where T : class
        {
            if (source == null)
            {
                return other;
            }

            if (other == null)
            {
                return source;
            }

            var sourceProperties = TypeDescriptor.GetProperties(source);
            var otherProperties = TypeDescriptor.GetProperties(other);
            var expando = new ExpandoObject();
            var dictionary = (IDictionary<string, object>)expando;


            foreach (PropertyDescriptor p in sourceProperties)
            {
                dictionary.Add(p.Name, p.GetValue(source));
            }

            foreach (PropertyDescriptor p in otherProperties)
            {
                if (!dictionary.ContainsKey(p.Name) && !isOverride)
                {
                    dictionary.Add(p.Name, p.GetValue(other));
                }
                else
                {
                    throw new Exception($"源对象已经包含了名为{p.Name}的属性,如果要覆盖请给isOverride参数传true值");
                }
            }

            return expando;

        }
    }
}
