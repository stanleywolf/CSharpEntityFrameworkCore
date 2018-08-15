using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01.TestAutoMapping.Data.Models
{
    public class ProductStock
    {
        public int Quantity { get; set; }

        [Key]
        public int StorageId { get; set; }
        public virtual Storage Storage { get; set; }

        [Key]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        
    }
}
