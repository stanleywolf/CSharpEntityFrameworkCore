using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using SoftJail.Data.Models;
using SoftJail.Data.Models.Enums;
using SoftJail.DataProcessor.ImportDto;

namespace SoftJail.DataProcessor
{

    using Data;
    using System;

    public class Deserializer
    {
        private const string FailureMessage = "Invalid Data";
       
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var deserializeDep = JsonConvert.DeserializeObject<DepartmentDto[]>(jsonString);

            var sb = new StringBuilder();

            List<Department> departments = new List<Department>();

            foreach (var departmentDto in deserializeDep)
            {
                if (!IsValid(departmentDto)
                    || !departmentDto.Cells.All(IsValid))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var department = new Department()
                {
                    Name = departmentDto.Name
                };


                List<Cell> cells = new List<Cell>();
                foreach (var cellDto in departmentDto.Cells)
                {
                    var cell = new Cell()
                    {
                        Department = department,
                        CellNumber = cellDto.CellNumber,
                        HasWindow = cellDto.HasWindow
                    };

                    cells.Add(cell);
                }
                department.Cells = cells;
                departments.Add(department);

                sb.AppendLine($"Imported {department.Name} with {department.Cells.Count} cells");
        }

            context.Departments.AddRange(departments);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var deserializePrisoner = JsonConvert.DeserializeObject<PrisonerDto[]>(jsonString);

            var sb = new StringBuilder();

            List<Prisoner> prisoners = new List<Prisoner>();

            foreach (var prisonerDto in deserializePrisoner)
            {
                if (!IsValid(prisonerDto)
                    || !prisonerDto.Mails.All(IsValid))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                List<Mail> mails = new List<Mail>();
                foreach (var prisonerDtoMail in prisonerDto.Mails)
                {
                    var mail = new Mail()
                    {
                        Description = prisonerDtoMail.Description,
                        Sender = prisonerDtoMail.Sender,
                        Address = prisonerDtoMail.Address
                    };
                    mails.Add(mail);
                }

                var interDate = DateTime.ParseExact(prisonerDto.IncarcerationDate, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);

                if (prisonerDto.ReleaseDate == null)
                {
                    var prisoner = new Prisoner()
                    {
                        FullName = prisonerDto.FullName,
                        Nickname = prisonerDto.Nickname,
                        Age = prisonerDto.Age,
                        IncarcerationDate = interDate,
                        Bail = prisonerDto.Bail,
                        CellId = prisonerDto.CellId,
                        Mails = mails
                    };

                    prisoners.Add(prisoner);

                    sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
                }
                else
                {
                    var relDate = DateTime.ParseExact(prisonerDto.ReleaseDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture);

                    var prisoner = new Prisoner()
                    {
                        FullName = prisonerDto.FullName,
                        Nickname = prisonerDto.Nickname,
                        Age = prisonerDto.Age,
                        IncarcerationDate = interDate,
                        ReleaseDate = relDate,
                        Bail = prisonerDto.Bail,
                        CellId = prisonerDto.CellId,
                        Mails = mails
                    };

                    prisoners.Add(prisoner);

                    sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
                }
            }

            context.Prisoners.AddRange(prisoners);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(OfficerDto[]), new XmlRootAttribute("Officers"));
            var deserXml = (OfficerDto[]) serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();
            List<Officer> officers = new List<Officer>();

            foreach (var officerDto in deserXml)
            {
                if (!IsValid(officerDto)
                    || !officerDto.Prisoners.All(IsValid))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                List<OfficerPrisoner> prosonersoff = new List<OfficerPrisoner>();

                foreach (var officerDtoPrisoner in officerDto.Prisoners)
                {


                    var prisoner = context.Prisoners.FirstOrDefault(x => x.Id == officerDtoPrisoner.Id);

                    var officerPrisoners = new OfficerPrisoner()
                    {
                        Prisoner = prisoner
                    };

                    prosonersoff.Add(officerPrisoners);
                }
                Position position;
                Weapon weapon;
                if (!Enum.TryParse(officerDto.Position, true, out position) ||
                    !Enum.TryParse(officerDto.Weapon, true, out weapon))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                
                 //position = EnSum.Parse<Position>(officerDto.Position);
                 //weapon = Enum.Parse<Weapon>(officerDto.Weapon);

                var officer = new Officer()
                {
                    FullName = officerDto.Name,
                    Salary = officerDto.Money,
                    Position = position,
                    Weapon = weapon,
                    DepartmentId = officerDto.DepartmentId,
                    OfficerPrisoners = prosonersoff
                };

                officers.Add(officer);

                sb.AppendLine($"Imported {officer.FullName} ({prosonersoff.Count} prisoners)");
            }
            context.Officers.AddRange(officers);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        private static bool IsValid(object obj)
        {
            var validateContext = new ValidationContext(obj);
            var validResult = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validateContext, validResult, true);
        }
    }
}