using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace P01.ProductShop.ExportDtos
{
    [XmlType("sold-products")]
   public  class SoldProduct
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("product")]
        public ProductDtoTwo[] ProductDtos { get; set; }
    }
}
