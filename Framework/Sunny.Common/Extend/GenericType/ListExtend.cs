namespace System.Collections.Generic
{
    static public class ListExtend
    {
        /// <summary>
        /// 对集合中的项进行扩展,返回一个dynamic集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="func">如ToDynamic(x=>{x.Extend(new{EnumCn=x.EnumX.GetDescribe()})})</param>
        /// <returns></returns>
        static public List<dynamic> ToDynamic<T>(this List<T> source, Func<T, dynamic> func)
        {
            List<dynamic> list = new List<dynamic>();
            source.ForEach(x => list.Add(func.Invoke(x)));
            return list;

        }
    }
}
