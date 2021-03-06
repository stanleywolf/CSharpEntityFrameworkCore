﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace P01.ProductShop.ExportDtos
{
    [XmlType("product")]
   public  class ProductDtoTwo
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}
