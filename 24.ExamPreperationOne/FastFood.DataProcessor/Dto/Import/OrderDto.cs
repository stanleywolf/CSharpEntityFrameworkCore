using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using FastFood.Models.Enums;

namespace FastFood.DataProcessor.Dto.Import
{
    [XmlType("Order")]
    public class OrderDto
    {
        [Required]
        [XmlElement("Customer")]
        public string Customer { get; set; }

        [XmlElement("Employee")]
        public string Employee { get; set; }

        [Required]
        [XmlElement("DateTime")]
        public string DateTime { get; set; }

        [XmlElement("Type")]
        public string Type { get; set; }

        [XmlArray("Items")]
        public OrderItemsDto[] OrderItemsDto { get; set; }
    }

    [XmlType("Item")]
    public class OrderItemsDto
    {
        [Required]
        [XmlElement("Name")]
        public string  Name { get; set; }

        [Required]
        [XmlElement("Quantity")]
        [Range(1, Int32.MaxValue)]
        public int Quantity { get; set; }
    }
}
