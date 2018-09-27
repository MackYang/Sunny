using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Repository.DbModel
{
    public class Category:BaseModel
    {


        public Student Student { get; set; }
       
        public StudentAddress StudentAddress { get; set; }
        public string CategoryName { get; set; }

        public   IList<PassageCategory> PassageCategories { get; set; }
    }
}
