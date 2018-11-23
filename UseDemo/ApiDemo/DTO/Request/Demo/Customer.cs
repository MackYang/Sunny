using Sunny.Common.Helper;
using System;
using static ApiDemo.DTO.Request.Enummm;

namespace ApiDemo.FluentValidation2
{
    public class Customer
    {

        public long Id{ get; set; }
        public String Surname { get; set; }
        public String Forename { get; set; }
        public int Discount { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public String Address { get; set; }
        public String Postcode { get; set; }
        public bool HasDiscount { get; set; }

        public IntEnum LocalType { get; set; } = IntEnum.GPS;

        public decimal Price { get; set; } = decimal.MaxValue;
        public decimal? PriceNull { get; set; }
        public decimal PriceDefault { get; set; }

        public string PriceCN { get; set; } = decimal.MaxValue.ToString();

        public double DoubleNum { get; set; }= double.MaxValue;
        public string DoubleCN { get; set; } = double.MaxValue.ToString();

        public float FloatNum { get; set; }= float.MaxValue;

        public string FloatCN { get; set; } = float.MaxValue.ToString();

        public long LongNum { get; set; } = long.MaxValue;
        public string LongCn { get; set; } = long.MaxValue.ToString();

        public DateTime Now { get; set; } = DateTime.Now;
        public Int64 IntNum { get; set; } = Int64.MaxValue;

        public string IntCn { get; set; } = Int64.MaxValue.ToString();

       
        public long PuaID { get; set; } = IdHelper.GenId();

        public string STRINGTEST { get; set; }
    }
}
