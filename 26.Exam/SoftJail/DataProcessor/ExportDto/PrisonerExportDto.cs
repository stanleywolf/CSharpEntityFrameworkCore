using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ExportDto
{
    [XmlType("Prisoner")]
    public class PrisonerExportDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IncarcerationDate { get; set; }

        [XmlArray("EncryptedMessages")]
        public MailExportDto[] EncryptedMessages { get; set; }
    }

    [XmlType("Message")]
    public class MailExportDto
    {
        [XmlElement("Description")]
        public string Message { get; set; }   
    }

}
