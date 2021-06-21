using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice11june.Domain.Model
{
    public class OrderItem : IEntity
    {
        public int Order_Id { get; set; }

        public int Product_Id { get; set; }

        public int Quantity { get; set; }
    }
}
