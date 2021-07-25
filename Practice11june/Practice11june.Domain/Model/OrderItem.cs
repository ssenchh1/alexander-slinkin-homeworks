

namespace Practice11june.Domain.Model
{
    public class OrderItem : IEntity
    {
        public int Order_Id { get; set; }

        public int Product_Id { get; set; }

        public int Quantity { get; set; }
    }
}
