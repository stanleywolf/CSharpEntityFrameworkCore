﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using P01.ProductShop.Services.Entity;

namespace P01.ProductShop.Services
{
    public class User
    {
        public User()
        {
            this.ProductsSold = new List<Product>();
            this.ProductsBought = new List<Product>();
        }
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public ICollection<Product> ProductsSold { get; set; }

        public ICollection<Product> ProductsBought { get; set; }
    }
}
