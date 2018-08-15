using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using PetClinic.Models;

namespace PetClinic.Dto.ImportDtos
{
    [XmlType("Procedure")]
    public class ProcedureDto
    {
        [XmlElement("Vet")]
        [Required]
        public string Vet { get; set; }

        [XmlElement("Animal")]
        [Required]
        public string Animal { get; set; }

        [XmlElement("DateTime")]
        [Required]
        public string DateTime { get; set; }

        [XmlArray("AnimalAids")]
        public AnimalAidDtoFour[] AnimalAids { get; set; }
    }
}
