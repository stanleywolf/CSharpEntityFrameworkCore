using System;
using System.Collections.Generic;
using System.Text;

namespace P01.ProductShop.Services.Entity
{
    public class CategoryProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
