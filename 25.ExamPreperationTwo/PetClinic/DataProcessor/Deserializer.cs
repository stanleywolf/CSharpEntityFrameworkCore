using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AutoMapper;
using Newtonsoft.Json;
using PetClinic.Dto.ImportDtos;
using PetClinic.Models;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace PetClinic.DataProcessor
{
    using System;

    using PetClinic.Data;

    public class Deserializer
    {
        private const string ERROR_MASSAGE = "Error: Invalid data.";

        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            var deserializeJson = JsonConvert.DeserializeObject<AnimalAidDto[]>(jsonString);

            var sb = new StringBuilder();

            List<AnimalAid> animalAids = new List<AnimalAid>();

            foreach (var animalAidDto in deserializeJson)
            {
                var animAidExist = animalAids.Any(x => x.Name == animalAidDto.Name);
                if (!IsValid(animalAidDto) || animAidExist)
                {
                    sb.AppendLine(ERROR_MASSAGE);
                    continue;
                }
                var animalAid = Mapper.Map<AnimalAid>(animalAidDto);
                animalAids.Add(animalAid);

                sb.AppendLine($"Record {animalAidDto.Name} successfully imported.");
            }
            context.AnimalAids.AddRange(animalAids);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }        

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            var deserializeJson = JsonConvert.DeserializeObject<AnimalDto[]>(jsonString);

            var sb = new StringBuilder();

            List<Animal> animals = new List<Animal>();

            foreach (var animalDto in deserializeJson)
            {

                var passportExist = animals.Any(x => x.Passport.SerialNumber == animalDto.Passport.SerialNumber);

                if (!IsValid(animalDto)
                    || !IsValid(animalDto.Passport)
                    || passportExist)
                {
                    sb.AppendLine(ERROR_MASSAGE);
                    continue;
                }

                var animal = Mapper.Map<Animal>(animalDto);

                animals.Add(animal);
                sb.AppendLine($"Record {animal.Name} Passport №: {animal.Passport.SerialNumber} successfully imported.");

            }
            context.Animals.AddRange(animals);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(VetDto[]), new XmlRootAttribute("Vets"));
            var deserXml = (VetDto[])serializer.Deserialize(new StringReader(xmlString));
            
            var sb = new StringBuilder();
            List<Vet> vets = new List<Vet>();

            foreach (var vetDto in deserXml)
            {
                var phoneExist = vets.Any(x => x.PhoneNumber == vetDto.PhoneNumber);

                if (!IsValid(vetDto) || phoneExist)
                {
                    sb.AppendLine(ERROR_MASSAGE);
                    continue;
                }
                var vet = Mapper.Map<Vet>(vetDto);

                vets.Add(vet);

                sb.AppendLine($"Record {vet.Name} successfully imported.");
            }
            context.Vets.AddRange(vets);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ProcedureDto[]), new XmlRootAttribute("Procedures"));
            var deserXml = (ProcedureDto[])serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();
            List<Procedure> procedures = new List<Procedure>();

            foreach (var procedureDto in deserXml)
            {
                var vetObj = context.Vets.SingleOrDefault(x => x.Name == procedureDto.Vet);

                var animalobj = context.Animals.SingleOrDefault(x => x.PassportSerialNumber == procedureDto.Animal);
                
                var validAnimalAidsProcerures = new List<ProcedureAnimalAid>();
                var allaidsExist = true;

                foreach (var animalAidDtoFour in procedureDto.AnimalAids)
                {
                    var animalAidObj = context.AnimalAids.SingleOrDefault(x => x.Name == animalAidDtoFour.Name);

                    if (animalAidObj == null || validAnimalAidsProcerures.Any(x => x.AnimalAid.Name == animalAidDtoFour.Name))
                    {
                        allaidsExist = false;
                        break;
                    }                    
                    var animalAidProcedure = new ProcedureAnimalAid()
                    {
                        AnimalAid = animalAidObj
                    };
                    validAnimalAidsProcerures.Add(animalAidProcedure);
                }
                if (!IsValid(procedureDto)
                    || !procedureDto .AnimalAids .All(IsValid)
                    || vetObj == null
                    || animalobj == null
                    || !allaidsExist)
                {
                    sb.AppendLine(ERROR_MASSAGE);
                    continue;
                }
                var procedure = new Procedure()
                {
                    Animal = animalobj,
                    Vet = vetObj,
                    DateTime = DateTime.ParseExact(procedureDto.DateTime,"dd-MM-yyyy",CultureInfo.InvariantCulture),
                    ProcedureAnimalAids = validAnimalAidsProcerures
                };

                procedures.Add(procedure);

                sb.AppendLine($"Record successfully imported.");
            }

            context.Procedures.AddRange(procedures);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }
        private static bool IsValid(object obj)
        {
            var context = new ValidationContext(obj);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, context, results, true);
        }
    }
}
