using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice11june.Domain.Model
{
    public class Merchant : IEntity
    {
        public int Merchant_Id { get; set; }

        public string Name { get; set; }

        public int Country_Code { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? User_id { get; set; }
    }
}
