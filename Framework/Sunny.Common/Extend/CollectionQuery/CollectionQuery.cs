using Sunny.Common.Extend.CollectionQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace System.Linq
{
    static public class CollectionQuery
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="list"> 数据源 </param>
        /// <param name="pageIndex"> 第几页 </param>
        /// <param name="pageSize"> 每页记录数 </param>
        /// <param name="recordTotal"> 记录总数 </param>
        /// <returns></returns>
        public static List<T> Pagination<T>(this IQueryable<T> list, int pageIndex, int pageSize, out int recordTotal)
        {
            recordTotal = list.Count();
            return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 分页并返回包含分页信息的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="pageInfo">分页要求</param>
        /// <returns></returns>
        public static PageData<T> Pagination<T>(this IQueryable<T> list,PageInfo pageInfo)
        {
            pageInfo.RecordTotal = list.Count();
            var data=list.Skip((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize);
            return new PageData<T>() { List = data.ToList(), PageInfo = pageInfo };
        }

    }
}
