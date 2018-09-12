using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Repository.DbModel
{
    public class PassageCategory
    {
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int PassageId { get; set; }

        public Passage Passage { get; set; }
    }
}
