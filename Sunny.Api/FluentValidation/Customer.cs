using System;

namespace Sunny.Api.FluentValidation
{
    public class Customer
    {
        public String Surname { get; set; }
        public String Forename { get; set; }
        public int Discount { get; set; }
        public String Address { get; set; }
        public String Postcode { get; set; }
        public bool HasDiscount { get; set; }
    }
}
