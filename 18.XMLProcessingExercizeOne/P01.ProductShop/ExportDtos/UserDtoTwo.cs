using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace P01.ProductShop.ExportDtos
{
    [XmlRoot("users")]
    public class UserDtoTwo
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("user")]
        public UserDtoTwoTwo[] User { get; set; }
    }
}
