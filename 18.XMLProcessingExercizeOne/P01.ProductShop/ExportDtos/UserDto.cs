using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace P01.ProductShop.ExportDtos
{
    [XmlType("user")]
    public class UserDto
    {
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }

        [XmlArray("sold-products")]
        public SoldProductDto[] SoldProduct { get; set; }
    }
}
