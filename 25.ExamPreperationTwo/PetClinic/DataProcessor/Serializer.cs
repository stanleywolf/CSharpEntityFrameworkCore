using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using PetClinic.Dto.ExportDtos;
using Formatting = Newtonsoft.Json.Formatting;

namespace PetClinic.DataProcessor
{
    using System;

    using PetClinic.Data;

    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var animals = context.Animals
                .Where(x => x.Passport.OwnerPhoneNumber == phoneNumber)
                .Select(x => new
                {
                    OwnerName = x.Passport.OwnerName,
                    AnimalName = x.Name,
                    Age = x.Age,
                    SerialNumber = x.PassportSerialNumber,
                    RegisteredOn = x.Passport.RegistrationDate.ToString("dd-MM-yyyy",CultureInfo.InvariantCulture)
                })
                .OrderBy(x => x.Age)
                .ThenBy(x => x.SerialNumber)
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(animals, Formatting.Indented);

            return jsonString;
        }

        public static string ExportAllProcedures(PetClinicContext context)
        {
            var procedure = context.Procedures
                .OrderBy(x => x.DateTime)
                .Select(x => new ProcedureExportDto()
                {
                    Passport = x.Animal.PassportSerialNumber,
                    OwnerNumber = x.Animal.Passport.OwnerPhoneNumber,
                    DateTime = x.DateTime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                    AnimalAids = x.ProcedureAnimalAids.Select(z => new AnimalAidExportDto()
                        {
                            Name = z.AnimalAid.Name,
                            Price = z.AnimalAid.Price
                        })
                        .ToArray(),
                    TotalPrice = x.ProcedureAnimalAids.Sum(d => d.AnimalAid.Price)
                })
                .OrderBy(p => p.Passport)
                .ToArray();

            var sb = new StringBuilder();

            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(ProcedureExportDto[]), new XmlRootAttribute("Procedures"));

            serializer.Serialize(new StringWriter(sb), procedure, xmlNamespaces);

            return sb.ToString();
        }
    }
}
