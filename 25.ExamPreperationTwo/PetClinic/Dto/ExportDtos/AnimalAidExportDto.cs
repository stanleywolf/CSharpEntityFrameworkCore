using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace PetClinic.Dto.ExportDtos
{
    [XmlType("AnimalAid")]
    public class AnimalAidExportDto
    {
        
        public string Name { get; set; }
       
        public decimal Price { get; set; }
    }
}
