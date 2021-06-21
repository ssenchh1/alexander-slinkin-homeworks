using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice11june.Domain.Model
{
    public class Product : IEntity
    {
        public int Product_Id { get; set; }

        public int Merchant_Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
