using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Common.Extend.CollectionQuery
{
    /// <summary>
    /// 带分页的查询条件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageQuery<T>
    {
        /// <summary>
        /// 查询条件
        /// </summary>
        public T Condition { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PageInfo PageInfo { get; set; }
    }
}
