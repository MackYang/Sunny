using Sunny.Repository.DbModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDemo.DbModel
{
    public class Passage: BaseModel
    {
        //文章编号
        
       

        //标题
        public string Title { get; set; }

       
        //最后编辑时间
        public DateTime LastEditTime { get; set; }
        //文章分类（使用技术等）
        public   IList<PassageCategory> PassageCategories { get; set; }
    }
}
