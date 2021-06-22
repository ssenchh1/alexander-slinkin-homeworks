using System;

namespace Practice11june.Domain.Model
{
    public class Order : IEntity
    {
        public int Order_Id { get; set; }

        public int User_Id { get; set; }

        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public string OrderJson { get; set; }
    }
}
