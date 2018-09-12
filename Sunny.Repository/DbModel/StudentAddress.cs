using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunny.Repository.DbModel
{
    public class StudentAddress
    {
       
        public int StudentAddressId { get; set; }

        public string Address1 { get; set; }
       
        public int Zipcode { get; set; }
        public string State { get; set; }
        public string  Country { get; set; }

        public  Student Student { get; set; }

       public int StudentId { get; set; }
    }
}
