using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunny.Repository.DbModel
{
    public class BaseModel
    {

        public long Id { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public long CreaterId { get; set; }

        public DateTime UpdateTime { get; set; }

        public long UpdaterId { get; set; }
    }
}
