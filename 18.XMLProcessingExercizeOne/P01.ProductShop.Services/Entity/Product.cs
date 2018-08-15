using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01.ProductShop.Services.Entity
{
    public class Product
    {
        public Product()
        {
            this.CategoryProducts = new List<CategoryProduct>();
        }
        [Key]
        public int Id { get; set; }

        [MinLength(3)]
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int? BuyerId { get; set; }
        public User Buyer { get; set; }

        [Required]
        public int SellerId { get; set; }
        public User Seller { get; set; }

        public ICollection<CategoryProduct> CategoryProducts { get; set; }

    }
}
