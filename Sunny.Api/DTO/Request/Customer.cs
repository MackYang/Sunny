using System;
using static Sunny.Api.DTO.Request.Enummm;

namespace Sunny.Api.FluentValidation2
{
    public class Customer
    {
        public String Surname { get; set; }
        public String Forename { get; set; }
        public int Discount { get; set; }
        public String Address { get; set; }
        public String Postcode { get; set; }
        public bool HasDiscount { get; set; }

        public IntEnum LocalType { get; set; } = IntEnum.GPS;
    }
}
