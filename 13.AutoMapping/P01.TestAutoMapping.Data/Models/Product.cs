using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P01.TestAutoMapping.Data.Models
{
    public class Product
    {
        public Product()
        {
            this.ProductStocks = new List<ProductStock>();
        }
        [Key]
        public int ProductId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime ExpireDate { get; set; }

        public virtual ICollection<ProductStock> ProductStocks { get; set; }
    }
}
