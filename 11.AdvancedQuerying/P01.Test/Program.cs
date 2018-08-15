using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using P01.Test.Data;
using P01.Test.Data.Models;
using Z.EntityFramework.Plus;

namespace P01.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            var context = new TestDbContext();

           
        }
    }

    public class Cascade
    {
        //Employee newEntry = new Employee()
        //{
        //    FirstName = "Yoan",
        //    LastName = "Nikolov",
        //    JobTitle = "BigPuppy",
        //    Age = 3,
        //    ManagerOfProject = new List<Project>()
        //    {
        //        new Project()
        //        {
        //            Name = "Puppy"
        //        }
        //    }
        //};

        //context.Employees.Add(newEntry);
        //var employee = context.Employees.Find(12);
        //context.Employees.Remove(employee);
        //context.SaveChanges();

        //modelBuilder.OnDelete - Cascade,SetNull,
    }
    public class ConcurrencyCheck
    {
        //var context = new TestDbContext();
        //var context2 = new TestDbContext();

        //Employee nikola = context.Employees.Find(10);
        //Employee nikola2 = context.Employees.Find(10);

        //nikola.Age = 7;
        //nikola2.Age = 8;

        //try
        //{
        //    context.SaveChanges();
        //    context2.SaveChanges();
        //}
        //catch (DbUpdateConcurrencyException ex)
        //{
        //    int currentMax = int.MinValue;
        //    foreach (var entry in ex.Entries)
        //    {
        //        if (entry.Entity is Employee)
        //        {
        //            var proposedValues = entry.CurrentValues;
        //            foreach (var value in proposedValues.Properties)
        //            {
        //                if (value.Name == "Age")
        //                {
        //                    if (currentMax < (int) proposedValues[value])
        //                    {
        //                        currentMax = (int) proposedValues[value];
        //                    }
        //                }
        //            }
        //        }

        //        var dbValues = entry.GetDatabaseValues();
        //        var prop = dbValues.Properties.FirstOrDefault(x => x.Name == "Age");
        //        dbValues[prop] = currentMax;

        //        entry.OriginalValues.SetValues(dbValues);
        //    }
        //}
    }
    public class TypeOfLoading
    {
        //Explicit
        //Eger
        //lazzy loading
    }
    public class BulkOperation
    {
        //context.Employees
        //    .Where(u => u.FirstName == "Yoan")
        //    .Delete();

        //context.Employees
        //    .Where(x => x.Age == 18)
        //    .Update(x => new Employee() {Age = 21});

        //context.SaveChanges();
    }
    public class StoredProcedure
    {
        //var param = new SqlParameter("@minAge", 18);
        //string query = "EXEC CheckAgeForNull @minAge";

        //context.Database.ExecuteSqlCommand(query, param);
    }
    //RepositoryPatern
    public class Repository<T> where T : class
    {
        private TestDbContext context => new TestDbContext();

        public void Insert(T obj)
        {
            this.context.Entry(obj).State = EntityState.Added;
        }
    }
    public class ObjectStateTracking
    {
        //var context = new TestDbContext();
        //Employee employee = new Employee()
        //{
        //    FirstName = "Dragan",
        //    LastName = "Shopov",
        //    JobTitle = "CEO"
        //};
        //context.Employees.Add(employee);

        //Employee entity = context.Employees.Find(1);

        //entity.JobTitle = "Lier";

        ////dont change a entity
        //context.Entry(entity).State = EntityState.Unchanged;

        ////deleted status
        //context.Entry(entity).State = EntityState.Deleted;

        //context.SaveChanges();

    }
    public class SQLQuarry
    {

        //string querry = "Select * from Employees" + " Where JobTitle = {0}";
        //var result = context.Employees.FromSql(querry, "CEO").ToArray();

        //string querry2 = "Select * from Employees";
        //var result2 = context.Employees.FromSql(querry).Where(x => x.JobTitle == "CEO").ToArray();


    }
    public class Seed
    {

        //string[] firstName = new string[] { "Nikola", "Stanislav", "Petia", "Yoan", "Georgi" };
        //string[] lastNames = new string[] { "Nikolov", "Peev", "Peeva-Nikolova", "Belchinski" };
        //string[] jobTitles = new string[] { "CEO", "Developer", "HR", "Junior .Net" };


        //for (int i = 0; i < 10; i++)
        //{
        //    Random rand = new Random();
        //    int nameIndex = rand.Next(0, 5);
        //    int familyIndex = rand.Next(0, 4);
        //    int jobIndex = rand.Next(0, 4);

        //    context.Employees.Add(new Employee()
        //    {
        //        FirstName = firstName[nameIndex],
        //        LastName = lastNames[familyIndex],
        //        JobTitle = jobTitles[jobIndex]
        //    });
        //}

        //context.SaveChanges();
    }
}
