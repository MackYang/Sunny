using Sunny.Repository.DbModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDemo.DbModel
{
    public class Category:BaseModel
    {

        public string CategoryName { get; set; }

        public   IList<PassageCategory> PassageCategories { get; set; }

    }
}
