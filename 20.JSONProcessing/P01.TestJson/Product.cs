using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace P01.TestJson
{
    public class Product
    {
        [JsonProperty(PropertyName = "name",Required = Required.DisallowNull)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "products",NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<Product> Products { get; set; }
    }
}
