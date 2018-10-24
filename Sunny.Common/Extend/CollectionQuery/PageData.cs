using System;
using System.Collections.Generic;

namespace Sunny.Common.Extend.CollectionQuery
{
    /// <summary>
    /// 包含分页信息的数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageData<T>
    {
        /// <summary>
        /// 数据
        /// </summary>
        public List<T> List { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PageInfo PageInfo { get; set; }

        /// <summary>
        /// 将数据中的每一项转为dynamic,并返回一个新实例,
        /// 通常用于要对数据项进行扩展时,比如为数据项添加枚举的中文意思并返回给前端
        /// 如ToDynamic(x=>{x.Extend(new{EnumCn=x.EnumX.GetDescribe()})})
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public PageData<dynamic> ToDynamic(Func<T, dynamic> func)
        {
            PageData<dynamic> result = new PageData<dynamic>();
            result.PageInfo = PageInfo;
            if (List != null && List.Count > 0)
            {
                List<dynamic> listDynamic = new List<dynamic>();
                List.ForEach(x => listDynamic.Add(func.Invoke(x)));
                result.List = listDynamic;
            }
            return result;
        }
    }

}
