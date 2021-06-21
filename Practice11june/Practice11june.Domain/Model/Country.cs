using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice11june.Domain.Model
{
    public class Country : IEntity
    {
        [Key]
        public int Country_Code { get; set; }

        public string Name { get; set; }
    }
}
