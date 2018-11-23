using System;
using System.Collections.Generic;

namespace Sunny.Common.Extend.CollectionQuery
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public class PageInfo
    {
        /// <summary>
        /// 当前页索引
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int RecordTotal { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageTotal
        {
            get
            {
                // (recordTotal-1)/pageSize+1当recordTotal=0时,pageTotal=1
                return (int)Math.Ceiling(RecordTotal / (double)PageSize);
            }
        }
         
    }

}
