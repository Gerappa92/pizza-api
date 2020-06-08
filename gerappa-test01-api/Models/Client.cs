using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gerappa_test01_api.Models
{
    public class Client : CosmoEntity
    {
        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "houseNumber")]
        public string HouseNumber { get; set; }

        [JsonProperty(PropertyName = "flatNumber")]
        public string FlatNumber { get; set; }

        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "zipCode")]
        public string  ZipCode{ get; set; }
    }
}
