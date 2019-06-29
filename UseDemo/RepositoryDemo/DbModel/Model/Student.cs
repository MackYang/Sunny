using Sunny.Repository.DbModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDemo.DbModel
{
    public class Student : BaseModel
    {
        public Student() { }

       
        public string StudentName { get; set; }

        public string Test { get; set; }

        private string AA2 { get; set; }

        

        public StudentAddress Address { get; set; }

        public decimal Score { get; set; }


        
    }
}
