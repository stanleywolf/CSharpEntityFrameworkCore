using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;

namespace P01.TestJson
{
    class Program
    {
        static void Main(string[] args)
        {
           List<Product> products = new List<Product>()
           {
               new Product()
               {
                   Name = "Vegetables",
                   Products = new List<Product>()
                   {
                       new Product() { Name = "Cucamber"},
                       new Product() { Name = "Tomato"}
                   }
               },
               new Product()
               {
                   Name = "Fruits",
                   Products = new List<Product>()
                   {
                       new Product() { Name = "Apples"},
                       new Product() { Name = "Banana"},
                       new Product() { Name = "Mandarine"}
                   }
               }
           };
            string json = JsonConvert.SerializeObject(products,Formatting.Indented);
            Console.WriteLine(json);

            #region .Net Serializer

            var serializer = new DataContractJsonSerializer(typeof(Product));

            Product product = new Product()
            {
                Name = "Vegetables",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        Name = "Cucumber"
                    },
                    new Product()
                    {
                        Name = "Tomato"
                    }
                }
            };

            string jsons;
            using (var stream = new MemoryStream())     
            {
                serializer.WriteObject(stream,product);

                byte[] arr = stream.ToArray();

                jsons = Encoding.UTF8.GetString(arr);
            }
            Console.WriteLine(jsons);

            string deserJson = @"{
    ""Name"": ""Vegetables"",
    ""Products"": [
      {
        ""Name"": ""Cucamber""
      },
      {
        ""Name"": ""Tomato""
      }
    ]
  }";
            byte[] data = Encoding.UTF8.GetBytes(deserJson);
            using (var stream = new MemoryStream(data))
            {
                var productsDes = (Product) serializer.ReadObject(stream);
            }

            #endregion
        }
    }
}
