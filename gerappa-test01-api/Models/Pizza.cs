﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace gerappa_test01_api.Models
{
    public class Pizza : CosmoEntity
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Vegetarian { get; set; }
    }
}
