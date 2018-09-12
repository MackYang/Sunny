using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Repository.DbModel
{
    public class Category
    {
       
        public int CategoryId { get; set; }

       
        public string CategoryName { get; set; }

        public   IList<PassageCategory> PassageCategories { get; set; }
    }
}
