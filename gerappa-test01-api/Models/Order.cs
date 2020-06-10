using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gerappa_test01_api.Models
{
    public class Order : CosmoEntity
    {
        public Pizza Pizza { get; set; }
        public Client Client { get; set; }
        public decimal Price { get; set; }
    }
}
