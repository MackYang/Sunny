using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Repository.DbModel
{
    public class Passage
    {
        //文章编号
        
        public int PassageId { get; set; }

        //标题
        public string Title { get; set; }

       
        //最后编辑时间
        public DateTime LastEditTime { get; set; }
        //文章分类（使用技术等）
        public   IList<PassageCategory> PassageCategories { get; set; }
    }
}
