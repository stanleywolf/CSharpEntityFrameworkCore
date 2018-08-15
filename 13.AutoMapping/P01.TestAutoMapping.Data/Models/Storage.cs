using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01.TestAutoMapping.Data.Models
{
    public class Storage
    {
        public Storage()
        {
            this.ProductStocks = new List<ProductStock>();
        }
        [Key]
        public int StorageId { get; set; }

        public string Name { get; set; }
        public string Location { get; set; }

        public virtual ICollection<ProductStock> ProductStocks { get; set; }
    }
}
