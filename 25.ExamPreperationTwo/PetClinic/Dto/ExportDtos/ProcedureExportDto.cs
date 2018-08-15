using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PetClinic.Dto.ExportDtos
{
    [XmlType("Procedure")]
    public class ProcedureExportDto
    {
        public string Passport { get; set; }

        public string OwnerNumber { get; set; }

        public string DateTime { get; set; }

        public AnimalAidExportDto[] AnimalAids { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
