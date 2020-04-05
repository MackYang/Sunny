using Sunny.Repository.DbModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDemo.DbModel
{
    public class PassageCategory : IRelationMap
    {
        public long CategoryId { get; set; }

        public Category Category { get; set; }

        public long PassageId { get; set; }

        public Passage Passage { get; set; }

    }
}
