using System;

namespace Sunny.Api.DTO.Request.Demo
{
    public class Buyer
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public OrderInfo Order { get; set; }

        public BuyerComment Comment { get; set; }
    }

    public class BuyerSub : Buyer
    {

        public string Address { get; set; }
    }

    public class OrderInfo
    {

        public Decimal Price { get; set; }

        public int ProductCount { get; set; }

    }

    public class Seller
    {

        public string NaMe { get; set; }

        public DateTime SellTime { get; set; }

        public string Phone { get; set; }

        public OrderInfo Order { get; set; }

        public SellerComment Comment { get; set; }

    }

    public class SellerSub : Seller
    {
        public string Address { get; set; }
    }

    public class BuyerComment
    {

        public string Content { get; set; }

        public string BuyerName { get; set; }

    }

    public class SellerComment
    {

        public int Content { get; set; }
        public string SellerName { get; set; }
    }
}
