using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Repository.DbModel
{
    public class PassageCategory : BaseModel
    {
        public long CategoryId { get; set; }

        public Category Category { get; set; }

        public long PassageId { get; set; }

        public Passage Passage { get; set; }
    }
}
