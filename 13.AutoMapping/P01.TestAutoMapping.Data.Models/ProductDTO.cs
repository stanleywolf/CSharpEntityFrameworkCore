using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P01.TestAutoMapping.Data.Models
{
    public class ProductDTO
    {
        
        public string Name { get; set; }

        public int InStorageCount { get; set; }

        public string ExpireMonth { get; set; }

        public string ExpireYear { get; set; }
        
    }
}
