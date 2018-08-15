using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;

namespace P02_DatabaseFirst
{
   public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new SoftUniContext();
            //P03Employees(context);
            //P04EmployeesOverSalary(context);
            //P05EmployeesResearch(context);
            //P06AddingNewAddress(context);
            //P06AddingNewAddressLast(context);
            //P07EmployeesAndProjects(context);
            //P08AdressesByTown(context);
            //P09Employee147(context);
            //P10DepartmentsMore5Emp(context);
            //P11FindLatest10Projects(context);
            //P12IncreaseSalaries(context);
            //P13EmplStartWithSA(context);
            P14Delete(context);

        }

        private static void P14Delete(SoftUniContext context)
        {
            using (context)
            {
                var projects = context.EmployeesProjects
                    .Where(x => x.ProjectId == 2);
                context.EmployeesProjects.RemoveRange(projects);
                var project = context.Projects.Find(2);
                context.Projects.Remove(project);
                context.SaveChanges();

                var proj = context.Projects.Take(10).ToArray();
                foreach (var p in proj)
                {
                    Console.WriteLine(p.Name);
                }

            }
        }

        private static void P13EmplStartWithSA(SoftUniContext context)
        {
            using (context)
            {
                var employee = context.Employees
                    .Where(e => EF.Functions.Like(e.FirstName,"Sa%"))
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle,
                        e.Salary
                    })
                    .OrderBy(f => f.FirstName)
                    .ThenBy(l => l.LastName)
                    .ToArray();

                foreach (var e in employee)
                {
                    Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})");
                }
            }
        }

        private static void P12IncreaseSalaries(SoftUniContext context)
        {
            using (context)
            {
                var employees = context.Employees
                    .Where(e => e.Department.Name == "Engineering" || e.Department.Name == "Tool Design" ||
                                e.Department.Name == "Marketing" || e.Department.Name == "Information Services")
                    .Select(x => new Employee()
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Salary = x.Salary
                    })
                    .OrderBy(f => f.FirstName)
                    .ThenBy(l => l.LastName)
                    .ToArray();

                foreach (Employee e in employees)
                {
                    e.Salary *= 1.12m;
                    Console.WriteLine($"{e.FirstName} {e.LastName} (${e.Salary:f2})");
                }
                context.SaveChanges();
            }
        }

        private static void P11FindLatest10Projects(SoftUniContext context)
        {
            using (context)
            {
                var projects = context.Projects
                    .OrderBy(x => x.StartDate)
                    .Select(p => new
                    {
                        PName = p.Name,
                        Desc = p.Description,
                        SDate = p.StartDate
                    })
                    .Skip(context.Projects.Count() - 10)          
                    .OrderBy(r => r.PName)
                    .ToArray();

                foreach (var p in projects)
                {
                    Console.WriteLine($"{p.PName}{Environment.NewLine}{p.Desc}{Environment.NewLine}{p.SDate}");
                }
            }
        }

        private static void P10DepartmentsMore5Emp(SoftUniContext context)
        {
            using (context)
            {
                var departments = context.Departments
                    .Where(x => x.Employees.Count > 5)
                    .OrderBy(x => x.Employees.Count)
                    .ThenBy(x => x.Name)
                    .Select(x => new
                    {
                        DepName = x.Name,
                        ManagerName = x.Manager.FirstName + " " + x.Manager.LastName,
                        Employees = x.Employees
                        .Select(e => new
                            {
                               e.FirstName,
                               e.LastName,
                               e.JobTitle
                            })
                            .OrderBy(v => v.FirstName)
                            .ThenBy(v => v.LastName)
                    })
                    
                    .ToArray();

                foreach (var d in departments)
                {
                    Console.WriteLine($"{d.DepName} - {d.ManagerName}");
                    foreach (var e in d.Employees)
                    {
                        Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                    }
                    Console.WriteLine("----------");
                }
            }
        }

        private static void P09Employee147(SoftUniContext context)
        {
            using (context)
            {
                var employee = context
                    .Employees
                    .Include(x => x.EmployeesProjects)
                    .ThenInclude(x => x.Project)
                    .FirstOrDefault(x => x.EmployeeId == 147);

                Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

                foreach (var p in employee.EmployeesProjects.OrderBy(x => x.Project.Name))
                {
                    Console.WriteLine(p.Project.Name);
                }
            }
        }

        private static void P08AdressesByTown(SoftUniContext context)
        {
            using (context)
            {
                var adresses = context.Addresses
                    .OrderByDescending(x => x.Employees.Count)
                    .ThenBy(x => x.Town.Name)
                    .ThenBy(x => x.AddressText)
                    .Select(a => new
                    {
                        AddressText = a.AddressText,
                        TownName = a.Town.Name,
                        EmployeeCount = a.Employees.Count
                    })
                    .Take(10)
                    .ToArray();

                foreach (var a in adresses)
                {
                    Console.WriteLine($"{a.AddressText}, {a.TownName} - {a.EmployeeCount} employees");
                }
            }
        }

        private static void P07EmployeesAndProjects(SoftUniContext context)
        {
            using (context)
            {
                var employees = context.Employees
                    .Where(x => x.EmployeesProjects.Any(s =>
                        s.Project.StartDate.Year >= 2001 && s.Project.StartDate.Year <= 2003))
                    .Select(x => new
                    {
                        EmployeeName = x.FirstName + " " + x.LastName,
                        ManagerName = x.Manager.FirstName + " " + x.Manager.LastName,
                        Project = x.EmployeesProjects.Select(s => new
                        {
                            ProjectName = s.Project.Name,
                            StartDate = s.Project.StartDate,
                            EndDate = s.Project.EndDate
                        })
                    })
                    .Take(30)
                    .ToArray();

                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.EmployeeName} – Manager: {e.ManagerName}");
                    foreach (var p in e.Project)
                    {
                        Console.WriteLine($"--{p.ProjectName} - {p.StartDate.ToString("M/d/yyyy h:mm:ss tt",CultureInfo.InvariantCulture)} - {p.EndDate?.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture) ?? "not finished"}");
                    }
                }
            }
        }

        private static void P06AddingNewAddressLast(SoftUniContext context)
        {
            using (context)
            {
                var addresses = context.Employees
                    .OrderByDescending(e => e.AddressId)
                    .Select(e => e.Address.AddressText)
                    .Take(10).ToArray();

                Console.WriteLine(string.Join(Environment.NewLine,addresses));
            }
        }

        private static void P06AddingNewAddress(SoftUniContext context)
        {
            using (context)
            {
                var address = new Address()
                {
                    AddressText = "Vitoshka 15",
                    TownId = 4
                };
                
                var nakov = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");
                nakov.Address = address;

                context.SaveChanges();
            }
        }

        private static void P05EmployeesResearch(SoftUniContext context)
        {
            using (context)
            {
                var employees = context.Employees
                    .Where(e => e.Department.Name == "Research and Development")
                    .OrderBy(e => e.Salary)
                    .ThenByDescending(e => e.FirstName)
                    .Select(e => new
                    {
                        FN = e.FirstName,
                        LN = e.LastName,
                        DN = e.Department.Name,
                        S = e.Salary
                    }).ToArray();

                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.FN} {e.LN} from {e.DN} - ${e.S:f2}");
                }
            }
        }

        private static void P04EmployeesOverSalary(SoftUniContext context)
        {
            using (context)
            {
                var employee = context.Employees
                    .OrderBy(x => x.FirstName)
                    .Where(x => x.Salary > 50000)
                    .Select(x => new
                    {
                        FirstName = x.FirstName
                    }).ToArray();

                foreach (var e in employee)
                {
                    Console.WriteLine(e.FirstName);
                }
            }
        }

        private static void P03Employees(SoftUniContext context)
        {
            using (context)
            {
                var employees = context.Employees
                    .OrderBy(x => x.EmployeeId)
                    .Select(x => new
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        MiddleName = x.MiddleName,
                        JobTitle = x.JobTitle,
                        Salary = x.Salary
                    })
                    .ToArray();

                using (StreamWriter sw = new StreamWriter("../Employees.txt"))
                {
                    foreach (var e in employees)
                    {
                        sw.WriteLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}");
                    }
                }
            }
        }
    }
}
