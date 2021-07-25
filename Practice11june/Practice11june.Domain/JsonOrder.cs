using System;
using System.Collections.Generic;

namespace Practice11june.Domain
{
    public class JsonOrder
    {
        public int OrderId { get; set; }

        public JsonUser User { get; set; }

        public List<JsonProduct> Products { get; set; }
    }

    public class JsonUser
    {
        public string FullName { get; set; }

        public string Email { get; set; }
    }

    public class JsonProduct
    {
        public JsonMerchant Merchant { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }

    public class JsonMerchant
    {
        public string Name { get; set; }
    }
}
