namespace System
{
    public static class ObjectExtend
    {
        /// <summary>
        /// 将objValue的值转换成defValue的类型,如果转换失败,返回defValue
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="objValue">要转换的原数据</param>
        /// <param name="defValue">转换失败时返回的默认值</param>
        /// <returns></returns>
        static public T ConvertTo<T>(this object objValue, T defValue)
        {
            try
            {
                return (T)Convert.ChangeType(objValue, typeof(T));
            }
            catch
            {
                return defValue;
            }

        }
    }
}
