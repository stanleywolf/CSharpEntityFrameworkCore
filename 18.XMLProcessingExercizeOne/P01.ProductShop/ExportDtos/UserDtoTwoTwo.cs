using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace P01.ProductShop.ExportDtos
{
    [XmlType("user")]
    public class UserDtoTwoTwo
    {
        [XmlAttribute("first-name")]
        public string Firstname { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }

        [XmlAttribute("age")]
        public string Age { get; set; }

        [XmlElement("sold-products")]
        public SoldProduct SoldProducts { get; set; }
    }
}
